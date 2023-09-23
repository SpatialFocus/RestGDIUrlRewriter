// <copyright file="MaybeExtensions.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business;

using CSharpFunctionalExtensions;

public static class MaybeExtensions
{
	public static Result<T> Or<T>(this Maybe<T> maybe, Func<Result<T>> fallbackOperation) =>
		maybe.HasNoValue ? fallbackOperation() : maybe.Value;
}