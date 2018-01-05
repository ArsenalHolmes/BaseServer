using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializeTool
{
    public static class Serialize
    {
        public static byte[] GetSeriaLizeByteArr(object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            ms.Position = 0;
            bf.Serialize(ms, obj);
            byte[] s = ms.ToArray();
            return s;
        }
    }
}
