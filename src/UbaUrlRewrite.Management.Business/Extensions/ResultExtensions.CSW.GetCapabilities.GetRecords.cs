// <copyright file="ResultExtensions.CSW.GetCapabilities.GetRecords.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Extensions;

using CSharpFunctionalExtensions;
using UbaUrlRewrite.Management.Business.Parsing;

public static partial class CSWResultExtensions
{
	public static Result<string> GetUrl(this CSW.GetRecordById getRecordById) => Result.Success(getRecordById).GetUrl();

	public static Result<string> GetUrl(this Result<CSW.GetRecordById> getRecordById) => getRecordById.GetUrl<CSW.GetRecordById>();

	public static Result<string> GetUrl<T>(this Result<T> getRecords) where T : TypedXmlElementWrapper =>
		getRecords.Bind(element =>
		{
			Maybe<string> url = element.Root.Element("DCP")?.Element("HTTP")?.Element("Get")?.Attribute("href")?.Value ??
				Maybe<string>.None;

			return url.Map(uri => new Uri(uri).ToString()).ToResult("Elements for Get url missing");
		});

	public static Result<string> PostUrl(this CSW.GetRecords getRecords) => Result.Success(getRecords).PostUrl();

	public static Result<string> PostUrl(this Result<CSW.GetRecords> getRecords) => getRecords.PostUrl<CSW.GetRecords>();

	public static Result<string> PostUrl<T>(this Result<T> getRecords) where T : TypedXmlElementWrapper =>
		getRecords.Bind(element =>
		{
			Maybe<string> url = element.Root.Element("DCP")?.Element("HTTP")?.Element("Post")?.Attribute("href")?.Value ??
				Maybe<string>.None;

			return url.Map(uri => new Uri(uri).ToString()).ToResult("Elements for Post url missing");
		});

	public static Result<bool> SupportsISOApplicationProfile<T>(this Result<T> getRecords) where T : TypedXmlElementWrapper =>
		getRecords.Bind(x =>
		{
			ICollection<string>? schemas = x.Root.Elements("Parameter")
				.SingleOrDefault(element => element.Attribute("name")?.Value == "outputSchema")
				?.Descendants("Value") // Prefer Descendants to be http://www.opengis.net/ows and http://www.opengis.net/ows/2.0 compliant
				.Select(element => element.Value)
				.ToList();

			return schemas == null
				? Result.Failure<bool>("Parameter element for 'outputSchema' missing")
				: Result.Success(schemas.Any(schema => schema == "http://www.isotc211.org/2005/gmd"));
		});

	public static Result<bool> SupportsISOApplicationProfile(this Result<CSW.GetRecords> getRecords) =>
		getRecords.SupportsISOApplicationProfile<CSW.GetRecords>();

	public static Result<bool> SupportsISOApplicationProfile(this CSW.GetRecords getRecords) =>
		Result.Success(getRecords).SupportsISOApplicationProfile();

	public static Result<bool> SupportsISOApplicationProfile(this Result<CSW.GetRecordById> getRecordById) =>
		getRecordById.SupportsISOApplicationProfile<CSW.GetRecordById>();

	public static Result<bool> SupportsISOApplicationProfile(this CSW.GetRecordById getRecordById) =>
		Result.Success(getRecordById).SupportsISOApplicationProfile();
}