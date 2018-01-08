using System;
using System.Collections.Generic;

using System.Net.Sockets;
using System.Text;

using BaseServer;

namespace Socket0108Server
{
    class NewServer : BaseSocket
    {
        public NewClient nc;
        public override BaseClient clientConnect(Socket client, Action<BaseClient> closeCallBack)
        {
            nc = new NewClient(client, closeCallBack);
            return nc;
        }
    }
}
