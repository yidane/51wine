using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Model;
using WeiXinPF.Web.UI;

namespace WeiXinPF.Web.admin.diancai
{
    public partial class credentials_detail : ManagePage
    {
        protected double totalAmount=0.0;
        BLL.wx_diancai_dingdan_manage gbll = new BLL.wx_diancai_dingdan_manage();
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                RptBind();

            }
        }

        #region 数据绑定=================================
        private void RptBind()
        {
            int shopid = MXRequest.GetQueryInt("shopid")==0?GetShopId():MXRequest.GetQueryInt("shopid");
            string condition = "";
            if (!String.IsNullOrEmpty(startDate.Text.Trim()))
            {
                condition += " modifytime>'" + startDate.Text.Trim() + "' ";
            }
            if (!String.IsNullOrEmpty(endDate.Text.Trim()))
            {
                if (!String.IsNullOrEmpty(condition))
                    condition += " and ";
                condition += " modifytime<'" + endDate.Text.Trim() + "' ";
            }
            if (!String.IsNullOrEmpty(dingdanId.Text.Trim()))
            {
                if (!String.IsNullOrEmpty(condition))
                    condition += " and ";
                condition += " orderNumber LIKE '%" + dingdanId.Text.Trim() + "%' ";
            }
            if (!String.IsNullOrEmpty(paidmin.Text.Trim()))
            {
                if (!String.IsNullOrEmpty(condition))
                    condition += " and ";
                condition += " payAmount>" + paidmin.Text.Trim() + " ";
            }
            if (!String.IsNullOrEmpty(paidmax.Text.Trim()))
            {
                if (!String.IsNullOrEmpty(condition))
                    condition += " and ";
                condition += " payAmount<" + paidmax.Text.Trim() + " ";
            }
            if (!String.IsNullOrEmpty(orderperson.Text.Trim()))
            {
                if (!String.IsNullOrEmpty(condition))
                    condition += " and ";
                condition += " customerName LIKE '%" + orderperson.Text.Trim() + "%' ";
            }
            this.rptList.DataSource = gbll.GetCredentialsList(shopid, condition, out this.totalAmount);
            this.rptList.DataBind();


        }
        #endregion

       
        /// <summary>
        ///  
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rp = e.Item.FindControl("rp") as Repeater;
                wx_diancai_credentials_detail list = e.Item.DataItem as wx_diancai_credentials_detail;

                rp.DataSource = gbll.GetCredentialsCommodityList(list.id);
                rp.DataBind();
            }
        }

        protected void serch_OnClick(object sender, EventArgs e)
        {
            RptBind();
        }
    }
}