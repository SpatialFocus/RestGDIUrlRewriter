// <copyright file="ResultExtensions.WFS.GetCapabilities.ServiceIdentification.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Extensions;

using System.Xml.Linq;
using CSharpFunctionalExtensions;
using UbaUrlRewrite.Management.Business.Parsing;

public static partial class ResultExtensions
{
	public static Result<IEnumerable<string>>
		Abstracts(this Result<Maybe<WFS.GetCapabilitiesServiceIdentification>> serviceIdentification) =>
		serviceIdentification.Bind(root => root.Map(maybe => maybe.Abstracts())
			.GetValueOrDefault(Result.Failure<IEnumerable<string>>("ServiceIdentification element missing")));

	public static Result<IEnumerable<string>> Abstracts(this Result<WFS.GetCapabilitiesServiceIdentification> serviceIdentification) =>
		serviceIdentification.Bind(x =>
		{
			return Result.Success(x.Root.Elements("Abstract").Select(element => element.Value));
		});

	public static Result<IEnumerable<string>> Abstracts(this WFS.GetCapabilitiesServiceIdentification serviceIdentification) =>
		Result.Success(serviceIdentification).Abstracts();

	public static Result<IEnumerable<string>>
		Keywords(this Result<Maybe<WFS.GetCapabilitiesServiceIdentification>> serviceIdentification) =>
		serviceIdentification.Bind(root => root.Map(maybe => maybe.Keywords())
			.GetValueOrDefault(Result.Failure<IEnumerable<string>>("ServiceIdentification element missing")));

	public static Result<IEnumerable<string>> Keywords(this Result<WFS.GetCapabilitiesServiceIdentification> serviceIdentification) =>
		serviceIdentification.Bind(x =>
		{
			return Result.Success(x.Root.Elements("Keywords").Elements("Keyword").Select(element => element.Value));
		});

	public static Result<IEnumerable<string>> Keywords(this WFS.GetCapabilitiesServiceIdentification serviceIdentification) =>
		Result.Success(serviceIdentification).Keywords();

	public static Result<IEnumerable<string>> Titles(this Result<Maybe<WFS.GetCapabilitiesServiceIdentification>> serviceIdentification) =>
		serviceIdentification.Bind(root => root.Map(maybe => maybe.Titles())
			.GetValueOrDefault(Result.Failure<IEnumerable<string>>("ServiceIdentification element missing")));

	public static Result<IEnumerable<string>> Titles(this Result<WFS.GetCapabilitiesServiceIdentification> serviceIdentification) =>
		serviceIdentification.Bind(x =>
		{
			return Result.Success(x.Root.Elements("Title").Select(element => element.Value));
		});

	public static Result<IEnumerable<string>> Titles(this WFS.GetCapabilitiesServiceIdentification serviceIdentification) =>
		Result.Success(serviceIdentification).Titles();

	public static Result<IEnumerable<string>>
		Versions(this Result<Maybe<WFS.GetCapabilitiesServiceIdentification>> serviceIdentification) =>
		serviceIdentification.Bind(root => root.Map(maybe => maybe.Versions())
			.GetValueOrDefault(Result.Failure<IEnumerable<string>>("ServiceIdentification element missing")));

	public static Result<IEnumerable<string>> Versions(this Result<WFS.GetCapabilitiesServiceIdentification> serviceIdentification) =>
		serviceIdentification.Bind(x =>
		{
			IEnumerable<string> versions = x.Root.Elements("ServiceTypeVersion").Select(element => element.Value).ToList();

			if (!versions.Any())
			{
				return Result.Failure<IEnumerable<string>>("Elements of ServiceTypeVersion missing");
			}

			return Result.Success(versions);
		});

	public static Result<IEnumerable<string>> Versions(this WFS.GetCapabilitiesServiceIdentification serviceIdentification) =>
		Result.Success(serviceIdentification).Versions();
}