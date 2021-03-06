﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using OneGulp.WeChat.MP;
using OneGulp.WeChat.MP.AdvancedAPIs;
using OneGulp.WeChat.MP.CommonAPIs;
using WeiXinPF.Application.DomainModules.Photo;
using WeiXinPF.BLL;
using WeiXinPF.Common;
using WeiXinPF.WeiXinComm;

namespace WeiXinPF.WebService
{
    public class MonsterWebService : BaseWebService
    {
        /// <summary>
        /// 获取湖怪活动基本信息
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="wid"></param>
        [WebMethod]
        public void GetBaseInfo(int aid, int wid)
        {
            try
            {
                var service = new PhotoService();
                var info = service.GetModel(aid);
                var now = DateTime.Now;
                //获取状态
                var begin = DateTime.Parse(info.beginDate);
                var end = DateTime.Parse(info.endDate);
                if (begin > now)
                {
                    info.status_s = "nostart";
                }
                else if (end <= DateTime.Now)
                {
                    info.status_s = "end";
                }
                else
                {
                    info.status_s = "success";
                }
                Context.Response.Write(AjaxResult.Success(info));


            }
            catch (JsonException jsEx)
            {

                Context.Response.Write(AjaxResult.Error(jsEx.Message, jsEx.ErrorType));
            }
            catch (Exception ex)
            {
                var s = "";
                if (ex.InnerException != null)
                {
                    s = ex.InnerException.Message;
                }
                Context.Response.Write(AjaxResult.Error(s));
            }
        }

        /// <summary>
        /// 获取场景
        /// </summary>
        /// <param name="wid"></param>
        [WebMethod]
        public void GetScenics(int wid)
        {
            try
            {
                var scenicBll = new wx_travel_scenic();
                var detailBll = new wx_travel_scenicDetail();

                var scenic = scenicBll.GetModelList("wid=" + wid).FirstOrDefault();
                if (scenic != null)
                {
                    var scenics = detailBll.GetModelByScenicId(scenic.Id);

                    if (scenics.Any())
                    {
                        var dtos = scenics.Select(p => new
                        {
                            name = p.Name,
                            img =string.Format("url({0})", p.Cover) ,
                            url = string.Format("{0}/weixin/scenic/detail.aspx?id={1}",
                            MyCommFun.getWebSite(), p.Id)
                        });


                        Context.Response.Write(AjaxResult.Success(dtos));
                    }
                }





            }
            catch (JsonException jsEx)
            {

                Context.Response.Write(AjaxResult.Error(jsEx.Message, jsEx.ErrorType));
            }
            catch (Exception ex)
            {
                var s = "";
                if (ex.InnerException != null)
                {
                    s = ex.InnerException.Message;
                }
                Context.Response.Write(AjaxResult.Error(s));
            }
        }



        /// <summary>
        /// 下载微信上图片
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="wid"></param>
        [WebMethod]
        public void DownLoadImage(string mediaId, int wid)
        {
            AjaxResult result = null;
            try
            {
                string error;
                var accessToken = WeiXinPF.WeiXinComm.WeiXinCRMComm.getAccessToken(wid, out error);
                if (!string.IsNullOrEmpty(error))
                {
                    throw new Exception(error);
                }

                if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(mediaId))
                {
                    throw new ArgumentNullException("参数不能为空！");
                }


                string imageName = Guid.NewGuid().ToString();
                string imagePath = GetImagePath(imageName);
                string path = MyCommFun.GetRootPath() + imagePath;

               var image=  WeiXinMediaFun.SaveImage(accessToken, mediaId);

                var imgUrl = string.Format("{0}/{1}", MyCommFun.getWebSite(), imagePath);

                image.Save(path);

                result = AjaxResult.Success(new { imgName = imageName, imgUrl = imgUrl });
            }
            catch (Exception ex)
            {
                result = AjaxResult.Error(ex.Message);
            }

            HttpContext.Current.Response.Write(result);
        }



        /// <summary>
        /// 裁剪图片
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="imgName"></param>
        [WebMethod]
        public void CropImage(int x, int y, int width, int height, string imgName)
        {
            AjaxResult result = null;
            try
            {
                ImageCut ic = new ImageCut(x, y, width, height);

                var cutImageName = Guid.NewGuid().ToString();
                //string path = MyCommFun.GetRootPath() + GetImagePath(cutImageName);
                string cutPath = MyCommFun.GetRootPath() + GetImagePath(cutImageName);
                var sourcePath = MyCommFun.GetRootPath() + GetImagePath(imgName);
                Bitmap cuted = ic.KiCut(new Bitmap(Image.FromFile(sourcePath)));

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

            string folderName = "weixin/photo/uploadimages";
            string path = MyCommFun.GetRootPath() + folderName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
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
