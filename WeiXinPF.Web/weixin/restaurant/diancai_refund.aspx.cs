﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.weixin.restaurant
{
    public partial class diancai_refund : System.Web.UI.Page
    {
        public string RestruantName = string.Empty;
        public string OrderNumber = string.Empty;
        public string CaiPinName = string.Empty;
        public double Price = 0;
        public int NoUseCount = 0;

        //请求参数
        private int shopid = 0;
        private int dingdan = 0;
        private string openid = string.Empty;
        private int caiid = 0;
        public int wid = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                shopid = MyCommFun.RequestInt("shopid");
                dingdan = MyCommFun.RequestInt("dingdan");
                openid = MyCommFun.QueryString("openid");
                caiid = MyCommFun.RequestInt("caiid");
                wid = MyCommFun.RequestInt("wid");

                if (!IsPostBack)
                {
                    var result = new BLL.wx_diancai_dingdan_manage().GetDingdanRefundDetail(shopid, dingdan, openid, caiid);

                    if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                    {
                        RestruantName = result.Tables[0].Rows[0].Field<string>("hotelName");
                        OrderNumber = result.Tables[0].Rows[0].Field<string>("orderNumber");
                        CaiPinName = result.Tables[0].Rows[0].Field<string>("cpName");
                        Price = result.Tables[0].Rows[0].Field<double>("price");
                        NoUseCount = result.Tables[0].Rows.Count;
                        this.txtRefundAmount.Text = string.Format("{0}元", Price);

                        //缓存订单菜id
                        var caiidListString = string.Empty;
                        foreach (DataRow row in result.Tables[0].Rows)
                        {
                            if (result.Tables[0].Rows.IndexOf(row) == result.Tables[0].Rows.Count - 1)
                            {
                                caiidListString = caiidListString + row.Field<Guid>("IdentifyingCodeId");
                            }
                            else
                            {
                                caiidListString = caiidListString + row.Field<Guid>("IdentifyingCodeId") + ",";
                            }
                        }

                        this.caiidList.Value = caiidListString;
                    }
                    else
                    {
                        OrderNumber = "该订单号不存在或该订单中不存在未消费的菜品";
                        this.btnRefund.Visible = false;
                    }
                }
            }
            catch (Exception exception)
            {
                Response.Write(exception.Message);
            }
        }

        protected void btnRefund_Click(object sender, EventArgs e)
        {
            try
            {
                var refundCount = Convert.ToInt32(this.txtRefundCount.Text);
                var caiidListString = this.caiidList.Value;
                var caiidListArr = caiidListString.Split(',');
                var refundAmount = Convert.ToDecimal(this.txtRefundAmount.Text.Replace("元", ""));
                if (caiidListArr.Length < refundCount)
                    throw new Exception("退菜数量超过总数");

                if (caiidListArr.Length >= refundCount)
                {
                    var refundCaiidList = new List<Guid>();
                    for (int index = 0; index < refundCount; index++)
                    {
                        refundCaiidList.Add(new Guid(caiidListArr[index]));
                    }

                    //(int shopinfiId, string openid, int wid, int refundAmount, int dingdanid, int caiid, List<int> caipinIdList)
                    new BLL.wx_diancai_dingdan_manage().RefundDiancai(shopid, openid, wid, (int)(refundAmount * 100) * refundCount, dingdan, caiid, refundCaiidList);

                    Response.Redirect(string.Format("diancai_oder.aspx?openid={0}&type=refund", openid));
                }
            }
            catch (Exception)
            {

            }
        }
    }
}