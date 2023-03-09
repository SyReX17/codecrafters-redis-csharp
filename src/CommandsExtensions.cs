namespace codecrafters_redis;

public static class CommandsExtensions
{
    public static void HandleCommands(this string[] args, StreamWriter sw)
    {
        var i = 0;

        Console.WriteLine("StartHandle...");

        while (i < args.Length)
        {
            var output = "";
            if (args[i] == "*1" && args[i + 2].ToLower().Contains("ping"))
            {
                Console.WriteLine("ping command");
                output = "+PONG\r\n";
            }
            else if (args[i] == "*1" && args[i + 2].ToLower().Contains("echo"))
            {
                Console.WriteLine("echo command");
                output = $"+{args[i + 4]}\r\n";
            }

            Console.WriteLine("start response...");
            if (!string.IsNullOrEmpty(output))
            {
                sw.Write(output);
                sw.Flush();
            }
            Console.WriteLine("move next...");
            i += 2;
        }
    }
}