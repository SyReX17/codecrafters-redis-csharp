using System.Runtime.Caching;

namespace codecrafters_redis.Storage;

public static class Storage
{
    private static readonly MemoryCache MC = new MemoryCache("mc");

    public static void Set(string key, string value, int? px)
    {
        if (px == null)
        {
            var policy = new CacheItemPolicy();
            MC.Set(key, value, policy);
            return;
        }
        
        var expire = new DateTimeOffset().AddMilliseconds(Convert.ToDouble(px));
        MC.Set(key, value, expire);
    }

    public static string? Get(string key)
    {
        return MC[key].ToString();
    }
}