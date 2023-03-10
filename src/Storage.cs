using System.Collections.Concurrent;

namespace codecrafters_redis;

public static class Storage
{
    private static readonly ConcurrentDictionary<string, string> Cd = new();

    public static void Set(string key, string value)
    {
        Cd.AddOrUpdate(key, value, (key, value) => value);
    }

    public static string Get(string key)
    {
        return Cd[key];
    }
}