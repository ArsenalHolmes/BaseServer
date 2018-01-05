using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace BaseServer
{
    public abstract class BaseSocket
    {
        
        public virtual string IP { get { return "127.0.0.1"; } }
        public virtual int port { get { return 54321; } }
        public virtual int MaxCount { get { return 5; } }

        public virtual bool isHeartBeat { get { return true; } }
        public virtual int HeartTime { get { return 1000; } }

        Action<BaseClient> closeCallBack;
        protected List<BaseClient> list;

        Socket SocketServer;

        public BaseSocket()
        {
            SocketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            SocketServer.Bind(new IPEndPoint(IPAddress.Parse(IP), port));
            SocketServer.Listen(MaxCount);
            SocketServer.BeginAccept(AsyncAccept,SocketServer);
            list = new List<BaseClient>();
            if (isHeartBeat)
            {
                Thread t = new Thread(Heartbeat);
                t.Start();
            }
            closeCallBack = (b) => { list.Remove(b); };
        }

        private void AsyncAccept(IAsyncResult ar)
        {
            Socket client = SocketServer.EndAccept(ar);

            list.Add(clientConnect(client, closeCallBack));

            SocketServer.BeginAccept(AsyncAccept, SocketServer);
        }

        public abstract BaseClient clientConnect(Socket client, Action<BaseClient> closeCallBack);

        public void Heartbeat()
        {
            List<BaseClient> removeList = new List<BaseClient>();
            //TODO 心跳检测
            while (true)
            {
                Thread.Sleep(HeartTime);
                if (list.Count==0)
                {
                    continue;
                }
                foreach (var item in list)
                {
                    if (!item.SendMsg(Encoding.UTF8.GetBytes("q")))
                    {
                        removeList.Add(item);
                    }
                }
                foreach (var item in removeList)
                {
                    list.Remove(item);
                }
            }

        }
    }
}
