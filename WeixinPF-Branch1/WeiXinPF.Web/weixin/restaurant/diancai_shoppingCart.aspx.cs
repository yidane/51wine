using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeiXinPF.Web.weixin.restaurant
{
    public partial class diancai_shoppingCart : WeiXinPage
    {
        public int shopid = 0;
        public string shopping = "";
        public string categories = "";
        public string openid = "";
        public string hotelName = "";
        public static int idf = 0;
        public string name = "";
        public string phone = "";

        public string wid = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //OAuth2认证
            string thisUrl = MyCommFun.getWebSite() + "/weixin/restaurant/diancai_shoppingCart.aspx" + Request.Url.Query;
            int widInt = MyCommFun.RequestWid();
            var bll = new BLL.wx_userweixin();
            Model.wx_userweixin uWeiXinModel = bll.GetModel(widInt);
            OAuth2BaseProc(uWeiXinModel, "index", thisUrl);

            if (!Page.IsPostBack)
            {
                BindFormControl();
            }
        }



        protected void BindFormControl()
        {
            BLL.wx_diancai_caipin_category categorybll = new BLL.wx_diancai_caipin_category();
            BLL.wx_diancai_member menberbll = new BLL.wx_diancai_member();
            Model.wx_diancai_member member = new Model.wx_diancai_member();

            BLL.wx_diancai_shopinfo shopBll = new BLL.wx_diancai_shopinfo();
            Model.wx_diancai_shopinfo shopinfo = new Model.wx_diancai_shopinfo();

            openid = MyCommFun.QueryString("openid");
            shopid = MyCommFun.RequestInt("shopid");
            wid = MyCommFun.QueryString("wid");

            if (openid == "" || shopid == 0)
            {
                return;
            }

            shopinfo = shopBll.GetModel(shopid);

            if (shopinfo == null)
            {
                return;
            }

            hotelName = shopinfo.hotelName;
            idf = MyCommFun.RequestInt("id");

            member = menberbll.GetModel(shopid, openid);
            if (member != null)
            {
                name = member.memberName;
                phone = member.menberTel;
            }

            categories = "{";

            DataSet category1 = categorybll.GetList(shopid);
            if (category1.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < category1.Tables[0].Rows.Count; i++)
                {
                    categories += "\"" + category1.Tables[0].Rows[i]["id"].ToString() + "\"" + ":" + "\"" + category1.Tables[0].Rows[i]["categoryName"].ToString() + "\"" + ",";
                }
            }
            categories = categories.Substring(0, categories.Length - 1);
            categories += "}";
        }
    }
}