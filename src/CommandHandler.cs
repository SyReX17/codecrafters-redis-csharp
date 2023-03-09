namespace codecrafters_redis;

public static class CommandHandler
{
    public static void Handle(StreamWriter sw, object command)
    {
        var response = ((string)command).ToLower() switch
        {
            "ping" => "+PONG\r\n"
        };
        
        sw.Write(response);
    }
    
    public static void Handle(StreamWriter sw, object[] commands)
    {
        var response = ((string)commands[0]).ToLower() switch
        {
            "echo" => $"+{((string)commands[1])}\r\n"
        };
        
        sw.Write(response);
    }
}