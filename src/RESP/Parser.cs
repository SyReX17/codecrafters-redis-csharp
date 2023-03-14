using System.Text;
using codecrafters_redis.RESP.Enums;
using codecrafters_redis.RESP.Models;

namespace codecrafters_redis.RESP;

public static class Parser
{
    private enum FirstByte
    {
        SimpleString = '+',
        BulkString = '$',
        Array = '*',
        Integer = ':',
        Error = '-'
    }
    
    public static RespValue Parse(StreamReader reader)
    {
        var type = (FirstByte)reader.Read();
        return type switch
        {
            FirstByte.SimpleString => ParseSingleString(reader),
            FirstByte.BulkString => ParseBulkString(reader),
            FirstByte.Array => ParseArray(reader),
            FirstByte.Integer => ParseInteger(reader),
            FirstByte.Error => ParseError(reader),
            _ => throw new FormatException("Invalid RESP string format.")
        };
    }

    private static RespValue ParseSingleString(StreamReader reader)
    {
        var result = new RespValue
        {
            Type = RespType.SimpleString,
            Value = reader.ReadLine() ?? ""
        };

        return result;
    }

    private static RespValue ParseBulkString(StreamReader reader)
    {
        var len = int.Parse(reader.ReadLine()!);
        var str = reader.ReadTo(len);

        var result = new RespValue
        {
            Type = RespType.BulkString,
            Value = len < 0 ? null : str
        };

        return result;
    }

    private static RespValue ParseArray(StreamReader reader)
    {
        var len = int.Parse(reader.ReadLine()!);
        if (len < 0)
            return null;

        var values = new RespValue[len];
        for (var i = 0; i < len; i++)
        {
            values[i] = Parse(reader);
        }

        var result = new RespValue
        {
            Type = RespType.Array,
            Values = values
        };

        return result;
    }

    private static RespValue ParseInteger(StreamReader reader)
    {
        var result = new RespValue
        {
            Type = RespType.Integer,
            Value = reader.ReadLine() ?? ""
        };

        return result;
    }
    
    private static RespValue ParseError(StreamReader reader)
    {
        var result = new RespValue
        {
            Type = RespType.Error,
            Value = reader.ReadLine() ?? ""
        };

        return result;
    }
    
    private static string ReadTo(this StreamReader reader, int len)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < len; i++)
        {
            sb.Append((char)reader.Read());
        }

        // for "\r\n"
        reader.ReadLine();
        return sb.ToString();
    }
}