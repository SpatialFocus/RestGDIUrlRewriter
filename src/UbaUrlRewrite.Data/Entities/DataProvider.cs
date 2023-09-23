// <copyright file="DataProvider.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Data.Entities;

public class DataProvider
{
	public DataProvider(string name, string url)
	{
		Name = name;
		Url = new Uri(url).GetLeftPart(UriPartial.Path);
	}

	public int Id { get; set; }

	// TODO: Unique index
	public string Name { get; set; }

	// GetCapabilitiesResponse
	public string RawXml { get; set; } = string.Empty;

	public virtual ICollection<DataRecord> Records { get; set; } = new List<DataRecord>();

	public Status Status { get; set; } = new();

	public bool SupportsISOApplicationSchema { get; set; }

	public int TotalRecords { get; set; }

	public string Url { get; set; }

	public string Version { get; set; } = string.Empty;

	public string GetRecordByIdUrl { get; set; } = string.Empty;
}