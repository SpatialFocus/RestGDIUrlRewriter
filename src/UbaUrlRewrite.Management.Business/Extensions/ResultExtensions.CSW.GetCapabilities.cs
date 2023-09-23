// <copyright file="ResultExtensions.CSW.GetCapabilities.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Extensions;

using System.Xml.Linq;
using CSharpFunctionalExtensions;
using UbaUrlRewrite.Management.Business.Parsing;

public static partial class ResultExtensions
{
	public static Result<CSW.GetRecords> GetRecordsOperation(this Result<CSW.GetCapabilities> getCapabilities) =>
		getCapabilities.Bind(x =>
		{
			XElement? getRecords = x.Root.Element("OperationsMetadata")
				?.Elements("Operation")
				.SingleOrDefault(element => element.Attribute("name")?.Value == "GetRecords");

			if (getRecords == null)
			{
				return Result.Failure<CSW.GetRecords>("Operation element for 'GetRecords' missing");
			}

			return Result.Success((CSW.GetRecords)getRecords);
		});

	public static Result<CSW.GetRecordById> GetRecordByIdOperation(this Result<CSW.GetCapabilities> getCapabilities) =>
		getCapabilities.Bind(x =>
		{
			XElement? getRecordById = x.Root.Element("OperationsMetadata")
				?.Elements("Operation")
				.SingleOrDefault(element => element.Attribute("name")?.Value == "GetRecordById");

			if (getRecordById == null)
			{
				return Result.Failure<CSW.GetRecordById>("Operation element for 'GetRecordById' missing");
			}

			return Result.Success((CSW.GetRecordById)getRecordById);
		});

	public static Result<CSW.GetRecords> GetRecordsOperation(this CSW.GetCapabilities getCapabilities) =>
		Result.Success(getCapabilities).GetRecordsOperation();

	public static Result<CSW.GetRecordById> GetRecordByIdOperation(this CSW.GetCapabilities getCapabilities) =>
		Result.Success(getCapabilities).GetRecordByIdOperation();

	public static Result<string> LatestSupportedVersion(this CSW.GetCapabilities getCapabilities) =>
		Result.Success(getCapabilities).LatestSupportedVersion();

	public static Result<string> LatestSupportedVersion(this CSW.GetCapabilities getCapabilities, ICollection<string> acceptedVersions) =>
		Result.Success(getCapabilities).LatestSupportedVersion(acceptedVersions);

	public static Result<string> LatestSupportedVersion(this Result<CSW.GetCapabilities> getCapabilities) =>
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

	public static Result<string> LatestSupportedVersion(this Result<CSW.GetCapabilities> getCapabilities, ICollection<string> acceptedVersions) =>
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

	public static Result<Maybe<CSW.GetCapabilitiesServiceIdentification>> ServiceIdentification(this CSW.GetCapabilities getCapabilities) =>
		Result.Success(getCapabilities).ServiceIdentification();

	public static Result<Maybe<CSW.GetCapabilitiesServiceIdentification>> ServiceIdentification(
		this Result<CSW.GetCapabilities> getCapabilities) =>
		getCapabilities.Bind(x =>
		{
			XElement? serviceIdentification = x.Root.Element("ServiceIdentification");

			if (serviceIdentification != null)
			{
				if (serviceIdentification.Elements("ServiceType").SingleOrDefault()?.Value != "CSW")
				{
					return Result.Failure<Maybe<CSW.GetCapabilitiesServiceIdentification>>("ServiceType element missing or not 'CSW'");
				}
			}

			return serviceIdentification != null
				? (CSW.GetCapabilitiesServiceIdentification)serviceIdentification
				: Maybe<CSW.GetCapabilitiesServiceIdentification>.None;
		});

	public static Result<bool> SupportsVersion(this CSW.GetCapabilities getCapabilities, string version) =>
		Result.Success(getCapabilities).SupportsVersion(version);

	public static Result<bool> SupportsVersion(this Result<CSW.GetCapabilities> getCapabilities, string version) =>
		getCapabilities.Bind(x =>
		{
			return x.ServiceIdentification()
				.Bind(maybe =>
				{
					return maybe.Map(serviceIdentification => serviceIdentification.Versions().Map(versions => versions.Contains(version)))
						.GetValueOrDefault(getCapabilities.Version().Map(v => v == version));
				});
		});

	public static Result<string> Version(this CSW.GetCapabilities getCapabilities) => Result.Success(getCapabilities).Version();

	public static Result<string> Version(this Result<CSW.GetCapabilities> getCapabilities) =>
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