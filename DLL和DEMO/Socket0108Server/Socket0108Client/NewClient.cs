using System;
using System.Collections.Generic;
using System.Text;


namespace Socket0108Client
{
    class NewClient : BaseClient.BaseClient
    {
        public NewClient(string ip,int port):base(ip,port)
        {

        }
        public override void CloseEvent()
        {
            throw new NotImplementedException();
        }

        public override void ProcessData(byte[] msg)
        {
            if (msg == null || msg.Length == 1)
            {
                return;
            }
            object obj = SerializeTool.Deserialize.DeSerializeByByteArr<object>(msg);
            Console.WriteLine(obj);
        }
    }
}
