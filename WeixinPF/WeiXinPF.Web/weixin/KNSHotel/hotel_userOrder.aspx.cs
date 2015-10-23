using System;
using System.Data;
using WeiXinPF.Common;

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
                if (string.Equals(type, "pay"))
                {
                    menuStr = string.Format(
                @"<a class='Pay' href='hotel_userOrder.aspx?openid={0}&wid={1}&type=all'>全部</a>
            <a class='Pay menu-active' href='hotel_userOrder.aspx?openid={0}&wid={1}&type=pay'>已付款</a>
            <a class='Refund' href='hotel_userOrder.aspx?openid={0}&wid={1}&type=refund'>退单</a>", openid, wid);
                     
                }
                else if (string.Equals(type, "refund"))
                {
                    menuStr = string.Format(
               @"<a class='Pay' href='hotel_userOrder.aspx?openid={0}&wid={1}&type=all'>全部</a>
            <a class='Pay' href='hotel_userOrder.aspx?openid={0}&wid={1}&type=pay'>已付款</a>
            <a class='Refund menu-active' href='hotel_userOrder.aspx?openid={0}&wid={1}&type=refund'>退单</a>", openid, wid);
                }
                else
                {
                    menuStr = string.Format(
               @" <a class='Pay menu-active' href='hotel_userOrder.aspx?openid={0}&wid={1}&type=all'>全部</a>
            <a class='Pay' href='hotel_userOrder.aspx?openid={0}&wid={1}&type=pay'>已付款</a>
            <a class='Refund' href='hotel_userOrder.aspx?openid={0}&wid={1}&type=refund'>退单</a>", openid, wid);
                }
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

        public void List(string openid,int wid)
        {

            DataSet dr = dingdanbll.GetUserOrderList(openid, wid, type);
            if(dr.Tables[0].Rows.Count >0)
            {
                order += "  <ul class=\"round\"> ";
                for(int i=0;i<dr.Tables[0].Rows.Count;i++)
                {
                    int hotelid = dr.Tables[0].Rows[i].Field<int>("hotelid");
                    var time = DateTime.Parse(dr.Tables[0].Rows[i]["orderTime"].ToString());
                    var orderTime = string.Format("{0:yyyy/MM/dd HH:mm}", time);
                    var arriveTime = string.Format("{0:yyyy/MM/dd HH:mm}", DateTime.Parse(dr.Tables[0].Rows[i]["arriveTime"].ToString()));
                    if (dr.Tables[0].Rows[i]["orderStatus"].ToString() == "0")
                    {
                        order += "<li class=\"title\"><a href=\"hotel_order_edite.aspx?dingdanid="
                            + dr.Tables[0].Rows[i]["id"].ToString() + "&hotelid=" + hotelid
                            + "&roomid=" + roomid + "&openid=" + openid + "\"><span>"
                           + "<b>" + orderTime + "</b>" + "<b style='margin-left:0.5rem'>" + dr.Tables[0].Rows[i]["hotelName"].ToString() + "</b>";//05月29日 9时39分
                    }
                    else
                    {
                        order += "<li class=\"title\"><a href=\"hotel_order_xianshi.aspx?dingdanid="
                            + dr.Tables[0].Rows[i]["id"].ToString() + "&hotelid=" + hotelid
                            + "&roomid=" + roomid + "&openid=" + openid + "\"><span>"
                            +  "<b>"+ orderTime + "</b>" + "<b style='margin-left:0.5rem'>" + dr.Tables[0].Rows[i]["hotelName"].ToString() + "</b>";//05月29日 9时39分
                    }

                    var orderStatus = dr.Tables[0].Rows[i].Field<int>("orderStatus");
                    var status = StatusManager.OrderStatus.GetStatusDict(orderStatus);
                    order += "<em class=\""+ status.CssClass +"\">" + status.StatusName + "</em></span>";

//                    if (status.StatusID==1)
//                    {
//                        order += "<em class=\"ok\">"+ status.StatusName+ "</em></span>";  
//                    
//                    }
//                    else if (status.StatusID == 0)
//                    {
//                        order += "<em class=\"no\">" + status.StatusName + "</em></span>";  
//                    }
//                    else if (status.StatusID ==2)
//                    {
//                        order += "<em class=\"error\">" + status.StatusName + "</em></span>";
//                    }

                    order += "</a></li><li><div class=\"text\">";
                    order += "<p>订单编号：" + dr.Tables[0].Rows[i]["OrderNumber"].ToString() + "</p>";
                    order += "<p>预约商家：" + dr.Tables[0].Rows[i]["hotelName"].ToString() + "</p>";
                    order += "<p>类型：" + dr.Tables[0].Rows[i]["roomType"].ToString() + "</p>";
                    order += "<p>数量：" + dr.Tables[0].Rows[i]["orderNum"].ToString() + "间</p>";
                    order += "<p>付款：" + dr.Tables[0].Rows[i]["price"].ToString() + "元</p>";
                    order += "<p>到店日期：" + arriveTime + "</p>";
                    order += "</div></li>";
                }

                order += " </ul> ";
            }
        }
    }
}