// <copyright file="CSW.GetRecordsResponse.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Parsing;

using System.Xml.Linq;

public static partial class CSW
{
	public class GetRecordsResponse : TypedXmlElementWrapper
	{
		public GetRecordsResponse(XElement root) : base(root)
		{
		}

		public static explicit operator GetRecordsResponse(XElement element) => new(element);
	}
}