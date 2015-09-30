using System;
using System.Collections.Generic;
using System.Data;
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
            confirmnumber.CausesValidation = true;
        }


        #region 验证订单
        protected void confirm_dingdan_Click(object sender, EventArgs e)
        {
            string number = confirmnumber.Text.Trim();
            string condition = "identifyingcode='" + number + "'";
            DataSet ds = manage.GetCommodityList(condition);
            DataTable table = ds.Tables[0];
            if (table.Rows.Count >= 1)
            {
                if (table.Rows[0]["shopinfoid"].ToString() == shopid.ToString())
                {
                    string icode = number;
                    string id = table.Rows[0]["id"].ToString();
                    string cid = table.Rows[0]["cid"].ToString();
                    string url = "commodity_detail.aspx?cid=" + cid + "&shopid=" + shopid + "&id=" + id;
                    Response.Redirect(url);
                }
                else
                {
                    Response.Write("<script language='javascript' type='text/javascript'>alert('该订单非本店订单，请确认！')</script>");
                }
            }
        }

        protected void confirmnumber_Validating()
        {

        }
        #endregion
    }
}