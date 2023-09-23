// <copyright file="Responses.WFS.V202.GetCapabilities.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Test.Core;

public static partial class Responses
{
	public static class WFS
	{
		public static class V101
		{
			public static class GetCapabilities
			{
				public static string Valid =>
					@"<WFS_Capabilities version=""1.1.0"" schemaLocation=""http://www.opengis.net/wfs http://schemas.opengis.net/wfs/1.1.0/wfs.xsd http://inspire.ec.europa.eu/schemas/inspire_dls/1.0 http://inspire.ec.europa.eu/schemas/inspire_ds/1.0/inspire_dls.xsd http://inspire.ec.europa.eu/schemas/common/1.0 http://inspire.ec.europa.eu/schemas/common/1.0/common.xsd"">
						<ServiceIdentification>
							<Title>Subnetz 302 Wien: RBW Nebenfahrbahnen, Straßenabschnitte und Eigenschaften, Verkehrsnetze, ÖVDAT</Title>
							<Abstract>Subnetz 302 Wien: RBW Nebenfahrbahnen, Straßenabschnitte und Eigenschaften, Verkehrsnetze, ÖVDAT</Abstract>
							<Keywords>
								<Keyword>Verkehrsnetze</Keyword>
								<Type codeSpace=""https://standards.iso.org/iso/19139/resources/gmxCodelists.xml#MD_KeywordTypeCode"">theme</Type>
							</Keywords>
							<Keywords>
								<Keyword>infoFeatureAccessService</Keyword>
							</Keywords>
							<ServiceType>WFS</ServiceType>
							<ServiceTypeVersion>2.0.0</ServiceTypeVersion>
							<ServiceTypeVersion>1.1.0</ServiceTypeVersion>
							<Fees>conditionsUnknown | license</Fees>
							<AccessConstraints>otherRestrictions</AccessConstraints>
							<AccessConstraints>license</AccessConstraints>
						</ServiceIdentification>
						<OperationsMetadata>
							<Operation name=""GetFeature"">
								<DCP>
									<HTTP>
										<Get href=""https://haleconnect.com/ows/services/org.789.0727f597-e611-4e3f-9363-b8ec70253867_wfs?"" />
										<Post href=""https://haleconnect.com/ows/services/org.789.0727f597-e611-4e3f-9363-b8ec70253867_wfs"" />
									</HTTP>
								</DCP>
								<Parameter name=""resultType"">
									<Value>results</Value>
									<Value>hits</Value>
								</Parameter>
								<Parameter name=""outputFormat"">
									<Value>application/gml+xml; version=3.2</Value>
									<Value>text/xml; subtype=gml/3.2.1</Value>
								</Parameter>
							</Operation>
								<Parameter name=""outputFormat"">
									<Value>application/gml+xml; version=3.2</Value>
									<Value>text/xml; subtype=gml/3.2.1</Value>
								</Parameter>
						</OperationsMetadata>
						<FeatureTypeList>
							<FeatureType>
								<Name>tn:TrafficFlowDirection</Name>
								<Title>tn:TrafficFlowDirection</Title>
								<DefaultSRS>http://www.opengis.net/def/crs/EPSG/0/4258</DefaultSRS>
								<OtherSRS>http://www.opengis.net/def/crs/EPSG/0/4326</OtherSRS>
								<OutputFormats>
									<Format>application/gml+xml; version=3.2</Format>
									<Format>text/xml; subtype=gml/3.2.1</Format>
								</OutputFormats>
								<WGS84BoundingBox>
									<LowerCorner>-180.000000 -90.000000</LowerCorner>
									<UpperCorner>180.000000 90.000000</UpperCorner>
								</WGS84BoundingBox>
								<MetadataURL type=""19139"" format=""text/xml"">https://haleconnect.com/services/bsp/org.789.0727f597-e611-4e3f-9363-b8ec70253867/md/dataset/dataset1</MetadataURL>
							</FeatureType>
						</FeatureTypeList>
					</WFS_Capabilities>";
			}
		}

		public static class V200
		{
			public static class GetCapabilities
			{
				public static string Valid =>
					@"<WFS_Capabilities version=""2.0.0"" schemaLocation=""http://www.opengis.net/wfs/2.0 http://schemas.opengis.net/wfs/2.0/wfs.xsd http://inspire.ec.europa.eu/schemas/inspire_dls/1.0 http://inspire.ec.europa.eu/schemas/inspire_ds/1.0/inspire_dls.xsd http://inspire.ec.europa.eu/schemas/common/1.0 http://inspire.ec.europa.eu/schemas/common/1.0/common.xsd"">
						<ServiceIdentification>
							<Title>Subnetz 302 Wien: RBW Nebenfahrbahnen, Straßenabschnitte und Eigenschaften, Verkehrsnetze, ÖVDAT</Title>
							<Abstract>Subnetz 302 Wien: RBW Nebenfahrbahnen, Straßenabschnitte und Eigenschaften, Verkehrsnetze, ÖVDAT</Abstract>
							<Keywords>
								<Keyword>Verkehrsnetze</Keyword>
								<Type codeSpace=""https://standards.iso.org/iso/19139/resources/gmxCodelists.xml#MD_KeywordTypeCode"">theme</Type>
							</Keywords>
							<Keywords>
								<Keyword>infoFeatureAccessService</Keyword>
							</Keywords>
							<ServiceType codeSpace=""http://www.opengeospatial.org/"">WFS</ServiceType>
							<ServiceTypeVersion>2.0.0</ServiceTypeVersion>
							<ServiceTypeVersion>1.1.0</ServiceTypeVersion>
							<Fees>conditionsUnknown | license</Fees>
							<AccessConstraints>otherRestrictions</AccessConstraints>
							<AccessConstraints>license</AccessConstraints>
						</ServiceIdentification>
						<OperationsMetadata>
							<Operation name=""DescribeFeatureType"">
								<DCP>
									<HTTP>
										<Get href=""https://haleconnect.com/ows/services/org.789.0727f597-e611-4e3f-9363-b8ec70253867_wfs?"" />
										<Post href=""https://haleconnect.com/ows/services/org.789.0727f597-e611-4e3f-9363-b8ec70253867_wfs"" />
									</HTTP>
								</DCP>
							</Operation>
							<Operation name=""GetFeature"">
								<DCP>
									<HTTP>
										<Get href=""https://haleconnect.com/ows/services/org.789.0727f597-e611-4e3f-9363-b8ec70253867_wfs?"" />
										<Post href=""https://haleconnect.com/ows/services/org.789.0727f597-e611-4e3f-9363-b8ec70253867_wfs"" />
									</HTTP>
								</DCP>
								<Parameter name=""outputFormat"">
									<AllowedValues>
										<Value>application/gml+xml; version=4.0</Value>
										<Value>text/xml; subtype=gml/4.0.0</Value>
									</AllowedValues>
								</Parameter>
							</Operation>
							<Parameter name=""version"">
								<AllowedValues>
									<Value>2.0.0</Value>
									<Value>1.1.0</Value>
								</AllowedValues>
							</Parameter>
							<Parameter name=""srsName"">
								<AllowedValues>
									<Value>http://www.opengis.net/def/crs/EPSG/0/4258</Value>
									<Value>http://www.opengis.net/def/crs/EPSG/0/4326</Value>
								</AllowedValues>
							</Parameter>
							<Parameter name=""outputFormat"">
								<AllowedValues>
									<Value>application/gml+xml; version=3.2</Value>
									<Value>text/xml; subtype=gml/3.2.0</Value>
								</AllowedValues>
							</Parameter>
						</OperationsMetadata>
						<FeatureTypeList>
							<FeatureType>
								<Name>tn:TrafficFlowDirection</Name>
								<Title>tn:TrafficFlowDirection</Title>
								<DefaultCRS>http://www.opengis.net/def/crs/EPSG/0/4258</DefaultCRS>
								<OtherCRS>http://www.opengis.net/def/crs/EPSG/0/4326</OtherCRS>
								<OutputFormats>
									<Format>application/gml+xml; version=2.0</Format>
									<Format>text/xml; subtype=gml/2.0.0</Format>
								</OutputFormats>
								<WGS84BoundingBox>
									<LowerCorner>-180.000000 -90.000000</LowerCorner>
									<UpperCorner>180.000000 90.000000</UpperCorner>
								</WGS84BoundingBox>
								<MetadataURL href=""https://haleconnect.com/services/bsp/org.789.0727f597-e611-4e3f-9363-b8ec70253867/md/dataset/dataset1"" />
							</FeatureType>
						</FeatureTypeList>
					</WFS_Capabilities>";
			}
		}
	}
}