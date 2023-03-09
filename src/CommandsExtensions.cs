namespace codecrafters_redis;

public static class CommandsExtensions
{
    public static void HandleCommands(this string[] args, StreamWriter sw)
    {
        var i = 0;

        while (i < args.Length)
        {
            var output = args[i] switch
            {
                "*1" => args[i + 2].ToLower() switch
                {
                    "ping" => "+PONG\r\n"
                },
                "*2" => args[i + 2].ToLower() switch
                {
                    "echo" => $"+{args[i + 4]}\r\n"
                }
            };

            i += 2;
        }
    }
}