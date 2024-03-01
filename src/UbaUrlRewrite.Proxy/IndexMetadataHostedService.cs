// <copyright file="IndexMetadataHostedService.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Proxy;

public class IndexMetadataHostedService : BackgroundService
{
	private readonly IServiceProvider serviceProvider;

	public IndexMetadataHostedService(IServiceProvider serviceProvider)
	{
		this.serviceProvider = serviceProvider;
	}

	protected override Task ExecuteAsync(CancellationToken cancellationToken) =>
		this.serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IndexMetadataService>().RunAsync(cancellationToken);
}