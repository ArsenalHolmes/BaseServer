using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketSTest
{
    class newClient : BaseClient.BaseClient
    {
        public newClient(string ip, int port) : base(ip, port)
        {

        }
        public override void CloseEvent()
        {
            Console.WriteLine("断开");
        }

        public override void UnDataPack(byte[] msg)
        {
            msgList.AddRange(msg.ToArray());
            ProcessData();
        }

        public override void ConnectError()
        {
            Console.WriteLine("连接失误");
        }


        List<byte> msgList = new List<byte>();

        public void ProcessData()
        {
            byte[] msg = EncodingTool.ToolEncoding.DecodePacket(ref msgList);
            if (msg == null)
            {
                return;
            }
            object obj = SerializeTool.Deserialize.DeSerializeByByteArr<object>(msg);
            Console.WriteLine(obj);
        }
    }
}
