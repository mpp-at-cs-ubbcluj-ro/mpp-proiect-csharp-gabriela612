using System.Net.Sockets;
using System.Threading;

namespace Utills.networking;

public abstract class AbsConcurrentServer : AbstractServer
{
    public AbsConcurrentServer(string host, int port) : base(host, port)
    {}

    public override void processRequest(TcpClient client)
    {
                
        Thread t = createWorker(client);
        t.Start();
                
    }

    protected abstract  Thread createWorker(TcpClient client);
}