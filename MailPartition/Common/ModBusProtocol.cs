using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace MailPartition.Common
{
    public enum FunctionCode : byte
    {
        /// <summary>
        /// Read Multiple Registers
        /// </summary>
        Read = 3,

        /// <summary>
        /// Write Multiple Registers
        /// </summary>
        Write = 16
    }

    public enum OperationAction : byte
    {
        /// <summary>
        /// Error action, transfer left or right
        /// </summary>
        Error = 0,

        /// <summary>
        /// Normal action, tranfer directly
        /// </summary>
        Normal = 1,

    }

    public enum MarkupBit : byte
    {
        /// <summary>
        /// Error markup bit 0x00
        /// </summary>
        Error = 0,

        /// <summary>
        /// Normal markup bit 0x0A
        /// </summary>
        Normal = 10
    }

    /// <summary>
    /// PLC ModBus TCP发送数据
    /// </summary>
    internal class ModBusRequest
    {
        public ModBusRequestHeader ModBusReqHeader { get; set; }
        public ModBusRequestBody ModBusReqBody { get; set; }

        public ModBusRequest()
        {
            ModBusReqHeader = new ModBusRequestHeader();
            ModBusReqBody = new ModBusRequestBody();
        }

        public void ToBytes(byte[] arr, int startPos)
        {
            ModBusReqHeader.ToBytes(arr, startPos);
            ModBusReqBody.ToBytes(arr, startPos + ModBusRequestHeader.TotalLength);
        }
    }

    /// <summary>
    /// PLC ModBus TCP 回复数据
    /// </summary>
    internal class ModBusResponse
    {
        public ModBusResponseHeader ModBusResHeader { get; set; }
        public ModBusResponseBody ModBusResBody { get; set; }

        public ModBusResponse()
        {
            ModBusResHeader = new ModBusResponseHeader();
            ModBusResBody = new ModBusResponseBody();
        }

        public void ToBytes(byte[] arr, int startPos)
        {
            ModBusResHeader.ToBytes(arr, startPos);
            //ModBusResBody.ToBytes(arr, startPos + ModBusResponseHeader.TotalLength);
        }
    }

    internal class ModBusRequestHeader
    {
        public UInt16 TransId { get; set; } // transaction identifier
        public UInt16 ProtocolId { get; set; }  // defalut 0x0000 for ModBus protocol
        public UInt16 Length { get; set; } //后续字节数
        public Byte UnitId { get; set; }  // Unit Identifier, intra-system routing purpose
        public FunctionCode FuncCode { get; set; }  // Function code
        public UInt16 StartAddr { get; set; }  // start address of register
        public UInt16 RegisNum { get; set; }  // Register number
        public Byte DataLength { get; set; }  // 有效数据字节数

        public const int TotalLength = 13;   // MBAP 头总共13个字节，第14个字节开始是数据内容

        public ModBusRequestHeader()
        {
            this.TransId = 0;
            this.ProtocolId = 0;
            this.Length = 0;
            this.UnitId = 0;
            this.FuncCode = FunctionCode.Write;
            this.StartAddr = UInt16.Parse(ConfigurationManager.AppSettings["StartingAddress"]);
            this.RegisNum = UInt16.Parse(ConfigurationManager.AppSettings["RegisterNumber"]);
            this.DataLength = Byte.Parse(ConfigurationManager.AppSettings["DataLength"]);
        }

        // arr 是大端
        public ModBusRequestHeader(byte[] arr, int startPos):this()
        {
            if (arr.Length - startPos >= TotalLength)
            {
                this.TransId = ValueHelper.ToUInt16(arr, startPos);
                this.ProtocolId = ValueHelper.ToUInt16(arr, startPos + 2);
                this.Length = ValueHelper.ToUInt16(arr, startPos + 4);
                this.UnitId = arr[startPos + 6];
                this.FuncCode = (FunctionCode)arr[startPos + 7];
                this.StartAddr = ValueHelper.ToUInt16(arr, startPos + 8);
                this.RegisNum = ValueHelper.ToUInt16(arr, startPos + 10);
                this.DataLength = arr[startPos + 12];
            }
        }

        // arr 是大端
        public void ToBytes(byte[] arr, int startPos)
        {
            if (arr.Length - startPos >= TotalLength)
            {
                Array.Copy(ValueHelper.GetBytes(TransId), 0, arr, startPos, 2);
                Array.Copy(ValueHelper.GetBytes(ProtocolId), 0, arr, startPos + 2, 2);
                Array.Copy(ValueHelper.GetBytes(Length), 0, arr, startPos + 4, 2);
                arr[startPos + 6] = UnitId;
                arr[startPos + 7] = (byte)FuncCode;
                Array.Copy(ValueHelper.GetBytes(StartAddr), 0, arr, startPos + 8, 2);
                Array.Copy(ValueHelper.GetBytes(RegisNum), 0, arr, startPos + 10, 2);
                arr[startPos + 12] = DataLength;
            }
        }
    }

    /// <summary>
    /// 用于PC发送邮件处理决策数据给PLC
    /// </summary>
    internal class ModBusRequestBody
    {
        public UInt16 MailId { get; set; } // 对应邮件ID
        public OperationAction Ope { get; set; } //对应采取的操作
        public MarkupBit Mark { get; set; } //对应的标志位

        public const int TotalLength = 6; //总共有效数据字节数

        public ModBusRequestBody()
        {
            this.MailId = 0;
            this.Ope = OperationAction.Normal;
            this.Mark = MarkupBit.Normal;
        }

        public void ToBytes(byte[] arr, int startPos)
        {
            if (arr.Length - startPos >= TotalLength)
            {
                Array.Copy(ValueHelper.GetBytes(MailId), 0, arr, startPos + 0, 2);
                Array.Copy(ValueHelper.GetBytes((UInt16)Ope), 0, arr, startPos + 2, 2);
                Array.Copy(ValueHelper.GetBytes((UInt16)Mark), 0, arr, startPos + 4, 2);
            }
        }
    }

    /// <summary>
    /// 用于PC收到PLC数据时自动回复
    /// </summary>
    internal class ModBusResponseHeader
    {
        public UInt16 TransId { get; set; } // transaction identifier
        public UInt16 ProtocolId { get; set; }  // defalut 0x0000 for ModBus protocol
        public UInt16 Length { get; set; } //后续字节数 默认是0x0006
        public Byte UnitId { get; set; }  // Unit Identifier, intra-system routing purpose
        public FunctionCode FuncCode { get; set; }  // Function code 与request一致
        public UInt16 StartAddr { get; set; }  // start address of register
        public UInt16 RegisNum { get; set; }  // Register number

        public const int TotalLength = 12;   // MBAP response头总共12个字节，将request头中的前面12个字节自动回复确认

        public ModBusResponseHeader()
        {
            this.ProtocolId = 0;
            this.Length = 6;
            this.FuncCode = FunctionCode.Write;
        }

        public ModBusResponseHeader(ModBusRequestHeader modBusReqHeader): this()
        {
            this.TransId = modBusReqHeader.TransId;
            this.UnitId = modBusReqHeader.UnitId;
            this.StartAddr = modBusReqHeader.StartAddr;
            this.RegisNum = modBusReqHeader.RegisNum;
        }

        public void ToBytes(byte[] arr, int startPos)
        {
            if (arr.Length - startPos >= TotalLength)
            {
                Array.Copy(ValueHelper.GetBytes(TransId), 0, arr, startPos, 2);
                Array.Copy(ValueHelper.GetBytes(ProtocolId), 0, arr, startPos + 2, 2);
                Array.Copy(ValueHelper.GetBytes(Length), 0, arr, startPos + 4, 2);
                arr[startPos + 6] = UnitId;
                arr[startPos + 7] = (byte)FuncCode;
                Array.Copy(ValueHelper.GetBytes(StartAddr), 0, arr, startPos + 8, 2);
                Array.Copy(ValueHelper.GetBytes(RegisNum), 0, arr, startPos + 10, 2);
            }
        }
    }

    /// <summary>
    /// PC给PLC自动回复是数据内容为空
    /// </summary>
    internal class ModBusResponseBody
    {
        public void ToBytes(byte[] arr, int startPos)
        {
        }
    }
}
