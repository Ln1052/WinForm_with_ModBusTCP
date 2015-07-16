using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MailPartition.Common
{
    public class Flag
    {
        #region PLC连接状态PBox显示
        public static readonly Bitmap BmpGray = Properties.Resources.gray_32;
        public static readonly Bitmap BmpGreen = Properties.Resources.green_32;
        public static readonly Bitmap BmpRed = Properties.Resources.red_32;
        #endregion

        #region PLC socket连接状态
        public static bool PLCConnected = false;
        #endregion

        #region OBR socket连接状态
        public static bool OBRConnected = false;
        #endregion

        #region picturebox 等待图像显示信息
        #endregion
    }
}
