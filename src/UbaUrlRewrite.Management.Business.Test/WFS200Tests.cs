// <copyright file="WFS200Tests.cs" company="Spatial Focus GmbH">
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

public class WFS200Tests
{
	[Fact]
	public async Task GetCapabilitiesSucceedsForCompleteResponse()
	{
		FeatureClient metadataClient = new(FlurlHelper.Prepare(Responses.WFS.V200.GetCapabilities.Valid));
		Result<WFS.GetCapabilities> capabilities = await metadataClient.GetCapabilitiesAsync(string.Empty);

		Result<WFS.GetFeature> featureOperation = capabilities.GetFeatureOperation();
		Result<WFS.DescribeFeatureType> featureTypeOperation = capabilities.DescribeFeatureTypeOperation();

		Assert.Equal("2.0.0", capabilities.Version());
		Assert.Equal("2.0.0", capabilities.LatestSupportedVersion());
		Assert.Equal("1.1.0", capabilities.LatestSupportedVersion(new[] { "1.1.0" }));
		Assert.Equal(new[] { "application/gml+xml; version=3.2", "text/xml; subtype=gml/3.2.0" }, capabilities.OutputFormats().Value);

		Assert.Equal("https://haleconnect.com/ows/services/org.789.0727f597-e611-4e3f-9363-b8ec70253867_wfs?", featureTypeOperation.GetUrl().Value);

		Assert.Equal("https://haleconnect.com/ows/services/org.789.0727f597-e611-4e3f-9363-b8ec70253867_wfs", featureOperation.PostUrl().Value);
		Assert.Equal(new[] { "application/gml+xml; version=4.0", "text/xml; subtype=gml/4.0.0" }, featureOperation.OutputFormats().Value);

		IEnumerable<WFS.FeatureType> featureTypes = capabilities.FeatureTypes().Value.ToList();

		Assert.Single(featureTypes);
		Assert.Equal("tn:TrafficFlowDirection", featureTypes.Single().Name().Value);
		Assert.Equal(new[] { "application/gml+xml; version=2.0", "text/xml; subtype=gml/2.0.0" }, featureTypes.Single().OutputFormats());
	}
}