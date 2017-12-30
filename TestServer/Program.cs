using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TestServer
{
    class Program
    {
        public static Socket SocketServer;
        public static Socket client;
        static void Main(string[] args)
        {
            SocketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            SocketServer.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6666));
            SocketServer.Listen(5);
            SocketServer.BeginAccept(AsyncAccept, SocketServer);
            while (true)
            {

            }
        }
        public static byte[] msgArr;
        private static void AsyncAccept(IAsyncResult ar)
        {
            client = SocketServer.EndAccept(ar);
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
