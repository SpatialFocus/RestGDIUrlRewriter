// <copyright file="Requests.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Proxy
{
	using Yarp.ReverseProxy.Forwarder;

	public static class Requests
	{
		public static Func<HttpContext, HttpRequestMessage, ValueTask> DescribeFeatureType(CacheEntry cachedEntry) =>
			(_, proxyRequest) =>
			{
				QueryString queryString = new QueryString().Add("service", "WFS")
					.Add("version", cachedEntry.ServiceEndpointVersion)
					.Add("request", "DescribeFeatureType")
					.Add("typeName", cachedEntry.FeatureType);

				proxyRequest.Method = HttpMethod.Get;
				proxyRequest.RequestUri = RequestUtilities.MakeDestinationAddress(cachedEntry.DescribeFeatureTypeUrl.Replace("wfs", "ows"),
					string.Empty, queryString);

				proxyRequest.Headers.Host = cachedEntry.ServiceEndpointHost;

				return default;
			};

		public static Func<HttpContext, HttpRequestMessage, ValueTask> GetFeatureByLocalId(CacheEntry cachedEntry, string localId,
			string? outputFormat) =>
			(_, proxyRequest) =>
			{
				proxyRequest.Method = HttpMethod.Post;

				string content;

				switch (cachedEntry.ServiceEndpointVersion)
				{
					case "1.1.0":
					case "1.1.3":
						content = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
							<GetFeature service=""WFS"" version=""{cachedEntry.DataProviderVersion}""
								xmlns:ogc=""http://www.opengis.net/ogc""
								xmlns=""http://www.opengis.net/wfs""
								xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
								xsi:schemaLocation=""http://www.opengis.net/wfs ../wfs/1.0.0/WFS-basic.xsd"">
								<Query typeName=""{cachedEntry.FeatureType}"">
									<ogc:Filter>
										<ogc:PropertyIsEqualTo>
											<ogc:PropertyName>{cachedEntry.FeatureTypeNamespacePrefix}:inspireId/base:Identifier/base:localId</ogc:PropertyName>
											<ogc:Literal>{localId}</ogc:Literal>
										</ogc:PropertyIsEqualTo>
									</ogc:Filter>
								</Query>
							</GetFeature>";
						break;

					case "2.0.0":
					case "2.0.2":
						content = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
							<GetFeature service=""WFS"" version=""{cachedEntry.DataProviderVersion}""
								xmlns:ogc=""http://www.opengis.net/ogc""
								xmlns=""http://www.opengis.net/wfs/2.0""
								xmlns:fes=""http://www.opengis.net/fes/2.0""
								xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
								xsi:schemaLocation=""http://www.opengis.net/wfs/2.0 http://schemas.opengis.net/wfs/2.0/wfs.xsd"">
								<Query typeNames=""{cachedEntry.FeatureType}"">
									<ogc:Filter>
										<fes:PropertyIsEqualTo>
											<fes:ValueReference>{cachedEntry.FeatureTypeNamespacePrefix}:inspireId/base:Identifier/base:localId</fes:ValueReference>
											<fes:Literal>{localId}</fes:Literal>
										</fes:PropertyIsEqualTo>
									</ogc:Filter>
								</Query>
							</GetFeature>";
						break;

					default:
						throw new InvalidOperationException("WFS version not supported");
				}

				proxyRequest.Content = new StringContent(content);
				proxyRequest.RequestUri = RequestUtilities.MakeDestinationAddress(cachedEntry.GetFeaturePostUrl.Replace("wfs", "ows"),
					string.Empty, outputFormat == null ? QueryString.Empty : default(QueryString).Add("outputFormat", outputFormat));
				proxyRequest.Headers.Host = cachedEntry.ServiceEndpointHost;

				return default;
			};

		public static Func<HttpContext, HttpRequestMessage, ValueTask> GetFeatureByLocalIdAndVersionId(CacheEntry cachedEntry,
			string localId, string versionId, string? outputFormat) =>
			(_, proxyRequest) =>
			{
				proxyRequest.Method = HttpMethod.Post;

				string content;

				switch (cachedEntry.ServiceEndpointVersion)
				{
					case "1.1.0":
					case "1.1.3":
						content = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
							<GetFeature service=""WFS"" version=""{cachedEntry.DataProviderVersion}""
								xmlns:ogc=""http://www.opengis.net/ogc""
								xmlns=""http://www.opengis.net/wfs""
								xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
								xsi:schemaLocation=""http://www.opengis.net/wfs ../wfs/1.0.0/WFS-basic.xsd"">
								<Query typeName=""{cachedEntry.FeatureType}"">
									<ogc:Filter>
										<ogc:And>
											<ogc:PropertyIsEqualTo>
												<ogc:PropertyName>{cachedEntry.FeatureTypeNamespacePrefix}:inspireId/base:Identifier/base:localId</ogc:PropertyName>
												<ogc:Literal>{localId}</ogc:Literal>
											</ogc:PropertyIsEqualTo>
											<ogc:PropertyIsEqualTo>
												<ogc:PropertyName>{cachedEntry.FeatureTypeNamespacePrefix}:inspireId/base:Identifier/base:versionId</ogc:PropertyName>
												<ogc:Literal>{versionId}</ogc:Literal>
											</ogc:PropertyIsEqualTo>
										</ogc:And>
									</ogc:Filter>
								</Query>
							</GetFeature>";
						break;

					case "2.0.0":
					case "2.0.2":
						content = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
							<GetFeature service=""WFS"" version=""{cachedEntry.DataProviderVersion}""
								xmlns:ogc=""http://www.opengis.net/ogc""
								xmlns=""http://www.opengis.net/wfs/2.0""
								xmlns:fes=""http://www.opengis.net/fes/2.0""
								xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
								xsi:schemaLocation=""http://www.opengis.net/wfs/2.0 http://schemas.opengis.net/wfs/2.0/wfs.xsd"">
								<Query typeNames=""{cachedEntry.FeatureType}"">
									<ogc:Filter>
										<fes:And>
											<fes:PropertyIsEqualTo>
												<fes:ValueReference>{cachedEntry.FeatureTypeNamespacePrefix}:inspireId/base:Identifier/base:localId</fes:ValueReference>
												<fes:Literal>{localId}</fes:Literal>
											</fes:PropertyIsEqualTo>
											<fes:PropertyIsEqualTo>
												<fes:ValueReference>{cachedEntry.FeatureTypeNamespacePrefix}:inspireId/base:Identifier/base:versionId</fes:ValueReference>
												<fes:Literal>{versionId}</fes:Literal>
											</fes:PropertyIsEqualTo>
										</fes:And>
									</ogc:Filter>
								</Query>
							</GetFeature>";
						break;

					default:
						throw new InvalidOperationException("WFS version not supported");
				}

				proxyRequest.Content = new StringContent(content);
				proxyRequest.RequestUri = RequestUtilities.MakeDestinationAddress(cachedEntry.GetFeaturePostUrl.Replace("wfs", "ows"),
					string.Empty, outputFormat == null ? QueryString.Empty : default(QueryString).Add("outputFormat", outputFormat));
				proxyRequest.Headers.Host = cachedEntry.ServiceEndpointHost;

				return default;
			};

		public static Func<HttpContext, HttpRequestMessage, ValueTask> GetRecordById(CacheEntry cachedEntry) =>
			(_, proxyRequest) =>
			{
				QueryString queryString = new QueryString().Add("service", "CSW")
					.Add("version", cachedEntry.DataProviderVersion)
					.Add("request", "GetRecordById")
					.Add("id", cachedEntry.MetadataId)
					.Add("ElementSetName", "full")
					.Add("outputSchema", "http://www.isotc211.org/2005/gmd");

				proxyRequest.Method = HttpMethod.Get;
				proxyRequest.RequestUri = RequestUtilities.MakeDestinationAddress(cachedEntry.GetRecordByIdUrl.Replace("wfs", "ows"),
					string.Empty, queryString);

				proxyRequest.Headers.Host = cachedEntry.DataProviderHost;

				return default;
			};
	}
}