using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Model.KNSHotel;

namespace WeiXinPF.Web.weixin.KNSHotel
{
    public partial class hotel_order : WeiXinPage
    {
        public int hotelid = 0;
        public int roomid = 0;
        public string openid = "";
        public string image = "";
        BLL.wx_hotel_dingdan dingdanbll = new BLL.wx_hotel_dingdan();
        public string order="";
        public int numdingdan = 0;
 

        protected void Page_Load(object sender, EventArgs e)
        {
            hotelid = MyCommFun.RequestInt("hotelid");
            openid = MyCommFun.QueryString("openid");
            roomid = MyCommFun.RequestInt("roomid");
            if (!Page.IsPostBack)
            {

                BLL.wx_hotels_info infobll = new BLL.wx_hotels_info();
                Model.wx_hotels_info info = new Model.wx_hotels_info();
                info = infobll.GetModel(hotelid);
                if (info!=null)
                {               
                image = info.topPic;
                }

                BLL.wx_hotel_dingdan dingdanbll = new BLL.wx_hotel_dingdan();
                DataSet dr = dingdanbll.GetList(openid, hotelid);
                if (dr.Tables[0].Rows.Count > 0)
                {
                    numdingdan = dr.Tables[0].Rows.Count;
                }
                else
                {
                    numdingdan = 0;
                }


                List(openid, hotelid);
            }
        }

        public void List(string openid,int hotelid)
        {

            DataSet dr = dingdanbll.GetList(openid,hotelid);
            if(dr.Tables[0].Rows.Count >0)
            {
                order += "  <ul class=\"round\"> ";
                for(int i=0;i<dr.Tables[0].Rows.Count;i++)
                {

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
                            + "<b>" + orderTime + "</b>" + "<b style='margin-left:0.5rem'>" + dr.Tables[0].Rows[i]["hotelName"].ToString() + "</b>";//05月29日 9时39分
                    }

                    var orderStatus = dr.Tables[0].Rows[i].Field<int>("orderStatus");
                    var status = HotelStatusManager.OrderStatus.GetStatusDict(orderStatus);
                    order += "<em class=\"" + status.CssClass + "\">" + status.StatusName + "</em></span>";
                    //                    if (dr.Tables[0].Rows[i]["orderStatus"].ToString() == "0")
                    //                    {
                    //                        order += "<li class=\"title\"><a href=\"hotel_order_edite.aspx?dingdanid=" + dr.Tables[0].Rows[i]["id"].ToString() + "&hotelid=" + hotelid + "&roomid=" + roomid + "&openid=" + openid + "\"><span>" + dr.Tables[0].Rows[i]["createDate"].ToString() + "订单详情";//05月29日 9时39分
                    //                    }
                    //                    else
                    //                    {
                    //                        order += "<li class=\"title\"><a href=\"hotel_order_xianshi.aspx?dingdanid=" + dr.Tables[0].Rows[i]["id"].ToString() + "&hotelid=" + hotelid + "&roomid=" + roomid + "&openid=" + openid + "\"><span>" + dr.Tables[0].Rows[i]["createDate"].ToString() + "订单详情";//05月29日 9时39分
                    //                    }
                    //
                    //                 
                    //                    if(dr.Tables[0].Rows[i]["orderStatus"].ToString()=="1")
                    //                    {
                    //                        order += "<em class=\"ok\">成功</em></span>";  
                    //                    
                    //                    }
                    //                    else if (dr.Tables[0].Rows[i]["orderStatus"].ToString() == "0")
                    //                    {
                    //                        order += "<em class=\"no\">未处理</em></span>";  
                    //                    }
                    //                    else if (dr.Tables[0].Rows[i]["orderStatus"].ToString() == "2")
                    //                    {
                    //                        order += "<em class=\"error\">失败</em></span>";
                    //                    }

                    //                    order += "</a></li><li><div class=\"text\"><p>预约商家：" + dr.Tables[0].Rows[i]["hotelName"].ToString() + "</p>";
                    //                    order += "<p>类型：" + dr.Tables[0].Rows[i]["roomType"].ToString() + "</p><p>预订数量：" + dr.Tables[0].Rows[i]["orderNum"].ToString() + "间</p>";
                    //                    order += "<p>预定日期：" + dr.Tables[0].Rows[i]["orderTime"].ToString() + "</p></div></li>";

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