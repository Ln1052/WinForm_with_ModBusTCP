using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using MailPartition.Common;
using System.Threading;


namespace MailPartition.SocketConn
{
    internal class ModBusTCPIPWrapper : IDisposable
    {
        private static ModBusTCPIPWrapper _instance = null;

        private ModBusTCPIPWrapper()
        {
        }

        public static ModBusTCPIPWrapper Instance()
        {
            if(_instance == null)
            {
                _instance = new ModBusTCPIPWrapper();
            }

            return _instance;
        }

        private SocketClientWrapper socketClientWrapper = new SocketClientWrapper();

        private SocketServerWrapper socketServerWrapper = new SocketServerWrapper();

        /// <summary>
        /// 客户端连接PLC
        /// </summary>
        public void Connect()
        {
            this.socketClientWrapper.Connect();
        }

        /// <summary>
        /// 服务器监听PLC的连接
        /// </summary>
        public void Listen()
        {
            this.socketServerWrapper.Listen();

            ServerReceive();
        }

        /// <summary>
        /// 作为服务器接收PLC数据
        /// </summary>
        /// <returns></returns>
        public void ServerReceive()
        {
            while (null != this.socketServerWrapper.TempClientSocket)
            {
                //接收PLC客户端发送过来的消息并回复
                byte[] receiveBuf = new byte[256];
                if (this.socketServerWrapper.Read(receiveBuf))
                {
                    ModBusRequestHeader tempModBusReqHeader = new ModBusRequestHeader(receiveBuf, 0);
                    ModBusResponseHeader respHeader = new ModBusResponseHeader(tempModBusReqHeader);

                    //将接收到的数据追加到消息队列中
                    PLCData data = new PLCData(receiveBuf, ModBusRequestHeader.TotalLength);

                    lock (BasicData.PLCQueue)
                    {
                        BasicData.PLCQueue.Enqueue(data);
                    }

                    byte[] resp = new byte[ModBusResponseHeader.TotalLength];
                    respHeader.ToBytes(resp, 0);

                    //默认回复PLC ModBus protocol头
                    this.socketServerWrapper.Write(resp);
                }
                else //客户端终端连接
                {
                    Flag.PLCConnected = false;

                    this.socketServerWrapper.TempClientSocket.Close();

                    this.socketServerWrapper.TempClientSocket = null;

                    Log.Write("Server： PLC断开了连接.");
                }
            }

            //重新监听
            Listen();
        }

        /// <summary>
        /// 作为服务器回复PLC数据
        /// </summary>
        /// <returns></returns>
        public void ServerSend(byte[] data)
        {
        }

        /// <summary>
        /// 作为客户端给PLC发送数据
        /// </summary>
        /// <param name="data"></param>
        public void ClientSend(string mailId, OperationAction ope)
        {
            if (Flag.PLCConnected)
            {
                ModBusRequest tempModBusReqData = new ModBusRequest();
                tempModBusReqData.ModBusReqHeader.TransId = 1;  // 暂时不需要设置
                tempModBusReqData.ModBusReqHeader.StartAddr = UInt16.Parse(ConfigurationManager.AppSettings["StartingAddress"]); //60H
                tempModBusReqData.ModBusReqHeader.RegisNum = UInt16.Parse(ConfigurationManager.AppSettings["RegisterNumber"]);  //3
                tempModBusReqData.ModBusReqHeader.Length = (UInt16)(Byte.Parse(ConfigurationManager.AppSettings["DataLength"]) + 7); // 6+7
                tempModBusReqData.ModBusReqHeader.DataLength = Byte.Parse(ConfigurationManager.AppSettings["DataLength"]); //6

                tempModBusReqData.ModBusReqBody.MailId = UInt16.Parse(mailId == string.Empty ? "0" : mailId);
                tempModBusReqData.ModBusReqBody.Mark = MarkupBit.Normal;
                tempModBusReqData.ModBusReqBody.Ope = ope;

                int arrLen = tempModBusReqData.ModBusReqHeader.Length + ModBusRequestBody.TotalLength;  //6 = Transaction ID(2) + Protocol ID(2) + Length(2)
                byte[] data = new byte[arrLen];
                tempModBusReqData.ToBytes(data, 0);



                try
                {
                    this.socketClientWrapper.Write(data);

                    //读取PLC反馈信息，主要用于调试查看
                    byte[] responseHeader = this.socketClientWrapper.Read(20);
                }
                catch (Exception e)
                {
                    Log.Write("Client： 向PLC发送数据时异常： " + e.Message);
                }

            }
            else
            {
                Log.Write("Client： 与PLC连接断开，无法发送");

                MessageBox.Show("Client： 与PLC连接断开，无法发送");
            }

        }

        #region IDisposable 成员

        public void Dispose()
        {
            this.socketClientWrapper.Dispose();

            this.socketServerWrapper.Dispose();

        }
        #endregion
    }
}
