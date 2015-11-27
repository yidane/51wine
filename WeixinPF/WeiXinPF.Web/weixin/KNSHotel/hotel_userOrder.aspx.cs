using System;
using System.Data;
using System.Text;
using WeiXinPF.Common;
using WeiXinPF.Model.KNSHotel;

namespace WeiXinPF.Web.weixin.KNSHotel
{
    public partial class hotel_userOrder : WeiXinPage
    {
//        public int hotelid = 0;
        public int roomid = 0;
        public string openid = "";
        public int wid = 0;
       
        BLL.wx_hotel_dingdan dingdanbll = new BLL.wx_hotel_dingdan();
        public string order="";
        public int numdingdan = 0;
        public string type = "all";
        public string menuStr = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
//            hotelid = MyCommFun.RequestInt("hotelid");
            openid = MyCommFun.QueryString("openid");
            roomid = MyCommFun.RequestInt("roomid");
            wid = MyCommFun.RequestInt("wid");
            if (!string.IsNullOrEmpty(MyCommFun.QueryString("type")))
            {
                type = MyCommFun.QueryString("type");
            }
            var userbll = new BLL.wx_userweixin();
            Model.wx_userweixin uWeiXinModel = userbll.GetModel(wid);
            OAuth2BaseProc(uWeiXinModel, "hotel_userOrder", Request.Url.AbsoluteUri);

            if (!Page.IsPostBack)
            {
                menuStr = GetMenuStr(openid, wid, type);
                BLL.wx_hotels_info infobll = new BLL.wx_hotels_info();
                Model.wx_hotels_info info = new Model.wx_hotels_info();
                
               

                BLL.wx_hotel_dingdan dingdanbll = new BLL.wx_hotel_dingdan();
                DataSet dr = dingdanbll.GetUserOrderList(openid, wid,type);
                if (dr.Tables[0].Rows.Count > 0)
                {
                    numdingdan = dr.Tables[0].Rows.Count;
                }
                else
                {
                    numdingdan = 0;
                }


                List(openid, wid);
            }
        }

        private string GetMenuStr(string openid,int wid, string type)
        {
            string cssPay = "normal", cssRefund = "normal", cssAll ="normal";
            if (string.Equals(type, "pay"))
            {
                cssPay = "active";
            }
            else if (string.Equals(type, "refund"))
            {
                cssRefund = "active";
            }
            else
            {
                cssAll = "active";
            }
            return  string.Format(
               @"<li class='{2}'><a href='hotel_userOrder.aspx?openid={0}&wid={1}&type=all'>全部</a></li>
            <li class='{3}'><a href='hotel_userOrder.aspx?openid={0}&wid={1}&type=pay'>已付款</a></li>
            <li class='{4}'><a href='hotel_userOrder.aspx?openid={0}&wid={1}&type=refund'>退单</a></li>"
            , openid, wid, cssAll, cssPay,cssRefund);
        }

        public void List(string openid,int wid)
        {

            DataSet dr = dingdanbll.GetUserOrderList(openid, wid, type);
            DataTable dataTable = dr.Tables[0];
            if(dataTable.Rows.Count >0)
            {
                var detailBuilder = new StringBuilder();
                
                for(int i=0;i<dataTable.Rows.Count;i++)
                {
                    int id = dataTable.Rows[i].Field<int>("id");
                    int hotelid = dataTable.Rows[i].Field<int>("hotelid");
                    var time = DateTime.Parse(dataTable.Rows[i]["orderTime"].ToString());
                    var arriveTime = DateTime.Parse(dataTable.Rows[i]["arriveTime"].ToString());
                    var leaveTime = DateTime.Parse(dataTable.Rows[i]["leaveTime"].ToString());
                    var price = dataTable.Rows[i].Field<double>("price");
                    var orderNumber = dataTable.Rows[i].Field<string>("orderNumber");
                    var orderNum= dataTable.Rows[i].Field<int>("orderNum");
                    var hotelName = dataTable.Rows[i].Field<string>("hotelName");
                    //总花费
                    var dateSpan = leaveTime - arriveTime;
                    var totalPrice = price * orderNum * dateSpan.Days;
                    var orderTime = string.Format("{0:yyyy/MM/dd HH:mm}", time);
//                    var strArriveTime= string.Format("{0:yyyy/MM/dd HH:mm}",arriveTime);
//                    var strLeaveTime= string.Format("{0:yyyy/MM/dd HH:mm}",leaveTime);
                    var orderStatus = dataTable.Rows[i].Field<int>("orderStatus");
                    var status = HotelStatusManager.OrderStatus.GetStatusDict(orderStatus);


                    string alink;
                    detailBuilder.Append("<ul><li>"); 
                    if (dataTable.Rows[i]["orderStatus"].ToString() == "0")
                    {
                        alink = string.Format("<a href=\"hotel_order_edite.aspx?dingdanid={0}&hotelid={1}&roomid={2}&openid={3}\">"
                            , id, hotelid, roomid, openid);
                    }
                    else
                    {
                        alink = string.Format("<a href=\"hotel_order_xianshi.aspx?dingdanid={0}&hotelid={1}&roomid={2}&openid={3}\">"
                            , id, hotelid, roomid, openid);
                    }
                     
                    detailBuilder.Append(alink);
                    detailBuilder.Append("<div class=\"info_01\">");
                    detailBuilder.AppendFormat("<h3>{0}</h3>", hotelName);
                    detailBuilder.AppendFormat("<p>实付<b>￥{0}</b>共<b>{1}</b>件商品</p>", totalPrice, orderNum);
                    detailBuilder.Append("<span class=\"wave_blue_icon\"></span>");
                    detailBuilder.Append("</div>");
                    detailBuilder.Append("<div class=\"info_02\">");
                    detailBuilder.Append("<dl>");
                    detailBuilder.AppendFormat("<dd><b class=\"i_gray_icon\"></b>订单编号 {0}</dd>", orderNumber);
                    detailBuilder.AppendFormat("<dd><b class=\"time_gray_icon\"></b>购票日期 {0}</dd>", orderTime);
                    //此处应该有购票日期
                    detailBuilder.Append("</dl>");
                    detailBuilder.Append("</div>");
                    detailBuilder.Append("<div class=\"info_03\">");
                     
                    detailBuilder.AppendFormat("<span>{0}</span>", status.StatusName);
                    detailBuilder.Append("</div>");
                    detailBuilder.Append("</a>");
                    detailBuilder.Append("</li>");
                    detailBuilder.Append("</ul>");
                     
                }

                order = detailBuilder.ToString();
            }
        }
    }
}