using codecrafters_redis;

// You can use print statements as follows for debugging, they'll be visible when running tests.
Console.WriteLine("Logs from your program will appear here!");

var server = new ServerListener();

try
{
    server.RunListener();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

