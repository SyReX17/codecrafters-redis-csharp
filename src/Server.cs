using System.Net;
using System.Net.Sockets;

// You can use print statements as follows for debugging, they'll be visible when running tests.
Console.WriteLine("Logs from your program will appear here!");

// Uncomment this block to pass the first stage
var server = new TcpListener(IPAddress.Any, 6379);
server.Start();
using var socket = server.AcceptTcpClient();
var stream = socket.GetStream();
try
{
    using var reader = new StreamReader(stream);
    using var writer = new StreamWriter(stream);
    writer.AutoFlush = true;
    while(reader.ReadLine() is {} line) 
    {
        if (line == "ping")
        {
            writer.Write("+PONG\r\n");
            writer.Flush();
        }
    }
}
catch (IOException e)
{
    Console.WriteLine(e.Message);
}
finally
{
    try
    {
        socket.Close();
    }
    catch (IOException e)
    {
        Console.WriteLine(e.Message);
    }
}

