using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using MailPartition.Common;

namespace MailPartition.Database
{
    //存入数据库中有关邮件处理的方式
    public enum ManualOperation : byte
    {
        /// <summary>
        /// 超时未处理邮件，自动处理
        /// </summary>
        No = 0,

        /// <summary>
        /// 手动点击按钮确定邮件处理方式
        /// </summary>
        Yes = 1,
    }

    /// <summary>
    /// 数据库操作相关类
    /// </summary>
    public class SqlHelper : IDisposable
    {
        /************************************************
         * 所用数据库及数据表基本信息
         * Database: mail_info
         * table: mail_ope_info
         * columns: id int auto increase primary key
         * ******** mail_id int not null 
         * ******** barcode varchar(15)
         * ******** ope tinyint
         * ******** time datetime
         * ******** volume int
         * ******** weight smallint
         * ******** manual_ope tinyint
        **************************************************/

        public const string InsertStr = "insert into mail_ope_info(mail_id,barcode,ope,time,volume,weight,manual_ope) values({0},'{1}',{2},'{3}',{4},{5},{6})";

        private static readonly string ConnStr = ConfigurationManager.AppSettings["DBConnStr"];

        private static SqlHelper _instance = null;

        private SqlHelper()
        {
        }

        public static SqlHelper Instance()
        {
            if (SqlHelper._instance == null)
                SqlHelper._instance = new SqlHelper();

            return SqlHelper._instance;
        }

        private MySqlConnection Conn = new MySqlConnection(ConnStr);

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        /// <returns></returns>
        public bool OpenConn()
        {
            if (Conn.State == System.Data.ConnectionState.Open)
            {
                return true;
            }
            else
            {
                try
                {
                    Conn.Open();

                    if (Conn.State == System.Data.ConnectionState.Open)
                        return true;
                    else
                        return false;
                }
                catch(Exception e)
                {
                    Log.Write("打开数据库连接时异常： " + e.Message);
                    return false;
                }
            }
        }

        public void CloseConn()
        {
            try
            {
                if (Conn.State == System.Data.ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
            catch (Exception e)
            {
                Log.Write("关闭数据库连接时异常： " + e.Message);
            }
        }

        /// <summary>
        /// 对数据库执行制定操作
        /// </summary>
        /// <param name="cmdTxt"></param>
        public void ExecNonQuery(string cmdTxt)
        {
            //建立连接
            if(!OpenConn())
                return;
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(cmdTxt, Conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandTimeout = 10;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Log.Write("执行数据库操作异常： " + e.Message);
            }
        }

        /// <summary>
        /// 获取一个数据集
        /// </summary>
        /// <param name="cmdTxt"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string cmdTxt)
        {
            //建立连接
            if (!OpenConn())
                return null;

            DataSet ds = new DataSet();

            try
            {
                using (MySqlDataAdapter mda = new MySqlDataAdapter(cmdTxt, Conn))
                {
                    mda.Fill(ds);
                }
            }
            catch (Exception e)
            {
                Log.Write("获取数据集时异常： " + e.Message);
                return null;
            }

            return ds;
        }

        /// <summary>
        /// 获取一个数据表
        /// </summary>
        /// <param name="cmdTxt"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string cmdTxt)
        {
            DataSet ds = GetDataSet(cmdTxt);

            if (ds == null || ds.Tables.Count < 1)
            {
                return null;
            }

            return ds.Tables[0] as DataTable;
        }

        #region IDisposable 成员

        public void Dispose()
        {
            CloseConn();
        }

        #endregion
    }
}
