using System;
using BaseServer;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Reflection;
using SerializeTool;
using EncodingTool;
using System.Collections.Generic;

class DataUnlockPack : BaseDataUnlockPack
{
    public List<byte> msgCacheList = new List<byte>();

    public override void DataUnLockPack(byte[] msgArr)
    {
        msgCacheList.AddRange(msgArr);
        ProcessMsg();
    }

    public void ProcessMsg()
    {
        byte[] msg = ToolEncoding.DecodePacket(ref msgCacheList);
        if (msg == null) return;

        try
        {
            object obj = Deserialize.DeSerializeByByteArr<object>(msg);

            Console.WriteLine(obj);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}

