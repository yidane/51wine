using System;
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
        public decimal Price = 0;
        public int NoUseCount = 0;

        //请求参数
        private int shopid = 0;
        private int dingdan = 0;
        private string openid = string.Empty;
        private int caiid = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                shopid = MyCommFun.RequestInt("shopid");
                dingdan = MyCommFun.RequestInt("dingdan");
                openid = MyCommFun.QueryString("openid");
                caiid = MyCommFun.RequestInt("caiid");

                if (!IsPostBack)
                {
                    var result = new BLL.wx_diancai_dingdan_manage().GetDingdanRefundDetail(shopid, dingdan, openid, caiid);

                    if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                    {
                        RestruantName = result.Tables[0].Rows[0].Field<string>("hotelName");
                        OrderNumber = result.Tables[0].Rows[0].Field<string>("orderNumber");
                        CaiPinName = result.Tables[0].Rows[0].Field<string>("cpName");
                        Price = result.Tables[0].Rows[0].Field<decimal>("price");
                        NoUseCount = result.Tables[0].Rows.Count;

                        //缓存订单菜id
                        var caiidListString = string.Empty;
                        foreach (DataRow row in result.Tables[0].Rows)
                        {
                            if (result.Tables[0].Rows.IndexOf(row) == result.Tables[0].Rows.Count - 1)
                            {
                                caiidListString = caiidListString + row.Field<int>("id");
                            }
                            else
                            {
                                caiidListString = caiidListString + row.Field<int>("id") + ",";
                            }
                        }

                        this.caiidList.Value = caiidListString;
                    }
                }
            }
            catch (Exception exception)
            {

            }
        }

        protected void btnRefund_Click(object sender, EventArgs e)
        {
            var refundCount = Convert.ToInt32(this.txtRefundCount.Text);
            var caiidListString = this.caiidList.Value;
            var caiidListArr = caiidListString.Split(',');
            if (caiidListArr.Length < refundCount)
                throw new Exception("退菜数量超过总数");

            if (caiidListArr.Length >= refundCount)
            {
                var refundCaiidList = new List<int>();
                for (int index = 0; index < refundCount; index++)
                {
                    refundCaiidList.Add(Convert.ToInt32(refundCaiidList));
                }

                new BLL.wx_diancai_dingdan_manage().RefundDiancai(dingdan, caiid, refundCaiidList);

                Response.Redirect(string.Format("diancai_oder.aspx?openid={0}&type=refund", openid));
            }
        }
    }
}