# REST GDI Agrar URL-Rewriter

Die URL-Rewriter Softwarekomponente soll die automatisierte Auflösung des `gml:identifier` ermöglichen. Der `gml:identifier` ist der von ISO 19136:2020 definierte, global eindeutige Identifikator für Objekte. Die Zusammensetzung bzw. das Muster, das bei der Implementierung des `gml:identifier` Anwendung findet, ist nicht Teil des Standards. In Österreich wurden von der INSPIRE Assistenzstelle Vorschläge für die Umsetzung der sogenannten `InspireId` erarbeitet.

Diese setzt sich aus den folgenden Komponenten zusammen:

 - `namespace`: Der Namespace beginnt mit der Domain des Datenanbieters gefolgt vom eindeutigen Identifikator des Metadatensatzes. Im Anschluss folgt der Character String des Namespace des GML-Applikationsschemas, welchem getrennt durch einen Punkt der Feature Type Name des exponierten und angebotenen Feature Types folgt.
 - `localId`: Eindeutiger Identifikator eines Objektes auf Datensatz/Datenbankinstanz Ebene (dieser Identifikator ist nicht global eindeutig)
 - `versionId`: Zeichenkette, welche die Versionsnummer des Objektes repräsentiert, falls mehrere Versionen ein und desselben Objektes existieren. Speziell für Datensätze / Datenbankinstanzen relevant, welche eine Objekthistorisierung implementiert haben. Oft wird eine Zeichenkette verwendet, welche ein Datum repräsentiert.

Die zusammengesetzte InspireId entspricht dem `gml:identifier`. Die `versionId` ist optional, was bedeutet, dass diese vorkommen kann aber nicht zwingend vorkommen muss. Das lässt sich auf die Tatsache zurückführen, dass rund 90% aller derzeit existierenden Datensätze sowie Datenbankinstanzen keine Objekthistorisierung aufweisen. Wenn nur ein Objekt vorgehalten ist, setzt sich der `gml:identifier` lediglich aus den Bestandteilen `namespace/localId` der `InspireId` zusammen.

Durch das im Rahmen der INSPIRE Assistenzstelle definierte Muster der `InspireId` lässt sich diese vollautomatisch mittels einer Softwarekomponente auflösen. Unter Auflösung versteht man, dass die URL des `gml:identifier` das konkrete Objekt im GML Encoding (= default) ausliefert. Des Weiteren müssen die Objekte auch in den alternativen Encodings, welche die jeweilige Webservertechnologie anbietet zur Verfügung gestellt werden. Um das Objekt in einem alternativen Encoding anfordern zu können, wird nach dem `gml:identifier` der Parameter `&outputFormat={alternatives_encoding}` angeführt.

## Konfiguration

Als Input für den URL-Rewriter dient eine Liste an Metadaten-Servern, welche aktuell in den App Settings (`appsettings.json`) gespeichert sind. Diese müssen dem CSW Standard (Catalogue Service for the Web) entsprechen, bei dem folgende Versionen unterstützt werden:

 - 2.0.0, 2.0.1, 2.0.2
 - 3.0.0

Unterstützt ein Metadaten-Server den CSW Standard nicht, oder bietet er keine der o.a. Versionen an, so wird dieser ignoriert und nicht für die Indizierung herangezogen.

## Weitere Informationen

Nähere Informationen zur Umsetzung und Funktionsweise des URL-Rewriter finden sich in der Offline-Dokumentation.

----

Made with :heart: by [Spatial Focus](https://spatial-focus.net/)
