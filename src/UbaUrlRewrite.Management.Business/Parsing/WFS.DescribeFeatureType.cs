// <copyright file="WFS.DescribeFeatureType.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Parsing
{
	using System.Xml.Linq;

	public static partial class WFS
	{
		public class DescribeFeatureType : TypedXmlElementWrapper
		{
			public DescribeFeatureType(XElement root) : base(root)
			{
			}

			public static explicit operator DescribeFeatureType(XElement element) => new(element);
		}
	}
}