using BaseMsg;
using System;
using System.Collections.Generic;

using System.Text;

namespace Socket0108Server
{
    class NewDataPack : BaseServer.BaseDataUnlockPack
    {
        public override void ProcessData(byte[] msg)
        {
            if (msg == null || msg.Length == 1)
            {
                return;
            }
            object obj = SerializeTool.Deserialize.DeSerializeByByteArr<object>(msg);

            if (obj == null)
            {
                Console.WriteLine("obj位空");
                return;
            }
            //Console.WriteLine(obj);

            MsgBase ms = (MsgBase)obj;
            Console.WriteLine(ms.msgNum);

            foreach (var item in ms.list)
            {
                Console.WriteLine(item);
            }
        }
    }
}
