// <copyright file="Status.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Data.Entities;

public class Status
{
	public DateTime LastUpdated { get; set; } = DateTime.Now;

	public StatusEnum Value { get; set; } = StatusEnum.New;

	public string Message { get; set; } = string.Empty;
}