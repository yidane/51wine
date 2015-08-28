using System;
using System.Security.Cryptography;
using System.Text;
using Travel.Infrastructure.CommonFunctions;

namespace Travel.Infrastructure.OTAWebService
{
    public class OTAConfigManager
    {
        public static string ServiceUrl
        {
            get
            {
                //return WebConfigureHelper.Appsettings.OTAConfigSetting.OTAServiceUrl;
                return "http://223.223.179.17:8090/service.asmx";
            }
        }

        public static string MerchantCode
        {
            get
            {
                return "weixin.qq.com";
            }
        }

        public static string MerchantKey
        {
            get { return "25ED9785746E41E9A832A2AFFDF6BF54"; }
        }

        public static string ParkCode
        {
            //get { return "BERJPARK"; }
            get { return "gspf"; }
        }

        /// <summary>
        /// 获取OTA签名信息
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string GetSignature(params object[] args)
        {
            var signBeforeStringBuilder = new StringBuilder();
            foreach (var t in args)
                signBeforeStringBuilder.Append(t);

            var signBefore = signBeforeStringBuilder.ToString();
            var md5 = MD5.Create();
            var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(signBefore));
            var sb = new StringBuilder();
            foreach (var b in bs)
            {
                sb.Append(b.ToString("x2"));
            }

            var base64 = Encoding.UTF8.GetBytes(sb.ToString().ToUpper());
            return Convert.ToBase64String(base64);
        }
    }
}
