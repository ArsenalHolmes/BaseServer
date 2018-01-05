using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseServer;
using System.Net.Sockets;

namespace SocketSTest
{
    class newClient : BaseServer.BaseClient
    {
        public newClient(Socket c , Action<BaseClient> b):base(c,b)
        {

        }
        public override IClientCallBackInterface ClientClose => new CallBack();
        public override BaseDataUnlockPack pack => new newMsg();
    }
}
