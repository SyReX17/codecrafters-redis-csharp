namespace codecrafters_redis;

public static class CommandHandler
{
    public static void Handle(StreamWriter sw, object command)
    {
        var response = "";
        if (command is object[] commands)
        {
            response = ((string)commands[0]).ToLower() switch
            {
                "echo" => $"+{((string)commands[1])}\r\n",
                "ping" => "+PONG\r\n"
            };
        }
        else
        {
            response = ((string)command).ToLower() switch
            {
                "ping" => "+PONG\r\n"
            };
        }
        
        
        sw.Write(response);
    }
}