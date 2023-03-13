using codecrafters_redis.RESP.Enums;

namespace codecrafters_redis.RESP.Models;

public class RespRequest
{
    public RespType Type { get; set; }
    
    public string? Value { get; set; }

    public RespRequest[]? Values { get; set; }
}