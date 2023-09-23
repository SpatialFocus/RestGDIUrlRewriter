// <copyright file="ResultExtensions.WFS.GetCapabilities.FeatureType.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Extensions;

using System.Xml.Linq;
using CSharpFunctionalExtensions;
using UbaUrlRewrite.Management.Business.Parsing;

public static partial class ResultExtensions
{
	public static Result<string> Name(this WFS.FeatureType featureType) =>
		Maybe.From(featureType.Root.Element("Name")?.Value!).ToResult("Name element missing");

	public static Result<string> Name(this Result<WFS.FeatureType> featureType) => featureType.Bind(element => element.Name());

	public static IEnumerable<string> OutputFormats(this WFS.FeatureType featureType) =>
		featureType.Root.Elements("OutputFormats")
			?.Elements("Format")
			.Where(value => !string.IsNullOrWhiteSpace(value.Value))
			.Select(value => value.Value) ?? Array.Empty<string>();

	public static Result<IEnumerable<string>> OutputFormats(this Result<WFS.FeatureType> featureType) =>
		featureType.Bind(element => Result.Success(element.OutputFormats()));
}