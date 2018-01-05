using System;
using BaseServer;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Reflection;
using SerializeTool;

class DataUnlockPack : BaseDataUnlockPack
{
    public override void DataUnLockPack(byte[] msgArr)
    {
        //string s = Encoding.UTF8.GetString(msgArr);
        //Console.WriteLine(s);
        try
        {
            //IFormatter formatter = new BinaryFormatter();
            //formatter.Binder = new UBinder();
            //MemoryStream stream = new MemoryStream(msgArr,0,msgArr.Length);
            object obj = Deserialize.DeSerializeByByteArr<Person>(msgArr);

            //BinaryFormatter bf = new BinaryFormatter();
            //stream.Close();

            Person p = obj as Person;
            Console.WriteLine(p.name);
            Console.WriteLine(p.age);
            Console.WriteLine(p.dic[p.name]);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }



    }
}

public class UBinder : SerializationBinder
{
    public override Type BindToType(string assemblyName, string typeName)
    {
        Assembly ass = Assembly.GetExecutingAssembly();
        return ass.GetType(typeName);
    }
}
