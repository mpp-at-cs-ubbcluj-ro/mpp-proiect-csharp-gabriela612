using System;
using System.Net.Sockets;
using System.Threading;
using Utills.services;

namespace Utills.networking;

public class ConcurrentServer : AbsConcurrentServer
{
    private IServices server;
    private ClientWorker worker;
    public ConcurrentServer(string host, int port, IServices server) : base(host, port)
    {
        this.server = server;
        Console.WriteLine("ConcurrentServer...");
    }
    protected override Thread createWorker(TcpClient client)
    {
        worker = new ClientWorker(server, client);
        return new Thread(new ThreadStart(worker.run));
    }
}
