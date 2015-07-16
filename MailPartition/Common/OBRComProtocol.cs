using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MailPartition.Common
{
    internal class OBRComProtocol
    {
        //用于表示特殊的 multi-barcode 长度
        //public const Byte[] MultiBarcode = {0x39, 0x39}; //multi barcode identifier (39H: 9)
        public const Byte STX = 0x02;   //start of text(STX)
        public const Byte TelegramType = 0x43; //telegram type (43H : Upper C)
        public const Byte ErrorTelegramType = 0x46; // error telegram type(46H : Upper F)
        //public static readonly Byte[] IndexNum = { 0x00, 0x00, 0x02, 0x05 }; //index number 00 25
        public static readonly Byte[] Separator = { 0x2C, 0x23 }; // separator for Header and barcode(2CH: ','  23H: '#')
        public const Byte ETX = 0x03;  //end of text(ETX)
        public const UInt16 ErrorMailID = 10000; //Error mailID
        public const Byte LeastBytes = 12; // The least bytes of stream from OBR(no barcode length)

        public UInt16 MailId { get; set; }
        public List<UInt16> BarCodeLengths { get; set; }
        public List<String> BarCodes { get; set; }

        public OBRComProtocol()
        {
            this.MailId = ErrorMailID;
            this.BarCodeLengths = new List<UInt16>();
            this.BarCodes = new List<string>();
        }

        //根据接收到的字节数组实例化对象
        public OBRComProtocol(Byte[] value, int startPos)
            : this()
        {
            int tempIndex = startPos;
            try
            {
                //小于最少字节数，则没有条码信息
                if (value.Length <= LeastBytes)
                    return;

                //匹配文件头
                if (value[tempIndex ++] != STX)
                    return;

                //匹配telegram type
                if (value[tempIndex ++] != TelegramType)
                    return;

                //取邮件ID（index number）
                this.MailId = ValueHelper.CharsToUInt16(value, tempIndex);
                tempIndex += 4;

                //匹配条码个数及每个的长度，同时添加到BarCodeLengths数列中
                int barLengthIndex = tempIndex;
                while (!((value[tempIndex] == Separator[0]) &&
                    (value[tempIndex+1] == Separator[1])))
                {
                    BarCodeLengths.Add(ValueHelper.CharsToUInt16(value, barLengthIndex, 2));
                    barLengthIndex += 2;
                    tempIndex += 2;
                }
                //跳过分隔符',' '#'
                tempIndex += 2;

                //根据条码长度，取每个条码，并添加到BarCodes数列中
                for (int barIndex = 0; barIndex < BarCodeLengths.Count; barIndex++)
                {
                    //排除长度为0的条码
                    if(BarCodeLengths[barIndex] > 0)
                    {
                        string str = ValueHelper.GetString(value, tempIndex, BarCodeLengths[barIndex]);
                        BarCodes.Add(str);
                        tempIndex += BarCodeLengths[barIndex];
                    }
                }

                //匹配文件尾
                if(value[tempIndex++] != ETX)
                {
                    //尾部未匹配
                }

                //CheckSum
                if(!CheckSum(value))
                {
                    //Check sum异常
                }

            }catch(Exception)
            {
            }

        }

        public static bool CheckSum(Byte[] value)
        {
            return true;
        }
    }
}
