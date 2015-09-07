using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using Travel.Infrastructure.CommonFunctions.Ajax;
using Travel.Infrastructure.WeiXin.Common;

namespace Travel.Services.WebService
{
    public class MonsterService : BaseWebService
    {
        [WebMethod]
        public void DownLoadImage(string mediaId)
        {
            AjaxResult result = null;
            try
            {
                string accessToken = new Credential().AccessToken;

                if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(mediaId))
                {
                    throw new ArgumentNullException("参数不能为空！");
                }

                var helper = new HttpHelper("http://file.api.weixin.qq.com/cgi-bin/media/get");
                var image = helper.GetImage(new FormData()
                {
                    {"access_token", accessToken},
                    {"media_id", mediaId}
                });

                string imageName = Guid.NewGuid().ToString();
                string imagePath = GetImagePath(imageName);
                image.Save(WebHelper.MapPath(imagePath));

                result = AjaxResult.Success(new { imgName = imageName, imgUrl = WebHelper.MapUrl(imagePath) });
            }
            catch (Exception ex)
            {
                result = AjaxResult.Error(ex.Message);
            }

            HttpContext.Current.Response.Write(result);
        }

        [WebMethod]
        public void CropImage(int x, int y, int width, int height, string imgName)
        {
            AjaxResult result = null;
            try
            {
                ImageCut ic = new ImageCut(x, y, width, height);

                var cutImageName = Guid.NewGuid().ToString();
                string cutPath = WebHelper.MapPath(GetImagePath(cutImageName));

                Bitmap cuted = ic.KiCut(new Bitmap(Image.FromFile(WebHelper.MapPath(GetImagePath(imgName)))));

                cuted.Save(cutPath, ImageFormat.Jpeg);
                result = AjaxResult.Success(cutImageName);
            }
            catch (Exception ex)
            {
                result = AjaxResult.Error(ex.Message);
            }

            HttpContext.Current.Response.Write(result);
        }

        #region 私有方法
        private string GetImagePath(string imageName)
        {
            string folderName = "photo/uploadimages";
            if (!Directory.Exists(WebHelper.MapPath(folderName)))
            {
                Directory.CreateDirectory(WebHelper.MapPath(folderName));
            }

            return string.Format("{0}/{1}.jpg", folderName, imageName);
        }
        #endregion
    }

    public class ImageCut
    {
        /// <summary>
        /// 剪裁 -- 用GDI+
        /// </summary>
        /// <param name="b">原始Bitmap</param>
        /// <param name="StartX">开始坐标X</param>
        /// <param name="StartY">开始坐标Y</param>
        /// <param name="iWidth">宽度</param>
        /// <param name="iHeight">高度</param>
        /// <returns>剪裁后的Bitmap</returns>
        public Bitmap KiCut(Bitmap b)
        {
            if (b == null)
            {
                return null;
            }

            int w = b.Width;
            int h = b.Height;

            if (X >= w || Y >= h)
            {
                return null;
            }

            if (X + Width > w)
            {
                Width = w - X;
            }

            if (Y + Height > h)
            {
                Height = h - Y;
            }

            try
            {
                Bitmap bmpOut = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);

                Graphics g = Graphics.FromImage(bmpOut);
                g.DrawImage(b, new Rectangle(0, 0, Width, Height), new Rectangle(X, Y, Width, Height), GraphicsUnit.Pixel);
                g.Dispose();

                return bmpOut;
            }
            catch
            {
                return null;
            }
        }

        public int X = 0;
        public int Y = 0;
        public int Width = 120;
        public int Height = 120;
        public ImageCut(int x, int y, int width, int heigth)
        {
            X = x;
            Y = y;
            Width = width;
            Height = heigth;
        }
    }
}