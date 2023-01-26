using System.Net;
using System.Net.Sockets;

// You can use print statements as follows for debugging, they'll be visible when running tests.
Console.WriteLine("Logs from your program will appear here!");

// Uncomment this block to pass the first stage
TcpListener server = new TcpListener(IPAddress.Any, 6379);
server.Start();
using var socket = server.AcceptSocket();
using var stream = new NetworkStream(socket);
using var reader = new StreamReader(stream);
using var writer = new StreamWriter(stream);
writer.AutoFlush = true;

try
{
    var inputLine = reader.ReadLine();
    while (!string.IsNullOrEmpty(inputLine))
    {
        if (inputLine == "ping")
        {
            writer.WriteLine("+PONG\r");
            writer.Flush();
            break;
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

