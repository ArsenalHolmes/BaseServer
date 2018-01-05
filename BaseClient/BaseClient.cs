using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;

namespace BaseClient
{
    public abstract class BaseClient
    {
        protected Socket client;
        protected byte[] msgByte;
        protected Queue<byte[]> msgQueue;
        public virtual IClientCallBack callBack { get { return null; } }

        public BaseClient(string ip, int port)
        {
            msgByte = new byte[1024];
            msgQueue = new Queue<byte[]>();

            try
            {
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client.Connect(new IPEndPoint(IPAddress.Parse(ip), port));

                client.BeginReceive(msgByte, 0, 1024, SocketFlags.None, ReceiveAsyn, client);
            }
            catch (Exception)
            {
                ConnectError();
            }

        }

        private void ReceiveAsyn(IAsyncResult ar)
        {
            try
            {
                int count = client.EndReceive(ar);
                byte[] newByte = new byte[count];
                Array.Copy(msgByte, 0, newByte, 0, count);
                msgQueue.Enqueue(newByte);

                if (callBack != null)
                {
                    callBack.ReceiveCallBack();
                }
            }
            catch (Exception)
            {
                CloseEvent();
            }

            client.BeginReceive(msgByte, 0, 1024, SocketFlags.None, ReceiveAsyn, client);
        }

        public virtual void Update()
        {
            while (msgQueue.Count > 0)
            {
                UnDataPack(msgQueue.Dequeue());
            }
        }

        /// <summary>
        /// 消息解析
        /// </summary>
        /// <param name="msg"></param>
        public abstract void UnDataPack(byte[] msg);

        public void BeginSend(byte[] msg)
        {
            try
            {
                client.BeginSend(msg, 0, msg.Length, SocketFlags.None, SendAsyn, client);
            }
            catch (Exception)
            {
                CloseEvent();
            }

        }

        private void SendAsyn(IAsyncResult ar)
        {
            int count = client.EndSend(ar);
            if (callBack != null)
            {
                callBack.SendCallBack();
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public abstract void CloseEvent();

        /// <summary>
        /// 连接失败
        /// </summary>
        public virtual void ConnectError()
        {

        }
    }
}
