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
    public partial class diancai_orderDetail : System.Web.UI.Page
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

            var builder = new StringBuilder();

            int index = 1;
            foreach (KeyValuePair<int, List<OrderCaipinDetail>> pair in caipinDetail)
            {
                if (pair.Value != null && pair.Value.Count > 0)
                {
                    builder.Append(@"<section>");
                    builder.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"cpbiaoge\">");
                    builder.Append("<tr>");
                    builder.AppendFormat("<td class=\"cc\" style=\"width: 5%\">{0}</td>", index);
                    builder.AppendFormat("<td class=\"cc\" style=\"width: 33%\">{0}</td>", pair.Value[0].cpName);
                    builder.AppendFormat("<td class=\"cc\" style=\"width: 17%\">{0}</td>", pair.Value.Count);
                    builder.AppendFormat("<td class=\"cc\" style=\"width: 25%\">{0}元</td>", pair.Value.Count * pair.Value[0].price);
                    builder.Append("<td class=\"cc\" style=\"width: 23%\">");
                    builder.AppendFormat("<a class='btn btn-success' href=\"diancai_refund.aspx?shopid={0}&dingdan={1}&openid={2}&caiid={3}\">申请退款</a>", shopid, orderId, openid, pair.Key);//组合订单ID和菜品ID作为Button的主键
                    builder.Append("</td>");
                    builder.Append("</tr>");
                    builder.Append("</table>");
                    builder.Append("<div class=\"silde-background \" id='silde-module-" + pair.Key + "'>");
                    builder.Append("<div class=\"swiper-container gpd-ablum swiper-container-horizontal\">");
                    builder.AppendFormat("<div class=\"swiper-wrapper silde-center\" style=\"transition-duration: 0ms; transform: translate3d(0px, 0px, 0px);\" id=\"div-{0}-{1}\">", orderId, pair.Key);

                    foreach (OrderCaipinDetail detail in pair.Value)
                    {
                        builder.Append("<div class='swiper-slide'>");
                        builder.AppendFormat("<img id='Img3' class='img-border' src=\"ErCodeHandler.ashx?key={0}\">", detail.identifyingcode);
                        builder.Append("</div>");
                    }   
                   
                    builder.Append("</div>");
                    builder.Append("<div class='swiper-pagination'></div>");
                       
                    builder.Append("</div>");
                    builder.Append("</div>");
                    builder.Append("</section>");
                }
                index++;
            }

            this.detail.InnerHtml = builder.ToString();
        }

        protected void Refund_Click(object sender, EventArgs e)
        {
            
        }
    }

    /// <summary>
    /// 订单详情对象
    /// </summary>
    public class OrderDetail
    {
        public string customerName { get; set; }
        public string customerTel { get; set; }
        public string orderNumber { get; set; }
        public int OrderID { get; set; }
        public double payAmount { get; set; }
        public DateTime oderTime { get; set; }
        /// <summary>
        /// 过期时间为订单时间之后一年
        /// </summary>
        public DateTime OrderDeadTime
        {
            get { return oderTime.AddYears(1); }
        }

        public string hotelName { get; set; }
        public string dcRename { get; set; }
        public double xplace { get; set; }
        public double yplace { get; set; }
        public string tel { get; set; }
        public DateTime hoteltimeBegin { get; set; }
        public DateTime hoteltimeEnd { get; set; }
        public DateTime hoteltimeBegin1 { get; set; }
        public DateTime hoteltimeEnd1 { get; set; }
        public DateTime hoteltimeBegin2 { get; set; }
        public DateTime hoteltimeEnd2 { get; set; }

        public Dictionary<int, List<OrderCaipinDetail>> OrderCaipinDetail = new Dictionary<int, List<OrderCaipinDetail>>();

        public void GetDetail(int orderId)
        {
            var result = new BLL.wx_diancai_dingdan_manage().GetOrderDetail(orderId);
            if (result != null && result.Tables.Count > 0)
            {
                var orderTable = result.Tables[0];
                var orderCaipinTable = result.Tables[1];

                if (orderTable.Rows.Count > 0)
                {
                    var orderTableFirstRow = orderTable.Rows[0];
                    this.OrderID = orderTableFirstRow.Field<int>("OrderID");
                    this.customerName = orderTableFirstRow.Field<string>("customerName");
                    this.customerTel = orderTableFirstRow.Field<string>("customerTel");
                    this.dcRename = orderTableFirstRow.Field<string>("dcRename");
                    this.hotelName = orderTableFirstRow.Field<string>("hotelName");
                    this.hoteltimeBegin = orderTableFirstRow.Field<DateTime>("hoteltimeBegin");
//                    this.hoteltimeBegin1 = orderTableFirstRow.Field<DateTime>("hoteltimeBegin1");
//                    this.hoteltimeBegin2 = orderTableFirstRow.Field<DateTime>("hoteltimeBegin2");
                    this.hoteltimeEnd = orderTableFirstRow.Field<DateTime>("hoteltimeEnd");
//                    this.hoteltimeEnd1 = orderTableFirstRow.Field<DateTime>("hoteltimeEnd1");
//                    this.hoteltimeEnd2 = orderTableFirstRow.Field<DateTime>("hoteltimeEnd2");
                    this.oderTime = orderTableFirstRow.Field<DateTime>("oderTime");
                    this.orderNumber = orderTableFirstRow.Field<string>("orderNumber");
                    this.payAmount = orderTableFirstRow.Field<double>("payAmount");
                    this.tel = orderTableFirstRow.Field<string>("tel");
                    this.xplace = orderTableFirstRow.Field<double>("xplace");
                    this.yplace = orderTableFirstRow.Field<double>("yplace");
                }

                if (orderCaipinTable.Rows.Count > 0)
                {
                    foreach (DataRow row in orderCaipinTable.Rows)
                    {
                        var newOrderCaipinDetail = new OrderCaipinDetail();
                        newOrderCaipinDetail.caiId = row.Field<int>("caiId");
                        newOrderCaipinDetail.identifyingcode = row.Field<string>("identifyingcode");
                        newOrderCaipinDetail.cpName = row.Field<string>("cpName");
                        newOrderCaipinDetail.price = row.Field<decimal>("price");
                        newOrderCaipinDetail.status = row.Field<int>("status");

                        if (this.OrderCaipinDetail.ContainsKey(newOrderCaipinDetail.caiId))
                        {
                            this.OrderCaipinDetail[newOrderCaipinDetail.caiId].Add(newOrderCaipinDetail);
                        }
                        else
                        {
                            this.OrderCaipinDetail.Add(newOrderCaipinDetail.caiId, new List<OrderCaipinDetail>() { newOrderCaipinDetail });
                        }
                    }
                }
            }
        }
    }

    public class OrderCaipinDetail
    {
        public int caiId { get; set; }
        public string cpName { get; set; }
        public string identifyingcode { get; set; }
        public decimal price { get; set; }
        public int status { get; set; }
    }
}