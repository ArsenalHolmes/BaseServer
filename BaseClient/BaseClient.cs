using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;

namespace BaseClient
{
    public abstract class BaseClient
    {
        protected Socket client;
        protected byte[] msgByte;
        public virtual IClientCallBack callBack { get { return null; } }
        public List<byte> msgList = new List<byte>();

        public BaseClient(string ip, int port)
        {
            msgByte = new byte[1024];
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

                UnDataPack(newByte);
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

        /// <summary>
        /// 消息解析
        /// </summary>
        /// <param name="msg"></param>
        public virtual void UnDataPack(byte[] msg) {
            msgList.AddRange(msg);
            byte[] msgArr = EncodingTool.ToolEncoding.DecodePacket(ref msgList);
            ProcessData(msgArr);
        }


        public abstract void ProcessData(byte[] msg);

        public virtual void BeginSend(object obj)
        {
            try
            {
                byte[] byteArray = EncodingTool.ToolEncoding.EncodePacket(SerializeTool.Serialize.GetSeriaLizeByteArr(obj));
                BeginSend(byteArray);
            }
            catch (Exception)
            {
                CloseEvent();
            }

        }

        public virtual void BeginSend(byte[] msg)
        {
            try
            {
                byte[] byteArray = EncodingTool.ToolEncoding.EncodePacket(msg);
                client.BeginSend(byteArray, 0, byteArray.Length, SocketFlags.None, SendAsyn, client);
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
