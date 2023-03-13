using System.Collections.Concurrent;
using System.Runtime.Caching;

namespace codecrafters_redis.Storage;

public static class Storage
{
    private static readonly ConcurrentDictionary<string, string> CD = new();

    public static void Set(string key, string value, int? px)
    {
        
        CD.Add(key, value, (key, value) => value);
    }

    public static string? Get(string key)
    {
        return CD[key].ToString();
    }
}