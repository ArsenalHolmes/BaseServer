using System;
using System.Collections.Generic;

using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            SelfSocket ss = new SelfSocket();
            while (true)
            {
                string str = Console.ReadLine();
                byte[] arr = SerializeTool.Serialize.GetSeriaLizeByteArr(str);
                ss.tempSC.SendMsg(arr);
            }
        }
    }
}
