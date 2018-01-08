using BaseMsg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;


namespace Socket0108Client
{
    class Program
    {
        static void Main(string[] args)
        {
            NewClient nc= new NewClient("127.0.0.1",54321);
            while (true)
            {
                string str = Console.ReadLine();

                MsgBase bm = new MsgBase();

                bm.msgNum = 10;
                bm.list.Add("5");
                bm.list.Add("1");
                bm.list.Add("9");

                string s = "t";
                int i = 6;
                List<string> li = new List<string>();
                li.Add("q");
                li.Add("w");
                li.Add("e");
                li.Add("r");

                List<byte> ms = new List<byte>();
                ms.AddRange(SerializeTool.Serialize.GetSeriaLizeByteArr(s));
                ms.AddRange(SerializeTool.Serialize.GetSeriaLizeByteArr(i));
                ms.AddRange(SerializeTool.Serialize.GetSeriaLizeByteArr(li));

                bm.msgArr = ms.ToArray();
                Console.WriteLine(bm.msgArr.Length+"msgArr的");
                byte[] byteArr = SerializeTool.Serialize.GetSeriaLizeByteArr(bm);
                nc.BeginSend(byteArr);
            }
        }
    }
}
