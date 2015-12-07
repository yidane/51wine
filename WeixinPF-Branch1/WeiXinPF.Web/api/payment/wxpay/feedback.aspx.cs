using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.API.Payment.wxpay;
using WeiXinPF.BLL;
using System.Net;
using System.IO;

namespace WeiXinPF.Web.api.payment.wxpay
{
    /// <summary>
    ///  维权接口页面
    /// </summary>
    public partial class feedback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             
        }


        /// <summary>
        /// 申请消除投诉
        /// 如您已经跟客户达成一致，可申请消除用户投诉！
        /// 向微信发送撤销投诉申请
        /// </summary>
        /// <returns></returns>
        private void MessageToTx(string accessToken, string openId, string feedBackId)
        {
            string url = "https://api.weixin.qq.com/payfeedback/update?access_token={0}&openid={1}&feedbackid={2}";
            url = string.Format(url, accessToken, openId, feedBackId);
            HttpWebRequest webRequest2 = (HttpWebRequest)WebRequest.Create(url);
            webRequest2.ContentType = "text/html; charset=UTF-8";
            webRequest2.Method = "GET";
            webRequest2.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";
            HttpWebResponse httpWebResponse2 = (HttpWebResponse)webRequest2.GetResponse();
            StreamReader swRead = new StreamReader(httpWebResponse2.GetResponseStream(), System.Text.Encoding.GetEncoding("UTF-8"));
            Response.Write(swRead.ReadToEnd());
        }

    }
}