using System;
using System.Net.Sockets;
using System.Threading;
using Utills.services;

namespace Utills.networking;

public abstract class ConcurrentServer:AbstractServer
{
            
    public ConcurrentServer(string host, int port) : base(host, port)
    {}

    public override void processRequest(TcpClient client)
    {
        Console.WriteLine("Con Ser : aici");
        Thread t = createWorker(client);
        Console.WriteLine("Con Ser : acolo");
        t.Start();
                
    }

    protected abstract  Thread createWorker(TcpClient client);
   
}

