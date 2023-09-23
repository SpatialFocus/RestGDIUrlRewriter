// <copyright file="TypedXmlElementWrapper.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business;

using System.Xml.Linq;

public class TypedXmlElementWrapper
{
	public TypedXmlElementWrapper(XElement root)
	{
		Root = root;
	}

	public XElement Root { get; }

	public static implicit operator XElement(TypedXmlElementWrapper elementWrapper) => elementWrapper.Root;
}