using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OneGulp.WeChat.MP;
using OneGulp.WeChat.MP.AdvancedAPIs;
using WeiXinPF.Common;

namespace WeiXinPF.Web.weixin.restaurant
{
    using WeiXinComm;
    public partial class Restaurant : System.Web.UI.MasterPage
    {
        public string title { get; set; }
        public int shopid { get; set; }
        public string openid { get; set; }
        public string hotelName { get; set; }

        public string wid { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //OAuth2
            string thisUrl = MyCommFun.getWebSite() + "/weixin/restaurant/index.aspx" + Request.Url.Query;
            var bll = new BLL.wx_userweixin();
            int widInt = MyCommFun.RequestWid();
            Model.wx_userweixin uWeiXinModel = bll.GetModel(widInt);
            OAuth2BaseProc(uWeiXinModel, "index", thisUrl);


            this.shopid = string.IsNullOrEmpty(WebHelper.GetQueryString("shopid")) ? 0 : int.Parse(WebHelper.GetQueryString("shopid"));
            this.openid = string.IsNullOrEmpty(WebHelper.GetQueryString("openid")) ? "loseopenid" : WebHelper.GetQueryString("openid");
            this.wid = string.IsNullOrEmpty(WebHelper.GetQueryString("wid")) ? "1" : WebHelper.GetQueryString("wid");
            this.title = this.GetTitle();

            if (!Page.IsPostBack)
            {
                if (this.shopid == 0)
                {
                    this.hotelName = "无选定商铺";
                }
                else
                {
                    var shopinfo = new BLL.wx_diancai_shopinfo().GetModel(this.shopid);

                    if (shopinfo == null)
                    {
                        return;
                    }
                    this.hotelName = shopinfo.hotelName;
                }
            }
        }

        /// <summary>
        /// 授权判断，页面跳转
        /// </summary>
        /// <param name="state"></param>
        /// <param name="targetUrl">目标地址</param>
        public void OAuth2BaseProc(Model.wx_userweixin model, string state, string targetUrl)
        {
            var code = MyCommFun.QueryString("code");
            openid = MyCommFun.QueryString("openid");

            if (!string.IsNullOrEmpty(openid))
                return;

            //如果不存在code,表示尚未和微信平台OAuth2
            if (string.IsNullOrEmpty(code))
            {
                if (targetUrl == null || targetUrl.Trim() == "")
                {
                    targetUrl = MyCommFun.getTotalUrl();
                }
                var newUrl = OAuthApi.GetAuthorizeUrl(model.AppId, targetUrl, state, OAuthScope.snsapi_base);
                Response.Redirect(newUrl);
            }
            else
            {
                var result = OAuthApi.GetAccessToken(model.AppId, model.AppSecret, code);
                Response.Redirect(string.Format("{0}&openid={1}", Request.Url, result.openid));
            }
        }

        private string GetTitle()
        {
            var pageName = WebHelper.GetPageName();

            switch (pageName)
            {
                case "caidan_guanyu.aspx":
                    return "关于";
                case "index.aspx":
                    return "美食选购";
                case "diancai_shoppingCart.aspx":
                    return "商铺购物车";
                case "diancai_dingdan.aspx":
                case "diancai_OrderList.aspx":
                    return "我的订单";
                default:
                    return "欢迎光临";
            }
        }
    }
}