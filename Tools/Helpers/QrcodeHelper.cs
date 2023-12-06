using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;

namespace Tools.Helpers
{
    public class QrcodeHelper
    {
        /// <summary>
        /// 生成二维码：根据传进去的数据 生成二维码
        /// </summary>
        /// <param name="data">用于生成二维码的数据</param>
        /// <returns></returns>
        public static System.Drawing.Image GenerateQRCode(String data)
        {
            //创建编码器，设置编码格式。Byte格式的编码，只要能转成Byte的数据，都可以进行编码，比如中文。NUMERIC 只能编码数字。
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;

            //大小，值越大生成的二维码图片像素越高
            qrCodeEncoder.QRCodeScale = 5;

            //版本,设置为0主要是防止编码的字符串太长时发生错误
            qrCodeEncoder.QRCodeVersion = 0;

            //生成二维码 Bitmap
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;//错误效验、错误更正(有4个等级)
            qrCodeEncoder.QRCodeBackgroundColor = System.Drawing.Color.White;//背景色
            qrCodeEncoder.QRCodeForegroundColor = System.Drawing.Color.Black;//前景色
            var pbImg = qrCodeEncoder.Encode(data, System.Text.Encoding.UTF8);
            return pbImg;
        }

        public static string ReadQrCode(Bitmap bitMap)
        {
            return new QRCodeDecoder().decode(new QRCodeBitmapImage(bitMap), System.Text.Encoding.UTF8);
        }
    }
}
