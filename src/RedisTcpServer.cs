using System.Net;
using System.Net.Sockets;

namespace codecrafters_redis;

public class RedisTcpServer
{
    public void RunServer()
    {
        var server = new TcpListener(IPAddress.Any, 6379);
        server.Start();
        try
        {
            while (true)
            {
                var newClient = server.AcceptTcpClient();

                Thread t = new Thread(HandleClient);
                t.Start(newClient);
            }
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void HandleClient(object obj)
    {
        var client = (TcpClient)obj;
        var stream = client.GetStream();
        
        using var reader = new StreamReader(stream);
        using var writer = new StreamWriter(stream);
        writer.AutoFlush = true;
        var commands = reader.ReadToEnd();
        ParseResp(commands).HandleCommands(writer);
    }

    private string[] ParseResp(string input)
    {
        return input.Split("\r\n");
    }
}