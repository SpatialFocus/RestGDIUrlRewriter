// <copyright file="FeatureLayer.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Data.Entities;

public class FeatureLayer
{
	public FeatureLayer(string name)
	{
		Name = name;
	}

	public int Id { get; set; }

	public string Name { get; set; }

	public virtual List<string> OutputFormats { get; set; } = new();
}