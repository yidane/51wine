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
    using WeiXinPF.Application.DomainModules.IdentifyingCode.Service;
    using WeiXinPF.Infrastructure.DomainDataAccess.IdentifyingCode.DTO;

    public partial class credentials_detail : ManagePage
    {
        protected double totalAmount = 0.0;
        BLL.wx_diancai_dingdan_manage gbll = new BLL.wx_diancai_dingdan_manage();
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                startDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                endDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                RptBind();
            }
        }

        #region 数据绑定=================================
        private void RptBind()
        {
            var data = SearchData();
            this.rptList.DataSource = data; //gbll.GetCredentialsList(shopid, condition, moduleName, out this.totalAmount);
            this.rptList.DataBind();
            this.totalAmount = data.Sum(item => item.RealAmount);
        }

        private IList<OrderDetailDTO> SearchData()
        {
            int shopid = MXRequest.GetQueryInt("shopid") == 0 ? GetShopId() : MXRequest.GetQueryInt("shopid");
            string condition = "";
            var moduleName = "restaurant";

            if (!String.IsNullOrEmpty(startDate.Text.Trim()))
            {
                condition += " FinishTime>='" + startDate.Text.Trim() + "' ";
            }
            if (!String.IsNullOrEmpty(endDate.Text.Trim()))
            {
                if (!String.IsNullOrEmpty(condition))
                    condition += " and ";
                condition += " FinishTime<='" + endDate.Text.Trim() + "' ";
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

            var result = IdentifyingCodeService.GetOrderDetail(shopid, moduleName, condition);

            return result;
        }

        private void DownloadExcel()
        {
            var dataTable=new DataTable();
            dataTable.Columns.Add("序号");
            dataTable.Columns.Add("订单编号");
            dataTable.Columns.Add("订单状态");
            dataTable.Columns.Add("订单关闭日期");
            dataTable.Columns.Add("预约人");
            dataTable.Columns.Add("总计");

            var data = SearchData();
            if (data != null && data.Count > 0)
            {

                DataRow newRow = null;
                for (int index = 0; index < data.Count; index++)
                {
                    newRow = dataTable.NewRow();
                    newRow[0] = index + 1;
                    newRow[1] = data[index].OrderNumber;
                    newRow[2] = data[index].PayStatus;
                    newRow[3] = data[index].ModifyTime;
                    newRow[4] = data[index].CustomerName;
                    newRow[5] = data[index].PayAmount;

                    dataTable.Rows.Add(newRow);
                }
            }

            CSVHelper.DownloadAsSCV(Response,"餐饮服务验证码",dataTable);
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
            var moduleName = "restaurant";
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rp = e.Item.FindControl("rp") as Repeater;
                var list = e.Item.DataItem as OrderDetailDTO;

                rp.DataSource = IdentifyingCodeService.GetIdentifyingCodeDTO(moduleName, list.Id); //gbll.GetCredentialsCommodityList(list.id, moduleName);
                rp.DataBind();
            }
        }

        protected void serch_OnClick(object sender, EventArgs e)
        {
            RptBind();
        }

        protected void btnDownloadExcel_OnClick(object sender, EventArgs e)
        {
            try
            {
                this.DownloadExcel();
            }
            catch (Exception exception)
            {
                
            }
        }
    }
}