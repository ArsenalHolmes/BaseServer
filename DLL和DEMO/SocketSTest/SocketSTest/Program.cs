using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseServer;

namespace SocketSTest
{
    class Program
    {
        //服务器
        public static newServer bs;
        static void Main(string[] args)
        {
            bs= new newServer();

            Console.WriteLine("输入内容");
            while (true)
            {
                Console.WriteLine("输入内容");
                string str = Console.ReadLine();
                bs.nc.SendMsg(SerializeTool.Serialize.GetSeriaLizeByteArr(str));
            }
        }
    }
}
