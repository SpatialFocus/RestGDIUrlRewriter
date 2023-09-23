// <copyright file="ResultExtensions.CSW.GetRecords.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Extensions;

using System.Web;
using System.Xml.Linq;
using CSharpFunctionalExtensions;
using UbaUrlRewrite.Management.Business.Parsing;

public static partial class CSWResultExtensions
{
	public static Result<Maybe<string>> FileIdentifier(this CSW.GetRecordsResponseMetadata metadata) =>
		Result.Success(metadata).FileIdentifier();

	public static Result<Maybe<string>> FileIdentifier(this Result<CSW.GetRecordsResponseMetadata> metadata) =>
		metadata.Bind(element =>
		{
			string? value = element.Root.Element("fileIdentifier")?.Value;

			return Result.Success(!string.IsNullOrWhiteSpace(value) ? Maybe.From(value) : Maybe<string>.None);
		});

	public static Result<IEnumerable<CSW.GetRecordsResponseMetadata>> Metadata(this CSW.GetRecordsResponse getRecordsResponse) =>
		Result.Success(getRecordsResponse).Metadata();

	public static Result<IEnumerable<CSW.GetRecordsResponseMetadata>> Metadata(this Result<CSW.GetRecordsResponse> getRecordsResponse) =>
		getRecordsResponse.Bind(x =>
		{
			IEnumerable<CSW.GetRecordsResponseMetadata>? metadata = x.Root.Element("SearchResults")
				?.Elements("MD_Metadata")
				.Select(element => new CSW.GetRecordsResponseMetadata(element));

			return metadata != null
				? Result.Success(metadata)
				: Result.Failure<IEnumerable<CSW.GetRecordsResponseMetadata>>("SearchResults element missing");
		});

	public static Result<Maybe<string>> Name(this CSW.GetRecordsResponseMetadata metadata) => Result.Success(metadata).Name();

	public static Result<Maybe<string>> Name(this Result<CSW.GetRecordsResponseMetadata> metadata) =>
		metadata.Bind(element =>
		{
			// This should exist at least once, relax to Maybe.None even if missing
			XElement? identificationInfo = element.Root.Elements("identificationInfo").FirstOrDefault();

			// Could be any AbstractMD_Identification implementation
			XElement? identification = identificationInfo?.Elements().SingleOrDefault();

			string? title = identification?.Element("citation")?.Element("CI_Citation")?.Element("title")?.Value;

			return Result.Success(!string.IsNullOrWhiteSpace(title) ? Maybe.From(title) : Maybe<string>.None);
		});

	public static Result<Maybe<string>> Name(this Result<Maybe<CSW.GetRecordsResponseOnlineResource>> onlineResource) =>
		onlineResource.Bind(root => root.Map(maybe => Result.Success(maybe.Name()))
			.GetValueOrDefault(Result.Failure<Maybe<string>>("OnlineResource element missing")));

	public static Result<Maybe<string>> Name(this Result<CSW.GetRecordsResponseOnlineResource> metadata) =>
		metadata.Bind(element => Result.Success(element.Name()));

	public static Maybe<string> Name(this CSW.GetRecordsResponseOnlineResource element)
	{
		string? value = element.Root.Element("name")?.Value;

		return !string.IsNullOrWhiteSpace(value) ? Maybe.From(value) : Maybe<string>.None;
	}

	public static Result<Maybe<string>> Protocol(this Result<Maybe<CSW.GetRecordsResponseOnlineResource>> onlineResource) =>
		onlineResource.Bind(root => root.Map(maybe => Result.Success(maybe.Protocol()))
			.GetValueOrDefault(Result.Failure<Maybe<string>>("OnlineResource element missing")));

	public static Result<Maybe<string>> Protocol(this Result<CSW.GetRecordsResponseOnlineResource> metadata) =>
		metadata.Bind(element => Result.Success(element.Protocol()));

	public static Maybe<string> Protocol(this CSW.GetRecordsResponseOnlineResource element)
	{
		string? value = element.Root.Element("protocol")?.Value;

		return !string.IsNullOrWhiteSpace(value) ? Maybe.From(value) : Maybe<string>.None;
	}

	public static Result<Maybe<string>> Url(this Result<Maybe<CSW.GetRecordsResponseOnlineResource>> onlineResource) =>
		onlineResource.Bind(root => root.Map(maybe => Result.Success(maybe.Url()))
			.GetValueOrDefault(Result.Failure<Maybe<string>>("OnlineResource element missing")));

	public static Result<Maybe<string>> Url(this Result<CSW.GetRecordsResponseOnlineResource> metadata) =>
		metadata.Bind(element => Result.Success(element.Url()));

	public static Maybe<string> Url(this CSW.GetRecordsResponseOnlineResource element)
	{
		string? value = element.Root.Element("linkage")?.Value;

		return !string.IsNullOrWhiteSpace(value) ? Maybe.From(value) : Maybe<string>.None;
	}

	public static Result<IEnumerable<CSW.GetRecordsResponseOnlineResource>> WfsEndpoints(this CSW.GetRecordsResponseMetadata metadata) =>
		Result.Success(metadata).WfsEndpoints();

	public static Result<IEnumerable<CSW.GetRecordsResponseOnlineResource>> WfsEndpoints(
		this Result<CSW.GetRecordsResponseMetadata> metadata) =>
		metadata.Bind(element =>
		{
			XElement? distributionInfo = element.Root.Element("distributionInfo");

			if (distributionInfo == null)
			{
				return Result.Failure<IEnumerable<CSW.GetRecordsResponseOnlineResource>>("distributionInfo Element missing");
			}

			IEnumerable<XElement> onlineResources = distributionInfo.Descendants("CI_OnlineResource").ToList();

			IEnumerable<CSW.GetRecordsResponseOnlineResource> explicitOnlineResources = onlineResources.Select(onlineResource => new CSW.GetRecordsResponseOnlineResource(onlineResource))
				.Where(x => x.Protocol().GetValueOrDefault()?.StartsWith("OGC:WFS-http-get-capabilities") == true);

			IEnumerable<CSW.GetRecordsResponseOnlineResource> implicitOnlineResources = onlineResources.Select(onlineResource => new CSW.GetRecordsResponseOnlineResource(onlineResource))
				.Where(x =>
				{
					if (x.Protocol().GetValueOrDefault()?.StartsWith("OGC:WFS-http-get-capabilities") != true &&
						Uri.TryCreate(x.Url().GetValueOrDefault(), UriKind.Absolute, out Uri? uri))
					{
						return HttpUtility.ParseQueryString(uri.Query)["SERVICE"] == "WFS";
					}

					return false;
				});

			return Result.Success(explicitOnlineResources.Concat(implicitOnlineResources));
		});
}