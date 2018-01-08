using System;
using System.Collections.Generic;

using System.Text;

namespace BaseServer
{
    public abstract class BaseDataUnlockPack
    {
        protected List<byte> msgList = new List<byte>();
        public virtual void DataUnLockPack(byte[] msgArr) {
            msgList.AddRange(msgArr);
            byte[] msg = EncodingTool.ToolEncoding.DecodePacket(ref msgList);
            ProcessData(msg);
        }

        public abstract void ProcessData(byte[] msg);
    }
}
