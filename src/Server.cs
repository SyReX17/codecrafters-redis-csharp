using System.Net;
using System.Net.Sockets;
using System.Text;

// You can use print statements as follows for debugging, they'll be visible when running tests.
Console.WriteLine("Logs from your program will appear here!");

// Uncomment this block to pass the first stage
TcpListener server = new TcpListener(IPAddress.Any, 6379);
server.Start();
server.AcceptSocket(); // wait for client

for (;;)
{
    using (var client = server.AcceptTcpClient())
    {
        var buffer = new byte[256];
        string data;

        var stream = client.GetStream();

        var i = stream.Read(buffer, 0, buffer.Length);
        
        data = Encoding.ASCII.GetString(buffer, 0, i);
        Console.WriteLine(data);
        data = data.ToUpper();

        var command = data.Split(" ");
        var response = command[0] switch
        {
            "PING" => command[1] switch
            {
                null or "" => "\"PONG\"",
                _ => command[1]
            },
            _ => ""
        };

        var responseBuffer = Encoding.ASCII.GetBytes(response);
        stream.Write(responseBuffer, 0, responseBuffer.Length);
    }
}
