namespace codecrafters_redis;

public static class CommandsExtensions
{
    public static void HandleCommands(this string[] args, StreamWriter sw)
    {
        var i = 0;

        while (i < args.Length)
        {
            var output = "";
            if (args[i] == "*1" && args[i + 2].ToLower().Contains("ping"))
            {
                output = "+PONG\r\n";
            }
            else if (args[i] == "*1" && args[i + 2].ToLower().Contains("echo"))
            {
                output = $"+{args[i + 4]}\r\n";
            }
            sw.WriteLine(output);
            i += 2;
        }
    }
}