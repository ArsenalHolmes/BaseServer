using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace BaseServer
{
    public abstract class BaseClient
    {
        Socket client;
        public Socket Client { get => client; }
        Action<BaseClient> closeCallBack;
        public virtual IClientCallBackInterface ClientClose { get { return null; } }

        public byte[] msgArr = new byte[4096];
        //public  byte[] msgArr { get { return new byte[1024];} }

        public abstract BaseDataUnlockPack pack { get; }

        public BaseClient(Socket client, Action<BaseClient> closeCallBack)
        {
            this.client = client;
            this.closeCallBack = closeCallBack;
            client.BeginReceive(msgArr, 0, msgArr.Length, SocketFlags.None, AsynReceive, client);
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
                Console.WriteLine(count);
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

            int len = msg.Length;
            byte[] lenArr = BitConverter.GetBytes(len);
            List<byte> byteSource = new List<byte>();
            byteSource.AddRange(lenArr);
            byteSource.AddRange(msg);

            try
            {
                client.BeginSend(byteSource.ToArray(), 0, byteSource.Count, SocketFlags.None, AsynSend, client);
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
                Console.WriteLine("断开连接");
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
