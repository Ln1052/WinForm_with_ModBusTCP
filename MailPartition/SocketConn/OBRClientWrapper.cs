using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using MailPartition.Common;

namespace MailPartition.SocketConn
{
    internal class OBRClientWrapper:IDisposable
    {
        public static string IP = ConfigurationManager.AppSettings["OBRIP"];
        private static int Port = Int32.Parse(ConfigurationManager.AppSettings["OBRPort"]);
        private static int TimeOut = Int32.Parse(ConfigurationManager.AppSettings["SocketTimeOut"]);

        private Socket socket = null;

        /// <summary>
        /// Client： 建立与OBR连接（默认自动重连5次）
        /// </summary>
        public void Connect()
        {
            //默认重连5次
            this.Connect(5);
        }

        public void Connect(int times)
        {
            if (times == 0)
                return;

            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, TimeOut);

            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(IP), Port);
            try
            {
                this.socket.Connect(ipe);
                Flag.OBRConnected = true;
                Log.Write("Client: 与OBR建立连接");

                //等待读取OBR发送过来的数据
                Read();
            }
            catch (Exception e)
            {
                Flag.OBRConnected = false;

                Log.Write("Client: 连接OBR失败 " + e.Message);

                //重连
                Log.Write("Client: 重连OBR…");
                Connect(--times);
            }
        }

        /// <summary>
        /// 从OBR中获取数据
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public void Read()
        {
            while (null != this.socket)
            {
                byte[] data = new byte[1024];
                try
                {
                    if (this.socket.Receive(data) <= 0)
                    {
                        Flag.OBRConnected = false;
                        Log.Write("Client: 与OBR连接断开");
                        Connect();
                    }
                    else //解析从OBR收到的数据
                    {
                        OBRComProtocol obrData = new OBRComProtocol(data, 0);
                        for (int index = 0; index < obrData.BarCodes.Count; index++)
                        {
                            lock (BasicData.OBRQueue)
                            {
                                BasicData.OBRQueue.Enqueue(new OBRData(obrData.MailId, obrData.BarCodes[index]));
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    Flag.OBRConnected = false;
                    Log.Write("Client: 与OBR连接异常： " + e.Message);
                    Connect();
                }
            }
        }

        /// <summary>
        /// Client： 向OBR发送数据
        /// </summary>
        /// <param name="data"></param>
        public void Write(byte[] data)
        {
            try
            {
                this.socket.Send(data);
            }
            catch (Exception e)
            {
                Flag.OBRConnected = false;

                Log.Write("Client: 向OBR发送数据时异常： " + e.Message);

                Connect();
            }
        }

        #region IDisposable 成员
        public void Dispose()
        {
            if (this.socket != null)
            {
                this.socket.Close();
                this.socket = null;
            }
        }
        #endregion
    }
}
