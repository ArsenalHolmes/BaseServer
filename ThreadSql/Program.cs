using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using ThreadSql.Properties;
using System.Threading;
using System.Data;
using System.IO;

namespace ThreadSql
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //FileStream fss = File.Create("1.xlsx");
            //fss.Write(buf, 0, buf.Length);
            //fss.Close();
        }

        public void DataTableTest()
        {

            DataTable tblDatas = new DataTable("Datas");
            DataColumn dc = null;
            dc = tblDatas.Columns.Add("ID", Type.GetType("System.Int32"));
            dc.AutoIncrement = true;//自动增加
            dc.AutoIncrementSeed = 1;//起始为1
            dc.AutoIncrementStep = 1;//步长为1
            dc.AllowDBNull = false;//

            dc = tblDatas.Columns.Add("Product", Type.GetType("System.String"));
            dc = tblDatas.Columns.Add("Version", Type.GetType("System.String"));
            dc = tblDatas.Columns.Add("Description", Type.GetType("System.String"));

            DataRow newRow;
            newRow = tblDatas.NewRow();
            newRow["Product"] = "大话西游";
            newRow["Version"] = "2.0";
            newRow["Description"] = "我很喜欢";
            tblDatas.Rows.Add(newRow);

            newRow = tblDatas.NewRow();
            newRow["Product"] = "梦幻西游";
            newRow["Version"] = "3.0";
            newRow["Description"] = "比大话更幼稚";
            tblDatas.Rows.Add(newRow);

            foreach (DataRow item in tblDatas.Rows)
            {
                foreach (var items in item.ItemArray)
                {
                    Console.Write(items);
                }
            }

        }

        public string GetSqlString(string tableName,Dictionary<string,string> dic)
        {
            string b = "select * from {0} where ";
            b = String.Format(b, tableName);

            string temp = "{0}={1} and ";
            string baseStr = "";
            foreach (var item in dic)
            {
                baseStr += String.Format(temp, item.Key, item.Value);
            }
            baseStr = baseStr.Remove(baseStr.Length - 4);
            string s = b + baseStr;
            return s;
        }

        public byte[] GetByteArrByPath(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            byte[] buf = new byte[fileInfo.Length];
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            fs.Read(buf, 0, buf.Length);
            fs.Close();
            Console.WriteLine(buf.Length);
            return buf;
        }


    }
}
