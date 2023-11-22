// <copyright file="HostingExtensions.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Proxy;

using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net;
using System.Net.Security;
using Flurl.Http.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using UbaUrlRewrite.Data;
using UbaUrlRewrite.Management.Business;
using Yarp.ReverseProxy.Forwarder;

internal static class HostingExtensions
{
	public static WebApplication ConfigurePipeline(this WebApplication app)
	{
		IHttpForwarder forwarder = app.Services.GetRequiredService<IHttpForwarder>();

		HttpMessageInvoker httpClient = new(new SocketsHttpHandler
		{
			UseProxy = false,
			AllowAutoRedirect = false,
			AutomaticDecompression = DecompressionMethods.None,
			UseCookies = false,
			ActivityHeadersPropagator = new ReverseProxyPropagator(DistributedContextPropagator.Current),
			ConnectTimeout = TimeSpan.FromSeconds(15),
			SslOptions = new SslClientAuthenticationOptions
			{
				RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true,
			},
		});

		ForwarderRequestConfig requestConfig = new() { ActivityTimeout = TimeSpan.FromSeconds(120), };

		app.MapGet("/{metadataId}", async (string metadataId,
			ConcurrentDictionary<string, CacheEntry> cache, HttpContext httpContext) =>
		{
			if (cache.TryGetValue(metadataId, out CacheEntry cachedEntry))
			{
				await forwarder.SendAsync(httpContext, cachedEntry.GetRecordByIdUrl, httpClient, requestConfig,
					Requests.GetRecordById(cachedEntry));
			}
		});

		app.MapGet("/{metadataId}/{applicationSchema}.{featureType}", async (string metadataId,
			ConcurrentDictionary<string, CacheEntry> cache, HttpContext httpContext) =>
		{
			if (cache.TryGetValue(metadataId, out CacheEntry cachedEntry))
			{
				await forwarder.SendAsync(httpContext, cachedEntry.GetFeaturePostUrl, httpClient, requestConfig,
					Requests.DescribeFeatureType(cachedEntry));
			}
		});

		app.MapGet("/{metadataId}/{applicationSchema}.{featureType}/{localId}", async (string metadataId, string localId,
			ConcurrentDictionary<string, CacheEntry> cache, HttpContext httpContext) =>
		{
			if (cache.TryGetValue(metadataId, out CacheEntry cachedEntry))
			{
				string? outputFormat = httpContext.Request.Query["outputFormat"].FirstOrDefault();

				if (outputFormat != null && !cachedEntry.SupportedOutputFormats.Contains(outputFormat))
				{
					httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
				}
				else
				{
					await forwarder.SendAsync(httpContext, cachedEntry.GetFeaturePostUrl, httpClient, requestConfig,
						Requests.GetFeatureByLocalId(cachedEntry, localId, outputFormat));
				}
			}
		});

		app.MapGet("/{metadataId}/{applicationSchema}.{featureType}/{localId}/{versionId}", async (string metadataId,
			string localId, string versionId, ConcurrentDictionary<string, CacheEntry> cache, HttpContext httpContext) =>
		{
			if (cache.TryGetValue(metadataId, out CacheEntry cachedEntry))
			{
				string? outputFormat = httpContext.Request.Query["outputFormat"].FirstOrDefault();

				if (outputFormat != null && !cachedEntry.SupportedOutputFormats.Contains(outputFormat))
				{
					httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
				}
				else
				{
					await forwarder.SendAsync(httpContext, cachedEntry.GetFeaturePostUrl, httpClient, requestConfig,
						Requests.GetFeatureByLocalIdAndVersionId(cachedEntry, localId, versionId, outputFormat));
				}
			}
		});

		return app;
	}

	public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
	{
		builder.Logging.ClearProviders();
		builder.Logging.AddSerilog(new LoggerConfiguration().MinimumLevel.Information()
			.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
			.WriteTo.Console(theme: AnsiConsoleTheme.Literate)
			.CreateLogger());

		builder.Services.AddHttpForwarder();

		OptionsWrapper<IndexMetadataOptions> options = new(new IndexMetadataOptions
		{
			DataProviderSettings = builder.Configuration.GetSection("MetadataEndpoints").Get<List<DataProviderSettings>>()!,
		});

		builder.Services.AddSingleton<IOptions<IndexMetadataOptions>>(options);
		builder.Services.AddHostedService<IndexMetadataHostedService>();

		builder.Services.AddDbContext<RewriteDbContext>(x => x.UseLazyLoadingProxies().UseInMemoryDatabase("memory"));

		builder.Services.AddTransient<IFlurlClientFactory, PerBaseUrlFlurlClientFactory>();

		builder.Services.AddTransient<MetadataClient>();
		builder.Services.AddTransient<FeatureClient>();

		builder.Services.AddSingleton<ConcurrentDictionary<string, CacheEntry>>();

		return builder.Build();
	}
}