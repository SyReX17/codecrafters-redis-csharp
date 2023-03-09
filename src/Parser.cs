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

    public static object Parse(string respString)
    {
        var reader = new System.IO.StringReader(respString);
        return Parse(reader);
    }
    
    private static object Parse(System.IO.StringReader reader)
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
    
    private static string ReadLine(System.IO.StringReader reader)
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

    private static string ParseSingleString(System.IO.StringReader reader)
    {
        return ReadLine(reader);
    }

    private static string ParseBulkString(System.IO.StringReader reader)
    {
        var len = int.Parse(ReadLine(reader));
        if (len < 0)
            return null;
        else 
            return ReadLine(reader);
    }

    private static object[] ParseArray(System.IO.StringReader reader)
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