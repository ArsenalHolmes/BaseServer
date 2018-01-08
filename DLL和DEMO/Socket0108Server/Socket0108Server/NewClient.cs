using System;
using System.Collections.Generic;

using System.Text;

using BaseServer;
using System.Net.Sockets;

namespace Socket0108Server
{
    class NewClient : BaseServer.BaseClient
    {
        public override BaseDataUnlockPack pack => new NewDataPack();

        public NewClient(Socket client, Action<BaseClient> closeCallBack):base (client,closeCallBack)
        {

        }
    }
}
