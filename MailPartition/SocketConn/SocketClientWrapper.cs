using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Configuration;
using MailPartition.Common;
using System.Windows.Forms;

namespace MailPartition.SocketConn
{
    internal class SocketClientWrapper : IDisposable
    {
        private static string IP = ConfigurationManager.AppSettings["PLCIP"];
        private static int Port = Int32.Parse(ConfigurationManager.AppSettings["PLCPort"]);
        private static int TimeOut = Int32.Parse(ConfigurationManager.AppSettings["SocketTimeOut"]);

        private Socket socket = null;

        /// <summary>
        /// Client: 建立与PLC连接（默认自动重连5次）
        /// </summary>
        /// <param name="times"></param>
        public void Connect(int times = 5)
        {
            if (times == 0)
                return;

            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, TimeOut);

            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(IP), Port);
            try
            {
                this.socket.Connect(ipe);
                Flag.PLCConnected = true;

                Log.Write(string.Format("Client: 与PLC建立连接, port: {0}", (this.socket.LocalEndPoint as IPEndPoint).Port));
            }
            catch (Exception e)
            {
                Flag.PLCConnected = false;

                Log.Write("Client: 连接PLC失败." + e.Message);

                //重连
                Log.Write("Client: 重连PLC…");
                Connect(--times);
            }
        }

        /// <summary>
        /// Client: 向PLC发送数据
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] Read(int length)
        {
            byte[] data = new byte[length];
            try
            {
                this.socket.Receive(data);
            }
            catch (Exception e)
            {
                Log.Write("Client: 接收PLC数据时异常： " + e.Message);
            }

            return data;
        }

        /// <summary>
        /// Client: 给PLC发送数据
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
                Flag.PLCConnected = false;

                Log.Write("Client: 给PLC发送数据时异常： " + e.Message);

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
