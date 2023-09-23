// <copyright file="ResultExtensions.WFS.GetCapabilities.DescribeFeatureType.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Extensions;

using CSharpFunctionalExtensions;
using UbaUrlRewrite.Management.Business.Parsing;

public static partial class ResultExtensions
{
	public static IEnumerable<string> OutputFormats(this WFS.DescribeFeatureType getRecords) =>
		getRecords.Root.Elements("Parameter")
			.Where(parameter => parameter.Attribute("name")?.Value == "outputFormat")
			.SelectMany(parameter => parameter.Descendants("Value"))
			.Select(value => value.Value);

	public static Result<IEnumerable<string>> OutputFormats(this Result<WFS.DescribeFeatureType> describeFeatureType) =>
		describeFeatureType.Bind(element => Result.Success(element.OutputFormats()));

	public static Result<string> GetUrl(this WFS.DescribeFeatureType getRecords) => Result.Success(getRecords).GetUrl();

	public static Result<string> GetUrl(this Result<WFS.DescribeFeatureType> getRecords) =>
		getRecords.Bind(x =>
		{
			Maybe<string> url = x.Root.Element("DCP")?.Element("HTTP")?.Element("Get")?.Attribute("href")?.Value ?? Maybe<string>.None;

			return url.ToResult("Elements for Get url missing");
		});
}