using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.weixin.WeChatPay
{
    public partial class MyOrderCenter : WeiXinPage
    {
        public string OpenID = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
#if  DEBUG
            openid = "obzTsw5qxlbwGYYZJC9b-91J-X1Y";
#else
            var thisUrl = MyCommFun.getWebSite() + "/weixin/WeChatPay/MyOrderCenter.aspx" + Request.Url.Query;
            var widInt = MyCommFun.RequestWid();
            var bll = new BLL.wx_userweixin();
            var uWeiXinModel = bll.GetModel(widInt);
            if (uWeiXinModel == null)
                throw new Exception("不合法的参数wid");
            OAuth2BaseProc(uWeiXinModel, "MyOrderCenter", thisUrl);
#endif
        }
    }
}