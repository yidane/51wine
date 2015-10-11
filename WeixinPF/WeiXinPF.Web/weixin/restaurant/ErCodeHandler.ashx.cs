using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;

namespace WeiXinPF.Web.weixin.restaurant
{
    /// <summary>
    /// ErCodeHandler 的摘要说明
    /// </summary>
    public class ErCodeHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var key = HttpContext.Current.Request.QueryString["key"];
            if (!string.IsNullOrEmpty(key))
            {
                var ercode = GetQrCodeImage(key);

                HttpContext.Current.Response.OutputStream.Write(ercode.GetBuffer(), 0, (int)ercode.Length);
            }
        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static MemoryStream GetQrCodeImage(string text)
        {
            var rtnMemoryStream = new MemoryStream();
            var encoder = new QrEncoder(ErrorCorrectionLevel.L);
            QrCode qr = null;
            if (!encoder.TryEncode(text, out qr))
                throw new Exception("生成二维码失败");

            var render = new GraphicsRenderer(new FixedModuleSize(8, QuietZoneModules.Zero));
            render.WriteToStream(qr.Matrix, ImageFormat.Png, rtnMemoryStream);

            return rtnMemoryStream;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}