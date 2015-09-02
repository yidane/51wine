using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;

namespace Travel.Infrastructure.CommonFunctions
{
    public class QrCodeHelper
    {
        public static MemoryStream GetQrCodeImage(string text)
        {
            var rtnMemoryStream = new MemoryStream();
            var encoder = new QrEncoder(ErrorCorrectionLevel.L);
            QrCode qr = null;
            if (!encoder.TryEncode(text, out qr))
                throw new Exception("生成二维码失败");

            var render = new GraphicsRenderer(new FixedModuleSize(2, QuietZoneModules.Two));
            render.WriteToStream(qr.Matrix, ImageFormat.Png, rtnMemoryStream);

            return rtnMemoryStream;
        }
    }
}
