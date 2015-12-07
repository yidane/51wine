using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.weixin.restaurant
{
    public partial class diancai_RefundOrderDetail : System.Web.UI.Page
    {
        public string RestruantName = string.Empty;
        public string rename = string.Empty;
        private int shopid = 0;
        private string openid = string.Empty;
        private int dingdan = 0;
        public string refundCode = string.Empty;

        //订单基本信息参数
        public string OrderNumber = string.Empty;
        public string GoodsName = string.Empty;
        public int GoodsCount = 0;
        public double GoodsPrice = 0.0;
        public string PayAmount = string.Empty;
        public string customeName = string.Empty;
        public string customerTel = string.Empty;
        public string orderRange = string.Empty;
        public string RestruantLocation = string.Empty;
        public string RestruantPhone = string.Empty;
        public int wid = 0;
        public double lat;
        public double lng;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                shopid = MyCommFun.RequestInt("shopid");
                openid = MyCommFun.QueryString("openid");
                dingdan = MyCommFun.RequestInt("dingdan");
                wid = MyCommFun.RequestInt("wid");
                refundCode = MyCommFun.QueryString("RefundCode");

                var shopinfo = new BLL.wx_diancai_shopinfo().GetModel(shopid);
                RestruantName = shopinfo.hotelName;
                RestruantLocation = shopinfo.address;
                rename = shopinfo.dcRename;

                if (dingdan > 0)
                {
                    if (shopinfo.xplace != null)
                        lat = Convert.ToDouble(shopinfo.xplace.Value);
                    if (shopinfo.yplace != null)
                        lng = Convert.ToDouble(shopinfo.yplace.Value);

                    BindOrderDetaile();
                }
            }
        }

        /// <summary>
        /// 绑定订单详情
        /// </summary>
        /// <param name="orderDetail"></param>
        public void BindOrderDetaile()
        {
            var manage = new BLL.wx_diancai_tuidan_manage();
            var result = manage.GetRefundDetailWithOrderDetail(shopid, dingdan, refundCode);
            if (result != null && result.Tables.Count > 1)
            {
                var table0 = result.Tables[0];
                var table1 = result.Tables[1];

                if (table0.Rows.Count > 0)
                {
                    orderRange = string.Format("{0}-{1}", table0.Rows[0]["beginDate"], table0.Rows[0]["endDate"]);
                    GoodsName = table0.Rows[0].Field<string>("cpName");
                    GoodsPrice = table0.Rows[0].Field<double>("price");
                    PayAmount = (table0.Rows[0].Field<double>("refundAmount") / 100).ToString();
                }

                if (table1.Rows.Count > 0)
                {
                    OrderNumber = table1.Rows[0]["OrderNumber"].ToString();
                    GoodsCount = table1.Rows.Count;
                    customeName = table1.Rows[0].Field<string>("customerName");
                    customerTel = table1.Rows[0].Field<string>("customerTel");
                    refundReason.InnerHtml = table1.Rows[0].Field<string>("refundReason");
                }

                var detailBuilder = new StringBuilder();
                detailBuilder.AppendFormat(@"<p>订单号：{0}</p>
                    <p>退款商品：{1}</p>
                    <p>退款数量：{2}</p>
                    <p>商品单价：{3}元</p>", OrderNumber, GoodsName, GoodsCount, GoodsPrice);

                this.detail.InnerHtml = detailBuilder.ToString();
            }
        }
    }
}