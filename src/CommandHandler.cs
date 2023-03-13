using codecrafters_redis.RESP.Enums;
using codecrafters_redis.RESP.Models;

namespace codecrafters_redis;

public static class CommandHandler
{
    public static void Handle(StreamWriter sw, RespRequest respRequest)
    {
        var response = "";
        if (respRequest.Type == RespType.Array)
        {
            response = respRequest.Values![0].Value!.ToLower() switch
            {
                "echo" => $"+{respRequest.Values![1].Value}\r\n",
                "ping" => "+PONG\r\n",
                "set" => HandleSet(respRequest.Values![1..]),
                "get" => HandleGet(respRequest.Values![1..])
            };
        }
        else
        {
            response = respRequest.Values![0].Value!.ToLower() switch
            {
                "ping" => "+PONG\r\n"
            };
        }
        
        sw.Write(response);
    }

    private static string HandleSet(RespRequest[] args)
    {
        var key = args[0].Value;
        var value = args[1].Value;
        Storage.Storage.Set(key!, value!);
        return "+OK\r\n";
    }
    
    private static string HandleGet(RespRequest[] args)
    {
        var key = args[0].Value;
        var result = Storage.Storage.Get(key!);
        return $"+{result}\r\n";
    }
    
}