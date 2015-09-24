using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.admin.diancai
{
    public partial class dingdan_confirm : Web.UI.ManagePage
    {
        public int shopid = 0;
        public string openid = "";
        public int id = 0;
        public string Dingdanlist = "";
        public string dingdanren = "";

        BLL.wx_diancai_dingdan_manage manage = new BLL.wx_diancai_dingdan_manage();
        Model.wx_diancai_dingdan_manage managemodel = new Model.wx_diancai_dingdan_manage();
        BLL.wx_diancai_shopinfo shopinfo = new BLL.wx_diancai_shopinfo();
        Model.wx_diancai_shopinfo sjopmodel = new Model.wx_diancai_shopinfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            id = MyCommFun.RequestInt("id");
            shopid = MyCommFun.RequestInt("shopid");
            openid = MyCommFun.QueryString("openid");
        }
    }
}