// <copyright file="CSW.GetRecordById.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Parsing;

using System.Xml.Linq;

public static partial class CSW
{
	public class GetRecordById : TypedXmlElementWrapper
	{
		public GetRecordById(XElement root) : base(root)
		{
		}

		public static explicit operator GetRecordById(XElement element) => new(element);
	}
}