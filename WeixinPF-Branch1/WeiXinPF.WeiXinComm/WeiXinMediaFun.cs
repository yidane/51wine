using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Net;
using OneGulp.WeChat.MP;
using OneGulp.WeChat.MP.AdvancedAPIs;
using OneGulp.WeChat.MP.CommonAPIs;

namespace WeiXinPF.WeiXinComm
{
    public static partial class WeiXinMediaFun
    {



        /// <summary>
        /// 获取文件流
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public static void Get(string accessToken, string mediaId, Stream ms)
        {

            MediaApi.Get(accessToken, mediaId, ms);

        }

        /// <summary>
        /// 获取文件流，并转换成图片
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public static Image SaveImage(string accessToken, string mediaId)
        {
            Image result = null;


            var url = "http://file.api.weixin.qq.com/cgi-bin/media/get";
            var str = string.Format("{0}?access_token={1}&media_id={2}", url, accessToken, mediaId);
            var ms = GetStream(str);



            //using (FileStream fs = new FileStream(fileName, FileMode.Create))
            //{
            //    ms.Position = 0;
            //    byte[] buffer = new byte[1024];
            //    int bytesRead = 0;
            //    while ((bytesRead = ms.Read(buffer, 0, buffer.Length)) != 0)
            //    {
            //        fs.Write(buffer, 0, bytesRead);
            //    }
            //    fs.Flush();
            //}


            result = ms.ToImage();




            return result;
        }


        public static Stream GetStream(string url)
        {
            var request = WebRequest.Create(url);
            request.Method = "GET";
            return request.GetResponse().GetResponseStream();
        }


        private static Image ToImage(this Stream stream)
        {
            Image img = default(Image);
            if (stream != null)
            {
                img = Image.FromStream(stream);
            }
            return img;
        }
    }
}
