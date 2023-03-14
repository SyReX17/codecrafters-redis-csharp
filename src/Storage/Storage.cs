using System.Collections.Concurrent;

namespace codecrafters_redis.Storage;

public static class Storage
{
    private static readonly ConcurrentDictionary<string, string> CD = new();

    public static void Set(string key, string value, int? px)
    {
        CD.TryAdd(key, value);
        
        if (px != null)
        {
            HandleExpire(key, (int)px);
        }
    }

    public static string? Get(string key)
    {
        return CD[key];
    }

    public static async Task HandleExpire(string key, int px)
    {
        var timer = new Timer(state =>
        {
            CD.TryRemove(key, out _);
        }, null, TimeSpan.FromMilliseconds(px), Timeout.InfiniteTimeSpan);
    }
}