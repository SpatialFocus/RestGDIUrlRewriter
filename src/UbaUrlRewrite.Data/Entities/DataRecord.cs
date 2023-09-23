// <copyright file="DataRecord.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Data.Entities;

public class DataRecord
{
	public int Id { get; set; }

	// FileIdentifier
	public string MetadataId { get; set; } = string.Empty;

	public string Name { get; set; } = string.Empty;

	// MD_Metadata
	public string RawXml { get; set; } = string.Empty;

	public virtual List<ServiceEndpoint> ServiceEndpoints { get; set; } = new();

	public virtual DataProvider DataProvider { get; set; } = null!;
}