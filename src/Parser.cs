using System.Text;

namespace codecrafters_redis;

public class Parser
{
    private enum RespType
    {
        SimpleString = '+',
        BulkString = '$',
        Array = '*'
    }
    
    public static object Parse(StreamReader reader)
    {
        var type = (RespType)reader.Read();
        switch (type)
        {
            case RespType.SimpleString:
                return ParseSingleString(reader);
            case RespType.BulkString:
                return ParseBulkString(reader);
            case RespType.Array:
                return ParseArray(reader);
            default:
                throw new FormatException("Invalid RESP string format.");
        }
    }
    
    private static string ReadLine(StreamReader reader)
    {
        var sb = new StringBuilder();
        while (true)
        {
            var ch = reader.Read();
            if (ch == '\r')
            {
                reader.Read();
                return sb.ToString();
            }
            sb.Append((char)ch);
        }
    }

    private static string ParseSingleString(StreamReader reader)
    {
        return ReadLine(reader);
    }

    private static string ParseBulkString(StreamReader reader)
    {
        var len = int.Parse(ReadLine(reader));
        if (len < 0)
            return null;
        else 
            return ReadLine(reader);
    }

    private static object[] ParseArray(StreamReader reader)
    {
        var len = int.Parse(ReadLine(reader));
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