// <copyright file="StatusEnum.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Data;

public enum StatusEnum
{
	New,

	Synchronizing,

	Finished,

	Failed,

	Canceled,
}