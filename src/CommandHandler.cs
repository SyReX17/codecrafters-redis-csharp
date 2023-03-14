using codecrafters_redis.RESP.Enums;
using codecrafters_redis.RESP.Extensions;
using codecrafters_redis.RESP.Models;

namespace codecrafters_redis;

public static class CommandHandler
{
    public static void Handle(StreamWriter sw, RespValue respValue)
    {
        RespValue response = null;
        if (respValue.Type == RespType.Array)
        {
            response = respValue.Values![0].Value!.ToLower() switch
            {
                "echo" => HandleEcho(respValue.Values![1..]),
                "ping" => HandlePing(),
                "set" => HandleSet(respValue.Values![1..]),
                "get" => HandleGet(respValue.Values![1..]),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        else if (respValue.Type == RespType.SimpleString)
        {
            response = respValue.Value!.ToLower() switch
            {
                "ping" => HandlePing(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        sw.Write(response!.Format());
    }

    private static RespValue HandlePing()
    {
        return new RespValue
        {
            Type = RespType.SimpleString,
            Value = "PONG"
        };
    }

    private static RespValue HandleEcho(RespValue[] args)
    {
        return new RespValue
        {
            Type = RespType.SimpleString,
            Value = args[0].Value!
        };
    }

    private static RespValue HandleSet(RespValue[] args)
    {
        var key = args[0].Value;
        var value = args[1].Value;
        int? px = args.Length > 3 ? Convert.ToInt32(args[3].Value) : null;
        Storage.Storage.Set(key!, value!, px);
        
        return new RespValue
        {
            Type = RespType.SimpleString,
            Value = "OK"
        };
    }
    
    private static RespValue HandleGet(RespValue[] args)
    {
        var key = args[0].Value;
        var result = Storage.Storage.Get(key!);
        
        return new RespValue
        {
            Type = RespType.SimpleString,
            Value = result
        };
    }
    
}