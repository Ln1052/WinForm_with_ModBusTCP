using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace MailPartition
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region 同一时间该程序只能启动一次

            bool createdNew;
            using(System.Threading.Mutex instance = new System.Threading.Mutex(true,"MutexName",out createdNew))
            {
                //表明首次运行
                if(createdNew)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FormMailPartition());
                }
                else
                {
                    //已经运行了该程序
                    MessageBox.Show("程序已经在运行……","运行提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    Application.Exit();
                }
            }
            
            #endregion
        }
    }
}
