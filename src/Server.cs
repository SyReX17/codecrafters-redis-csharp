using System.Net;
using System.Net.Sockets;
using System.Text;

// You can use print statements as follows for debugging, they'll be visible when running tests.
Console.WriteLine("Logs from your program will appear here!");

// Uncomment this block to pass the first stage
TcpListener server = new TcpListener(IPAddress.Any, 6379);
server.Start();
server.AcceptSocket(); // wait for client

using var client = server.AcceptTcpClient();
var stream = client.GetStream();
using var reader = new StreamReader(stream);
using var writer = new StreamWriter(stream);

string inputLine;
while (!string.IsNullOrEmpty(inputLine = reader.ReadLine()))
{
    if (inputLine == "ping")
    {
        Console.WriteLine(inputLine);
        writer.WriteLine("+PONG\r");
        writer.Flush();
        break;
    }
}

