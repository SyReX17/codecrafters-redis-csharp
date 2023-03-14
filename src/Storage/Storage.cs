using System.Collections.Concurrent;

namespace codecrafters_redis.Storage;

public static class Storage
{
    private static readonly ConcurrentDictionary<string, string> CD = new();

    public static void Set(string key, string value, int? px)
    {
        
        CD.AddOrUpdate(key, value, (key, value) => value);
    }

    public static string? Get(string key)
    {
        return CD[key].ToString();
    }
}