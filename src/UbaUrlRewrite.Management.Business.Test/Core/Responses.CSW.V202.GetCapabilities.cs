// <copyright file="Responses.CSW.V202.GetCapabilities.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Test.Core;

// https://demo.pycsw.org/cite/csw
public static partial class Responses
{
	public static partial class CSW
	{
		public static partial class V202
		{
			public static class GetCapabilities
			{
				public static string InvalidInvalidServiceType =>
					@"<csw:Capabilities xmlns:csw=""http://www.opengis.net/cat/csw/2.0.2""
						xmlns:dc=""http://purl.org/dc/elements/1.1/""
						xmlns:dct=""http://purl.org/dc/terms/""
						xmlns:gmd=""http://www.isotc211.org/2005/gmd""
						xmlns:gml=""http://www.opengis.net/gml""
						xmlns:ogc=""http://www.opengis.net/ogc""
						xmlns:ows=""http://www.opengis.net/ows""
						xmlns:xlink=""http://www.w3.org/1999/xlink""
						xmlns:xs=""http://www.w3.org/2001/XMLSchema""
						xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" version=""2.0.2"" updateSequence=""1613425159"" xsi:schemaLocation=""http://www.opengis.net/cat/csw/2.0.2 http://schemas.opengis.net/csw/2.0.2/CSW-discovery.xsd"">
						<ows:ServiceIdentification>
							<ows:Title>pycsw OGC CITE demo and Reference Implementation</ows:Title>
							<ows:Abstract>pycsw is an OARec and OGC CSW server implementation written in Python. pycsw fully implements the OGC API - Records and OpenGIS Catalogue Service Implementation Specification (Catalogue Service for the Web). Initial development started in 2010 (more formally announced in 2011). The project is certified OGC Compliant, and is an OGC Reference Implementation. Since 2015, pycsw is an official OSGeo Project. pycsw allows for the publishing and discovery of geospatial metadata via numerous APIs (CSW 2/CSW 3, OpenSearch, OAI-PMH, SRU). Existing repositories of geospatial metadata can also be exposed, providing a standards-based metadata and catalogue component of spatial data infrastructures. pycsw is Open Source, released under an MIT license, and runs on all major platforms (Windows, Linux, Mac OS X)</ows:Abstract>
							<ows:Keywords>
								<ows:Keyword>ogc</ows:Keyword>
								<ows:Keyword>cite</ows:Keyword>
								<ows:Keyword>compliance</ows:Keyword>
								<ows:Keyword>interoperability</ows:Keyword>
								<ows:Keyword>reference implementation</ows:Keyword>
								<ows:Type codeSpace=""ISOTC211/19115"">theme</ows:Type>
							</ows:Keywords>
							<ows:ServiceType codeSpace=""OGC"">INVALID</ows:ServiceType>
							<ows:ServiceTypeVersion>2.0.2</ows:ServiceTypeVersion>
							<ows:ServiceTypeVersion>3.0.0</ows:ServiceTypeVersion>
							<ows:Fees>None</ows:Fees>
							<ows:AccessConstraints>None</ows:AccessConstraints>
						</ows:ServiceIdentification>
					</csw:Capabilities>";

				public static string InvalidMissingServiceType =>
					@"<csw:Capabilities xmlns:csw=""http://www.opengis.net/cat/csw/2.0.2""
						xmlns:dc=""http://purl.org/dc/elements/1.1/""
						xmlns:dct=""http://purl.org/dc/terms/""
						xmlns:gmd=""http://www.isotc211.org/2005/gmd""
						xmlns:gml=""http://www.opengis.net/gml""
						xmlns:ogc=""http://www.opengis.net/ogc""
						xmlns:ows=""http://www.opengis.net/ows""
						xmlns:xlink=""http://www.w3.org/1999/xlink""
						xmlns:xs=""http://www.w3.org/2001/XMLSchema""
						xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" version=""2.0.2"" updateSequence=""1613425159"" xsi:schemaLocation=""http://www.opengis.net/cat/csw/2.0.2 http://schemas.opengis.net/csw/2.0.2/CSW-discovery.xsd"">
						<ows:ServiceIdentification>
							<ows:Title>pycsw OGC CITE demo and Reference Implementation</ows:Title>
							<ows:Abstract>pycsw is an OARec and OGC CSW server implementation written in Python. pycsw fully implements the OGC API - Records and OpenGIS Catalogue Service Implementation Specification (Catalogue Service for the Web). Initial development started in 2010 (more formally announced in 2011). The project is certified OGC Compliant, and is an OGC Reference Implementation. Since 2015, pycsw is an official OSGeo Project. pycsw allows for the publishing and discovery of geospatial metadata via numerous APIs (CSW 2/CSW 3, OpenSearch, OAI-PMH, SRU). Existing repositories of geospatial metadata can also be exposed, providing a standards-based metadata and catalogue component of spatial data infrastructures. pycsw is Open Source, released under an MIT license, and runs on all major platforms (Windows, Linux, Mac OS X)</ows:Abstract>
							<ows:Keywords>
								<ows:Keyword>ogc</ows:Keyword>
								<ows:Keyword>cite</ows:Keyword>
								<ows:Keyword>compliance</ows:Keyword>
								<ows:Keyword>interoperability</ows:Keyword>
								<ows:Keyword>reference implementation</ows:Keyword>
								<ows:Type codeSpace=""ISOTC211/19115"">theme</ows:Type>
							</ows:Keywords>
							<ows:ServiceTypeVersion>2.0.2</ows:ServiceTypeVersion>
							<ows:ServiceTypeVersion>3.0.0</ows:ServiceTypeVersion>
							<ows:Fees>None</ows:Fees>
							<ows:AccessConstraints>None</ows:AccessConstraints>
						</ows:ServiceIdentification>
					</csw:Capabilities>";

				public static string InvalidMissingVersionAttribute =>
					@"<csw:Capabilities xmlns:csw=""http://www.opengis.net/cat/csw/2.0.2""
						xmlns:dc=""http://purl.org/dc/elements/1.1/""
						xmlns:dct=""http://purl.org/dc/terms/""
						xmlns:gmd=""http://www.isotc211.org/2005/gmd""
						xmlns:gml=""http://www.opengis.net/gml""
						xmlns:ogc=""http://www.opengis.net/ogc""
						xmlns:ows=""http://www.opengis.net/ows""
						xmlns:xlink=""http://www.w3.org/1999/xlink""
						xmlns:xs=""http://www.w3.org/2001/XMLSchema""
						xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" updateSequence=""1613425159"" xsi:schemaLocation=""http://www.opengis.net/cat/csw/2.0.2 http://schemas.opengis.net/csw/2.0.2/CSW-discovery.xsd"">
						<ows:ServiceIdentification>
							<ows:Title>pycsw OGC CITE demo and Reference Implementation</ows:Title>
							<ows:Abstract>pycsw is an OARec and OGC CSW server implementation written in Python. pycsw fully implements the OGC API - Records and OpenGIS Catalogue Service Implementation Specification (Catalogue Service for the Web). Initial development started in 2010 (more formally announced in 2011). The project is certified OGC Compliant, and is an OGC Reference Implementation. Since 2015, pycsw is an official OSGeo Project. pycsw allows for the publishing and discovery of geospatial metadata via numerous APIs (CSW 2/CSW 3, OpenSearch, OAI-PMH, SRU). Existing repositories of geospatial metadata can also be exposed, providing a standards-based metadata and catalogue component of spatial data infrastructures. pycsw is Open Source, released under an MIT license, and runs on all major platforms (Windows, Linux, Mac OS X)</ows:Abstract>
							<ows:Keywords>
								<ows:Keyword>ogc</ows:Keyword>
								<ows:Keyword>cite</ows:Keyword>
								<ows:Keyword>compliance</ows:Keyword>
								<ows:Keyword>interoperability</ows:Keyword>
								<ows:Keyword>reference implementation</ows:Keyword>
								<ows:Type codeSpace=""ISOTC211/19115"">theme</ows:Type>
							</ows:Keywords>
							<ows:ServiceType codeSpace=""OGC"">CSW</ows:ServiceType>
							<ows:ServiceTypeVersion>2.0.2</ows:ServiceTypeVersion>
							<ows:ServiceTypeVersion>3.0.0</ows:ServiceTypeVersion>
							<ows:Fees>None</ows:Fees>
							<ows:AccessConstraints>None</ows:AccessConstraints>
						</ows:ServiceIdentification>
					</csw:Capabilities>";

				public static string Valid =>
					@"<csw:Capabilities xmlns:csw=""http://www.opengis.net/cat/csw/2.0.2""
						xmlns:dc=""http://purl.org/dc/elements/1.1/""
						xmlns:dct=""http://purl.org/dc/terms/""
						xmlns:gmd=""http://www.isotc211.org/2005/gmd""
						xmlns:gml=""http://www.opengis.net/gml""
						xmlns:ogc=""http://www.opengis.net/ogc""
						xmlns:ows=""http://www.opengis.net/ows""
						xmlns:xlink=""http://www.w3.org/1999/xlink""
						xmlns:xs=""http://www.w3.org/2001/XMLSchema""
						xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" version=""2.0.2"" updateSequence=""1613425159"" xsi:schemaLocation=""http://www.opengis.net/cat/csw/2.0.2 http://schemas.opengis.net/csw/2.0.2/CSW-discovery.xsd"">
						<ows:ServiceIdentification>
							<ows:Title>pycsw OGC CITE demo and Reference Implementation</ows:Title>
							<ows:Abstract>pycsw is an OARec and OGC CSW server implementation written in Python. pycsw fully implements the OGC API - Records and OpenGIS Catalogue Service Implementation Specification (Catalogue Service for the Web). Initial development started in 2010 (more formally announced in 2011). The project is certified OGC Compliant, and is an OGC Reference Implementation. Since 2015, pycsw is an official OSGeo Project. pycsw allows for the publishing and discovery of geospatial metadata via numerous APIs (CSW 2/CSW 3, OpenSearch, OAI-PMH, SRU). Existing repositories of geospatial metadata can also be exposed, providing a standards-based metadata and catalogue component of spatial data infrastructures. pycsw is Open Source, released under an MIT license, and runs on all major platforms (Windows, Linux, Mac OS X)</ows:Abstract>
							<ows:Keywords>
								<ows:Keyword>ogc</ows:Keyword>
								<ows:Keyword>cite</ows:Keyword>
								<ows:Keyword>compliance</ows:Keyword>
								<ows:Keyword>interoperability</ows:Keyword>
								<ows:Keyword>reference implementation</ows:Keyword>
								<ows:Type codeSpace=""ISOTC211/19115"">theme</ows:Type>
							</ows:Keywords>
							<ows:ServiceType codeSpace=""OGC"">CSW</ows:ServiceType>
							<ows:ServiceTypeVersion>2.0.0</ows:ServiceTypeVersion>
							<ows:ServiceTypeVersion>2.0.2</ows:ServiceTypeVersion>
							<ows:ServiceTypeVersion>3.0.0</ows:ServiceTypeVersion>
							<ows:Fees>None</ows:Fees>
							<ows:AccessConstraints>None</ows:AccessConstraints>
						</ows:ServiceIdentification>
						<ows:OperationsMetadata>
							<ows:Operation name=""GetRecords"">
								<ows:DCP>
									<ows:HTTP>
										<ows:Get xlink:type=""simple"" xlink:href=""https://demo.pycsw.org/cite/csw""/>
										<ows:Post xlink:type=""simple"" xlink:href=""https://demo.pycsw.org/cite/csw""/>
									</ows:HTTP>
								</ows:DCP>
								<ows:Parameter name=""CONSTRAINTLANGUAGE"">
									<ows:Value>CQL_TEXT</ows:Value>
									<ows:Value>FILTER</ows:Value>
								</ows:Parameter>
								<ows:Parameter name=""ElementSetName"">
									<ows:Value>brief</ows:Value>
									<ows:Value>full</ows:Value>
									<ows:Value>summary</ows:Value>
								</ows:Parameter>
								<ows:Parameter name=""outputFormat"">
									<ows:Value>application/json</ows:Value>
									<ows:Value>application/xml</ows:Value>
								</ows:Parameter>
								<ows:Parameter name=""outputSchema"">
									<ows:Value>http://gcmd.gsfc.nasa.gov/Aboutus/xml/dif/</ows:Value>
									<ows:Value>http://www.interlis.ch/INTERLIS2.3</ows:Value>
									<ows:Value>http://www.opengis.net/cat/csw/2.0.2</ows:Value>
									<ows:Value>http://www.opengis.net/cat/csw/csdgm</ows:Value>
									<ows:Value>http://www.w3.org/2005/Atom</ows:Value>
									<ows:Value>http://www.isotc211.org/2005/gmd</ows:Value>
								</ows:Parameter>
								<ows:Parameter name=""resultType"">
									<ows:Value>hits</ows:Value>
									<ows:Value>results</ows:Value>
									<ows:Value>validate</ows:Value>
								</ows:Parameter>
								<ows:Parameter name=""typeNames"">
									<ows:Value>csw:Record</ows:Value>
								</ows:Parameter>
								<ows:Constraint name=""SupportedDublinCoreQueryables"">
									<ows:Value>csw:AnyText</ows:Value>
									<ows:Value>dc:contributor</ows:Value>
									<ows:Value>dc:creator</ows:Value>
									<ows:Value>dc:date</ows:Value>
									<ows:Value>dc:format</ows:Value>
									<ows:Value>dc:identifier</ows:Value>
									<ows:Value>dc:language</ows:Value>
									<ows:Value>dc:publisher</ows:Value>
									<ows:Value>dc:relation</ows:Value>
									<ows:Value>dc:rights</ows:Value>
									<ows:Value>dc:source</ows:Value>
									<ows:Value>dc:subject</ows:Value>
									<ows:Value>dc:title</ows:Value>
									<ows:Value>dc:type</ows:Value>
									<ows:Value>dct:abstract</ows:Value>
									<ows:Value>dct:alternative</ows:Value>
									<ows:Value>dct:modified</ows:Value>
									<ows:Value>dct:spatial</ows:Value>
									<ows:Value>ows:BoundingBox</ows:Value>
								</ows:Constraint>
							</ows:Operation>
						</ows:OperationsMetadata>
					</csw:Capabilities>";

				public static string ValidWithMissingServiceIdentification =>
					@"<csw:Capabilities xmlns:csw=""http://www.opengis.net/cat/csw/2.0.2""
						xmlns:dc=""http://purl.org/dc/elements/1.1/""
						xmlns:dct=""http://purl.org/dc/terms/""
						xmlns:gmd=""http://www.isotc211.org/2005/gmd""
						xmlns:gml=""http://www.opengis.net/gml""
						xmlns:ogc=""http://www.opengis.net/ogc""
						xmlns:ows=""http://www.opengis.net/ows""
						xmlns:xlink=""http://www.w3.org/1999/xlink""
						xmlns:xs=""http://www.w3.org/2001/XMLSchema""
						xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" version=""2.0.2"" updateSequence=""1613425159"" xsi:schemaLocation=""http://www.opengis.net/cat/csw/2.0.2 http://schemas.opengis.net/csw/2.0.2/CSW-discovery.xsd"">
					</csw:Capabilities>";
			}
		}
	}
}