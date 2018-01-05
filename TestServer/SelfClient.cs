using System;

using BaseServer;
using System.Net.Sockets;

class SelfClient : BaseClient
{
    public override BaseDataUnlockPack pack => new DataUnlockPack();

    //public override byte[] msgArr => new byte[2048];

    public SelfClient(Socket client, Action<BaseClient> closeCallBack) : base(client, closeCallBack)
    {

    }

}

