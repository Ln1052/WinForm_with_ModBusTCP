using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;

namespace MailPartition.Common
{
    public class Log
    {
        //到达100条log时自动写入日志文档
        private static int StartNum = 0;
        private const int EndNum = 10;

        private static readonly string LogFileDirectory = Directory.Exists(ConfigurationManager.AppSettings["LogDirectory"]) ? 
            ConfigurationManager.AppSettings["LogDirectory"] : 
            Directory.CreateDirectory(ConfigurationManager.AppSettings["LogDirectory"]).ToString();
        private static readonly string LogFilePath = LogFileDirectory + "\\" + ConfigurationManager.AppSettings["LogFileName"];
        private static  FileStream LogFileStream = new FileStream(LogFilePath,FileMode.Append, FileAccess.Write);
        public static  StreamWriter LogStreamWrite = new StreamWriter(LogFileStream);

        public static void Write(string log)
        {
            if (LogStreamWrite == null)
                return;

            try
            {
                lock (LogStreamWrite)
                {
                    LogStreamWrite.Write("-----------------------------------------------------------" +
                                          Environment.NewLine.ToString() +
                                          DateTime.Now.ToString() +
                                          Environment.NewLine.ToString() +
                                          log +
                                          Environment.NewLine.ToString()
                                          );
                    StartNum++;

                    //达到指定条数，缓存中输出
                    if (StartNum == EndNum)
                    {
                        StartNum = 0;
                        LogStreamWrite.Flush();
                        LogFileStream.Flush();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        #region Dispose
        public static void Dispose()
        {
            if (LogStreamWrite != null)
            {
                LogStreamWrite.Close();
            }

            if (LogFileStream != null)
            {
                LogFileStream.Close();
            }
        }
        #endregion
    }
}
