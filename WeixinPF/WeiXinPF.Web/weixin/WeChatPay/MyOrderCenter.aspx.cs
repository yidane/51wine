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
            //string thisUrl = MyCommFun.getWebSite() + "/weixin/restaurant/diancai_shoppingCart.aspx" + Request.Url.Query;
            //int widInt = MyCommFun.RequestWid();
            //var bll = new BLL.wx_userweixin();
            //Model.wx_userweixin uWeiXinModel = bll.GetModel(widInt);
            //OAuth2BaseProc(uWeiXinModel, "MyOrderCenter", thisUrl);
        }
    }
}