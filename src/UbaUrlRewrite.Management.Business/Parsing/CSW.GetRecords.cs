// <copyright file="CSW.GetRecords.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Parsing;

using System.Xml.Linq;

public static partial class CSW
{
	public class GetRecords : TypedXmlElementWrapper
	{
		public GetRecords(XElement root) : base(root)
		{
		}

		public static explicit operator GetRecords(XElement element) => new(element);
	}
}