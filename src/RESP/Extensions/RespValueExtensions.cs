using codecrafters_redis.RESP.Enums;
using codecrafters_redis.RESP.Models;

namespace codecrafters_redis.RESP.Extensions;

public static class RespValueExtensions
{
    public static string Format(this RespValue value)
    {
        return value.Type switch
        {
            RespType.SimpleString => $"+{value.Value!}\r\n",
            RespType.BulkString => $"${value.Value!.Length}\r\n{value.Value!}\r\n",
            RespType.Integer => $":{value.Value!}\r\n",
            RespType.Error => $"-{value.Value!}\r\n",
            RespType.Array => $"*{value.Values!.Length}" + value.Values!.Select(item => item.Format()),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}