using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeiXinPF.Web.weixin.KNSHotel
{
    public partial class hotel_order_edite : WeiXinPage
    {
        public int hotelid = 0;
        public string openid = "";
        public string image = "";
       
        public string order = "";
        public int numdingdan = 0;
        public string dingdanid = "";
        BLL.wx_hotel_dingdan dingdanbll = new BLL.wx_hotel_dingdan();
        Model.wx_hotel_dingdan dingdan = new Model.wx_hotel_dingdan();
        public string createtime = "";
        public string zhuangtai = "";
        public string roomtype = "";
        public decimal yuanjia = 0;
        public decimal price = 0;
        public decimal jiesheng = 0;
        public int roomid = 0;

        public decimal totalyuanjia = 0;
        public decimal totaljiesheng = 0;
        public decimal totalPrice = 0;

        public string truename = string.Empty;

        public string tel = string.Empty;

        public string arriveTime = string.Empty;
        public string leaveTime = string.Empty;

        public int nums = 0;

        public string IdentityNumber { get; set; }

        public string OrderNumber { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            hotelid = MyCommFun.RequestInt("hotelid");
            openid = MyCommFun.QueryString("openid");
            dingdanid = MyCommFun.QueryString("dingdanid");
            roomid = MyCommFun.RequestInt("roomid");

            if(!IsPostBack)
            {
                var info = new BLL.wx_hotels_info().GetModel(hotelid);

                if (info != null)
                {
                    image = info.topPic;
                }
                
                //this.dingdanidnum.Value = dingdanid;
                //this.roomidnum.Value = roomid.ToString();
                //this.hotelidnum.Value = hotelid.ToString();
                //this.openidnum.Value = openid.ToString();

                getdingdan(dingdanid);
            }

        }

        public void getdingdan(string dingdanid)
        {
            int id = Convert.ToInt32(dingdanid);
            dingdan = dingdanbll.GetModel(id);
            if(dingdan!=null)
            {
                createtime = dingdan.orderTime.ToString();


                if (dingdan.orderStatus != null)
                {
                    var status = StatusManager.OrderStatus.GetStatusDict(dingdan.orderStatus.Value);
                    zhuangtai = "<em class=\"" + status.CssClass + "\">" + status.StatusName + "</em>";
                }

                //                    if(dingdan.orderStatus==0)
                //                    {
                //                        zhuangtai = "<em class=\"no\">待确认</em>";
                //                       
                //                    }
                //                    else if (dingdan.orderStatus == 1)
                //                    {
                //                        zhuangtai = "<em class=\"ok\">已确认</em>";
                //                    }
                //                    else if (dingdan.orderStatus == 2)
                //                    {
                //                        zhuangtai = "<em class=\"fail\">已拒绝</em>";
                //                    }
                //                    else if (dingdan.orderStatus == 3)
                //                    {
                //                        zhuangtai = "<em class=\"fail\">已付款</em>";
                //                    }
                //                    else
                //                    {
                //                        return;
                //                    }

                this.truename = dingdan.oderName;
                    this.tel = dingdan.tel;
                    this.arriveTime = dingdan.arriveTime.Value.ToString("yyyy/MM/dd");
                this.leaveTime= dingdan.leaveTime.Value.ToString("yyyy/MM/dd");
                this.IdentityNumber = dingdan.IdentityNumber;
                this.OrderNumber = dingdan.OrderNumber;
                    roomtype = dingdan.roomType;
                    this.nums = dingdan.orderNum.Value;
                    yuanjia = Convert.ToDecimal(dingdan.yuanjia);
                    price = Convert.ToDecimal(dingdan.price);
                    jiesheng = (yuanjia - price) * Convert.ToDecimal(dingdan.orderNum);

            
             
                    this.info.Value = dingdan.remark;

                //总花费
                var dateSpan = dingdan.leaveTime.Value- dingdan.arriveTime.Value ;
                this.totalPrice = dingdan.price.Value * dingdan.orderNum.Value * dateSpan.Days;
                this.totalyuanjia = dingdan.yuanjia.Value * dingdan.orderNum.Value * dateSpan.Days;
                this.totaljiesheng = totalyuanjia- totalPrice;

            }
        }
    }
}