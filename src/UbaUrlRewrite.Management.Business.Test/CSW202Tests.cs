// <copyright file="CSW202Tests.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Test;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using UbaUrlRewrite.Management.Business.Extensions;
using UbaUrlRewrite.Management.Business.Parsing;
using UbaUrlRewrite.Management.Business.Test.Core;
using Xunit;

public class CSW202Tests
{
	[Fact]
	public async Task GetCapabilitiesFailsForInvalidServiceType()
	{
		MetadataClient metadataClient = new(FlurlHelper.Prepare(Responses.CSW.V202.GetCapabilities.InvalidInvalidServiceType));
		Result<CSW.GetCapabilities> capabilities = await metadataClient.GetCapabilitiesAsync(string.Empty);

		Assert.Equal("ServiceType element missing or not 'CSW'", capabilities.ServiceIdentification().Error);
	}

	[Fact]
	public async Task GetCapabilitiesFailsForMissingServiceAttribute()
	{
		MetadataClient metadataClient = new(FlurlHelper.Prepare(Responses.CSW.V202.GetCapabilities.InvalidMissingVersionAttribute));
		Result<CSW.GetCapabilities> capabilities = await metadataClient.GetCapabilitiesAsync(string.Empty);

		Assert.Equal("Version attribute missing", capabilities.Version().Error);
	}

	[Fact]
	public async Task GetCapabilitiesFailsForMissingServiceType()
	{
		MetadataClient metadataClient = new(FlurlHelper.Prepare(Responses.CSW.V202.GetCapabilities.InvalidMissingServiceType));
		Result<CSW.GetCapabilities> capabilities = await metadataClient.GetCapabilitiesAsync(string.Empty);

		Assert.Equal("ServiceType element missing or not 'CSW'", capabilities.ServiceIdentification().Error);
	}

	[Fact]
	public async Task GetCapabilitiesSucceedsForCompleteResponse()
	{
		MetadataClient metadataClient = new(FlurlHelper.Prepare(Responses.CSW.V202.GetCapabilities.Valid));
		Result<CSW.GetCapabilities> capabilities = await metadataClient.GetCapabilitiesAsync(string.Empty);

		Result<CSW.GetRecords> getRecordsOperation = capabilities.GetRecordsOperation();

		Assert.Equal("2.0.2", capabilities.Version());
		Assert.Equal("3.0.0", capabilities.LatestSupportedVersion());
		Assert.Equal("2.0.0", capabilities.LatestSupportedVersion(new[] { "2.0.0" }));
		Assert.Equal("https://demo.pycsw.org/cite/csw", getRecordsOperation.PostUrl().Value);
		Assert.True(getRecordsOperation.SupportsISOApplicationProfile().Value);
	}

	[Fact]
	public async Task GetCapabilitiesSucceedsForMissingServiceTypeVersions()
	{
		MetadataClient metadataClient =
			new(FlurlHelper.Prepare(Responses.CSW.V202.GetCapabilities.ValidWithMissingServiceIdentification));
		Result<CSW.GetCapabilities> capabilities = await metadataClient.GetCapabilitiesAsync(string.Empty);

		Assert.Equal("2.0.2", capabilities.Version());
		Assert.Equal("2.0.2", capabilities.LatestSupportedVersion());
	}

	[Fact]
	public async Task GetRecords()
	{
		Result<CSW.GetCapabilities> capabilities =
			await new MetadataClient(FlurlHelper.Prepare(Responses.CSW.V202.GetRecords.ValidCapabilities))
				.GetCapabilitiesAsync(string.Empty);

		MetadataClient metadataClient = new(FlurlHelper.Prepare(Responses.CSW.V202.GetRecords.GetRecordsResourceWithWFSProtocol));
		Result<CSW.GetRecordsResponse> getRecords = await metadataClient.GetRecordsAsync(capabilities);

		Result<IEnumerable<CSW.GetRecordsResponseMetadata>> metadataCollection = getRecords.Metadata();
		Assert.Single(metadataCollection.Value);
		CSW.GetRecordsResponseMetadata metadata = metadataCollection.Value.Single();

		Assert.Equal("9e75d506-1b99-45d9-afa0-b421d5ede3ff", metadata.FileIdentifier().Value);

		Result<IEnumerable<CSW.GetRecordsResponseOnlineResource>> wfsEndpoints = metadata.WfsEndpoints();
		CSW.GetRecordsResponseOnlineResource wfsEndpoint = wfsEndpoints.Value.Single();

		Assert.Equal("WFS Downloaddienst", wfsEndpoint.Name().Value);
		Assert.Equal("OGC:WFS", wfsEndpoint.Protocol().Value);
		Assert.Equal("http://www.bev.gv.at/bev.webservice/inspire?service=WFS&request=GetCapabilities", wfsEndpoint.Url().Value);
	}
}