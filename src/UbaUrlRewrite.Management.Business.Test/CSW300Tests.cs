// <copyright file="CSW300Tests.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Test;

using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using UbaUrlRewrite.Management.Business.Extensions;
using UbaUrlRewrite.Management.Business.Parsing;
using UbaUrlRewrite.Management.Business.Test.Core;
using Xunit;

public class CSW300Tests
{
	[Fact]
	public async Task InvalidInvalidServiceType()
	{
		MetadataClient metadataClient = new(FlurlHelper.Prepare(Responses.CSW.V300.GetCapabilities.InvalidInvalidServiceType));
		Result<CSW.GetCapabilities> capabilities = await metadataClient.GetCapabilitiesAsync(string.Empty);

		Assert.Equal("ServiceType element missing or not 'CSW'", capabilities.ServiceIdentification().Error);
	}

	[Fact]
	public async Task InvalidMissingServiceAttribute()
	{
		MetadataClient metadataClient = new(FlurlHelper.Prepare(Responses.CSW.V300.GetCapabilities.InvalidMissingVersionAttribute));
		Result<CSW.GetCapabilities> capabilities = await metadataClient.GetCapabilitiesAsync(string.Empty);

		Assert.Equal("Version attribute missing", capabilities.Version().Error);
	}

	[Fact]
	public async Task InvalidMissingServiceType()
	{
		MetadataClient metadataClient = new(FlurlHelper.Prepare(Responses.CSW.V300.GetCapabilities.InvalidMissingServiceType));
		Result<CSW.GetCapabilities> capabilities = await metadataClient.GetCapabilitiesAsync(string.Empty);

		Assert.Equal("ServiceType element missing or not 'CSW'", capabilities.ServiceIdentification().Error);
	}

	[Fact]
	public async Task Valid()
	{
		MetadataClient metadataClient = new(FlurlHelper.Prepare(Responses.CSW.V300.GetCapabilities.Valid));
		Result<CSW.GetCapabilities> capabilities = await metadataClient.GetCapabilitiesAsync(string.Empty);
		Result<CSW.GetRecords> getRecordsOperation = capabilities.GetRecordsOperation();

		Assert.Equal("3.0.0", capabilities.Version());
		Assert.Equal("3.0.0", capabilities.LatestSupportedVersion());
		Assert.Equal("https://demo.pycsw.org/cite/csw", getRecordsOperation.PostUrl().Value);
		Assert.False(getRecordsOperation.SupportsISOApplicationProfile().Value);
	}

	[Fact]
	public async Task ValidWithMissingServiceTypeVersions()
	{
		MetadataClient metadataClient = new(FlurlHelper.Prepare(Responses.CSW.V300.GetCapabilities.ValidWithMissingServiceIdentification));
		Result<CSW.GetCapabilities> capabilities = await metadataClient.GetCapabilitiesAsync(string.Empty);

		Assert.Equal("3.0.0", capabilities.Version());
		Assert.Equal("3.0.0", capabilities.LatestSupportedVersion());
	}
}