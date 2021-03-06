﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;
using WeiXinPF.Web.weixin.WeChatPay;
using WeiXinPF.WeiXinComm;

namespace WeiXinPF.Web.weixin.restaurant
{
    public partial class AfterPay : System.Web.UI.Page
    {
        protected int shopid = 0;
        protected string payStatus = string.Empty;
        protected string newUrl = string.Empty;
        protected string clearCache = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            payStatus = Request.QueryString["PayStatus"];
            var payData = Request.QueryString["payData"];
            var ticket = Request.QueryString["ticket"];

            payData = EncryptionManager.AESDecrypt(payData, ticket);
            var payDataModel = JSONHelper.Deserialize<UnifiedOrderEntity>(payData);

            shopid = Convert.ToInt32(payDataModel.Extra["shopid"]);
            switch (payStatus.ToLower())
            {
                case "success":
                    //支付成功，清空购物车，跳转到当前店铺订单列表
                    clearCache = "cart.clear();";
                    newUrl =
                        string.Format(
                            "../restaurant/diancai_orderDetail.aspx?wid={0}&shopid={1}&dingdan={2}&openid={3}",
                            payDataModel.wid,
                            payDataModel.Extra["shopid"],
                            payDataModel.OrderId,
                            payDataModel.openid);
                    break;
                case "fail":
                case "cancel":
                    //支付失败，跳转到购物车
                    newUrl =
                        string.Format(
                            "../restaurant/diancai_shoppingCart.aspx?shopid={0}&openid={1}&wid={2}",
                            payDataModel.Extra["shopid"], payDataModel.openid, payDataModel.Extra["wid"]);
                    break;
            }
        }
    }
}