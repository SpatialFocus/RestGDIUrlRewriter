// <copyright file="CacheExtensions.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Proxy;

using System.Collections.Concurrent;

public static class CacheExtensions
{
	public static bool TryGetCacheEntry(this ConcurrentDictionary<string, ConcurrentDictionary<string, CacheEntry>> cache,
		string metadataId, string featureType, out CacheEntry cacheEntry)
	{
		if (cache.TryGetValue(metadataId, out ConcurrentDictionary<string, CacheEntry>? innerCache))
		{
			if (innerCache.TryGetValue(featureType, out CacheEntry entry))
			{
				cacheEntry = entry;
				return true;
			}
		}

		cacheEntry = default;
		return false;
	}

	public static bool TryGetFirstCacheEntry(this ConcurrentDictionary<string, ConcurrentDictionary<string, CacheEntry>> cache,
		string metadataId, out CacheEntry cacheEntry)
	{
		if (cache.TryGetValue(metadataId, out ConcurrentDictionary<string, CacheEntry>? innerCache))
		{
			ICollection<CacheEntry> entries = innerCache.Values;

			if (entries.Any())
			{
				cacheEntry = entries.First();
				return true;
			}
		}

		cacheEntry = default;
		return false;
	}
}