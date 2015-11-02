using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeiXinPF.Web.admin.diancai
{
    public partial class dingdan_manage : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;
        BLL.wx_diancai_dingdan_manage gbll = new BLL.wx_diancai_dingdan_manage();

        /*页面参数*/
        protected DateTime beginDate = DateTime.MinValue;
        protected DateTime endDate = DateTime.MinValue;
        protected int payAmountMin = -1;
        protected int payAmountMax = -1;
        protected string orderNumber = string.Empty;
        protected string customerName = string.Empty;
        protected string customerTel = string.Empty;
        protected int shopid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //获取链接参数，对控件赋值，查询
            this.shopid = MyCommFun.RequestInt("shopid") == 0 ? GetShopId() : MyCommFun.RequestInt("shopid");
            DateTime.TryParse(MyCommFun.QueryString("beginDate"), out beginDate);
            DateTime.TryParse(MyCommFun.QueryString("endDate"), out endDate);
            payAmountMin = MyCommFun.RequestInt("payAmountMin");
            payAmountMax = MyCommFun.RequestInt("payAmountMax");
            this.orderNumber = MyCommFun.QueryString("OrderNumber");
            this.customerName = MyCommFun.QueryString("CustomerName");
            this.customerTel = MyCommFun.QueryString("customerTel");

            this.pageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {
                BindControls();
                BindResult();
            }
        }

        private void BindControls()
        {
            if (beginDate != DateTime.MinValue)
                this.txtbeginDate.Text = beginDate.ToString();
            if (endDate != DateTime.MinValue)
                this.txtEndDate.Text = endDate.ToString();
            if (payAmountMax > -1)
                this.txtPayAmountMax.Text = payAmountMax.ToString();
            if (payAmountMin > -1)
                this.txtPayAmountMin.Text = payAmountMin.ToString();
            this.txtOrderNumber.Text = this.orderNumber;
            this.txtCustomerName.Text = this.customerName;
            this.txtCustomerTel.Text = this.customerTel;
        }

        #region 数据绑定=================================

        private DataSet QueryData()
        {
            //判断是否已经设置了微留言基本信息
            this.page = MXRequest.GetQueryInt("page", 1);
            var result = gbll.GetOrderList(shopid, this.pageSize, this.page, beginDate, endDate, payAmountMin,
                                           payAmountMax, orderNumber, customerName, customerTel, out this.totalCount);

            return result;
        }

        private void BindResult()
        {
            var result = QueryData();
            var sbll = new BLL.wx_diancai_dingdan_manage();
            if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
            {
                DataRow dr;

                int count = result.Tables[0].Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    dr = result.Tables[0].Rows[i];

                    var orderId = Convert.ToInt32(dr["id"]);
                    var detail = sbll.GetOrderCaipinDetail(orderId);
                    dr["Detail"] = detail;
                    dr["OrderNo"] = i + 1;
                }

                result.AcceptChanges();
            }
            this.rptList.DataSource = result;
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = this.GetNewUrl();
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and  orderNumber like  '%" + _keywords + "%' ");
            }

            return strTemp.ToString();
        }
        #endregion

        #region 返回每页数量=============================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("dingdan_manage_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(GetNewUrl());
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var result = QueryData();
                CSVHelper.DownloadAsSCV(Response, "订单管理", null, null, result.Tables[0]);
            }
            catch (Exception exception)
            {

            }
        }

        private string GetNewUrl()
        {
            var queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendFormat("dingdan_manage.aspx?shopid={0}", shopid);
            if (!string.IsNullOrEmpty(this.txtbeginDate.Text))
                queryStringBuilder.AppendFormat("&beginDate={0}", this.txtbeginDate.Text.Trim());
            if (!string.IsNullOrEmpty(this.txtEndDate.Text))
                queryStringBuilder.AppendFormat("&endDate={0}", this.txtEndDate.Text);
            if (!string.IsNullOrEmpty(this.txtPayAmountMax.Text))
                queryStringBuilder.AppendFormat("&payAmountMax={0}", this.txtPayAmountMax.Text);
            if (!string.IsNullOrEmpty(this.txtPayAmountMin.Text))
                queryStringBuilder.AppendFormat("&payAmountMin={0}", this.txtPayAmountMin.Text);
            if (!string.IsNullOrEmpty(this.txtOrderNumber.Text))
                queryStringBuilder.AppendFormat("&orderNumber={0}", this.txtOrderNumber.Text);
            if (!string.IsNullOrEmpty(this.txtCustomerName.Text))
                queryStringBuilder.AppendFormat("&customerName={0}", this.txtCustomerName.Text);
            if (!string.IsNullOrEmpty(this.txtCustomerTel.Text))
                queryStringBuilder.AppendFormat("&customerTel={0}", this.txtCustomerTel.Text);

            return queryStringBuilder.ToString();
        }

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("dingdan_manage_page_size", _pagesize.ToString(), 14400);
                }
            }

            Response.Redirect(this.GetNewUrl());
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // ChkAdminLevel("manager_list", MXEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0;
            int errorCount = 0;

            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    if (gbll.Delete(id))
                    {
                        sucCount += 1;
                    }
                    else
                    {
                        errorCount += 1;
                    }
                }
            }
            AddAdminLog(MXEnums.ActionEnum.Delete.ToString(), "信息" + sucCount + "条，失败" + errorCount + "条"); //记录日志

            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", GetNewUrl(), "Success");
        }

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

            }
        }

        public string shenghestr(object id, object hasSH)
        {
            if (hasSH.ToString().ToLower() == "true")
            {
                return "";
            }
            else
            {

                return "<a href=\"messagehasSH.aspx?id=" + id.ToString() + "\" class=\"shenghe\">审核</a>";
            }


        }
    }
}