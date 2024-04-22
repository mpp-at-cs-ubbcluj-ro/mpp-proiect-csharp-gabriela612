using System;
using System.Net.Sockets;
using System.Threading;
using Utills.services;

namespace Utills.networking;

public class ProtoConcurrentServer : ConcurrentServer
{
    private IServices server;
    private ProtoClientWorker worker;
    public ProtoConcurrentServer(string host, int port, IServices server)
        : base(host, port)
    {
        this.server = server;
        Console.WriteLine("ProtoConcurrentServer...");
    }
    protected override Thread createWorker(TcpClient client)
    {
        Console.WriteLine("Proto Con Ser : ajuns");
        worker = new ProtoClientWorker(server, client);
        Console.WriteLine("Proto Con Ser : mai departe");
        return new Thread(new ThreadStart(worker.run));
    }
}