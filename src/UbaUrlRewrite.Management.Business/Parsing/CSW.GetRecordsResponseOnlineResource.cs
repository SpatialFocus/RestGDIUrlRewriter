// <copyright file="CSW.GetRecordsResponseOnlineResource.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Parsing;

using System.Xml.Linq;

public static partial class CSW
{
	public class GetRecordsResponseOnlineResource : TypedXmlElementWrapper
	{
		public GetRecordsResponseOnlineResource(XElement root) : base(root)
		{
		}

		public static explicit operator GetRecordsResponseOnlineResource(XElement element) => new(element);
	}
}