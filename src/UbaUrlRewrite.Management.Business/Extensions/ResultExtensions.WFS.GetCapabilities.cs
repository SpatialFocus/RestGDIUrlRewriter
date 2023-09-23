// <copyright file="ResultExtensions.WFS.GetCapabilities.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Extensions;

using System.Xml.Linq;
using CSharpFunctionalExtensions;
using UbaUrlRewrite.Management.Business.Parsing;

public static partial class ResultExtensions
{
	public static Result<WFS.DescribeFeatureType> DescribeFeatureTypeOperation(this Result<WFS.GetCapabilities> getCapabilities) =>
		getCapabilities.Bind(x =>
		{
			XElement? describeFeatureType = x.Root.Element("OperationsMetadata")
				?.Elements("Operation")
				.SingleOrDefault(element => element.Attribute("name")?.Value == "DescribeFeatureType");

			if (describeFeatureType == null)
			{
				return Result.Failure<WFS.DescribeFeatureType>("Operation element for 'DescribeFeatureType' missing");
			}

			return Result.Success((WFS.DescribeFeatureType)describeFeatureType);
		});

	public static Result<WFS.DescribeFeatureType> DescribeFeatureTypeOperation(this WFS.GetCapabilities getCapabilities) =>
		Result.Success(getCapabilities).DescribeFeatureTypeOperation();

	public static IEnumerable<WFS.FeatureType> FeatureTypes(this WFS.GetCapabilities getCapabilities) =>
		getCapabilities.Root.Element("FeatureTypeList")?.Elements("FeatureType").Select(value => (WFS.FeatureType)value) ??
		Array.Empty<WFS.FeatureType>();

	public static Result<IEnumerable<WFS.FeatureType>> FeatureTypes(this Result<WFS.GetCapabilities> getCapabilities) =>
		getCapabilities.Bind(element => Result.Success(element.FeatureTypes()));

	public static Result<WFS.GetFeature> GetFeatureOperation(this Result<WFS.GetCapabilities> getCapabilities) =>
		getCapabilities.Bind(x =>
		{
			XElement? getFeature = x.Root.Element("OperationsMetadata")
				?.Elements("Operation")
				.SingleOrDefault(element => element.Attribute("name")?.Value == "GetFeature");

			if (getFeature == null)
			{
				return Result.Failure<WFS.GetFeature>("Operation element for 'GetFeature' missing");
			}

			return Result.Success((WFS.GetFeature)getFeature);
		});

	public static Result<WFS.GetFeature> GetFeatureOperation(this WFS.GetCapabilities getCapabilities) =>
		Result.Success(getCapabilities).GetFeatureOperation();

	public static Result<string> LatestSupportedVersion(this WFS.GetCapabilities getCapabilities) =>
		Result.Success(getCapabilities).LatestSupportedVersion();

	public static Result<string> LatestSupportedVersion(this WFS.GetCapabilities getCapabilities, ICollection<string> acceptedVersions) =>
		Result.Success(getCapabilities).LatestSupportedVersion(acceptedVersions);

	public static Result<string> LatestSupportedVersion(this Result<WFS.GetCapabilities> getCapabilities) =>
		getCapabilities.Bind(x =>
		{
			return x.ServiceIdentification()
				.Bind(maybe =>
				{
					return maybe.Map(serviceIdentification =>
							serviceIdentification.Versions().Map(versions => versions.OrderByDescending(version => version).First()))
						.GetValueOrDefault(x.Version);
				});
		});

	public static Result<string> LatestSupportedVersion(this Result<WFS.GetCapabilities> getCapabilities,
		ICollection<string> acceptedVersions) =>
		getCapabilities.Bind(x =>
		{
			return x.ServiceIdentification()
				.Bind(maybe =>
				{
					return maybe.Map(serviceIdentification => serviceIdentification.Versions()
							.Bind(versions => versions.Intersect(acceptedVersions)
								.OrderByDescending(version => version)
								.Select(Result.Success)
								.FirstOrDefault(Result.Failure<string>("Unsupported version"))))
						.GetValueOrDefault(x.Version()
							.Bind(version => acceptedVersions.Contains(version) ? version : Result.Failure<string>("Unsupported version")));
				});
		});

	public static IEnumerable<string> OutputFormats(this WFS.GetCapabilities getCapabilities) =>
		getCapabilities.Root.Element("OperationsMetadata")
			?.Elements("Parameter")
			.Where(parameter => parameter.Attribute("name")?.Value == "outputFormat")
			.SelectMany(parameter => parameter.Descendants("Value"))
			.Select(value => value.Value) ?? Array.Empty<string>();

	public static Result<IEnumerable<string>> OutputFormats(this Result<WFS.GetCapabilities> getCapabilities) =>
		getCapabilities.Bind(element => Result.Success(element.OutputFormats()));

	public static Result<Maybe<WFS.GetCapabilitiesServiceIdentification>> ServiceIdentification(this WFS.GetCapabilities getCapabilities) =>
		Result.Success(getCapabilities).ServiceIdentification();

	public static Result<Maybe<WFS.GetCapabilitiesServiceIdentification>> ServiceIdentification(
		this Result<WFS.GetCapabilities> getCapabilities) =>
		getCapabilities.Bind(x =>
		{
			XElement? serviceIdentification = x.Root.Element("ServiceIdentification");

			if (serviceIdentification != null)
			{
				if (serviceIdentification.Elements("ServiceType").SingleOrDefault()?.Value != "WFS")
				{
					return Result.Failure<Maybe<WFS.GetCapabilitiesServiceIdentification>>("ServiceType element missing or not 'WFS'");
				}
			}

			return serviceIdentification != null
				? (WFS.GetCapabilitiesServiceIdentification)serviceIdentification
				: Maybe<WFS.GetCapabilitiesServiceIdentification>.None;
		});

	public static Result<bool> SupportsVersion(this WFS.GetCapabilities getCapabilities, string version) =>
		Result.Success(getCapabilities).SupportsVersion(version);

	public static Result<bool> SupportsVersion(this Result<WFS.GetCapabilities> getCapabilities, string version) =>
		getCapabilities.Bind(x =>
		{
			return x.ServiceIdentification()
				.Bind(maybe =>
				{
					return maybe.Map(serviceIdentification => serviceIdentification.Versions().Map(versions => versions.Contains(version)))
						.GetValueOrDefault(getCapabilities.Version().Map(v => v == version));
				});
		});

	public static Result<string> Version(this WFS.GetCapabilities getCapabilities) => Result.Success(getCapabilities).Version();

	public static Result<string> Version(this Result<WFS.GetCapabilities> getCapabilities) =>
		getCapabilities.Bind(x =>
		{
			XAttribute? versionAttribute = x.Root.Attribute("version");

			if (versionAttribute == null || string.IsNullOrWhiteSpace(versionAttribute.Value))
			{
				return Result.Failure<string>("Version attribute missing");
			}

			return versionAttribute.Value;
		});
}