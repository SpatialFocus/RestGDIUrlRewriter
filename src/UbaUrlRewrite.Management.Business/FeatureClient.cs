// <copyright file="FeatureClient.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business;

using System.Xml.Linq;
using CSharpFunctionalExtensions;
using Flurl.Http;
using Flurl.Http.Configuration;
using UbaUrlRewrite.Management.Business.Parsing;

public class FeatureClient
{
	private readonly IFlurlClientFactory flurlClientFactory;

	public FeatureClient(IFlurlClientFactory flurlClientFactory)
	{
		this.flurlClientFactory = flurlClientFactory;
	}

	public Task<Result<WFS.GetCapabilities>> GetCapabilitiesAsync(string url) => GetCapabilitiesAsync(url, Maybe<string>.None);

	public async Task<Result<WFS.GetCapabilities>> GetCapabilitiesAsync(string url, Maybe<string> version)
	{
		// TODO: Polly? Result.Fail?
		// TODO: Application XML
		IFlurlRequest request = this.flurlClientFactory.Get(url)
			.Request()
			.SetQueryParam("service", "WFS")
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
			return Result.Failure<WFS.GetCapabilities>("Couldn't parse xml");
		}

		getCapabilities.StripNamespaces();

		return Result.Success((WFS.GetCapabilities)getCapabilities);
	}
}