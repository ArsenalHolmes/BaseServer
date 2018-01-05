using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace ThreadSql.Properties
{
    class Sql
    {
        MySqlConnection mySql;
        public Sql()
        {
            //server=localhost;User Id=root;password=;Database=blog
            string sql = "server=127.0.0.1;user=root;password=root;database=konglong";
            mySql = new MySqlConnection(sql);
        }

        public string GetPswByName(string sql)
        {
            mySql.Open();
            MySqlCommand com = new MySqlCommand(sql, mySql);

            MySqlDataReader read = com.ExecuteReader();
            read.Read();
            string s = read.GetString("Name");
            mySql.Close();

            return s;
        }

        //public void InsterData(string name,string psw)
        //{
        //    mySql.Open();
        //    string sql = "INSERT into data value('{0}','{1}')";
        //    sql = string.Format(sql, name,psw);

        //    MySqlCommand com = new MySqlCommand(sql, mySql);
        //    com.ExecuteNonQuery();
        //    mySql.Close();
        //}
    }
}
