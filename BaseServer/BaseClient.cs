using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace BaseServer
{
    public abstract class BaseClient
    {
        Socket client;
        public Socket Client { get => client; }
        Action<BaseClient> closeCallBack;
        public virtual IClientCallBackInterface ClientClose { get { return null; } }

        public byte[] msgArr = new byte[4096];

        public abstract BaseDataUnlockPack pack { get; }

        public BaseClient(Socket client, Action<BaseClient> closeCallBack)
        {
            this.client = client;
            this.closeCallBack = closeCallBack;
            client.BeginReceive(msgArr, 0, msgArr.Length, SocketFlags.None, AsynReceive, client);
            //client.BeginReceive(msgList, SocketFlags.None, AsynReceive, client);
        }

        /// <summary>
        /// 异步接受数据
        /// </summary>
        /// <param name="ar"></param>
        private void AsynReceive(IAsyncResult ar)
        {
            if (client == null) return;
            int count=0;
            try
            {
                count = client.EndReceive(ar);
            }
            catch (Exception e)
            {
                Close();
                return;
            }
            if (count==0)
            {
                Close();
                return;
            }
            byte[] newArr = new byte[count];
            Array.Copy(msgArr, 0, newArr, 0, count);
            pack.DataUnLockPack(newArr);

            client.BeginReceive(msgArr, 0, msgArr.Length, SocketFlags.None, AsynReceive, client);
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="msg"></param>
        public bool SendMsg(byte[] msg)
        {
            if (client == null) return false;

            byte[] byteNew = EncodingTool.ToolEncoding.EncodePacket(msg);

            try
            {
                client.BeginSend(byteNew, 0, byteNew.Length, SocketFlags.None, AsynSend, client);
            }
            catch (Exception)
            {
                Close();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 发送数据完成
        /// </summary>
        /// <param name="ar"></param>
        private void AsynSend(IAsyncResult ar)
        {
            int count = client.EndSend(ar);
            if (ClientClose!=null)
            {
                ClientClose.SendCallBack(this,count);
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public virtual void Close()
        {
            try
            {
                if (ClientClose!=null)
                {
                    ClientClose.ClientClose(this);
                }
                if (closeCallBack != null)
                {
                    closeCallBack(this);
                }
                else
                {
                    client.Close();
                }
                client = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("关闭出现错误");
            }
        }
    }
}
