using System;
using System.Collections.Generic;

using System.Text;


namespace Socket0108Server
{
    class Program
    {
        static void Main(string[] args)
        {
            NewServer ns = new NewServer();
            while (true)
            {
                string str = Console.ReadLine();
                ns.nc.SendMsg(str);
            }
        }
    }
}
