// <copyright file="WFS.FeatureType.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Parsing;

using System.Xml.Linq;

public static partial class WFS
{
	public class FeatureType : TypedXmlElementWrapper
	{
		public FeatureType(XElement root) : base(root)
		{
		}

		public static explicit operator FeatureType(XElement element) => new(element);
	}
}