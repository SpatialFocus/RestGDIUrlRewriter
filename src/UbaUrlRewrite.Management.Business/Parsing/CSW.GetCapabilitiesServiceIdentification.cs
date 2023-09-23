// <copyright file="CSW.GetCapabilitiesServiceIdentification.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Parsing;

using System.Xml.Linq;

public static partial class CSW
{
	public class GetCapabilitiesServiceIdentification : TypedXmlElementWrapper
	{
		public GetCapabilitiesServiceIdentification(XElement root) : base(root)
		{
		}

		public static explicit operator GetCapabilitiesServiceIdentification(XElement element) => new(element);
	}
}