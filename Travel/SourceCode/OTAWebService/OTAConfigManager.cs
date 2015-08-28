using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace OTAWebService
{
    public class OTAConfigManager
    {
        public static string MerchantCode
        {
            get { return "weixin.qq.com"; }
        }

        public static string MerchantKey
        {
            get { return "25ED9785746E41E9A832A2AFFDF6BF54"; }
        }

        /// <summary>
        /// 获取OTA签名信息
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string GetSignature(params string[] args)
        {
            var signBeforeStringBuilder = new StringBuilder();
            signBeforeStringBuilder.Append(string.Format("{0}{1}", MerchantCode, MerchantKey));
            for (int len = 0; len < args.Length - 2; len++)
            {
                signBeforeStringBuilder.Append("{" + len + "}");
            }

            var signBefore = string.Format(signBeforeStringBuilder.ToString(), args);
            var md5 = MD5.Create();
            var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(signBefore));
            var sb = new StringBuilder();
            foreach (var b in bs)
            {
                sb.Append(b.ToString("x2"));
            }

            var base64 = Encoding.UTF8.GetBytes(sb.ToString());
            return Convert.ToBase64String(base64);
        }
    }
}
