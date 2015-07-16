using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Configuration;
using MailPartition.Common;
using System.Windows.Forms;
using System.Threading;

namespace MailPartition.SocketConn
{
    internal class SocketServerWrapper : IDisposable
    {
        private static int ListenPort = Int32.Parse(ConfigurationManager.AppSettings["ListenPort"]);

        private Socket listenSocket = null;
        private Socket tempClientSocket = null;

        public Socket TempClientSocket 
        { 
            get { return this.tempClientSocket; }
            set { this.tempClientSocket = value; }
        }
        
        /// <summary>
        /// Server: 监听客户端（PLC）连接
        /// </summary>
        public void Listen()
        {
            if (null == this.listenSocket)
            {
                string host = ConfigurationManager.AppSettings["ListenIP"];
                IPAddress listenIP = IPAddress.Parse(host);
                IPEndPoint ipe = new IPEndPoint(listenIP, ListenPort);
                this.listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//创建一个Socket类
                this.listenSocket.Bind(ipe);//绑定502端口
                this.listenSocket.Listen(10);//开始监听
            }


            Log.Write("Server: 开始监听，等待PLC连接……");

            try
            {
                this.tempClientSocket = listenSocket.Accept(); //新建连接的socket

                Log.Write(string.Format("Server: PLC建立连接，PLC IP: {0}  port: {1}",
                    (this.tempClientSocket.RemoteEndPoint as IPEndPoint).Address.ToString(),
                    (this.tempClientSocket.RemoteEndPoint as IPEndPoint).Port.ToString())
                    );
            }
            catch (Exception e)
            {
                Log.Write("Server: 监听PLC连接时异常： " + e.Message);
            }

        }

        /// <summary>
        /// Server: 接收PLC数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Read(byte[] data)
        {
            int dataLen = 0;
            try
            {
                dataLen = this.tempClientSocket.Receive(data);
            }
            catch (Exception e)
            {
                Log.Write("Server: 接收PLC数据时异常： " + e.Message);
                return false;
            }

            if (dataLen <= 0)
            {
                return false;
            }


            return true;
        }

        /// <summary>
        /// Server: 向PLC发送数据
        /// </summary>
        /// <param name="data"></param>
        public void Write(byte[] data)
        {
            try
            {
                this.tempClientSocket.Send(data);
            }
            catch (Exception e)
            {
                Log.Write("Server: 给PLC发送数据时异常： " + e.Message);
            }
        }

        #region IDisposable 成员
        public void Dispose()
        {
            if (this.tempClientSocket != null)
            {
                this.tempClientSocket.Close();
                this.tempClientSocket = null;
            }

            if (this.listenSocket != null)
            {
                this.listenSocket.Close();
                this.listenSocket = null;
            }
        }
        #endregion
    }
}
