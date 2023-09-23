// <copyright file="CSW.GetRecordsResponseMetadata.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Parsing;

using System.Xml.Linq;

public static partial class CSW
{
	public class GetRecordsResponseMetadata : TypedXmlElementWrapper
	{
		public GetRecordsResponseMetadata(XElement root) : base(root)
		{
		}

		public static explicit operator GetRecordsResponseMetadata(XElement element) => new(element);
	}
}