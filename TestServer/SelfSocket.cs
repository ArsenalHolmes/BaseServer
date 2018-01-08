using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

using BaseServer;

class SelfSocket : BaseSocket
{
    public override string IP => "127.0.0.1";

    public override int port => 54321;

    public override int MaxCount => 5;

    public SelfSocket()
    {

    }

    public SelfClient tempSC;

    public override BaseClient clientConnect(Socket client, Action<BaseClient> closeCallBack)
    {
        tempSC= new SelfClient(client, closeCallBack); ;
        return tempSC;
    }
}


