// <copyright file="ServiceEndpoint.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Data.Entities;

public class ServiceEndpoint
{
	public string GetCapabilitiesUrl { get; set; } = string.Empty;

	public List<string> GetFeatureOutputFormats { get; set; } = new();

	public string DescribeFeatureTypeUrl { get; set; } = string.Empty;

	public string GetFeaturePostUrl { get; set; } = string.Empty;

	public ICollection<string> GlobalOutputFormats { get; set; } = new List<string>();

	public int Id { get; set; }

	public virtual ICollection<FeatureLayer> Layers { get; set; } = new List<FeatureLayer>();

	public string Name { get; set; } = string.Empty;

	// GetCapabilities
	public string RawXml { get; set; } = string.Empty;

	public Status Status { get; set; } = new();

	public string Version { get; set; } = string.Empty;
}