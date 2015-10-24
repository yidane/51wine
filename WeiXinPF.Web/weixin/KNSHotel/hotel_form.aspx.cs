using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.BLL;

namespace WeiXinPF.Web.weixin.KNSHotel
{
    public partial class hotel_form : WeiXinPage
    {
        public int hotelid = 0;
        public string openid = "";
        public int roomid = 0;
        BLL.wx_hotel_room roombll = new BLL.wx_hotel_room();
        Model.wx_hotel_room room = new Model.wx_hotel_room();
        BLL.wx_hotels_info hotelbll = new BLL.wx_hotels_info();
        Model.wx_hotels_info hotel = new Model.wx_hotels_info();

        BLL.wx_hotel_roompic picbll = new BLL.wx_hotel_roompic();

        BLL.wx_hotel_dingdan dingdanbll= new wx_hotel_dingdan();

        public int numdingdan = 0;
        public string roomtype = "";
        public string yuanjia = "";
        public string xianjia = "";
        public string peitao = "";
        public string hoteltel = "";
        public string tupian = "";
        public string tabid = "";
        public string jieshao = string.Empty;
        public string UseInstruction = string.Empty;
        public string RefundRule = string.Empty;


        public decimal price3 = 0;

        public decimal totalyuanjia = 0;
        public decimal ttaljiesheng = 0;
        public decimal totalPrice = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            hotelid = MyCommFun.RequestInt("hotelid");
            openid = MyCommFun.QueryString("openid");
            roomid = MyCommFun.RequestInt("roomid");

            if (!Page.IsPostBack)
            {
                SetUserLastInfo(openid);
                DataSet dr = dingdanbll.GetList(openid, hotelid);
                if (dr.Tables[0].Rows.Count > 0)
                {
                    numdingdan = dr.Tables[0].Rows.Count;
                }
                else
                {
                    numdingdan = 0;
                }

                list(roomid, hotelid);
            }

        }

        private void SetUserLastInfo(string openid)
        {
            var dingdan = dingdanbll.GetLastUserModel(openid);
            if (dingdan!=null)
            {
                oderName.Value = dingdan.oderName;
                identityNumber.Value = dingdan.IdentityNumber;
                tel.Value = dingdan.tel;
            }
        }

        public void list(int roomid, int hotelid)
        {
            room = roombll.GetModel(roomid);
            hotel = hotelbll.GetModel(hotelid);
            if (hotel!=null)
            {
                hoteltel = hotel.hotelPhone;
                jieshao = hotel.hotelIntroduct;
                
            }
            if (room!=null)
            {
                roomtype = room.roomType;
                yuanjia = room.roomPrice.ToString();
                xianjia = room.salePrice.ToString();
                price3 = Convert.ToDecimal(yuanjia) - Convert.ToDecimal(xianjia);
                peitao = room.facilities;

                UseInstruction = room.UseInstruction;
                RefundRule = room.RefundRule;

                
            }
            DataSet dr = picbll.GetList(roomid);
            if(dr.Tables[0].Rows.Count >0)
            {
                int j = 0;
                for (int i = 0; i < dr.Tables[0].Rows.Count;i++ )
                {
                    tupian += "  <li><p>" + dr.Tables[0].Rows[i]["title"].ToString() + "</p><a href=\"" + dr.Tables[0].Rows[i]["roomPictz"].ToString() + "\"><img src=\"" + dr.Tables[0].Rows[i]["roomPic"].ToString() + "\"></a></li>";
                    j += 1;
                    if (i==0)
                    {
                        tabid += "<li class='active'>" + j.ToString() + "</li>";
                    }
                    else
                    {
                        tabid += "<li>" + j.ToString() + "</li>";
                    }
                }
            }
            
        }
    }
}