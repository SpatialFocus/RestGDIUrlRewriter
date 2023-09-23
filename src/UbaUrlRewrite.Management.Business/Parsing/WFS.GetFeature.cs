// <copyright file="WFS.GetFeature.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Parsing;

using System.Xml.Linq;

public static partial class WFS
{
	public class GetFeature : TypedXmlElementWrapper
	{
		public GetFeature(XElement root) : base(root)
		{
		}

		public static explicit operator GetFeature(XElement element) => new(element);
	}
}