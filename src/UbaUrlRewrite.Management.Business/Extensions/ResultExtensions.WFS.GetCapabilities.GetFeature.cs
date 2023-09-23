// <copyright file="ResultExtensions.WFS.GetCapabilities.GetFeature.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Extensions;

using CSharpFunctionalExtensions;
using UbaUrlRewrite.Management.Business.Parsing;

public static partial class ResultExtensions
{
	public static IEnumerable<string> OutputFormats(this WFS.GetFeature getRecords) =>
		getRecords.Root.Elements("Parameter")
			.Where(parameter => parameter.Attribute("name")?.Value == "outputFormat")
			.SelectMany(parameter => parameter.Descendants("Value"))
			.Select(value => value.Value);

	public static Result<IEnumerable<string>> OutputFormats(this Result<WFS.GetFeature> getFeature) =>
		getFeature.Bind(element => Result.Success(element.OutputFormats()));

	public static Result<string> PostUrl(this WFS.GetFeature getRecords) => Result.Success(getRecords).PostUrl();

	public static Result<string> PostUrl(this Result<WFS.GetFeature> getRecords) =>
		getRecords.Bind(x =>
		{
			Maybe<string> url = x.Root.Element("DCP")?.Element("HTTP")?.Element("Post")?.Attribute("href")?.Value ?? Maybe<string>.None;

			return url.ToResult("Elements for Post url missing");
		});
}