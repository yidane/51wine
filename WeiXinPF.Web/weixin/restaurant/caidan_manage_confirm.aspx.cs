using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.weixin.restaurant
{
    public partial class caidan_manage_confirm : WeiXinPage
    {
        public int shopid = 0;
        BLL.wx_diancai_dingdan_manage managebll = new BLL.wx_diancai_dingdan_manage();
        Model.wx_diancai_dingdan_manage manage = new Model.wx_diancai_dingdan_manage();

        BLL.wx_diancai_shopinfo shopBll = new BLL.wx_diancai_shopinfo();
        Model.wx_diancai_shopinfo shopinfo = new Model.wx_diancai_shopinfo();
        public string hotelName = "";
        public string str = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                shopid = MyCommFun.RequestInt("shopid");
                shopinfo = shopBll.GetModel(shopid);
                hotelName = shopinfo.hotelName;
            }
        }
    }
}