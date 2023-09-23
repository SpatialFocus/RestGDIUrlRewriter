// <copyright file="RewriteDbContext.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UbaUrlRewrite.Data.Entities;

public class RewriteDbContext : DbContext
{
	public RewriteDbContext(DbContextOptions<RewriteDbContext> options) : base(options)
	{
	}

	public DbSet<DataProvider> DataProviders { get; set; } = null!;

	public DbSet<DataRecord> DataRecords { get; set; } = null!;

	public DbSet<FeatureLayer> FeatureLayers { get; set; } = null!;

	public DbSet<ServiceEndpoint> ServiceEndpoints { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<DataProvider>(entity =>
		{
			entity.OwnsOne(x => x.Status);

			entity.HasMany(x => x.Records).WithOne(x => x.DataProvider).IsRequired();
		});

		modelBuilder.Entity<DataRecord>(entity =>
		{
			entity.HasMany(x => x.ServiceEndpoints).WithOne().IsRequired();
		});

		modelBuilder.Entity<ServiceEndpoint>(entity =>
		{
			entity.OwnsOne(x => x.Status);

			entity.Property(x => x.GlobalOutputFormats)
				.HasConversion(v => string.Join('|', v), v => v.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList());

			entity.Property(x => x.GetFeatureOutputFormats)
				.HasConversion(v => string.Join('|', v), v => v.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList());

			entity.HasMany(x => x.Layers).WithOne().IsRequired();
		});

		modelBuilder.Entity<FeatureLayer>(entity =>
		{
			entity.Property(x => x.OutputFormats)
				.HasConversion(v => string.Join('|', v), v => v.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList());
		});
	}
}