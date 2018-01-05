using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseServer;

namespace SocketSTest
{
    class CallBack : IClientCallBackInterface
    {
        public void ClientClose(BaseClient bc)
        {
            
        }

        public void SendCallBack(BaseClient bc, int len)
        {
            
        }
    }
}
