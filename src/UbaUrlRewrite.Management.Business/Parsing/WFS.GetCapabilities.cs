// <copyright file="WFS.GetCapabilities.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Parsing;

using System.Xml.Linq;

public static partial class WFS
{
	public class GetCapabilities : TypedXmlElementWrapper
	{
		public GetCapabilities(XElement root) : base(root)
		{
		}

		public static explicit operator GetCapabilities(XElement element) => new(element);
	}
}