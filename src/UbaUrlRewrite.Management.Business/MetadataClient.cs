// <copyright file="MetadataClient.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business;

using System.Xml.Linq;
using CSharpFunctionalExtensions;
using Flurl.Http;
using Flurl.Http.Configuration;
using UbaUrlRewrite.Management.Business.Extensions;
using UbaUrlRewrite.Management.Business.Parsing;

public class MetadataClient
{
	private readonly IFlurlClientFactory flurlClientFactory;

	public MetadataClient(IFlurlClientFactory flurlClientFactory)
	{
		this.flurlClientFactory = flurlClientFactory;
	}

	public Task<Result<CSW.GetCapabilities>> GetCapabilitiesAsync(string url) => GetCapabilitiesAsync(url, Maybe<string>.None);

	public async Task<Result<CSW.GetCapabilities>> GetCapabilitiesAsync(string url, Maybe<string> version)
	{
		// TODO: Polly? Result.Fail?
		// TODO: Application XML
		IFlurlRequest request = this.flurlClientFactory.Get(url)
			.Request()
			.SetQueryParam("service", "CSW")
			.SetQueryParam("request", "GetCapabilities");

		if (version.HasValue && !string.IsNullOrWhiteSpace(version.Value))
		{
			request.SetQueryParam("version", version);
		}

		Stream stream = await request.GetAsync().ReceiveStream();

		XElement getCapabilities;

		try
		{
			getCapabilities = XElement.Load(stream);
		}
		catch (Exception e)
		{
			return Result.Failure<CSW.GetCapabilities>("Couldn't parse xml");
		}

		getCapabilities.StripNamespaces();

		return Result.Success((CSW.GetCapabilities)getCapabilities);
	}

	public async Task<Result<CSW.GetRecordsResponse>> GetRecordsAsync(Result<CSW.GetCapabilities> capabilities,
		Maybe<string> version = default, Maybe<int> pageIndex = default, Maybe<int> pageSize = default)
	{
		Result<string> requestedVersion = version.Or(() => capabilities.Bind(c => c.Version()))
			.Ensure(v => capabilities.SupportsVersion(v)
				.Bind(supported => supported ? supported : Result.Failure<bool>("Unsupported version")))
			.Ensure(() => capabilities.GetRecordsOperation().Ensure(getRecords => getRecords.PostUrl()));

		pageIndex = pageIndex.Or(0);
		pageSize = pageSize.Or(100);

		return await requestedVersion.Map(async v =>
		{
			string cswNamespace;

			switch (v)
			{
				case "2.0.0":
				case "2.0.1":
				case "2.0.2":
					cswNamespace = $"http://www.opengis.net/cat/csw/{v}";
					break;

				case "3.0.0":
					cswNamespace = "http://www.opengis.net/cat/csw/3.0";
					break;

				default:
					return Result.Failure<CSW.GetRecordsResponse>("Unsupported version");
			}

			// TODO: Polly? Result.Fail?
			// TODO: Application XML

			string content = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
				<csw:GetRecords
					xmlns:csw=""{cswNamespace}""
					xmlns:gmd=""http://www.isotc211.org/2005/gmd""
					outputSchema=""http://www.isotc211.org/2005/gmd""
					service=""CSW""
					version=""{v}""
					startPosition=""{1 + (pageIndex.Value * pageSize.Value)}""
					{(cswNamespace != "3.0.0" ? @"resultType=""results""" : string.Empty)}
					maxRecords=""{pageSize.Value}"">
						<csw:Query typeNames=""gmd:MD_Metadata"">
							<csw:ElementSetName>full</csw:ElementSetName>
						</csw:Query>
				</csw:GetRecords>";

			Stream stream = await this.flurlClientFactory.Get(capabilities.GetRecordsOperation().PostUrl().Value)
				.Request()
				.WithHeader("Content-Type", "application/xml")
				.PostAsync(new StringContent(content))
				.ReceiveStream();

			XElement getRecords = XElement.Load(stream);
			getRecords.StripNamespaces();

			return new CSW.GetRecordsResponse(getRecords);
		});
	}

	public Task<Result<int>> GetTotalRecordsAsync(Result<CSW.GetCapabilities> capabilities) =>
		GetTotalRecordsAsync(capabilities, Maybe<string>.None);

	public async Task<Result<int>> GetTotalRecordsAsync(Result<CSW.GetCapabilities> capabilities, Maybe<string> version)
	{
		Result<string> requestedVersion = version.Or(() => capabilities.Bind(c => c.Version()))
			.Ensure(v => capabilities.SupportsVersion(v).Bind(supported => supported ? true : Result.Failure<bool>("Unsupported version")))
			.Ensure(() => capabilities.GetRecordsOperation().Ensure(getRecords => getRecords.PostUrl()));

		return await requestedVersion.Map(async v =>
		{
			string cswNamespace;

			switch (v)
			{
				case "2.0.0":
				case "2.0.1":
				case "2.0.2":
					cswNamespace = $"http://www.opengis.net/cat/csw/{v}";
					break;

				case "3.0.0":
					cswNamespace = "http://www.opengis.net/cat/csw/3.0";
					break;

				default:
					return Result.Failure<int>("Unsupported version");
			}

			// TODO: Polly? Result.Fail?
			// TODO: Application XML
			Stream stream;

			try
			{
				string content = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
				<csw:GetRecords
					xmlns:csw=""{cswNamespace}""
					service=""CSW""
					version=""{v}""
					{(cswNamespace == "3.0.0" ? @"maxRecords=""0""" : @"resultType=""hits""")}>
						<csw:Query typeNames=""csw:Record"">
							<csw:ElementSetName>brief</csw:ElementSetName>
						</csw:Query>
				</csw:GetRecords>";

				stream = await this.flurlClientFactory.Get(capabilities.GetRecordsOperation().PostUrl().Value)
					.Request()
					.WithHeader("Content-Type", "application/xml")
					.PostAsync(new StringContent(content))
					.ReceiveStream();
			}
			catch (FlurlHttpException e)
			{
				return Result.Failure<int>(await e.GetResponseStringAsync());
			}

			XElement getRecords = XElement.Load(stream);
			getRecords.StripNamespaces();

			if (!int.TryParse(getRecords.Descendants("SearchResults")?.SingleOrDefault()?.Attribute("numberOfRecordsMatched")?.Value,
					out int numbersOfRecordsMatched))
			{
				return Result.Failure<int>("Couldn't read numberOfRecordsMatched");
			}

			return numbersOfRecordsMatched;
		});
	}
}