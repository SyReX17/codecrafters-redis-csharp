using System.Collections.Concurrent;

namespace codecrafters_redis.Storage;

public static class Storage
{
    private static readonly ConcurrentDictionary<string, string> CD = new();

    public static void Set(string key, string value, int? px)
    {
        if (px != null)
        {
            AddWithExpire(key, value, (int)px);
            return;
        }
        CD.TryAdd(key, value);
    }

    public static string? Get(string key)
    {
        CD.TryGetValue(key, out var value);
        return string.IsNullOrEmpty(value) ? null : value;
    }

    public static async Task AddWithExpire(string key, string value, int px)
    {
        CD.TryAdd(key, value);
        var timer = new Timer(state =>
        {
            CD.TryRemove(key, out _);
        }, null, TimeSpan.FromMilliseconds(px), Timeout.InfiniteTimeSpan);
    }
}