using System.Net;
using System.Net.Sockets;

namespace codecrafters_redis;

public class AsyncTcpServer
{
    private static readonly ManualResetEvent AllCompleted = new ManualResetEvent(false);

    public static void RunServer()
    {
        var server = new TcpListener(IPAddress.Any, 6379);
        server.Start();
        try
        {
            while (true)
            {
                AllCompleted.Reset();
                server.BeginAcceptTcpClient(AcceptCallback, server);
                AllCompleted.WaitOne();
            }
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static void AcceptCallback(IAsyncResult ar)
    {
        AllCompleted.Set();
        var client = (TcpClient)ar.AsyncState!;
        var stream = client.GetStream();
        
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
}