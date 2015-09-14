using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;

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
        public static void Get(string accessToken, string mediaId, MemoryStream ms)
        {

            MediaApi.Get(accessToken, mediaId, ms);
             
        }

        /// <summary>
        /// 获取文件流，并转换成图片
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public static  void SaveImage(string accessToken, string mediaId,string fileName)
        {
          //  Image result = null;

            using (MemoryStream ms = new MemoryStream())
            {
                Get(accessToken, mediaId, ms);


                
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    ms.Position = 0;
                    byte[] buffer = new byte[1024];
                    int bytesRead = 0;
                    while ((bytesRead = ms.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        fs.Write(buffer, 0, bytesRead);
                    }
                    fs.Flush();
                }


             //   result = ms.ToImage();
            }


           

           // return result;
        }



        private static Image ToImage(this MemoryStream stream)
        {
            Image img = default(Image);
            if (stream != null )
            {
                img = Image.FromStream(stream);
            }
            return img;
        }
    }
}
