using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace BaseServer
{
    abstract class BaseClient
    {
        Socket client;
        public Socket Client { get => client; }
        public virtual bool isHeartBeat { get { return true; } }
        byte[] msgArr;

        public abstract BaseDataUnlockPack pack { get; }

        public BaseClient(Socket client)
        {
            this.client = client;
            msgArr = new byte[1024];
            client.BeginReceive(msgArr, 0, msgArr.Length, SocketFlags.None, AsynReceive, client);
            if (isHeartBeat)
            {
                Thread t = new Thread(Heartbeat);
                t.Start();
            }
        }

        /// <summary>
        /// 异步接受数据
        /// </summary>
        /// <param name="ar"></param>
        private void AsynReceive(IAsyncResult ar)
        {
            int count = client.EndReceive(ar);
            byte[] newArr = new byte[count];
            Array.Copy(msgArr, 0, newArr, 0, count);
            int len = BitConverter.ToInt32(newArr, 0);
            pack.DataUnLockPack(newArr);
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="msg"></param>
        public void SendMsg(byte[] msg)
        {
            int len = msg.Length;
            byte[] lenArr = BitConverter.GetBytes(len);

            List<byte> byteSource = new List<byte>();
            byteSource.AddRange(lenArr);
            byteSource.AddRange(msg);

            client.Send(byteSource.ToArray());
        }

        public void Heartbeat()
        {
            //TODO 心跳检测

        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public virtual void Close()
        {
            client.Close();
        }
    }
}
