using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MailPartition.SocketConn;
using MailPartition.Common;
using System.Threading;
using System.Configuration;
using MailPartition.Database;
using MailPartition.ChildrenForms;

namespace MailPartition
{
    public partial class FormMailPartition : Form
    {
        //用于服务端线程去更新UI文本框相关邮件数据
        public delegate void receiveUpdateUI(UInt16 mailId, UInt16 volume, UInt16 weight);
        receiveUpdateUI UpdateUIFunction = null;

        //用于其他线程更新UI文本框条码数据
        public delegate void barcodeUpdateUI(string barcode);
        barcodeUpdateUI UpdateBarcodeFunction = null;

        //用于其他线程更新Timer
        public delegate void UpdataTimer();
        UpdataTimer UpdateTimerFunction = null;

        //用于其他线程更新UI图片信息
        public delegate void UpdatePic(string filePath);
        UpdatePic UpdatePicFunction = null;

        private ModBusTCPIPWrapper wrapper = null;
        private OBRClientWrapper obrWrapper = null;
        private SqlHelper sqlHandler = null;

        //更新UI文本框邮件数据所用的同步参数
        public static bool needUpdate = true;

        #region 窗体组件基本操作
        /// <summary>
        /// 窗体初始化
        /// </summary>
        public FormMailPartition()
        {
            InitializeComponent();
            wrapper = ModBusTCPIPWrapper.Instance();
            obrWrapper = new OBRClientWrapper();
            sqlHandler = SqlHelper.Instance();
            
            //委托实例化
            UpdateUIFunction = new receiveUpdateUI(UpdateUIAction);
            UpdateBarcodeFunction = new barcodeUpdateUI(UpdateBarcodeAction);
            UpdateTimerFunction = new UpdataTimer(UpdateTimerAction);
            UpdatePicFunction = new UpdatePic(UpdatePicAction);

            //PLC IP 文本框及状态更新
            this.textBoxPLCIP.Text = ConfigurationManager.AppSettings["PLCIP"];
            this.pictureBoxPLCConn.Image = Flag.BmpGray;
            //OBR IP 文本框及状态更新
            this.textBoxOBRIP.Text = ConfigurationManager.AppSettings["OBRIP"];
            this.pictureBoxOBRConn.Image = Flag.BmpGray;

            //连接状态更新计时器开启
            this.timerStatus.Start();
            
            //开启服务端监听即接收线程和客户端发送线程
            StartListenThread();
            StartConnectPLCThread();
            StartConnectOBRThread();

            //开启更新UI文本框线程
            UpdateUIThread();
        }

        
        private void FormMailPartition_FormClosing(object sender, FormClosingEventArgs e)
        {
            wrapper.Dispose();

            Log.Dispose();
        }

        private void buttonNormal_Click(object sender, EventArgs e)
        {
            OperationHandler(OperationAction.Normal, ManualOperation.Yes);
        }

        /// <summary>
        /// 邮件处理逻辑
        /// </summary>
        /// <param name="ope"></param>
        /// <param name="manual"></param>
        private void OperationHandler(OperationAction ope, ManualOperation manual)
        {
            if (this.textBoxID.Text.Trim() == string.Empty)
            {
                return;
            }

            this.timerNext.Stop();

            needUpdate = true;

            //发送处理结果给PLC
            wrapper.ClientSend(this.textBoxID.Text, ope);

            //实时将处理数据更新到数据库中
            ExecNonQuery(ope, manual);

            //图像清除
            this.pictureBoxPic.Image = null;
            //旋转按钮隐藏
            this.buttonRotateLeft.Visible = false;
            this.buttonRotateRight.Visible = false;

            //UI 邮件信息清空
            this.textBoxID.Text = string.Empty;
            this.textBoxVolume.Text = string.Empty;
            this.textBoxWeight.Text = string.Empty;
            this.textBoxBar.Text = string.Empty;
        }

        private void buttonError_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定对该邮件做异常处理？", "异常确认", MessageBoxButtons.OKCancel , MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                OperationHandler(OperationAction.Error, ManualOperation.Yes);
            }
        }

        /// <summary>
        /// 相关设置方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormSetting set = new FormSetting())
            {
                if (set.ShowDialog() == DialogResult.OK)
                {
                    this.timerNext.Interval = set.IntervalTime * 1000;//将秒转为毫秒
                }
            }
            //add for test
            //MessageBox.Show(this.timerNext.Interval.ToString());
        }

        /// <summary>
        /// 版本信息提示方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string versionMessage = "  [软件版本]  " + Application.ProductVersion +
                                    Environment.NewLine +
                                    "  [发行日期]  " + ConfigurationManager.AppSettings["IssuedDate"] +
                                    Environment.NewLine +
                                    "  [软件作者]  " + ConfigurationManager.AppSettings["Developer"] +
                                    Environment.NewLine +
                                    "  " + ConfigurationManager.AppSettings["Company"];

            MessageBox.Show(versionMessage, "关于本软件");

        }

        /// <summary>
        /// help提示方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string helpMessage = "  快捷键设置： " + Environment.NewLine + 
                                 "  F1: 帮助信息" + Environment.NewLine +
                                 "  F5: 正常确认          F6: 异常确认" + Environment.NewLine +
                                 "  F9: 图像左旋转90°  F10： 图像右旋转90°" + Environment.NewLine + 
                                 Environment.NewLine +
                                 "  当前邮件默认处理等待间隔时间（毫秒）： " + this.timerNext.Interval + 
                                 Environment.NewLine + Environment.NewLine + 
                                 "  图像存放目录： " + ConfigurationManager.AppSettings["PicDirectory"] +
                                 Environment.NewLine +
                                 "  图像备份目录： " + ConfigurationManager.AppSettings["PicDirectoryBackup"] +
                                 Environment.NewLine +
                                 "  运行日志文件： " + ConfigurationManager.AppSettings["LogDirectory"] + "\\" +
                                 ConfigurationManager.AppSettings["LogFileName"] +
                                 Environment.NewLine;

            MessageBox.Show(helpMessage, "帮助信息");
        }

        /// <summary>
        /// 为控件按钮识别快捷键
        /// F5 : 正常确认
        /// F6 ：异常确认
        /// F10：图片左转90°
        /// F11：图片右转90°
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMailPartition_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    {
                        buttonNormal_Click(this, EventArgs.Empty);
                        break;
                    }
                case Keys.F6:
                    {
                        buttonError_Click(this, EventArgs.Empty);
                        break;
                    }
                case Keys.F9:
                    {
                        buttonRotateLeft_Click(this, EventArgs.Empty);
                        break;
                    }
                case Keys.F10:
                    {
                        buttonRotateRight_Click(this, EventArgs.Empty);
                        break;
                    }
                default:
                    break;
            }
        }

        #region 图像左右旋转操作
        private void buttonRotateLeft_Click(object sender, EventArgs e)
        {
            if (this.pictureBoxPic.Image != null)
            {
                Image img = this.pictureBoxPic.Image;
                img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                this.pictureBoxPic.Image = img;
            }

            //add for test
            //if (BasicData.OBRQueue.Count > 0)
            //{
            //    OBRData obrData = new OBRData();
            //    lock(BasicData.OBRQueue)
            //    {
            //        obrData = BasicData.OBRQueue.Dequeue();
            //    }
            //    this.textBoxID.Text = obrData.MailId.ToString();
            //    this.textBoxBar.Text = obrData.BarCode;
            //}
            //////////////////////////////////////////////////////////////

            //add for test
            //实时将处理数据更新到数据库中
            //ExecNonQuery(OperationAction.Error, ManualOperation.Yes);

        }

        private void buttonRotateRight_Click(object sender, EventArgs e)
        {
            if (this.pictureBoxPic.Image != null)
            {
                Image img = this.pictureBoxPic.Image;
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                this.pictureBoxPic.Image = img;
            }

            //add for test
            //实时将处理数据更新到数据库中
            //ExecNonQuery(OperationAction.Normal, ManualOperation.No);
        }

        #endregion

        #endregion

        #region 服务端监听PLC连接并接收数据线程
        /// <summary>
        /// 启动服务器监听并实时接收PLC数据
        /// </summary>
        private void StartListenThread()
        {
            Thread listenThread = new Thread(new ThreadStart(wrapper.Listen));
            listenThread.IsBackground = true;
            listenThread.Name = "Server Listening";
            listenThread.Start();

            Application.DoEvents();
        }

        #endregion 

        #region 客户端建立与PLC连接线程
        /// <summary>
        /// 请求建立与PLC连接，以便根据需要向PLC发送数据
        /// </summary>
        private void StartConnectPLCThread()
        {
            Thread clientThread = new Thread(new ThreadStart(wrapper.Connect));
            clientThread.IsBackground = true;
            clientThread.Name = "Client connect to PLC";
            clientThread.Start();

            Application.DoEvents();
        }
        #endregion

        #region 客户端建立与OBR连接线程
        /// <summary>
        /// 请求建立与OBR连接，同时接受OBR发送过来的条码数据
        /// </summary>
        private void StartConnectOBRThread()
        {
            Thread clientThread = new Thread(new ThreadStart(obrWrapper.Connect));
            clientThread.IsBackground = true;
            clientThread.Name = "Client connect to OBR";
            clientThread.Start();

            Application.DoEvents();
        }
        #endregion

        #region UI 文本框记录邮件数据线程

        /// <summary>
        /// UI文本框邮件数据更新线程
        /// </summary>
        private void UpdateUIThread()
        {
            Thread updateUIThread = new Thread(new ThreadStart(UpdateUI));
            updateUIThread.IsBackground = true;
            updateUIThread.Name = "UI updating";
            updateUIThread.Start();

            Application.DoEvents();
        }

 
        /// <summary>
        /// 更新UI文本框邮件数据处理逻辑
        /// </summary>
        private void UpdateUI()
        {
            while(true)
            {
                if (needUpdate)
                {
                    if (BasicData.PLCQueue.Count > 0)
                    {
                        PLCData data = new PLCData();
                        lock (BasicData.PLCQueue)
                        {
                            data = BasicData.PLCQueue.Dequeue();
                        }

                        if (this.InvokeRequired)
                        {
                            //更新UI数据
                            this.Invoke(UpdateUIFunction, new object[] { data.MailId, data.Volume, data.Weight });

                            //开启计时器
                            this.Invoke(UpdateTimerFunction);

                        }
                        else
                        {
                            UpdateUIAction(data.MailId, data.Volume, data.Weight);

                            UpdateTimerAction();
                        }



                        needUpdate = false;

                        //查找对应的图像及条码信息并显示出来
                        bool findPic = false;
                        while (!findPic && !needUpdate)
                        {
                            //查找条码
                            if (BasicData.OBRQueue.Count > 0)
                            {
                                //OBRData obrData = new OBRData();
                                OBRData obrData = BasicData.OBRQueue.Peek();

                                //匹配Mail ID
                                // 当OBR中含有PLC未传出来的邮件ID
                                while (obrData.MailId < data.MailId && BasicData.OBRQueue.Count > 0)
                                {
                                    lock (BasicData.OBRQueue)
                                    {
                                        BasicData.OBRQueue.Dequeue();
                                    }
                                    obrData = BasicData.OBRQueue.Peek();
                                }
                                // 当ID等于PLC数据ID
                                if (obrData.MailId == data.MailId)
                                {
                                    lock (BasicData.OBRQueue)
                                    {
                                        BasicData.OBRQueue.Dequeue();
                                    }

                                    if (this.InvokeRequired)
                                    {
                                        //更新UI数据
                                        this.Invoke(UpdateBarcodeFunction, new object[] { obrData.BarCode });

                                    }
                                    else
                                    {
                                        UpdateBarcodeAction(obrData.BarCode);
                                    }
                                }

                                
                            }

                            //查找图像
                            string fileName = data.MailId.ToString() + ".jpg";
                            string filePath = ConfigurationManager.AppSettings["PicDirectory"] + fileName; //d:\MailPic\1.jpg
                            if (System.IO.File.Exists(filePath))
                            {
                                if (this.InvokeRequired)
                                {
                                    this.Invoke(UpdatePicFunction, new object[] { filePath });
                                }
                                else
                                {
                                    UpdatePicAction(filePath);
                                }

                                findPic = true;
                            }
                        }


                        //add for test
                        //Thread.Sleep(10);

                        //wrapper.ClientSend(data.MailId.ToString(), OperationAction.Normal);

                        //needUpdate = true;
                        //
                    }
                    
                }
            }
        }
        
        private void UpdateUIAction(UInt16 mailId, UInt16 volume, UInt16 weight)
        {
            this.textBoxID.Text = mailId.ToString();
            this.textBoxVolume.Text = volume.ToString();
            this.textBoxWeight.Text = weight.ToString();
        }

        private void UpdateBarcodeAction(string barcode)
        {
            this.textBoxBar.Text = barcode.Trim();
        }

        #endregion

        #region 计时器处理图像逻辑
        /// <summary>
        /// 计时器时间到了，采取相应措施
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerNext_Tick(object sender, EventArgs e)
        {
            //默认采取正常非手动处理
            OperationHandler(OperationAction.Normal, ManualOperation.No);            
        }

        /// <summary>
        /// 开启图像更新计时器
        /// </summary>
        private void UpdateTimerAction()
        {
            this.timerNext.Start();
        }

        #endregion

        #region 更新图像
        private void UpdatePicAction(string filePath)
        {
            this.pictureBoxPic.ImageLocation = filePath;
            this.pictureBoxPic.Load();

            //显示旋转按钮
            this.buttonRotateLeft.Visible = true;
            this.buttonRotateRight.Visible = true;
        }

        #endregion

        #region 更新PLC、OBR连接状态
        /// <summary>
        /// 更新PLC连接状态
        /// </summary>
        /// <param name="connected"></param>
        private void UpdatePLCConnAction(bool connected)
        {
            if (connected)
            {
                this.pictureBoxPLCConn.Image = Flag.BmpGreen;
            }
            else
            {
                this.pictureBoxPLCConn.Image = Flag.BmpRed;
            }
        }

        /// <summary>
        /// 更新OBR连接状态
        /// </summary>
        /// <param name="connected"></param>
        private void UPdateOBRConnAction(bool connected)
        {
            if (connected)
            {
                this.pictureBoxOBRConn.Image = Flag.BmpGreen;
            }
            else
            {
                this.pictureBoxOBRConn.Image = Flag.BmpRed;
            }
        }

        /// <summary>
        /// 更新PLC OBR连接状态计数器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerStatus_Tick(object sender, EventArgs e)
        {
            UpdatePLCConnAction(Flag.PLCConnected);

            UPdateOBRConnAction(Flag.OBRConnected);
        }

        #endregion

        #region 数据库操作

        private void ExecNonQuery(OperationAction ope, ManualOperation manual)
        {
            sqlHandler.ExecNonQuery(string.Format(SqlHelper.InsertStr,
                ValueHelper.LeftMailID(this.textBoxID.Text == string.Empty ? "0" : this.textBoxID.Text),
                this.textBoxBar.Text == string.Empty ? "000000000000" : this.textBoxBar.Text,
                (Byte)ope,
                DateTime.Now.ToString(),
                this.textBoxVolume.Text == string.Empty ? "0" : this.textBoxVolume.Text,
                this.textBoxWeight.Text == string.Empty ? "0" : this.textBoxWeight.Text,
                (Byte)manual)
                );
        }

        #endregion
    }
}
