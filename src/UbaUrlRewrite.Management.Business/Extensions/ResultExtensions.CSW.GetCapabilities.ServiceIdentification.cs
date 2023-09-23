// <copyright file="ResultExtensions.CSW.GetCapabilities.ServiceIdentification.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Extensions;

using System.Xml.Linq;
using CSharpFunctionalExtensions;
using UbaUrlRewrite.Management.Business.Parsing;

public static partial class CSWResultExtensions
{
	public static Result<IEnumerable<string>>
		Abstracts(this Result<Maybe<CSW.GetCapabilitiesServiceIdentification>> serviceIdentification) =>
		serviceIdentification.Bind(root => root.Map(maybe => maybe.Abstracts())
			.GetValueOrDefault(Result.Failure<IEnumerable<string>>("ServiceIdentification element missing")));

	public static Result<IEnumerable<string>> Abstracts(this Result<CSW.GetCapabilitiesServiceIdentification> serviceIdentification) =>
		serviceIdentification.Bind(x =>
		{
			return Result.Success(x.Root.Elements("Abstract").Select(element => element.Value));
		});

	public static Result<IEnumerable<string>> Abstracts(this CSW.GetCapabilitiesServiceIdentification serviceIdentification) =>
		Result.Success(serviceIdentification).Abstracts();

	public static Result<IEnumerable<string>>
		Keywords(this Result<Maybe<CSW.GetCapabilitiesServiceIdentification>> serviceIdentification) =>
		serviceIdentification.Bind(root => root.Map(maybe => maybe.Keywords())
			.GetValueOrDefault(Result.Failure<IEnumerable<string>>("ServiceIdentification element missing")));

	public static Result<IEnumerable<string>> Keywords(this Result<CSW.GetCapabilitiesServiceIdentification> serviceIdentification) =>
		serviceIdentification.Bind(x =>
		{
			return Result.Success(x.Root.Elements("Keywords").Elements("Keyword").Select(element => element.Value));
		});

	public static Result<IEnumerable<string>> Keywords(this CSW.GetCapabilitiesServiceIdentification serviceIdentification) =>
		Result.Success(serviceIdentification).Keywords();

	public static Result<IEnumerable<string>> Titles(this Result<Maybe<CSW.GetCapabilitiesServiceIdentification>> serviceIdentification) =>
		serviceIdentification.Bind(root => root.Map(maybe => maybe.Titles())
			.GetValueOrDefault(Result.Failure<IEnumerable<string>>("ServiceIdentification element missing")));

	public static Result<IEnumerable<string>> Titles(this Result<CSW.GetCapabilitiesServiceIdentification> serviceIdentification) =>
		serviceIdentification.Bind(x =>
		{
			return Result.Success(x.Root.Elements("Title").Select(element => element.Value));
		});

	public static Result<IEnumerable<string>> Titles(this CSW.GetCapabilitiesServiceIdentification serviceIdentification) =>
		Result.Success(serviceIdentification).Titles();

	public static Result<IEnumerable<string>>
		Versions(this Result<Maybe<CSW.GetCapabilitiesServiceIdentification>> serviceIdentification) =>
		serviceIdentification.Bind(root => root.Map(maybe => maybe.Versions())
			.GetValueOrDefault(Result.Failure<IEnumerable<string>>("ServiceIdentification element missing")));

	public static Result<IEnumerable<string>> Versions(this Result<CSW.GetCapabilitiesServiceIdentification> serviceIdentification) =>
		serviceIdentification.Bind(x =>
		{
			IEnumerable<string> versions = x.Root.Elements("ServiceTypeVersion").Select(element => element.Value).ToList();

			if (!versions.Any())
			{
				return Result.Failure<IEnumerable<string>>("Elements of ServiceTypeVersion missing");
			}

			return Result.Success(versions);
		});

	public static Result<IEnumerable<string>> Versions(this CSW.GetCapabilitiesServiceIdentification serviceIdentification) =>
		Result.Success(serviceIdentification).Versions();
}