// <copyright file="DataProviderSettings.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Proxy;

public class DataProviderSettings
{
	public required string Name { get; init; }

	public required string Url { get; init; }
}