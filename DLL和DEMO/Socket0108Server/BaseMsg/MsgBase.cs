using System;
using System.Collections.Generic;
using System.Text;


namespace BaseMsg
{
    [Serializable]
    public class MsgBase
    {
        public int msgNum;

        public List<string> list = new List<string>();

        public byte[] msgArr;

    }
}
