// <copyright file="XmlElementExtensions.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business;

using System.Xml.Linq;

public static class XmlElementExtensions
{
	// https://stackoverflow.com/a/1147012/1249506
	public static void StripNamespaces(this XElement rootElement)
	{
		foreach (XElement element in rootElement.DescendantsAndSelf())
		{
			// Update element name if a namespace is available
			if (element.Name.Namespace != XNamespace.None)
			{
				element.Name = XNamespace.None.GetName(element.Name.LocalName);
			}

			// Check if the element contains attributes with defined namespaces (ignore xml and empty namespaces)
			bool hasDefinedNamespaces = element.Attributes()
				.Any(attribute => attribute.IsNamespaceDeclaration ||
					(attribute.Name.Namespace != XNamespace.None && attribute.Name.Namespace != XNamespace.Xml));

			if (hasDefinedNamespaces)
			{
				// Ignore attributes with a namespace declaration
				// Strip namespace from attributes with defined namespaces, ignore xml / empty namespaces
				// Xml namespace is ignored to retain the space preserve attribute
				IEnumerable<XAttribute> attributes = element.Attributes()
					.Where(attribute => !attribute.IsNamespaceDeclaration)
					.Select(attribute => attribute.Name.Namespace != XNamespace.None && attribute.Name.Namespace != XNamespace.Xml
						? new XAttribute(XNamespace.None.GetName(attribute.Name.LocalName), attribute.Value)
						: attribute);

				// Replace with attributes result
				element.ReplaceAttributes(attributes);
			}
		}
	}
}