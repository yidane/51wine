using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using Newtonsoft.Json;

namespace Travel.Infrastructure.WeiXin.Common
{
    public class HttpHelper
    {
        public string Url { get; set; }

        public HttpHelper(string url)
        {
            Url = url;
        }

        /// <summary>
        /// 发送Get请求
        /// </summary>
        /// <param name="formData"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>(FormData formData)
        {
            var url = string.Format("{0}?{1}", Url, formData.Format());
            var ret = ReadFromResponse<T>(GetStream(url));
            return ret;
        }

        public Image GetImage(FormData formData)
        {
            var url = string.Format("{0}?{1}", Url, formData.Format());
            return ReadImageFromResponse(GetStream(url));
        }



        public T GetAnonymous<T>(FormData formData, T anonymous)
        {
            return Get<T>(formData);
        }

        public T Post<T>(string body, FormData formData = null)
        {
            var request = WebRequest.Create(string.Format("{0}?{1}", Url, formData == null ? "" : formData.Format()));
            request.Method = "POST";
            var sw = new StreamWriter(request.GetRequestStream());
            sw.WriteLine(body);
            sw.Flush();
            sw.Close();
            T ret;
            using (var rep = request.GetResponse())
            {
                ret = ReadFromResponse<T>(rep);
            }
            return ret;
        }

        private static T ReadFromResponse<T>(WebResponse rep)
        {
            T ret = default(T);
            var sm = rep.GetResponseStream();
            if (sm != null)
            {
                var sr = new StreamReader(sm);
                ret = JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
            }
            return ret;
        }

        private static T ReadFromResponse<T>(Stream stream)
        {
            T ret = default(T);
            if (stream != null)
            {
                var sr = new StreamReader(stream);
                ret = JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
            }
            return ret;
        }

        private static Image ReadImageFromResponse(Stream stream)
        {
            Image img = default(Image);
            if (stream != null)
            {
                img = Image.FromStream(stream);
            }
            return img;
        }

        /// <summary>
        /// 以GET方式获取指定url的响应流
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Stream GetStream(string url)
        {
            var request = WebRequest.Create(url);
            request.Method = "GET";
            return request.GetResponse().GetResponseStream();
        }
    }

    public class FormData : Dictionary<string, object>
    {
        /// <summary>
        /// 转换为http form格式字符串
        /// </summary>
        /// <returns></returns>
        public virtual string Format()
        {
            var lst = this.Select(e => string.Format("{0}={1}", e.Key, Uri.EscapeUriString(Convert.ToString(e.Value))));
            return string.Join("&", lst);
        }
    }
}
