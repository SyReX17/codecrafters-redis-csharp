using System.Collections.Concurrent;

namespace codecrafters_redis;

public static class Storage
{
    private static ConcurrentDictionary<string, string> _cd = new();

    public static void Set(string key, string value)
    {
        _cd.AddOrUpdate(key, value, (key, value) => value);
    }

    public static string Get(string key)
    {
        return _cd[key];
    }
}