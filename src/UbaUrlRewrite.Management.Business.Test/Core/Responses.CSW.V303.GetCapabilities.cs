// <copyright file="Responses.CSW.V303.GetCapabilitiesAsync.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Test.Core;

// https://demo.pycsw.org/cite/csw
public static partial class Responses
{
	public static partial class CSW
	{
		public static class V300
		{
			public static class GetCapabilities
			{
				public static string InvalidInvalidServiceType =>
					@"<csw30:Capabilities xmlns:csw=""http://www.opengis.net/cat/csw/2.0.2""
							xmlns:csw30=""http://www.opengis.net/cat/csw/3.0""
							xmlns:dc=""http://purl.org/dc/elements/1.1/""
							xmlns:dct=""http://purl.org/dc/terms/""
							xmlns:fes20=""http://www.opengis.net/fes/2.0""
							xmlns:gmd=""http://www.isotc211.org/2005/gmd""
							xmlns:gml=""http://www.opengis.net/gml""
							xmlns:ows=""http://www.opengis.net/ows""
							xmlns:ows11=""http://www.opengis.net/ows/1.1""
							xmlns:ows20=""http://www.opengis.net/ows/2.0""
							xmlns:xlink=""http://www.w3.org/1999/xlink""
							xmlns:xs=""http://www.w3.org/2001/XMLSchema""
							xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" version=""3.0.0"" updateSequence=""1613425159"" xsi:schemaLocation=""http://www.opengis.net/cat/csw/3.0 http://schemas.opengis.net/cat/csw/3.0/cswGetCapabilities.xsd"">
							<ows20:ServiceIdentification>
								<ows20:Title>pycsw OGC CITE demo and Reference Implementation</ows20:Title>
								<ows20:Abstract>pycsw is an OARec and OGC CSW server implementation written in Python. pycsw fully implements the OGC API - Records and OpenGIS Catalogue Service Implementation Specification (Catalogue Service for the Web). Initial development started in 2010 (more formally announced in 2011). The project is certified OGC Compliant, and is an OGC Reference Implementation. Since 2015, pycsw is an official OSGeo Project. pycsw allows for the publishing and discovery of geospatial metadata via numerous APIs (CSW 2/CSW 3, OpenSearch, OAI-PMH, SRU). Existing repositories of geospatial metadata can also be exposed, providing a standards-based metadata and catalogue component of spatial data infrastructures. pycsw is Open Source, released under an MIT license, and runs on all major platforms (Windows, Linux, Mac OS X)</ows20:Abstract>
								<ows20:Keywords>
									<ows20:Keyword>ogc</ows20:Keyword>
									<ows20:Keyword>cite</ows20:Keyword>
									<ows20:Keyword>compliance</ows20:Keyword>
									<ows20:Keyword>interoperability</ows20:Keyword>
									<ows20:Keyword>reference implementation</ows20:Keyword>
									<ows20:Type codeSpace=""ISOTC211/19115"">theme</ows20:Type>
								</ows20:Keywords>
								<ows20:ServiceType codeSpace=""OGC"">INVALID</ows20:ServiceType>
								<ows20:ServiceTypeVersion>2.0.2</ows20:ServiceTypeVersion>
								<ows20:ServiceTypeVersion>3.0.0</ows20:ServiceTypeVersion>
								<ows20:Fees>None</ows20:Fees>
								<ows20:AccessConstraints>None</ows20:AccessConstraints>
							</ows20:ServiceIdentification>
						</csw30:Capabilities>";

				public static string InvalidMissingServiceType =>
					@"<csw30:Capabilities xmlns:csw=""http://www.opengis.net/cat/csw/2.0.2""
							xmlns:csw30=""http://www.opengis.net/cat/csw/3.0""
							xmlns:dc=""http://purl.org/dc/elements/1.1/""
							xmlns:dct=""http://purl.org/dc/terms/""
							xmlns:fes20=""http://www.opengis.net/fes/2.0""
							xmlns:gmd=""http://www.isotc211.org/2005/gmd""
							xmlns:gml=""http://www.opengis.net/gml""
							xmlns:ows=""http://www.opengis.net/ows""
							xmlns:ows11=""http://www.opengis.net/ows/1.1""
							xmlns:ows20=""http://www.opengis.net/ows/2.0""
							xmlns:xlink=""http://www.w3.org/1999/xlink""
							xmlns:xs=""http://www.w3.org/2001/XMLSchema""
							xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" version=""3.0.0"" updateSequence=""1613425159"" xsi:schemaLocation=""http://www.opengis.net/cat/csw/3.0 http://schemas.opengis.net/cat/csw/3.0/cswGetCapabilities.xsd"">
							<ows20:ServiceIdentification>
								<ows20:Title>pycsw OGC CITE demo and Reference Implementation</ows20:Title>
								<ows20:Abstract>pycsw is an OARec and OGC CSW server implementation written in Python. pycsw fully implements the OGC API - Records and OpenGIS Catalogue Service Implementation Specification (Catalogue Service for the Web). Initial development started in 2010 (more formally announced in 2011). The project is certified OGC Compliant, and is an OGC Reference Implementation. Since 2015, pycsw is an official OSGeo Project. pycsw allows for the publishing and discovery of geospatial metadata via numerous APIs (CSW 2/CSW 3, OpenSearch, OAI-PMH, SRU). Existing repositories of geospatial metadata can also be exposed, providing a standards-based metadata and catalogue component of spatial data infrastructures. pycsw is Open Source, released under an MIT license, and runs on all major platforms (Windows, Linux, Mac OS X)</ows20:Abstract>
								<ows20:Keywords>
									<ows20:Keyword>ogc</ows20:Keyword>
									<ows20:Keyword>cite</ows20:Keyword>
									<ows20:Keyword>compliance</ows20:Keyword>
									<ows20:Keyword>interoperability</ows20:Keyword>
									<ows20:Keyword>reference implementation</ows20:Keyword>
									<ows20:Type codeSpace=""ISOTC211/19115"">theme</ows20:Type>
								</ows20:Keywords>
								<ows20:ServiceTypeVersion>2.0.2</ows20:ServiceTypeVersion>
								<ows20:ServiceTypeVersion>3.0.0</ows20:ServiceTypeVersion>
								<ows20:Fees>None</ows20:Fees>
								<ows20:AccessConstraints>None</ows20:AccessConstraints>
							</ows20:ServiceIdentification>
						</csw30:Capabilities>";

				public static string InvalidMissingVersionAttribute =>
					@"<csw30:Capabilities xmlns:csw=""http://www.opengis.net/cat/csw/2.0.2""
							xmlns:csw30=""http://www.opengis.net/cat/csw/3.0""
							xmlns:dc=""http://purl.org/dc/elements/1.1/""
							xmlns:dct=""http://purl.org/dc/terms/""
							xmlns:fes20=""http://www.opengis.net/fes/2.0""
							xmlns:gmd=""http://www.isotc211.org/2005/gmd""
							xmlns:gml=""http://www.opengis.net/gml""
							xmlns:ows=""http://www.opengis.net/ows""
							xmlns:ows11=""http://www.opengis.net/ows/1.1""
							xmlns:ows20=""http://www.opengis.net/ows/2.0""
							xmlns:xlink=""http://www.w3.org/1999/xlink""
							xmlns:xs=""http://www.w3.org/2001/XMLSchema""
							xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" updateSequence=""1613425159"" xsi:schemaLocation=""http://www.opengis.net/cat/csw/3.0 http://schemas.opengis.net/cat/csw/3.0/cswGetCapabilities.xsd"">
							<ows20:ServiceIdentification>
								<ows20:Title>pycsw OGC CITE demo and Reference Implementation</ows20:Title>
								<ows20:Abstract>pycsw is an OARec and OGC CSW server implementation written in Python. pycsw fully implements the OGC API - Records and OpenGIS Catalogue Service Implementation Specification (Catalogue Service for the Web). Initial development started in 2010 (more formally announced in 2011). The project is certified OGC Compliant, and is an OGC Reference Implementation. Since 2015, pycsw is an official OSGeo Project. pycsw allows for the publishing and discovery of geospatial metadata via numerous APIs (CSW 2/CSW 3, OpenSearch, OAI-PMH, SRU). Existing repositories of geospatial metadata can also be exposed, providing a standards-based metadata and catalogue component of spatial data infrastructures. pycsw is Open Source, released under an MIT license, and runs on all major platforms (Windows, Linux, Mac OS X)</ows20:Abstract>
								<ows20:Keywords>
									<ows20:Keyword>ogc</ows20:Keyword>
									<ows20:Keyword>cite</ows20:Keyword>
									<ows20:Keyword>compliance</ows20:Keyword>
									<ows20:Keyword>interoperability</ows20:Keyword>
									<ows20:Keyword>reference implementation</ows20:Keyword>
									<ows20:Type codeSpace=""ISOTC211/19115"">theme</ows20:Type>
								</ows20:Keywords>
								<ows20:ServiceType codeSpace=""OGC"">CSW</ows20:ServiceType>
								<ows20:ServiceTypeVersion>2.0.2</ows20:ServiceTypeVersion>
								<ows20:ServiceTypeVersion>3.0.0</ows20:ServiceTypeVersion>
								<ows20:Fees>None</ows20:Fees>
								<ows20:AccessConstraints>None</ows20:AccessConstraints>
							</ows20:ServiceIdentification>
						</csw30:Capabilities>";

				public static string Valid =>
					@"<csw30:Capabilities xmlns:csw=""http://www.opengis.net/cat/csw/2.0.2""
							xmlns:csw30=""http://www.opengis.net/cat/csw/3.0""
							xmlns:dc=""http://purl.org/dc/elements/1.1/""
							xmlns:dct=""http://purl.org/dc/terms/""
							xmlns:fes20=""http://www.opengis.net/fes/2.0""
							xmlns:gmd=""http://www.isotc211.org/2005/gmd""
							xmlns:gml=""http://www.opengis.net/gml""
							xmlns:ows=""http://www.opengis.net/ows""
							xmlns:ows11=""http://www.opengis.net/ows/1.1""
							xmlns:ows20=""http://www.opengis.net/ows/2.0""
							xmlns:xlink=""http://www.w3.org/1999/xlink""
							xmlns:xs=""http://www.w3.org/2001/XMLSchema""
							xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" version=""3.0.0"" updateSequence=""1613425159"" xsi:schemaLocation=""http://www.opengis.net/cat/csw/3.0 http://schemas.opengis.net/cat/csw/3.0/cswGetCapabilities.xsd"">
							<ows20:ServiceIdentification>
								<ows20:Title>pycsw OGC CITE demo and Reference Implementation</ows20:Title>
								<ows20:Abstract>pycsw is an OARec and OGC CSW server implementation written in Python. pycsw fully implements the OGC API - Records and OpenGIS Catalogue Service Implementation Specification (Catalogue Service for the Web). Initial development started in 2010 (more formally announced in 2011). The project is certified OGC Compliant, and is an OGC Reference Implementation. Since 2015, pycsw is an official OSGeo Project. pycsw allows for the publishing and discovery of geospatial metadata via numerous APIs (CSW 2/CSW 3, OpenSearch, OAI-PMH, SRU). Existing repositories of geospatial metadata can also be exposed, providing a standards-based metadata and catalogue component of spatial data infrastructures. pycsw is Open Source, released under an MIT license, and runs on all major platforms (Windows, Linux, Mac OS X)</ows20:Abstract>
								<ows20:Keywords>
									<ows20:Keyword>ogc</ows20:Keyword>
									<ows20:Keyword>cite</ows20:Keyword>
									<ows20:Keyword>compliance</ows20:Keyword>
									<ows20:Keyword>interoperability</ows20:Keyword>
									<ows20:Keyword>reference implementation</ows20:Keyword>
									<ows20:Type codeSpace=""ISOTC211/19115"">theme</ows20:Type>
								</ows20:Keywords>
								<ows20:ServiceType codeSpace=""OGC"">CSW</ows20:ServiceType>
								<ows20:ServiceTypeVersion>2.0.2</ows20:ServiceTypeVersion>
								<ows20:ServiceTypeVersion>3.0.0</ows20:ServiceTypeVersion>
								<ows20:Fees>None</ows20:Fees>
								<ows20:AccessConstraints>None</ows20:AccessConstraints>
							</ows20:ServiceIdentification>
							<ows20:OperationsMetadata>
								<ows20:Operation name=""GetRecords"">
									<ows20:DCP>
										<ows20:HTTP>
											<ows20:Get xlink:type=""simple"" xlink:href=""https://demo.pycsw.org/cite/csw""/>
											<ows20:Post xlink:type=""simple"" xlink:href=""https://demo.pycsw.org/cite/csw""/>
										</ows20:HTTP>
									</ows20:DCP>
									<ows20:Parameter name=""CONSTRAINTLANGUAGE"">
										<ows20:AllowedValues>
											<ows20:Value>CQL_TEXT</ows20:Value>
											<ows20:Value>FILTER</ows20:Value>
										</ows20:AllowedValues>
									</ows20:Parameter>
									<ows20:Parameter name=""ElementSetName"">
										<ows20:AllowedValues>
											<ows20:Value>brief</ows20:Value>
											<ows20:Value>full</ows20:Value>
											<ows20:Value>summary</ows20:Value>
										</ows20:AllowedValues>
									</ows20:Parameter>
									<ows20:Parameter name=""outputFormat"">
										<ows20:AllowedValues>
											<ows20:Value>application/atom+xml</ows20:Value>
											<ows20:Value>application/json</ows20:Value>
											<ows20:Value>application/xml</ows20:Value>
										</ows20:AllowedValues>
									</ows20:Parameter>
									<ows20:Parameter name=""outputSchema"">
										<ows20:AllowedValues>
											<ows20:Value>http://gcmd.gsfc.nasa.gov/Aboutus/xml/dif/</ows20:Value>
											<ows20:Value>http://www.interlis.ch/INTERLIS2.3</ows20:Value>
											<ows20:Value>http://www.opengis.net/cat/csw/3.0</ows20:Value>
											<ows20:Value>http://www.opengis.net/cat/csw/csdgm</ows20:Value>
											<ows20:Value>http://www.w3.org/2005/Atom</ows20:Value>
										</ows20:AllowedValues>
									</ows20:Parameter>
									<ows20:Parameter name=""typeNames"">
										<ows20:AllowedValues>
											<ows20:Value>csw30:Record</ows20:Value>
											<ows20:Value>csw:Record</ows20:Value>
										</ows20:AllowedValues>
									</ows20:Parameter>
									<ows20:Constraint name=""MaxRecordDefault"">
										<ows20:AllowedValues>
											<ows20:Value>10</ows20:Value>
										</ows20:AllowedValues>
									</ows20:Constraint>
									<ows20:Constraint name=""OpenSearchDescriptionDocument"">
										<ows20:AllowedValues>
											<ows20:Value></ows20:Value>
										</ows20:AllowedValues>
									</ows20:Constraint>
								</ows20:Operation>
							</ows20:OperationsMetadata>
						</csw30:Capabilities>";

				public static string ValidWithMissingServiceIdentification =>
					@"<csw30:Capabilities xmlns:csw=""http://www.opengis.net/cat/csw/2.0.2""
							xmlns:csw30=""http://www.opengis.net/cat/csw/3.0""
							xmlns:dc=""http://purl.org/dc/elements/1.1/""
							xmlns:dct=""http://purl.org/dc/terms/""
							xmlns:fes20=""http://www.opengis.net/fes/2.0""
							xmlns:gmd=""http://www.isotc211.org/2005/gmd""
							xmlns:gml=""http://www.opengis.net/gml""
							xmlns:ows=""http://www.opengis.net/ows""
							xmlns:ows11=""http://www.opengis.net/ows/1.1""
							xmlns:ows20=""http://www.opengis.net/ows/2.0""
							xmlns:xlink=""http://www.w3.org/1999/xlink""
							xmlns:xs=""http://www.w3.org/2001/XMLSchema""
							xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" version=""3.0.0"" updateSequence=""1613425159"" xsi:schemaLocation=""http://www.opengis.net/cat/csw/3.0 http://schemas.opengis.net/cat/csw/3.0/cswGetCapabilities.xsd"">
						</csw30:Capabilities>";
			}
		}
	}
}