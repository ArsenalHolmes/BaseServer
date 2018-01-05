using System;
using System.Collections.Generic;

namespace BaseClient
{
    public interface IClientCallBack
    {
        void ReceiveCallBack();
        void SendCallBack();
    }
}
