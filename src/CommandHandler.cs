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
                "echo" => $"+{(string)commands[1]}\r\n",
                "ping" => "+PONG\r\n",
                "set" => HandleSet(commands[1..]),
                "get" => HandleGet(commands[1..])
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

    private static string HandleSet(object[] args)
    {
        var key = (string)args[0];
        var value = (string)args[1];
        Storage.Set(key, value);
        return "+OK\r\n";
    }
    
    private static string HandleGet(object[] args)
    {
        var key = (string)args[0];
        var result = Storage.Get(key);
        return $"+{result}\r\n";
    }
    
}