// <copyright file="Program.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Playground;

using System.Xml.Linq;
using Castle.Core.Internal;
using CSharpFunctionalExtensions;
using Flurl.Http.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UbaUrlRewrite.Data;
using UbaUrlRewrite.Data.Entities;
using UbaUrlRewrite.Management.Business.Extensions;
using UbaUrlRewrite.Management.Business.Parsing;

public static class Program
{
	private const int MaxRecords = 20;

	public static async Task Main(string[] args)
	{
		string cswUrl = "https://geonetwork.rest-gdi.geo-data.space/geonetwork/srv/ger/csw";

		ServiceCollection services = new();
		services.AddDbContext<RewriteDbContext>(x => x.UseLazyLoadingProxies().UseInMemoryDatabase("memory"));

		services.AddTransient<IFlurlClientFactory, PerBaseUrlFlurlClientFactory>();

		services.AddTransient<MetadataClient>();
		services.AddTransient<FeatureClient>();

		ServiceProvider serviceProvider = services.BuildServiceProvider();

		int dataProviderId = await Program.AddServiceProvider(serviceProvider, cswUrl);

		await Program.UpdateServiceProviderCapabilities(serviceProvider, dataProviderId)
			.Check(_ => Program.UpdateServiceProviderRecords(serviceProvider, dataProviderId))
			.Check(async _ =>
			{
				List<int> serviceEndpointIds = await Program.ServiceEndpointIds(serviceProvider, dataProviderId);
				IEnumerable<Result> results = serviceEndpointIds.Select(async serviceEndpointId =>
						await Program.UpdateServiceEndpointCapabilities(serviceProvider, serviceEndpointId))
					.Select(x => x.Result);

				return Result.Combine(results);
			});

		using IServiceScope serviceScope = serviceProvider.CreateScope();
		IServiceProvider scopedServiceProvider = serviceScope.ServiceProvider;

		RewriteDbContext dbContext = scopedServiceProvider.GetRequiredService<RewriteDbContext>();
		DataProvider dataProvider = await dbContext.DataProviders.SingleAsync();
		ICollection<DataRecord> records = dataProvider.Records;

		foreach (DataRecord record in records.Where(x => x.ServiceEndpoints.Count > 0))
		{
			Console.WriteLine($"{record.Name} ({record.ServiceEndpoints.Count} WFS Endpoints):");

			foreach (ServiceEndpoint serviceEndpoint in record.ServiceEndpoints)
			{
				if (serviceEndpoint.GetCapabilitiesUrl.IsNullOrEmpty())
				{
					Console.WriteLine("> [Missing GetCapabilitiesUrl]");
					continue;
				}

				Console.Write($"> {serviceEndpoint.GetCapabilitiesUrl}");
				Console.WriteLine($" ({string.Join(", ", serviceEndpoint.Layers.Select(x => x.Name))})");
			}
		}
	}

	public static async Task<Result> UpdateServiceEndpointCapabilities(IServiceProvider serviceProvider, int serviceEndpointId)
	{
		using IServiceScope serviceScope = serviceProvider.CreateScope();
		IServiceProvider scopedServiceProvider = serviceScope.ServiceProvider;

		RewriteDbContext dbContext = scopedServiceProvider.GetRequiredService<RewriteDbContext>();

		ServiceEndpoint serviceEndpoint = await dbContext.ServiceEndpoints.SingleAsync(x => x.Id == serviceEndpointId);

		if (string.IsNullOrEmpty(serviceEndpoint.GetCapabilitiesUrl))
		{
			serviceEndpoint.Status.Value = StatusEnum.Failed;
			await dbContext.SaveChangesAsync();

			return Result.Failure("GetCapabilitiesUrl is empty");
		}

		serviceEndpoint.Status.Value = StatusEnum.Synchronizing;
		await dbContext.SaveChangesAsync();

		FeatureClient featureClient = scopedServiceProvider.GetRequiredService<FeatureClient>();

		return await featureClient.GetCapabilitiesAsync(serviceEndpoint.GetCapabilitiesUrl)
			.Tap(capabilities =>
			{
				serviceEndpoint.RawXml = capabilities.Root.ToString();
			})
			.Check(capabilities => capabilities.LatestSupportedVersion().Tap(version => serviceEndpoint.Version = version))
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

	public static async Task<Result<CSW.GetCapabilities>> UpdateServiceProviderCapabilities(IServiceProvider serviceProvider,
		int dataProviderId)
	{
		using IServiceScope serviceScope = serviceProvider.CreateScope();
		IServiceProvider scopedServiceProvider = serviceScope.ServiceProvider;

		RewriteDbContext dbContext = scopedServiceProvider.GetRequiredService<RewriteDbContext>();

		DataProvider dataProvider = await dbContext.DataProviders.SingleAsync(x => x.Id == dataProviderId);

		MetadataClient metadataClient = scopedServiceProvider.GetRequiredService<MetadataClient>();

		return await metadataClient.GetCapabilitiesAsync(dataProvider.Url)
			.Tap(capabilities =>
			{
				dataProvider.RawXml = capabilities.Root.ToString();
			})
			.Check(capabilities => capabilities.LatestSupportedVersion().Tap(version => dataProvider.Version = version))
			.Check(capabilities => capabilities.GetRecordsOperation()
				.SupportsISOApplicationProfile()
				.Check(isSupported =>
				{
					dataProvider.SupportsISOApplicationSchema = isSupported;

					return isSupported ? Result.Success() : Result.Failure("ISO Application profile not supported.");
				}))
			.Check(capabilities => capabilities.GetRecordByIdOperation()
				.SupportsISOApplicationProfile()
				.Check(isSupported =>
				{
					dataProvider.SupportsISOApplicationSchema = isSupported;

					return isSupported ? Result.Success() : Result.Failure("ISO Application profile not supported.");
				}))
			.Check(capabilities =>
			{
				return capabilities.GetRecordByIdOperation().GetUrl().Tap(url => dataProvider.GetRecordByIdUrl = url);
			})
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

	public static async Task<Result> UpdateServiceProviderRecords(IServiceProvider serviceProvider, int dataProviderId)
	{
		using IServiceScope serviceScope = serviceProvider.CreateScope();
		IServiceProvider scopedServiceProvider = serviceScope.ServiceProvider;

		RewriteDbContext dbContext = scopedServiceProvider.GetRequiredService<RewriteDbContext>();

		DataProvider dataProvider = await dbContext.DataProviders.SingleAsync(x => x.Id == dataProviderId);

		dataProvider.Status.Value = StatusEnum.Synchronizing;
		await dbContext.SaveChangesAsync();

		MetadataClient metadataClient = scopedServiceProvider.GetRequiredService<MetadataClient>();

		for (int i = 0; i < (Math.Min(Program.MaxRecords, dataProvider.TotalRecords - 1) / 30) + 1; i++)
		{
			await metadataClient.GetRecordsAsync(new CSW.GetCapabilities(XElement.Parse(dataProvider.RawXml)), dataProvider.Version, i, 30)
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

	private static async Task<int> AddServiceProvider(ServiceProvider serviceProvider, string cswUrl)
	{
		using IServiceScope serviceScope = serviceProvider.CreateScope();
		IServiceProvider scopedServiceProvider = serviceScope.ServiceProvider;

		RewriteDbContext dbContext = scopedServiceProvider.GetRequiredService<RewriteDbContext>();

		dbContext.DataProviders.Add(new DataProvider("demo", cswUrl));
		await dbContext.SaveChangesAsync();

		return dbContext.DataProviders.Single().Id;
	}

	private static Task<List<int>> ServiceEndpointIds(ServiceProvider serviceProvider, int dataProviderId)
	{
		using IServiceScope serviceScope = serviceProvider.CreateScope();
		IServiceProvider scopedServiceProvider = serviceScope.ServiceProvider;

		RewriteDbContext dbContext = scopedServiceProvider.GetRequiredService<RewriteDbContext>();

		return dbContext.DataProviders.Where(x => x.Id == dataProviderId)
			.SelectMany(x => x.Records)
			.SelectMany(x => x.ServiceEndpoints)
			.Select(x => x.Id)
			.ToListAsync();
	}
}