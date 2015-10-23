using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeiXinPF.Web.weixin.KNSHotel
{
    using System.Data;

    using OneGulp.WeChat.MP;
    using OneGulp.WeChat.MP.AdvancedAPIs;

    using WeiXinPF.Common;
    using WeiXinPF.WeiXinComm;

    public partial class KNSHotelMasterPage : System.Web.UI.MasterPage
    {
        public int hotelid { get; set; }
        public string openid { get; set; }
        public string title { get; set; }

        public int wid { get; set; }

        public int dingdannum { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            hotelid = MyCommFun.RequestInt("hotelid");
            openid = MyCommFun.QueryString("openid");
            this.title = this.GetTitle();
            string thisUrl = MyCommFun.getWebSite() + "/weixin/KNSHotel/index.aspx" + Request.Url.Query;
            var bll = new BLL.wx_userweixin();
            this.wid = MyCommFun.RequestWid();
            Model.wx_userweixin uWeiXinModel = bll.GetModel(wid);
            OAuth2BaseProc(uWeiXinModel, "index", thisUrl);

            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.openid) && this.hotelid >0)
                {
                    var dingdanbll = new BLL.wx_hotel_dingdan();
                    var hotelInfo = new BLL.wx_hotels_info().GetModel(this.hotelid);

                    if (hotelInfo != null)
                    {
                        this.wid = hotelInfo.wid.Value;
                    }

                    DataSet dr = dingdanbll.GetList(this.openid, this.hotelid);
                    if (dr.Tables[0].Rows.Count > 0)
                    {
                        this.dingdannum = dr.Tables[0].Rows.Count;
                    }
                    else
                    {
                        this.dingdannum = 0;
                    } 
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
#if DEBUG
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
#endif

        }

        private string GetTitle()
        {
            var pageName = WebHelper.GetPageName();

            switch (pageName)
            {
                case "hotel_detail.aspx":
                    return "关于";
                case "index.aspx":
                    return "房型选择";
                case "hotel_order.aspx":
                    return "订单";
                default:
                    return "订单";
            }
        }        
    }
}