// <copyright file="FlurlHelper.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Test.Core;

using System.Net.Http;
using System.Threading;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using NSubstitute;

public class FlurlHelper
{
	public static IFlurlClientFactory Prepare(string response)
	{
		IFlurlResponse flurlResponse = Substitute.For<IFlurlResponse>();
		flurlResponse.GetStreamAsync().Returns(response.ToStream());

		IFlurlRequest flurlRequest = Substitute.For<IFlurlRequest>();
		flurlRequest.Url.Returns(new Url());
		flurlRequest.SendAsync(Arg.Any<HttpMethod>(), Arg.Any<HttpContent>(), Arg.Any<CancellationToken>(), Arg.Any<HttpCompletionOption>())
			.Returns(info => flurlResponse);

		IFlurlClient flurlClient = Substitute.For<IFlurlClient>();
		flurlClient.Request(Arg.Any<object[]>()).Returns(flurlRequest);

		IFlurlClientFactory flurlClientFactory = Substitute.For<IFlurlClientFactory>();
		flurlClientFactory.Get(Arg.Any<Url>()).Returns(flurlClient);

		return flurlClientFactory;
	}
}