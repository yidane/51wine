using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.weixin.WeChatPay
{
    public partial class WeChatPay : System.Web.UI.Page
    {
        public int wid = 0;
        public string body = string.Empty;
        public string attach = string.Empty;
        public string out_trade_no = string.Empty;
        public int total_fee = 0;
        public string openid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Request.Form.Count > 0)
                //{
                //    var payData = Request.Form["payData"];
                //    if (payData != null)
                //    {
                //        var payDataModel = JSONHelper.Deserialize<UnifiedOrderRequest>(payData);

                //        if (payDataModel != null)
                //        {
                //            wid = payDataModel.wid;
                //            body = payDataModel.body;
                //            attach = payDataModel.attach;
                //            out_trade_no = payDataModel.out_trade_no;
                //            total_fee = payDataModel.total_fee;
                //            openid = payDataModel.openid;
                //        }
                //    }
                //}

                var payData = Request.QueryString["payData"];
                if (payData != null)
                {
                    var payDataModel = JSONHelper.Deserialize<UnifiedOrderRequest>(payData);

                    if (payDataModel != null)
                    {
                        wid = payDataModel.wid;
                        body = payDataModel.body;
                        attach = payDataModel.attach;
                        out_trade_no = payDataModel.out_trade_no;
                        total_fee = payDataModel.total_fee;
                        openid = payDataModel.openid;
                    }
                }
            }
        }
    }
}