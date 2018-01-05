using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using BaseServer;

namespace SocketSTest
{
    class newServer : BaseServer.BaseSocket
    {
        public override bool isHeartBeat => false;
        public newClient nc;
        public override BaseClient clientConnect(Socket client, Action<BaseClient> closeCallBack)
        {
            nc= new newClient(client, closeCallBack);
            return nc;
        }
    }
}
