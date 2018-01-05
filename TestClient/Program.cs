using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TestClient
{
    class Program
    {
        static byte[] msg = new byte[1024];
        static Socket client;
        static void Main(string[] args)
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 54321));

            client.BeginReceive(msg,0,1024,SocketFlags.None, Receive, client);
            while (true)
            {

            }
        }

        private static void Receive(IAsyncResult ar)
        {
            int count = client.EndReceive(ar);

            Console.WriteLine("客户端收到消息"+count);
            client.BeginReceive(msg, 0, 1024, SocketFlags.None, Receive, client);
        }
    }
}
