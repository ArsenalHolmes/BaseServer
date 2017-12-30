using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
    class Program
    {
        public static Socket client;
        static void Main(string[] args)
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //SocketServer.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6666));
            //client.BeginConnect(AsyncAccept, client);
            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6666));
        }

        public static byte[] msgArr;
        private static void AsyncAccept(IAsyncResult ar)
        {
            client = client.EndAccept(ar);
            msgArr = new byte[1024];
            client.BeginReceive(msgArr, 0, msgArr.Length, SocketFlags.None, AsynReceive, client);
        }

        private static void AsynReceive(IAsyncResult ar)
        {
            Console.WriteLine("收到消息");
        }

        public static void sendmsg()
        {
            client.Send(msgArr);
        }
    }
}
