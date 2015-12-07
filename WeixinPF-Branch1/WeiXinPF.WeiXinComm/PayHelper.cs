using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WeiXinPF.WeiXinComm
{
    public class PayHelper
    {
        private static bool? m_IsDebug = null;
        public static bool IsDebug
        {
            get
            {
                if (m_IsDebug == null)
                    m_IsDebug = string.Equals(System.Configuration.ConfigurationManager.AppSettings["RunMode"], "debug", StringComparison.CurrentCultureIgnoreCase);

                return m_IsDebug.Value;
            }
        }

        /// <summary>
        /// 生成支付链接
        /// </summary>
        /// <param name="payData"></param>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public static string GetPayUrl(string payData, string ticket)
        {
            var httpContext = System.Web.HttpContext.Current;
            if (httpContext == null)
                throw new Exception("方法必须在请求中执行");

            var port = WebHelper.GetHostPort();

            return string.Format(@"{0}weixin/wechatpay/wechatpay.aspx?payData={1}&ticket={2}", port, GetPayData(payData), ticket);
        }

        /// <summary>
        /// 生成支付链接
        /// </summary>
        /// <param name="payUrl"></param>
        /// <param name="payData"></param>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public static string GetPayUrl(string payUrl, string payData, string ticket)
        {
            return string.Format("{0}?payData={1}&ticket={2}", payUrl, GetPayData(payData), ticket);
        }

        /// <summary>
        /// 解决+号在url中取出来为空格的问题
        /// </summary>
        /// <param name="payData"></param>
        /// <returns></returns>
        private static string GetPayData(string payData)
        {
            return payData.Replace("+", "%2B");
            //return HttpUtility.HtmlEncode(payData);
        }
    }
}
