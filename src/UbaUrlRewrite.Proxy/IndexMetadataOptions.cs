// <copyright file="IndexMetadataOptions.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Proxy;

public class IndexMetadataOptions
{
	public List<DataProviderSettings> DataProviderSettings { get; init; } = new();
}