using codecrafters_redis.RESP.Enums;

namespace codecrafters_redis.RESP.Models;

public class RespValue
{
    public RespType Type { get; set; }
    
    public string? Value { get; set; }

    public RespValue[]? Values { get; set; }
}