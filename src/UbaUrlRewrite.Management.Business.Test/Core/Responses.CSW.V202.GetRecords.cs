// <copyright file="Responses.CSW.V202.GetRecords.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Test.Core;

public static partial class Responses
{
	public static partial class CSW
	{
		public static partial class V202
		{
			public static class GetRecords
			{
				public static string ValidCapabilities => @"<Capabilities version=""2.0.2"" schemaLocation=""http://www.opengis.net/cat/csw/2.0.2 http://schemas.opengis.net/csw/2.0.2/CSW-discovery.xsd"">
					<OperationsMetadata>
						<Operation name=""GetCapabilities"">
							<DCP>
								<HTTP>
									<Get href=""https://data.bev.gv.at/geonetwork/srv/eng/csw"" />
									<Post href=""https://data.bev.gv.at/geonetwork/srv/eng/csw"" />
								</HTTP>
							</DCP>
							<Parameter name=""sections"">
								<Value>ServiceIdentification</Value>
								<Value>ServiceProvider</Value>
								<Value>OperationsMetadata</Value>
								<Value>Filter_Capabilities</Value>
							</Parameter>
							<Constraint name=""PostEncoding"">
								<Value>XML</Value>
								<Value>SOAP</Value>
							</Constraint>
						</Operation>
						<Operation name=""GetRecords"">
							<DCP>
								<HTTP>
									<Get href=""https://data.bev.gv.at/geonetwork/srv/eng/csw"" />
									<Post href=""https://data.bev.gv.at/geonetwork/srv/eng/csw"" />
								</HTTP>
							</DCP>
							<Parameter name=""resultType"">
								<Value>hits</Value>
								<Value>results</Value>
								<Value>validate</Value>
							</Parameter>
							<Parameter name=""outputFormat"">
								<Value>application/xml</Value>
							</Parameter>
							<Parameter name=""outputSchema"">
								<Value>http://www.opengis.net/cat/csw/2.0.2</Value>
								<Value>http://www.isotc211.org/2005/gfc</Value>
								<Value>http://www.w3.org/ns/dcat#</Value>
								<Value>http://www.isotc211.org/2005/gmd</Value>
								<Value>http://standards.iso.org/iso/19115/-3/mdb/2.0</Value>
							</Parameter>
							<Parameter name=""typeNames"">
								<Value>csw:Record</Value>
								<Value>gfc:FC_FeatureCatalogue</Value>
								<Value>dcat</Value>
								<Value>gmd:MD_Metadata</Value>
								<Value>mdb:MD_Metadata</Value>
							</Parameter>
							<Parameter name=""CONSTRAINTLANGUAGE"">
								<Value>FILTER</Value>
								<Value>CQL_TEXT</Value>
							</Parameter>
							<Constraint name=""PostEncoding"">
								<Value>XML</Value>
								<Value>SOAP</Value>
							</Constraint>
							<Constraint name=""SupportedISOQueryables"">
								<Value>CreationDate</Value>
								<Value>GeographicDescriptionCode</Value>
								<Value>OperatesOn</Value>
								<Value>Modified</Value>
								<Value>DistanceUOM</Value>
								<Value>Operation</Value>
								<Value>ResourceIdentifier</Value>
								<Value>Format</Value>
								<Value>Identifier</Value>
								<Value>Language</Value>
								<Value>ServiceType</Value>
								<Value>OrganisationName</Value>
								<Value>KeywordType</Value>
								<Value>AnyText</Value>
								<Value>PublicationDate</Value>
								<Value>AlternateTitle</Value>
								<Value>Abstract</Value>
								<Value>HasSecurityConstraints</Value>
								<Value>Title</Value>
								<Value>CouplingType</Value>
								<Value>TopicCategory</Value>
								<Value>ParentIdentifier</Value>
								<Value>Subject</Value>
								<Value>ResourceLanguage</Value>
								<Value>TempExtent_end</Value>
								<Value>ServiceTypeVersion</Value>
								<Value>Type</Value>
								<Value>RevisionDate</Value>
								<Value>OperatesOnName</Value>
								<Value>Denominator</Value>
								<Value>DistanceValue</Value>
								<Value>TempExtent_begin</Value>
								<Value>OperatesOnIdentifier</Value>
							</Constraint>
							<Constraint name=""AdditionalQueryables"">
								<Value>SpecificationDate</Value>
								<Value>AccessConstraints</Value>
								<Value>ResponsiblePartyRole</Value>
								<Value>Degree</Value>
								<Value>Lineage</Value>
								<Value>OnlineResourceMimeType</Value>
								<Value>ConditionApplyingToAccessAndUse</Value>
								<Value>Date</Value>
								<Value>MetadataPointOfContact</Value>
								<Value>OnlineResourceType</Value>
								<Value>Relation</Value>
								<Value>SpecificationDateType</Value>
								<Value>Classification</Value>
								<Value>OtherConstraints</Value>
								<Value>SpecificationTitle</Value>
							</Constraint>
						</Operation>
					</OperationsMetadata>
				</Capabilities>";

				public static string GetRecordsResourceWithWFSProtocol => @"<GetRecordsResponse schemaLocation=""http://www.opengis.net/cat/csw/2.0.2 http://schemas.opengis.net/csw/2.0.2/CSW-discovery.xsd"">
					<SearchStatus timestamp=""2022-03-28T11:31:30"" />
					<SearchResults numberOfRecordsMatched=""265"" numberOfRecordsReturned=""100"" elementSet=""full"" nextRecord=""101"">
					<MD_Metadata schemaLocation=""http://www.isotc211.org/2005/gmd http://schemas.opengis.net/iso/19139/20060504/gmd/gmd.xsd"">
							<fileIdentifier>
								<CharacterString>9e75d506-1b99-45d9-afa0-b421d5ede3ff</CharacterString>
							</fileIdentifier>
							<language>
								<LanguageCode codeList=""http://www.loc.gov/standards/iso639-2/"" codeListValue=""ger"" />
							</language>
							<hierarchyLevel>
								<MD_ScopeCode codeList=""http://standards.iso.org/iso/19139/resources/gmxCodelists.xml#MD_ScopeCode"" codeListValue=""dataset"" />
							</hierarchyLevel>
							<contact>
								<CI_ResponsibleParty>
									<organisationName>
										<CharacterString>Bundesamt für Eich- und Vermessungswesen</CharacterString>
									</organisationName>
									<contactInfo>
										<CI_Contact>
											<address>
												<CI_Address>
													<electronicMailAddress>
														<CharacterString>kundenservice@bev.gv.at</CharacterString>
													</electronicMailAddress>
												</CI_Address>
											</address>
										</CI_Contact>
									</contactInfo>
									<role>
										<CI_RoleCode codeList=""http://standards.iso.org/iso/19139/resources/gmxCodelists.xml#CI_RoleCode"" codeListValue=""pointOfContact"" />
									</role>
								</CI_ResponsibleParty>
							</contact>
							<dateStamp>
								<DateTime>2022-01-18T08:49:59</DateTime>
							</dateStamp>
							<metadataStandardName>
								<CharacterString>ISO19115</CharacterString>
							</metadataStandardName>
							<metadataStandardVersion>
								<CharacterString>2003/Cor.1:2006</CharacterString>
							</metadataStandardVersion>
							<referenceSystemInfo>
								<MD_ReferenceSystem>
									<referenceSystemIdentifier>
										<RS_Identifier>
											<code>
												<CharacterString>EPSG:31281</CharacterString>
											</code>
											<codeSpace>
												<CharacterString>http://www.opengis.net/def/crs/EPSG/0/31281</CharacterString>
											</codeSpace>
										</RS_Identifier>
									</referenceSystemIdentifier>
								</MD_ReferenceSystem>
							</referenceSystemInfo>
							<referenceSystemInfo>
								<MD_ReferenceSystem>
									<referenceSystemIdentifier>
										<RS_Identifier>
											<code>
												<CharacterString>EPSG:31282</CharacterString>
											</code>
											<codeSpace>
												<CharacterString>http://www.opengis.net/def/crs/EPSG/0/31282</CharacterString>
											</codeSpace>
										</RS_Identifier>
									</referenceSystemIdentifier>
								</MD_ReferenceSystem>
							</referenceSystemInfo>
							<referenceSystemInfo>
								<MD_ReferenceSystem>
									<referenceSystemIdentifier>
										<RS_Identifier>
											<code>
												<CharacterString>EPSG:31283</CharacterString>
											</code>
											<codeSpace>
												<CharacterString>http://www.opengis.net/def/crs/EPSG/0/31283</CharacterString>
											</codeSpace>
										</RS_Identifier>
									</referenceSystemIdentifier>
								</MD_ReferenceSystem>
							</referenceSystemInfo>
							<referenceSystemInfo>
								<MD_ReferenceSystem>
									<referenceSystemIdentifier>
										<RS_Identifier>
											<code>
												<CharacterString>EPSG:3416</CharacterString>
											</code>
											<codeSpace>
												<CharacterString>http://www.opengis.net/def/crs/EPSG/0/3416</CharacterString>
											</codeSpace>
										</RS_Identifier>
									</referenceSystemIdentifier>
								</MD_ReferenceSystem>
							</referenceSystemInfo>
							<identificationInfo>
								<MD_DataIdentification>
									<citation>
										<CI_Citation>
											<title>
												<CharacterString>Festpunkte Lage</CharacterString>
											</title>
											<date>
												<CI_Date>
													<date>
														<DateTime>2021-07-01T00:00:00</DateTime>
													</date>
													<dateType>
														<CI_DateTypeCode codeList=""http://standards.iso.org/iso/19139/resources/gmxCodelists.xml#CI_DateTypeCode"" codeListValue=""publication"" />
													</dateType>
												</CI_Date>
											</date>
											<date>
												<CI_Date>
													<date>
														<DateTime>1990-01-01T00:00:00</DateTime>
													</date>
													<dateType>
														<CI_DateTypeCode codeList=""http://standards.iso.org/iso/19139/resources/gmxCodelists.xml#CI_DateTypeCode"" codeListValue=""creation"" />
													</dateType>
												</CI_Date>
											</date>
											<date>
												<CI_Date>
													<date>
														<DateTime>2021-07-01T00:00:00</DateTime>
													</date>
													<dateType>
														<CI_DateTypeCode codeList=""http://standards.iso.org/iso/19139/resources/gmxCodelists.xml#CI_DateTypeCode"" codeListValue=""revision"" />
													</dateType>
												</CI_Date>
											</date>
											<identifier>
												<MD_Identifier>
													<code>
														<CharacterString>https://doi.org/10.48677/9e75d506-1b99-45d9-afa0-b421d5ede3ff</CharacterString>
													</code>
												</MD_Identifier>
											</identifier>
										</CI_Citation>
									</citation>
									<abstract>
										<CharacterString>Das 2D/3D-Festpunktfeld stellt die Realisierung des österreichischen Systems der Landesvermessung durch physisch vermarkte Punkte dar. Die Koordinaten (Gauß-Krüger-Koordinaten) und die Höhe (normalorthometrische Höhe) dieser Festpunkte bilden die Basis für weiterführende vermessungstechnische Arbeiten, vor allem für den Kataster.</CharacterString>
									</abstract>
									<pointOfContact>
										<CI_ResponsibleParty>
											<individualName>
												<CharacterString>Abteilung Grundlagen</CharacterString>
											</individualName>
											<organisationName>
												<CharacterString>Bundesamt für Eich- und Vermessungswesen</CharacterString>
											</organisationName>
											<contactInfo>
												<CI_Contact>
													<address>
														<CI_Address>
															<electronicMailAddress>
																<CharacterString>kundenservice@bev.gv.at</CharacterString>
															</electronicMailAddress>
														</CI_Address>
													</address>
												</CI_Contact>
											</contactInfo>
											<role>
												<CI_RoleCode codeList=""http://standards.iso.org/iso/19139/resources/gmxCodelists.xml#CI_RoleCode"" codeListValue=""owner"" />
											</role>
										</CI_ResponsibleParty>
									</pointOfContact>
									<descriptiveKeywords>
										<MD_Keywords>
											<keyword>
												<CharacterString>INSPIRE</CharacterString>
											</keyword>
											<keyword>
												<CharacterString>Koordinatenreferenzsysteme</CharacterString>
											</keyword>
											<type>
												<MD_KeywordTypeCode codeList=""http://standards.iso.org/iso/19139/resources/gmxCodelists.xml#MD_KeywordTypeCode"" codeListValue=""theme"" />
											</type>
											<thesaurusName>
												<CI_Citation>
													<title>
														<CharacterString>GEMET - INSPIRE themes, version 1.0</CharacterString>
													</title>
													<date>
														<CI_Date>
															<date>
																<Date>2008-06-01</Date>
															</date>
															<dateType>
																<CI_DateTypeCode codeList=""http://standards.iso.org/iso/19139/resources/gmxCodelists.xml#CI_DateTypeCode"" codeListValue=""publication"" />
															</dateType>
														</CI_Date>
													</date>
													<identifier>
														<MD_Identifier>
															<code>
																<Anchor href=""http://sd.bev.gv.at/geonetwork/srv/eng/thesaurus.download?ref=external.theme.inspire-theme"">geonetwork.thesaurus.external.theme.inspire-theme</Anchor>
															</code>
														</MD_Identifier>
													</identifier>
												</CI_Citation>
											</thesaurusName>
										</MD_Keywords>
									</descriptiveKeywords>
									<descriptiveKeywords>
										<MD_Keywords>
											<keyword>
												<CharacterString>National</CharacterString>
											</keyword>
											<type>
												<MD_KeywordTypeCode codeList=""http://standards.iso.org/iso/19139/resources/gmxCodelists.xml#MD_KeywordTypeCode"" codeListValue=""theme"" />
											</type>
											<thesaurusName>
												<CI_Citation>
													<title>
														<CharacterString>Spatial scope</CharacterString>
													</title>
													<date>
														<CI_Date>
															<date>
																<Date>2019-05-22</Date>
															</date>
															<dateType>
																<CI_DateTypeCode codeList=""http://standards.iso.org/iso/19139/resources/gmxCodelists.xml#CI_DateTypeCode"" codeListValue=""publication"" />
															</dateType>
														</CI_Date>
													</date>
													<identifier>
														<MD_Identifier>
															<code>
																<Anchor href=""http://sd.bev.gv.at/geonetwork/srv/eng/thesaurus.download?ref=external.theme.SpatialScope"">geonetwork.thesaurus.external.theme.SpatialScope</Anchor>
															</code>
														</MD_Identifier>
													</identifier>
												</CI_Citation>
											</thesaurusName>
										</MD_Keywords>
									</descriptiveKeywords>
									<resourceConstraints>
										<MD_Constraints>
											<useLimitation>
												<CharacterString>Standardentgelte und Nutzungsbedingungen des BEV</CharacterString>
											</useLimitation>
										</MD_Constraints>
									</resourceConstraints>
									<resourceConstraints>
										<MD_Constraints>
											<useLimitation>
												<CharacterString>http://inspire.ec.europa.eu/metadata-codelist/LimitationsOnPublicAccess/noLimitations</CharacterString>
											</useLimitation>
										</MD_Constraints>
									</resourceConstraints>
									<resourceConstraints>
										<MD_LegalConstraints>
											<accessConstraints>
												<MD_RestrictionCode codeList=""http://standards.iso.org/iso/19139/resources/gmxCodelists.xml#MD_RestrictionCode"" codeListValue=""otherRestrictions"" />
											</accessConstraints>
											<otherConstraints>
												<CharacterString>Standardentgelte und Nutzungsbedingungen des BEV</CharacterString>
											</otherConstraints>
											<otherConstraints>
												<CharacterString>http://inspire.ec.europa.eu/metadata-codelist/ConditionsApplyingToAccessAndUse/noConditionsApply</CharacterString>
											</otherConstraints>
											<otherConstraints nilReason=""missing"">
												<Anchor href=""http://inspire.ec.europa.eu/metadata-codelist/LimitationsOnPublicAccess/noLimitations"" />
											</otherConstraints>
										</MD_LegalConstraints>
									</resourceConstraints>
									<spatialRepresentationType>
										<MD_SpatialRepresentationTypeCode codeList=""http://standards.iso.org/iso/19139/resources/gmxCodelists.xml#MD_SpatialRepresentationTypeCode"" codeListValue=""textTable"" />
									</spatialRepresentationType>
									<spatialResolution>
										<MD_Resolution>
											<distance>
												<Distance uom=""http://standards.iso.org/ittf/PubliclyAvailableStandards/ISO_19139_Schemas/resources/uom/ML_gmxUom.xml#m"">0.02</Distance>
											</distance>
										</MD_Resolution>
									</spatialResolution>
									<language>
										<LanguageCode codeList=""http://www.loc.gov/standards/iso639-2/"" codeListValue=""ger"" />
									</language>
									<topicCategory>
										<MD_TopicCategoryCode>location</MD_TopicCategoryCode>
									</topicCategory>
									<extent>
										<EX_Extent>
											<geographicElement>
												<EX_GeographicBoundingBox>
													<westBoundLongitude>
														<Decimal>9.53357</Decimal>
													</westBoundLongitude>
													<eastBoundLongitude>
														<Decimal>17.16639</Decimal>
													</eastBoundLongitude>
													<southBoundLatitude>
														<Decimal>46.40749</Decimal>
													</southBoundLatitude>
													<northBoundLatitude>
														<Decimal>49.01875</Decimal>
													</northBoundLatitude>
												</EX_GeographicBoundingBox>
											</geographicElement>
										</EX_Extent>
									</extent>
								</MD_DataIdentification>
							</identificationInfo>
							<distributionInfo>
								<MD_Distribution>
									<distributionFormat>
										<MD_Format>
											<name>
												<CharacterString>CSV</CharacterString>
											</name>
											<version>
												<CharacterString>Oktober 2005</CharacterString>
											</version>
											<specification>
												<CharacterString>IETF - RFC 4180</CharacterString>
											</specification>
										</MD_Format>
									</distributionFormat>
									<distributionFormat>
										<MD_Format>
											<name>
												<CharacterString>PDF</CharacterString>
											</name>
											<version>
												<CharacterString>Version 1.4 (Acrobat 5.x)</CharacterString>
											</version>
											<specification>
												<CharacterString>Adobe® Portable Document Format</CharacterString>
											</specification>
										</MD_Format>
									</distributionFormat>
									<distributor>
										<MD_Distributor>
											<distributorContact>
												<CI_ResponsibleParty>
													<individualName nilReason=""missing"">
														<CharacterString />
													</individualName>
													<organisationName>
														<CharacterString>Bundesamt für Eich- und Vermessungswesen</CharacterString>
													</organisationName>
													<contactInfo>
														<CI_Contact>
															<address>
																<CI_Address>
																	<electronicMailAddress nilReason=""missing"">
																		<CharacterString />
																	</electronicMailAddress>
																</CI_Address>
															</address>
														</CI_Contact>
													</contactInfo>
													<role>
														<CI_RoleCode codeList=""http://standards.iso.org/iso/19139/resources/gmxCodelists.xml#CI_RoleCode"" codeListValue="""" />
													</role>
												</CI_ResponsibleParty>
											</distributorContact>
										</MD_Distributor>
									</distributor>
									<transferOptions>
										<MD_DigitalTransferOptions>
											<onLine>
												<CI_OnlineResource>
													<linkage>
														<URL>http://www.bev.gv.at/pls/portal/url/PAGE/BEV_PORTAL_CONTENT_ALLGEMEIN/0200_PRODUKTE/0200_HIER_KATALOG/FESTPUNKTE%20LAGE/</URL>
													</linkage>
													<protocol>
														<CharacterString>WWW:LINK-1.0-http--link</CharacterString>
													</protocol>
													<name>
														<CharacterString>Informationen zum Produkt Festpunkte Lage</CharacterString>
													</name>
													<function>
														<CI_OnLineFunctionCode codeList=""http://standards.iso.org/iso/19139/resources/gmxCodelists.xml#CI_OnLineFunctionCode"" codeListValue=""information"" />
													</function>
												</CI_OnlineResource>
											</onLine>
											<onLine>
												<CI_OnlineResource>
													<linkage>
														<URL>http://data.bev.gv.at/geonetwork/srv/eng/csw?service=CSW&amp;request=GetRecordById&amp;version=2.0.2&amp;outputSchema=http%3A%2F%2Fwww.isotc211.org%2F2005%2Fgmd&amp;ElementSetName=full&amp;id=b14b50a5-702b-4b0c-b96f-b4a320c931ff</URL>
													</linkage>
													<protocol>
														<CharacterString>OGC:CSW</CharacterString>
													</protocol>
													<name>
														<CharacterString>Metadaten WMS Darstellungsdienst Koordinatenreferenzsysteme</CharacterString>
													</name>
												</CI_OnlineResource>
											</onLine>
											<onLine>
												<CI_OnlineResource>
													<linkage>
														<URL>http://data.bev.gv.at/geonetwork/srv/eng/csw?service=CSW&amp;request=GetRecordById&amp;version=2.0.2&amp;outputSchema=http%3A%2F%2Fwww.isotc211.org%2F2005%2Fgmd&amp;ElementSetName=full&amp;id=901849e0-c050-4c6b-b14e-0b9cccb8946c</URL>
													</linkage>
													<protocol>
														<CharacterString>OGC:CSW</CharacterString>
													</protocol>
													<name>
														<CharacterString>Metadaten WFS Downloaddienst (Produkt Web Service)</CharacterString>
													</name>
												</CI_OnlineResource>
											</onLine>
											<onLine>
												<CI_OnlineResource>
													<linkage>
														<URL>http://www.bev.gv.at/bev.webservice/inspire?service=WFS&amp;request=GetCapabilities</URL>
													</linkage>
													<protocol>
														<CharacterString>OGC:WFS</CharacterString>
													</protocol>
													<name>
														<CharacterString>WFS Downloaddienst</CharacterString>
													</name>
													<description>
														<CharacterString>WFS Downloaddienst GetCapabilities</CharacterString>
													</description>
												</CI_OnlineResource>
											</onLine>
											<onLine>
												<CI_OnlineResource>
													<linkage>
														<URL>http://wsa.bev.gv.at/GeoServer/Interceptor/Wms/CRS/INSPIRE_KUNDEN-382e30c7-69df-4a53-9331-c44821d9916e?REQUEST=GetCapabilities&amp;SERVICE=WMS&amp;VERSION=1.3.0</URL>
													</linkage>
													<protocol>
														<CharacterString>OGC:WMS-http-get-capabilities</CharacterString>
													</protocol>
													<name>
														<CharacterString>CRS.CoordinateReferenceSystem_GV_STABILISIERUNGEN</CharacterString>
													</name>
													<description>
														<CharacterString>CRS.CoordinateReferenceSystem_GV_STABILISIERUNGEN</CharacterString>
													</description>
												</CI_OnlineResource>
											</onLine>
											<onLine>
												<CI_OnlineResource>
													<linkage>
														<URL>https://doi.org/10.48677/9e75d506-1b99-45d9-afa0-b421d5ede3ff</URL>
													</linkage>
													<protocol>
														<CharacterString>DOI</CharacterString>
													</protocol>
													<name>
														<CharacterString>Digital Object Identifier (DOI)</CharacterString>
													</name>
												</CI_OnlineResource>
											</onLine>
										</MD_DigitalTransferOptions>
									</transferOptions>
								</MD_Distribution>
							</distributionInfo>
							<dataQualityInfo>
								<DQ_DataQuality>
									<scope>
										<DQ_Scope>
											<level>
												<MD_ScopeCode codeListValue=""dataset"" codeList=""http://standards.iso.org/iso/19139/resources/gmxCodelists.xml#MD_ScopeCode"" />
											</level>
										</DQ_Scope>
									</scope>
									<report>
										<DQ_DomainConsistency type=""gmd:DQ_DomainConsistency_Type"">
											<measureIdentification>
												<RS_Identifier>
													<code>
														<CharacterString>Conformity_001</CharacterString>
													</code>
													<codeSpace>
														<CharacterString>INSPIRE</CharacterString>
													</codeSpace>
												</RS_Identifier>
											</measureIdentification>
											<result>
												<DQ_ConformanceResult type=""gmd:DQ_ConformanceResult_Type"">
													<specification>
														<CI_Citation>
															<title>
																<CharacterString>Verordnung (EG) NR. 1089/2010 der Kommission vom 23. November 2010 zur Durchführung der Richtlinie 2007/2/EG des Europäischen Parlaments und des Rates hinsichtlich der Interoperabilität von Geodatensätzen und -diensten</CharacterString>
															</title>
															<date>
																<CI_Date>
																	<date>
																		<Date>2010-12-08</Date>
																	</date>
																	<dateType>
																		<CI_DateTypeCode codeList=""http://standards.iso.org/iso/19139/resources/gmxCodelists.xml#CI_DateTypeCode"" codeListValue=""publication"" />
																	</dateType>
																</CI_Date>
															</date>
														</CI_Citation>
													</specification>
													<explanation>
														<CharacterString>siehe referenzierte Spezifikation</CharacterString>
													</explanation>
													<pass>
														<Boolean>true</Boolean>
													</pass>
												</DQ_ConformanceResult>
											</result>
										</DQ_DomainConsistency>
									</report>
									<lineage>
										<LI_Lineage>
											<statement>
												<CharacterString>1) SCOPE:
													Das 2D/3D-Festpunktfeld stellt die Realisierung des nationalen Systems der Landesvermessung durch physisch vermarkte Punkte dar.
													
													2) ERSTELLUNGSPROZESS:
													Grundlage für die Realisierung war das 1862 begonnene mitteleuropäische Gradmessungsnetz. Aus diesem stammen auch heute noch 40 Punkte 1. Ordnung mit ihren damals bestimmten Koordinatenwerten. Nach dem 1. Weltkrieg wurde das Netz 1. Ordnung mit weiteren Neupunkten ergänzt, komplett neu übermessen und berechnet. Dieses bildete in Folge die Grundlage für weitere Verdichtungen der 2. bis 6. Ordnung. Einzelne Ergänzungen und Übermessungen finden auch heute noch statt. Diese historisch entstandene Realisierung des Systems MGI durch Koordinaten physischer Punkte - manchmal auch als Gebrauchskoordinaten bezeichnet - bildet das offizielle österreichische System der Landesvermessung.
													
													3) QUALITÄTSMASSNAHMEN:
													a) Edge-Matching-Status: -
													b) Positionsgenauigkeit: relative Genauigkeit zu Nachbarpunkten &lt; 7 cm; absolute Genauigkeit &lt; 1,2 m. Revision und Überprüfung der Punkte im Anlassfall
													
													4) UPDATE-INFORMATIONEN:
													Änderungen aufgrund von Revision und Überprüfung im Anlassfall</CharacterString>
											</statement>
										</LI_Lineage>
									</lineage>
								</DQ_DataQuality>
							</dataQualityInfo>
						</MD_Metadata>
					</SearchResults>
				</GetRecordsResponse>";
			}
		}
	}
}