using SocketSTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketCTest
{
    class Program
    {
        static newClient nc;
        static void Main(string[] args)
        {
            //客户端
            nc = new newClient("127.0.0.1", 54321);
            while (true)
            {
                string str = Console.ReadLine();
                nc.BeginSend(SerializeTool.Serialize.GetSeriaLizeByteArr(str));
            }
        }
    }
}
