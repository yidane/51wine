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
    using WeiXinPF.Application.DomainModules.IdentifyingCode.Service;

    public partial class dingdan_confirm : Web.UI.ManagePage
    {
        public int shopid = 0;
        public string openid = "";
        public int id = 0;
        public string Dingdanlist = "";
        public string dingdanren = "";

        public int wid = 0;

        public const string ModuleName = "restaurant";
        protected void Page_Load(object sender, EventArgs e)
        {
            id = MyCommFun.RequestInt("id");
            shopid = MyCommFun.RequestInt("shopid") == 0 ? GetShopId() : MyCommFun.RequestInt("shopid");
            wid = MyCommFun.RequestInt("wid") == 0 ? this.GetWeiXinCode().id : MyCommFun.RequestInt("wid");
            openid = MyCommFun.QueryString("openid");
            confirmnumber.CausesValidation = true;
        }


        #region 验证订单
        protected void confirm_dingdan_Click(object sender, EventArgs e)
        {
            var number = this.confirmnumber.Text.Trim();

            var identifyingCode = IdentifyingCodeService.GetConfirmIdentifyingCodeInfo(shopid, number, ModuleName, wid);

            if (identifyingCode == null)
            {
                Response.Write("<script language='javascript' type='text/javascript'>alert('该订单不存在或未付款，请确认！')</script>");
            }
            else if (identifyingCode.Status != 1)
            {
                Response.Write("<script language='javascript' type='text/javascript'>alert('该商品已消费或者退单，请确认！')</script>");
            }
            else
            {
                Response.Redirect("commodity_detail.aspx?cid=" + identifyingCode.IdentifyingCodeId + "&shopid=" + identifyingCode.ShopId + "&id=" + identifyingCode.OrderId);
            }

            //string condition = "identifyingcode='" + number + "'";
            //DataSet ds = manage.GetCommodityList(condition);
            //DataTable table = ds.Tables[0];
            //if (table.Rows.Count >= 1)
            //{
            //    if (table.Rows[0]["shopinfoid"].ToString() == shopid.ToString())
            //    {
            //        string icode = number;
            //        string id = table.Rows[0]["id"].ToString();
            //        string cid = table.Rows[0]["cid"].ToString();
            //        string url = "commodity_detail.aspx?cid=" + cid + "&shopid=" + shopid + "&id=" + id;
            //        Response.Redirect(url);
            //    }
            //    else
            //    {
            //        Response.Write("<script language='javascript' type='text/javascript'>alert('该订单非本店订单，请确认！')</script>");
            //    }
            //}
            //else
            //{
            //    Response.Write("<script language='javascript' type='text/javascript'>alert('该订单不存在或未付款，请确认！')</script>");
            //}
        }

        protected void confirmnumber_Validating()
        {

        }
        #endregion
    }
}