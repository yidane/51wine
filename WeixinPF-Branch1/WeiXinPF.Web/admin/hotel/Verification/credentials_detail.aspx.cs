using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Model;
using WeiXinPF.Web.UI;

namespace WeiXinPF.Web.admin.hotel.Verification
{
    using WeiXinPF.Application.DomainModules.IdentifyingCode.Service;
    using WeiXinPF.Infrastructure.DomainDataAccess.IdentifyingCode.DTO;

    public partial class credentials_detail : ManagePage
    {
        protected double totalAmount=0.0;
        protected double refundAmount = 0.0;
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
            var detail = GetData();
            this.rptList.DataSource = detail; // gbll.GetCredentialsList(shopid, condition, moduleName, out this.totalAmount);
            this.rptList.DataBind();
            this.totalAmount = detail.Sum(item => item.RealAmount);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        private IList<OrderDetailDTO> GetData()
        {
            int shopid = MXRequest.GetQueryInt("hotelid") == 0 ? this.GetHotelId() : MXRequest.GetQueryInt("hotelid");
            string condition = "";
            var moduleName = "hotel";

            if (!String.IsNullOrEmpty(startDate.Text.Trim()))
            {
                condition += " modifytime>='" + startDate.Text.Trim() + "' ";
            }
            if (!String.IsNullOrEmpty(endDate.Text.Trim()))
            {
                if (!String.IsNullOrEmpty(condition))
                    condition += " and ";
                condition += " modifytime<='" + endDate.Text.Trim() + "' ";
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

            var detail = IdentifyingCodeService.GetOrderDetail(shopid, moduleName, condition);
            return detail;
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
            var moduleName = "hotel";
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rp = e.Item.FindControl("rp") as Repeater;
                var list = e.Item.DataItem as OrderDetailDTO;

                rp.DataSource = IdentifyingCodeService.GetIdentifyingCodeDTO(moduleName, list.Id); // gbll.GetCredentialsCommodityList(list.Id, moduleName);
                rp.DataBind();
            }
        }

        protected void serch_OnClick(object sender, EventArgs e)
        {
            RptBind();
        }

        protected void btnExport_OnClick(object sender, EventArgs e)
        {
            try
            {
                var result = GetData();
                if (result.Any()|| result.Any())
                {
                    List<string> headerList = new List<string>() {  "订单编号", "订单状态", "订单关闭日期",
                    "预约人","数量","商品名称","入住时间"
                    ,"离店时间","商品单价","总计" };
                    List<int> columnIndexList = new List<int>() { 3, 2, 11, 4, 5, 6, 7, 8, 9, 10 };
                    CSVHelper.DownloadAsSCV<OrderDetailDTO>(Response, "服务凭据查询", result,
                        columnIndexList, headerList);
                }
              
            }
            catch
            {

            }
        }
    }
}