using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;
using WeiXinPF.Web.weixin.restaurant;

namespace WeiXinPF.Web.admin.diancai
{
    using WeiXinPF.Application.DomainModules.IdentifyingCode.Service;

    public partial class dingdan_confirm : Web.UI.ManagePage
    {
        public int shopid = 0;
        public string Dingdanlist = "";
        public string dingdanren = "";

        public int wid = 0;

        public const string ModuleName = "restaurant";
        protected void Page_Load(object sender, EventArgs e)
        {
            shopid = GetShopId();
            wid = this.GetWeiXinCode().id;
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
            else if (identifyingCode.Status != StatusManager.DishStatus.NoUsed.StatusID 
                && identifyingCode.Status != StatusManager.DishStatus.RefundFaild.StatusID)
            {
                Response.Write("<script language='javascript' type='text/javascript'>alert('该商品已消费或者退单，请确认！')</script>");
            }
            else
            {
                Response.Redirect("commodity_detail.aspx?cid=" + identifyingCode.IdentifyingCodeId + "&shopid=" + identifyingCode.ShopId + "&id=" + identifyingCode.OrderId);
            }
        }

        protected void confirmnumber_Validating()
        {

        }
        #endregion
    }
}