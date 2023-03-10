using System.Text;

namespace codecrafters_redis;

public static class Parser
{
    private enum FirstByte
    {
        SimpleString = '+',
        BulkString = '$',
        Array = '*'
    }
    
    public static object Parse(StreamReader reader)
    {
        var type = (FirstByte)reader.Read();
        return type switch
        {
            FirstByte.SimpleString => ParseSingleString(reader),
            FirstByte.BulkString => ParseBulkString(reader),
            FirstByte.Array => ParseArray(reader),
            _ => throw new FormatException("Invalid RESP string format.")
        };
    }
    
    private static string ReadTo(this StreamReader reader, int len)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < len; i++)
        {
            sb.Append((char)reader.Read());
        }
        
        return sb.ToString();
    }

    private static string ParseSingleString(StreamReader reader)
    {
        return reader.ReadLine() ?? "";
    }

    private static string ParseBulkString(StreamReader reader)
    {
        var len = int.Parse(reader.ReadLine()!);
        
        return len < 0 ? null : reader.ReadTo(len);
    }

    private static object[] ParseArray(StreamReader reader)
    {
        var len = int.Parse(reader.ReadLine()!);
        if (len < 0)
            return null;

        var values = new object[len];
        for (var i = 0; i < len; i++)
        {
            values[i] = Parse(reader);
        }

        return values;
    }
}