using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ExcelTest
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream fs = new FileStream("d:\\666.csv", FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.GetEncoding(936));

            Console.WriteLine(sr);
            string str = "";
            string s = Console.ReadLine();
            while ( (str = sr.ReadLine()) != null)
            {
                string[] xu = str.Split(',');
                foreach (var item in xu)
                {
                    Console.Write(item.Trim()+" ");
                }
                Console.WriteLine("**************");
            }
            sr.Close();
        }
    }
}
