﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.admin.diancai
{
    public partial class diancai_dingdanRefund_manage : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;

        /*页面参数*/
        protected BLL.wx_diancai_tuidan_manage refundManager = new BLL.wx_diancai_tuidan_manage();
        protected DateTime beginDate = DateTime.MinValue;
        protected DateTime endDate = DateTime.MinValue;
        protected int payAmountMin = -1;
        protected int payAmountMax = -1;
        protected string refundNumber = string.Empty;
        protected string orderNumber = string.Empty;
        protected int refundStatus = -1;
        protected string customerName = string.Empty;
        protected string customerTel = string.Empty;
        protected int shopid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //获取链接参数，对控件赋值，查询
            this.shopid = MyCommFun.RequestInt("shopid") == 0 ? GetShopId() : MyCommFun.RequestInt("shopid");
            DateTime.TryParse(MyCommFun.QueryString("beginDate"), out beginDate);
            DateTime.TryParse(MyCommFun.QueryString("endDate"), out endDate);
            refundNumber = MyCommFun.QueryString("refundNo");
            refundStatus = MyCommFun.RequestInt("refundStatus");
            refundStatus = refundStatus == 0 ? -1 : refundStatus;
            this.orderNumber = MyCommFun.QueryString("OrderNumber");
            this.customerName = MyCommFun.QueryString("CustomerName");
            this.customerTel = MyCommFun.QueryString("customerTel");

            //获取页码
            this.page = MXRequest.GetQueryInt("page", 1);
            this.totalCount = int.Parse(this.total.Value);

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
            if (string.IsNullOrEmpty(refundNumber))
                this.txtRefundNo.Text = refundNumber;
            if (refundStatus > -1)
                this.dboRefundStatus.SelectedValue = refundStatus.ToString();
            this.txtOrderNumber.Text = this.orderNumber;
            this.txtCustomerName.Text = this.customerName;
            this.txtCustomerTel.Text = this.customerTel;
        }

        private string GetRefundStatusItem(string refundStatusId)
        {
            var CurrentItem = this.dboRefundStatus.Items.Cast<ListItem>().FirstOrDefault(item => string.Equals(item.Value, refundStatusId));
            return CurrentItem != null ? CurrentItem.Text : string.Empty;
        }

        #region 数据绑定=================================
        private void BindResult()
        {
            var result = GetData();
            this.rptList.DataSource = result;
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = this.GetNewUrl(true);
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }

        private DataSet GetData(bool isDownload = false)
        {
            var result = refundManager.GetRefundList(shopid, isDownload ? totalCount : this.pageSize, isDownload ? 1 : this.page,
                                                                                    beginDate, endDate, payAmountMin,
                                                                                    payAmountMax, refundNumber, orderNumber,
                                                                                    customerName, customerTel, refundStatus, out this.totalCount);

            this.total.Value = totalCount.ToString();

            if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
            {
                DataRow dr;

                int count = result.Tables[0].Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    dr = result.Tables[0].Rows[i];

                    var refundnumber = dr["RefundNumber"].ToString();
                    var detail = refundManager.GetRefundDetail(refundnumber);
                    dr["Detail"] = detail;
                    dr["OrderNo"] = i + 1;

                    var status = dr["refundStatus"] != null && dr["refundStatus"] != DBNull.Value ? dr["refundStatus"].ToString() : string.Empty;
                    dr["RefundStatusDesc"] = GetRefundStatusItem(status);
                }

                result.AcceptChanges();
            }

            return result;
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords)
        {
            var strTemp = new StringBuilder();
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

        private string GetNewUrl(bool isCommon = false)
        {
            var queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendFormat("diancai_dingdanRefund_manage.aspx?shopid={0}", shopid);
            if (!string.IsNullOrEmpty(this.txtbeginDate.Text))
                queryStringBuilder.AppendFormat("&beginDate={0}", this.txtbeginDate.Text.Trim());
            if (!string.IsNullOrEmpty(this.txtEndDate.Text))
                queryStringBuilder.AppendFormat("&endDate={0}", this.txtEndDate.Text);
            if (this.dboRefundStatus.SelectedIndex > -1)
                queryStringBuilder.AppendFormat("&refundStatus={0}", this.dboRefundStatus.SelectedValue);
            queryStringBuilder.AppendFormat("&orderNumber={0}", this.txtOrderNumber.Text);
            if (!string.IsNullOrEmpty(this.txtCustomerName.Text))
                queryStringBuilder.AppendFormat("&customerName={0}", this.txtCustomerName.Text);
            if (!string.IsNullOrEmpty(this.txtCustomerTel.Text))
                queryStringBuilder.AppendFormat("&customerTel={0}", this.txtCustomerTel.Text);

            //页码
            queryStringBuilder.AppendFormat("&page={0}", isCommon ? "__id__" : this.page.ToString());
            queryStringBuilder.AppendFormat("&totalCount={0}", totalCount);

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
            //// ChkAdminLevel("manager_list", MXEnums.ActionEnum.Delete.ToString()); //检查权限
            //int sucCount = 0;
            //int errorCount = 0;

            //for (int i = 0; i < rptList.Items.Count; i++)
            //{
            //    int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
            //    CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
            //    if (cb.Checked)
            //    {
            //        if (refundManager.Delete(id))
            //        {
            //            sucCount += 1;
            //        }
            //        else
            //        {
            //            errorCount += 1;
            //        }
            //    }
            //}
            //AddAdminLog(MXEnums.ActionEnum.Delete.ToString(), "信息" + sucCount + "条，失败" + errorCount + "条"); //记录日志

            //JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", GetNewUrl(), "Success");
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

        protected void btnExport_OnClick(object sender, EventArgs e)
        {
            try
            {
                var result = GetData(true);
                var headerList = new List<string>()
                    {
                            "序号",
                            "退单号",
                            "订单号",
                            "预订人",
                            "电话",
                            "预定时间",
                            "退单商品",
                            "退款总额",
                            "退单状态"
                    };
                var columnIndex = new List<int>() { 11, 2, 3, 4, 5, 6, 7, 8, 10 };
                CSVHelper.DownloadAsSCV(Response, "退单管理", result.Tables[0], columnIndex, headerList);
            }
            catch (Exception)
            {
            }
        }
    }
}