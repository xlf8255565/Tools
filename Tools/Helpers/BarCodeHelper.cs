using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ZXing;
using ZXing.Common;

namespace QrCodeDecode.Helpers
{
    /// <summary>
    /// 一维码和二维码创建类
    /// </summary>
    public class BarCodeHelper
    {
        public static Bitmap CreateBarCode(int height, int width, string text)
        {
            var wr = new BarcodeWriter
            {
                Options = new EncodingOptions
                {
                    Height = height,
                    Width = width,
                    //PureBarcode = true,
                },
                Format = BarcodeFormat.CODE_128
            };
            return new Bitmap(wr.Write(text), new Size(width, height));
        }

        public static Bitmap CreateQrCode(int height, int width, string text)
        {
            var wr = new BarcodeWriter
            {
                Options = new EncodingOptions
                {
                    Height = height,
                    Width = width,
                },
                Format = BarcodeFormat.QR_CODE
            };
            return new Bitmap(wr.Write(text), new Size(width, height));
        }

        public static string GetQrCodeText(string imageFile)
        {
            var reader = new BarcodeReader { Options = { CharacterSet = "UTF-8" } };
            using (var bmp = new Bitmap(imageFile))
            {
                var result = reader.Decode(bmp);
                return result.Text;
            }
        }
    }
}
