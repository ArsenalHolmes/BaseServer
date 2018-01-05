using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSql
{
    class DicTable
    {
        List<string> HeadList = new List<string>();
        Dictionary<int, Dictionary<string,string>> table = new Dictionary<int, Dictionary<string, string>>();

        public Dictionary<string,string> this[int index]
        {
            get {
                if (index>=table.Count)
                {
                    AddDict();
                }
                return table[index];
            }
        }

        public void AddDict()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            foreach (var item in HeadList)
            {
                d.Add(item, "");
            }
            table.Add(table.Count, d);
        }

        


    }
}
