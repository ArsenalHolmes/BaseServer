using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketSTest
{
    class newMsg : BaseServer.BaseDataUnlockPack
    {
        List<byte> msgList = new List<byte>();
        public override void DataUnLockPack(byte[] msgArr)
        {
            msgList.AddRange(msgArr.ToArray());
            ProcessData();
        }

        public void ProcessData()
        {
            byte[] msg = EncodingTool.ToolEncoding.DecodePacket(ref msgList);
            if (msg==null)
            {
                return;
            }
            object obj = SerializeTool.Deserialize.DeSerializeByByteArr<object>(msg);
            Console.WriteLine(obj);
        }
    }
}
