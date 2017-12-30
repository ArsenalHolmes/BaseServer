using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace BaseServer
{
    public abstract class BaseSocket
    {
        
        public abstract string IP { get; }
        public abstract int port { get; }
        public abstract int MaxCount { get;}

        Socket SocketServer;

        public BaseSocket()
        {
            SocketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            SocketServer.Bind(new IPEndPoint(IPAddress.Parse(IP), port));
            SocketServer.Listen(MaxCount);
            SocketServer.BeginAccept(AsyncAccept,SocketServer);
        }

        private void AsyncAccept(IAsyncResult ar)
        {
            Socket client = SocketServer.EndAccept(ar);
            clientConnect(client);

            SocketServer.BeginAccept(AsyncAccept, SocketServer);
        }

        public abstract void clientConnect(Socket client);
    }
}
