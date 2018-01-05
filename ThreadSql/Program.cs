using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using ThreadSql.Properties;
using System.Threading;

namespace ThreadSql
{
    class Program
    {
        static string[] strArr = { "从中兴研发主管坠亡来看，什么是程序员的不能承受之重？", "如何看待2018风暴英雄大改？", "如何评价现今LGD战队的DOTA2分部？出路在何方？", "华尔街真如电影《华尔街之狼》里那么荒淫无度么？" };
        static void Main(string[] args)
        {
            //while (true)
            //{

            //    foreach (var item in strArr)
            //    {
            //        Thread t = new Thread(new ParameterizedThreadStart(ThreadTest));
            //        t.Start(item);
            //    }
            //    //Thread.Sleep(0.2f);
            //}
            string s = "999999";
            byte[] msg = Encoding.UTF8.GetBytes(s);
            byte[] newMsg = new byte[msg.Length];
            Array.Copy(newMsg, 0, msg, 0, newMsg.Length);

            Console.WriteLine(msg.Length);
            Console.WriteLine(newMsg.Length);

            
        }

        public static void ThreadTest(object st)
        {
            Sql s = new Sql();
            //Console.WriteLine(s.GetPswByName(st.ToString())+"       "+DateTime.Now);
            s.InsterData("7", "e");
        }
    }
}
