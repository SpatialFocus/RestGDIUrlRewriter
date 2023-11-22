// <copyright file="IndexMetadataHostedService.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Proxy;

using System.Collections.Concurrent;
using System.Xml.Linq;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UbaUrlRewrite.Data;
using UbaUrlRewrite.Data.Entities;
using UbaUrlRewrite.Management.Business;
using UbaUrlRewrite.Management.Business.Extensions;
using UbaUrlRewrite.Management.Business.Parsing;

public class IndexMetadataHostedService : BackgroundService
{
	private const int PageSize = 10;
	private readonly ILogger<IndexMetadataOptions> logger;
	private readonly IndexMetadataOptions options;
	private readonly IServiceProvider serviceProvider;

	public IndexMetadataHostedService(IOptions<IndexMetadataOptions> options, IServiceProvider serviceProvider,
		ILogger<IndexMetadataOptions> logger)
	{
		this.serviceProvider = serviceProvider;
		this.logger = logger;
		this.options = options.Value;
	}

	protected override async Task ExecuteAsync(CancellationToken cancellationToken)
	{
		this.logger.LogInformation("Gathering metadata and service endpoints");

		ConcurrentBag<int> dataProviderIds = new();

		await Parallel.ForEachAsync(this.options.DataProviderSettings, cancellationToken, async (settings, innerCancellationToken) =>
		{
			try
			{
				dataProviderIds.Add(await AddDataProvider(settings.Name, settings.Url));
			}
			catch (Exception e)
			{
				this.logger.LogError(e, "Exception occurred while added data provider.");
			}
		});

		await Parallel.ForEachAsync(dataProviderIds, cancellationToken, async (dataProviderId, innerCancellationToken) =>
		{
			try
			{
				await UpdateServiceProviderCapabilities(dataProviderId)
					.Check(_ => UpdateServiceProviderRecords(dataProviderId))
					.Check(async _ =>
					{
						List<int> serviceEndpointIds = await ServiceEndpointIds(dataProviderId);

						await Parallel.ForEachAsync(serviceEndpointIds, innerCancellationToken,
							async (serviceEndpointId, innerCancellationToken2) =>
							{
								try
								{
									await UpdateServiceEndpointCapabilities(serviceEndpointId);
								}
								catch (Exception e)
								{
									this.logger.LogError(e, "Exception occurred while updating service endpoint capabilities.");
								}
							});

						return Result.Success();
					});
			}
			catch (Exception e)
			{
				this.logger.LogError(e, "Exception occurred while added data provider.");
			}
		});

		await BuildIndexAsync(cancellationToken);

		this.logger.LogInformation("Gathering metadata and service endpoints finished");
	}

	private async Task<int> AddDataProvider(string name, string cswUrl)
	{
		using IServiceScope serviceScope = this.serviceProvider.CreateScope();
		IServiceProvider scopedServiceProvider = serviceScope.ServiceProvider;

		RewriteDbContext dbContext = scopedServiceProvider.GetRequiredService<RewriteDbContext>();

		dbContext.DataProviders.Add(new DataProvider(name, cswUrl));
		await dbContext.SaveChangesAsync();

		return dbContext.DataProviders.Single().Id;
	}

	private async Task BuildIndexAsync(CancellationToken cancellationToken = default)
	{
		using IServiceScope serviceScope = this.serviceProvider.CreateScope();
		IServiceProvider scopedServiceProvider = serviceScope.ServiceProvider;

		ConcurrentDictionary<string, CacheEntry> index =
			scopedServiceProvider.GetRequiredService<ConcurrentDictionary<string, CacheEntry>>();

		RewriteDbContext dbContext = scopedServiceProvider.GetRequiredService<RewriteDbContext>();

		foreach (DataProvider dataProvider in await dbContext.DataProviders.ToListAsync(cancellationToken))
		{
			if (dataProvider.Status.Value != StatusEnum.Finished)
			{
				continue;
			}

			foreach (DataRecord record in dataProvider.Records.Where(x => x.ServiceEndpoints.Count > 0))
			{
				foreach (ServiceEndpoint serviceEndpoint in record.ServiceEndpoints)
				{
					if (serviceEndpoint.Status.Value != StatusEnum.Finished)
					{
						continue;
					}

					index.TryAdd(record.MetadataId,
						new CacheEntry
						{
							MetadataId = record.MetadataId,
							DataProviderHost = new Uri(dataProvider.GetRecordByIdUrl).Host,
							DataProviderVersion = dataProvider.Version,
							GetRecordByIdUrl = dataProvider.GetRecordByIdUrl,
							ServiceEndpointHost = new Uri(serviceEndpoint.GetCapabilitiesUrl).Host,
							ServiceEndpointVersion = serviceEndpoint.Version,
							FeatureType = serviceEndpoint.Name,
							FeatureTypeNamespacePrefix = serviceEndpoint.Name.Split(":").First(),
							DescribeFeatureTypeUrl = new Uri(serviceEndpoint.DescribeFeatureTypeUrl).GetLeftPart(UriPartial.Path),
							GetFeaturePostUrl = serviceEndpoint.GetFeaturePostUrl,
							SupportedOutputFormats = new HashSet<string>(serviceEndpoint.GetFeatureOutputFormats),
						});

					Console.Write($"> {record.MetadataId}: ");
					Console.Write($"{serviceEndpoint.GetCapabilitiesUrl}");
					Console.WriteLine($" ({string.Join(", ", serviceEndpoint.Layers.Select(x => x.Name))})");
				}
			}
		}
	}

	private Task<List<int>> ServiceEndpointIds(int dataProviderId)
	{
		using IServiceScope serviceScope = this.serviceProvider.CreateScope();
		IServiceProvider scopedServiceProvider = serviceScope.ServiceProvider;

		RewriteDbContext dbContext = scopedServiceProvider.GetRequiredService<RewriteDbContext>();

		return dbContext.DataProviders.Where(x => x.Id == dataProviderId)
			.SelectMany(x => x.Records)
			.SelectMany(x => x.ServiceEndpoints)
			.Select(x => x.Id)
			.ToListAsync();
	}

	private async Task<Result> UpdateServiceEndpointCapabilities(int serviceEndpointId)
	{
		using IServiceScope serviceScope = this.serviceProvider.CreateScope();
		IServiceProvider scopedServiceProvider = serviceScope.ServiceProvider;

		RewriteDbContext dbContext = scopedServiceProvider.GetRequiredService<RewriteDbContext>();

		ServiceEndpoint serviceEndpoint = await dbContext.ServiceEndpoints.SingleAsync(x => x.Id == serviceEndpointId);

		if (string.IsNullOrEmpty(serviceEndpoint.GetCapabilitiesUrl))
		{
			serviceEndpoint.Status.Value = StatusEnum.Failed;
			serviceEndpoint.Status.Message = "GetCapabilitiesUrl is empty";
			await dbContext.SaveChangesAsync();

			return Result.Failure("GetCapabilitiesUrl is empty");
		}

		serviceEndpoint.Status.Value = StatusEnum.Synchronizing;
		await dbContext.SaveChangesAsync();

		FeatureClient featureClient = scopedServiceProvider.GetRequiredService<FeatureClient>();

		return await featureClient.GetCapabilitiesAsync(serviceEndpoint.GetCapabilitiesUrl)
			.Tap(capabilities => serviceEndpoint.RawXml = capabilities.Root.ToString())
			.Check(capabilities => capabilities.LatestSupportedVersion(new[] { "1.1.0", "1.1.3", "2.0.0", "2.0.2" })
				.Tap(version => serviceEndpoint.Version = version))
			.Check(capabilities =>
				capabilities.DescribeFeatureTypeOperation().GetUrl().Tap(url => serviceEndpoint.DescribeFeatureTypeUrl = url))
			.Check(capabilities => capabilities.GetFeatureOperation().PostUrl().Tap(url => serviceEndpoint.GetFeaturePostUrl = url))
			.Tap(capabilities =>
			{
				serviceEndpoint.GlobalOutputFormats = capabilities.OutputFormats().ToList();
				capabilities.GetFeatureOperation()
					.OutputFormats()
					.Tap(outputFormats => serviceEndpoint.GetFeatureOutputFormats = outputFormats.ToList());
			})
			.Check(capabilities =>
			{
				if (!capabilities.FeatureTypes().Any(x => x.Name().IsSuccess))
				{
					return Result.Failure("FeatureTypes definition missing");
				}

				if (!string.IsNullOrWhiteSpace(serviceEndpoint.Name))
				{
					WFS.FeatureType? featureType = capabilities.FeatureTypes()
						.SingleOrDefault(x => x.Name().IsSuccess && x.Name().Value == serviceEndpoint.Name);

					if (featureType != null)
					{
						serviceEndpoint.Layers.Add(new FeatureLayer(featureType.Name().Value)
						{
							OutputFormats = featureType.OutputFormats().ToList(),
						});

						return Result.Success();
					}
				}

				foreach (WFS.FeatureType featureType in capabilities.FeatureTypes().Where(x => x.Name().IsSuccess))
				{
					serviceEndpoint.Layers.Add(new FeatureLayer(featureType.Name().Value)
					{
						OutputFormats = featureType.OutputFormats().ToList(),
					});
				}

				return Result.Success();
			})
			.Tap(async () =>
			{
				serviceEndpoint.Status.Value = StatusEnum.Finished;
				await dbContext.SaveChangesAsync();
			})
			.OnFailure(async message =>
			{
				serviceEndpoint.Status.Value = StatusEnum.Failed;
				serviceEndpoint.Status.Message = message;

				await dbContext.SaveChangesAsync();
			});
	}

	private async Task<Result<CSW.GetCapabilities>> UpdateServiceProviderCapabilities(int dataProviderId)
	{
		using IServiceScope serviceScope = this.serviceProvider.CreateScope();
		IServiceProvider scopedServiceProvider = serviceScope.ServiceProvider;

		RewriteDbContext dbContext = scopedServiceProvider.GetRequiredService<RewriteDbContext>();

		DataProvider dataProvider = await dbContext.DataProviders.SingleAsync(x => x.Id == dataProviderId);

		MetadataClient metadataClient = scopedServiceProvider.GetRequiredService<MetadataClient>();

		return await metadataClient.GetCapabilitiesAsync(dataProvider.Url)
			.Tap(capabilities => dataProvider.RawXml = capabilities.Root.ToString())
			.Check(capabilities => capabilities.LatestSupportedVersion(new[] { "2.0.0", "2.0.1", "2.0.2", "3.0.0", })
				.Tap(version => dataProvider.Version = version))
			.Check(capabilities => capabilities.GetRecordsOperation()
				.SupportsISOApplicationProfile()
				.Check(isSupported =>
				{
					dataProvider.SupportsISOApplicationSchema = isSupported;
					return isSupported ? Result.Success() : Result.Failure("ISO Application profile not supported.");
				}))
			.Check(capabilities => capabilities.GetRecordByIdOperation()
				.Check(x => x.SupportsISOApplicationProfile())
				.GetUrl()
				.Tap(url => dataProvider.GetRecordByIdUrl = url))
			.Check(async capabilities => (await metadataClient.GetTotalRecordsAsync(capabilities)).Tap(total =>
			{
				dataProvider.TotalRecords = total;
			}))
			.Tap(async () =>
			{
				dataProvider.Status.Value = StatusEnum.Finished;
				await dbContext.SaveChangesAsync();
			})
			.OnFailure(async message =>
			{
				dataProvider.Status.Value = StatusEnum.Failed;
				dataProvider.Status.Message = message;

				await dbContext.SaveChangesAsync();
			});
	}

	private async Task<Result> UpdateServiceProviderRecords(int dataProviderId)
	{
		using IServiceScope serviceScope = this.serviceProvider.CreateScope();
		IServiceProvider scopedServiceProvider = serviceScope.ServiceProvider;

		RewriteDbContext dbContext = scopedServiceProvider.GetRequiredService<RewriteDbContext>();

		DataProvider dataProvider = await dbContext.DataProviders.SingleAsync(x => x.Id == dataProviderId);

		dataProvider.Status.Value = StatusEnum.Synchronizing;
		await dbContext.SaveChangesAsync();

		MetadataClient metadataClient = scopedServiceProvider.GetRequiredService<MetadataClient>();

		for (int i = 0; i < ((dataProvider.TotalRecords - 1) / IndexMetadataHostedService.PageSize) + 1; i++)
		{
			await metadataClient
				.GetRecordsAsync(new CSW.GetCapabilities(XElement.Parse(dataProvider.RawXml)), dataProvider.Version, i,
					IndexMetadataHostedService.PageSize)
				.Bind(x => x.Metadata())
				.Tap(data =>
				{
					foreach (CSW.GetRecordsResponseMetadata metadata in data)
					{
						DataRecord record = new() { RawXml = metadata.Root.ToString(), };

						Result.Success(metadata)
							.Tap(x =>
							{
								x.Name().Tap(maybe => maybe.Map(name => record.Name = name));
							})
							.Tap(x =>
							{
								x.FileIdentifier().Tap(maybe => maybe.Map(fileIdentifier => record.MetadataId = fileIdentifier));
							});

						metadata.WfsEndpoints()
							.Tap(endpoints =>
							{
								foreach (CSW.GetRecordsResponseOnlineResource onlineResource in endpoints)
								{
									ServiceEndpoint serviceEndpoint = new();

									onlineResource.Name().Map(name => serviceEndpoint.Name = name);
									onlineResource.Url()
										.Map(url =>
										{
											return serviceEndpoint.GetCapabilitiesUrl = new Uri(url).GetLeftPart(UriPartial.Path);
										});

									record.ServiceEndpoints.Add(serviceEndpoint);
								}
							});

						dataProvider.Records.Add(record);
					}
				})
				.Tap(async () =>
				{
					dataProvider.Status.Value = StatusEnum.Finished;
					await dbContext.SaveChangesAsync();
				})
				.OnFailure(async message =>
				{
					dataProvider.Status.Value = StatusEnum.Failed;
					dataProvider.Status.Message = message;

					await dbContext.SaveChangesAsync();
				});
		}

		return Result.Success();
	}
}

public struct CacheEntry
{
	public required string DataProviderHost { get; init; }

	public required string ServiceEndpointHost { get; init; }

	public required string FeatureTypeNamespacePrefix { get; init; }

	public required string FeatureType { get; init; }

	public required string GetFeaturePostUrl { get; init; }

	public required HashSet<string> SupportedOutputFormats { get; init; }

	public required string GetRecordByIdUrl { get; init; }

	public required string DescribeFeatureTypeUrl { get; init; }

	public required string MetadataId { get; init; }

	public required string DataProviderVersion { get; init; }

	public required string ServiceEndpointVersion { get; init; }
}