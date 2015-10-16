using System;
using System.Collections.Generic;
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
        BLL.wx_diancai_dingdan_manage managebll = new BLL.wx_diancai_dingdan_manage();
        Model.wx_diancai_dingdan_manage manage = new Model.wx_diancai_dingdan_manage();
        public string str = "";
        BLL.wx_diancai_shopinfo shopBll = new BLL.wx_diancai_shopinfo();
        Model.wx_diancai_shopinfo shopinfo = new Model.wx_diancai_shopinfo();

        //订单基本信息参数
        public string OrderNumber = string.Empty;
        public string PayAmount = string.Empty;
        public string customeName = string.Empty;
        public string customerTel = string.Empty;
        public string orderRange = string.Empty;
        public string RestruantLocation = string.Empty;
        public string RestruantPhone = string.Empty;
        public double lat;
        public double lng;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                shopid = MyCommFun.RequestInt("shopid");
                openid = MyCommFun.QueryString("openid");
                dingdan = MyCommFun.RequestInt("dingdan");


                shopinfo = shopBll.GetModel(shopid);
                RestruantName = shopinfo.hotelName;
                rename = shopinfo.dcRename;
                if (dingdan > 0)
                {
                    //List(openid);

                    var orderDetail = new OrderDetail();
                    orderDetail.GetDetail(dingdan);
                    lat = orderDetail.xplace;
                    lng = orderDetail.yplace;
                    BindOrderDetaile(orderDetail);
                }
            }
        }

        /// <summary>
        /// 绑定订单详情
        /// </summary>
        /// <param name="orderDetail"></param>
        public void BindOrderDetaile(OrderDetail orderDetail)
        {
            OrderNumber = orderDetail.orderNumber;
            PayAmount = orderDetail.payAmount.ToString();
            customeName = orderDetail.customerName;
            customerTel = orderDetail.customerTel;
            orderRange = string.Format("{0}-{1}", orderDetail.oderTime.ToString("yyyy-MM-dd"), orderDetail.OrderDeadTime.ToString("yyyy-MM-dd"));

            BindOrderCaiPinDetail(orderDetail.OrderID, orderDetail.OrderCaipinDetail);
        }

        /// <summary>
        /// 绑定菜品详情
        /// </summary>
        /// <param name="caipinDetail"></param>
        public void BindOrderCaiPinDetail(int orderId, Dictionary<int, List<OrderCaipinDetail>> caipinDetail)
        {
            if (caipinDetail == null || caipinDetail.Count == 0)
                return;

            string str = string.Empty;

            str += @"<p>订单号：9310849202720</p>
                    <p>退款商品：脆鸡八分堡套餐</p>
                    <p>退款数量：1</p>
                    <p>商品单价：26元</p>";
            


            this.detail.InnerHtml = str;
        }

        protected void Refund_Click(object sender, EventArgs e)
        {

        }
    }


}