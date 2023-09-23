// <copyright file="StringExtensions.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Management.Business.Test.Core;

using System.IO;

public static class StringExtensions
{
	public static Stream ToStream(this string str)
	{
		MemoryStream stream = new();

		StreamWriter writer = new(stream);
		writer.Write(str);
		writer.Flush();

		stream.Position = 0;

		return stream;
	}
}