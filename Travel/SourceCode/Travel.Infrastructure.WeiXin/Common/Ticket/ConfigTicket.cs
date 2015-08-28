using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Travel.Infrastructure.WeiXin.Common.Ticket
{
    public class ConfigTicket : Ticket
    {
        protected override string type { get { return "jsapi"; } }

        public ConfigTicket(Credential credential)
            : base(credential)
        {
        }

        /// <summary>
        /// 生成signature
        /// </summary>
        /// <returns></returns>
        public string CreateSignature(string jsapi_ticket, string noncestr, string timestamp, string url)
        {
            if (string.IsNullOrEmpty(jsapi_ticket))
                throw new ArgumentNullException("jsapi_ticket", "jsapi_ticket不能为空");
            if (string.IsNullOrEmpty(jsapi_ticket))
                throw new ArgumentNullException("noncestr", "noncestr不能为空");
            if (string.IsNullOrEmpty(jsapi_ticket))
                throw new ArgumentNullException("timestamp", "timestamp不能为空");
            if (string.IsNullOrEmpty(jsapi_ticket))
                throw new ArgumentNullException("url", "url不能为空");

            if (url.Trim().IndexOf('#') > 0)
                url = url.Trim().Split('#')[0];

            var sha1BeforeString = string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}", jsapi_ticket, noncestr, timestamp, url);

            return SHA1(sha1BeforeString);
        }
    }
}
