// <copyright file="CacheEntry.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Proxy;

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

	public required bool IsInspireService { get; init; }

	public string IdentifierBase => IsInspireService ? $"{FeatureTypeNamespacePrefix}:inspireId" : "rest-gdi-agrar:Identifier";
}