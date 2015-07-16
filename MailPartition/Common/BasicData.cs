using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MailPartition.Common
{
    public static class BasicData
    {
        public static Queue<PLCData> PLCQueue = new Queue<PLCData>();

        public static Queue<OBRData> OBRQueue = new Queue<OBRData>();
    }

    /// <summary>
    /// 表示PLC发送过来的数据
    /// </summary>
    public class PLCData
    {
        public UInt16 MailId { get; set; }  //邮件ID

        public UInt16 Volume { get; set; }  //体积（单位：立方厘米）

        public UInt16 Weight { get; set; }  //重量（单位：10g）

        public UInt16 MarkBit { get; set; } //标志位（保留）

        public const int TotalLength = 8;

        public PLCData()
        {
        }

        public PLCData(UInt16 mailId, UInt16 volume, UInt16 weight)
        {
            this.MailId = mailId;
            this.Volume = volume;
            this.Weight = weight;
        }

        /// <summary>
        /// 从PLC收到的数据字节中初始化实例
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="startPos"></param>
        public PLCData(Byte[] arr, int startPos)
        {
            if (arr.Length - startPos >= TotalLength)
            {
                this.MailId = ValueHelper.ToUInt16(arr, startPos + 0);
                this.Volume = ValueHelper.ToUInt16(arr, startPos + 2);
                this.Weight = ValueHelper.ToUInt16(arr, startPos + 4);
            }
        }
        
    }

    /// <summary>
    /// 表示OBR发送过来的数据
    /// </summary>
    public class OBRData
    {
        public UInt16 MailId { get; set; }  //邮件ID 用于验证和PLC数据对应

        public string BarCode { get; set; } //邮件条码

        //public Byte BarLength { get; set; }

        public OBRData()
        {
        }

        public OBRData(UInt16 mailId, string barCode)
        {
            this.MailId = mailId;
            this.BarCode = barCode;
        }
    }
}
