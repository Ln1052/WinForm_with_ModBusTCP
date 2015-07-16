using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MailPartition.Common
{
    public class ValueHelper
    {
        /// <summary>
        /// 大端byte[2]数组转化为UInt16值
        /// </summary>
        public static UInt16 ToUInt16(byte[] arr, int startPos)
        {
            return (UInt16)(((UInt16)arr[startPos] << 8) | (UInt16)(arr[startPos + 1]));
        }

        /// <summary>
        /// UInt16的值转化为大端byte[2]
        /// </summary>
        public static byte[] GetBytes(UInt16 value)
        {
            byte[] data = BitConverter.GetBytes(value);
            Array.Reverse(data);

            return data;
        }

        /// <summary>
        /// Byte值返回对应ASCII字符
        /// 48->0
        /// 65->A
        /// 97->a
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static char GetChar(byte value)
        {
            return Convert.ToChar(value);
        }

        /// <summary>
        /// ASCII字符数组转化为字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="startPos"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string GetString(byte[] value, int startPos, int count)
        {
            return System.Text.Encoding.ASCII.GetString(value, startPos, count);
        }

        /// <summary>
        /// 将四个ASCII字符转化为对应的整数值
        /// '1' '2' '3' '4' => 1234
        /// </summary>
        /// <param name="value"></param>
        /// <param name="startPos"></param>
        /// <returns></returns>
        public static UInt16 CharsToUInt16(byte[] value, int startPos, int number = 4)
        {
            try
            {
                if (number == 2)
                {
                    return (UInt16)(Byte.Parse(GetChar(value[startPos + 0]).ToString()) * (UInt16)10 +
                            Byte.Parse(GetChar(value[startPos + 1]).ToString()) * 1);
                }
                else
                {
                    return (UInt16)(Byte.Parse(GetChar(value[startPos + 0]).ToString()) * (UInt16)1000 +
                            Byte.Parse(GetChar(value[startPos + 1]).ToString()) * (UInt16)100 +
                            Byte.Parse(GetChar(value[startPos + 2]).ToString()) * (UInt16)10 +
                            Byte.Parse(GetChar(value[startPos + 3]).ToString()) * 1);
                }
            }
            catch (Exception e)
            {
                Log.Write("解析OBR数据流邮件ID（条码长度）时异常： " + e.Message);
                return (UInt16)10000;
            }
        }

        /// <summary>
        /// 左边机器默认加前缀1
        /// </summary>
        /// <param name="mailId"></param>
        /// <returns></returns>
        public static string LeftMailID(string mailId)
        {
            return string.Format("1{0:00000}", int.Parse(mailId));
        }

        /// <summary>
        /// 右边机器默认加前缀2
        /// </summary>
        /// <param name="mailId"></param>
        /// <returns></returns>
        public static string RightMailID(string mailId)
        {
            return string.Format("2{0:00000}", int.Parse(mailId));
        }
    }
}
