using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeiXinPF.Web.weixin.restaurant
{
    using WeiXinComm;
    public partial class Restaurant : System.Web.UI.MasterPage
    {
        public string title { get; set; }
        public int shopid { get; set; }
        public string openid { get; set; }
        public string hotelName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.shopid = string.IsNullOrEmpty(WebHelper.GetQueryString("shopid")) ? 0 : int.Parse(WebHelper.GetQueryString("shopid"));
            this.openid = string.IsNullOrEmpty(WebHelper.GetQueryString("openid")) ? "loseopenid" : WebHelper.GetQueryString("openid");
            this.title = this.GetTitle();

            if (!Page.IsPostBack)
            {
                if (this.shopid == 0)
                {
                    this.hotelName = "无选定商铺";
                }
                else
                {
                    this.hotelName = new BLL.wx_diancai_shopinfo().GetModel(this.shopid).hotelName;
                }                
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
                default:
                    return "欢迎光临";
            }
        }
    }
}