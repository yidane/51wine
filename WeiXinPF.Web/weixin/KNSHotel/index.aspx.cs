﻿using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeiXinPF.Web.weixin.KNSHotel
{
    public partial class index : WeiXinPage
    {
        public int hotelid = 0;
        public string openid = "";

        public int wid = 0;
        BLL.wx_hotels_info hotelbll = new BLL.wx_hotels_info();
        Model.wx_hotels_info hotel = new Model.wx_hotels_info();

        BLL.wx_hotel_room roombll = new BLL.wx_hotel_room();
        public string yuding = "";
        public string shuoming = "";
        public string dizhi = "";
        public string xplace = "";
        public string yplace = "";
        public string image = "";
        public string tel = "";
        public string numdingdan = "";
        public int dingdannum = 0;
        public bool listMode = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            hotelid = MyCommFun.RequestInt("hotelid");
            wid = MyCommFun.RequestInt("wid");
            openid = MyCommFun.QueryString("openid");

            string thisUrl = MyCommFun.getWebSite() + "/weixin/KNSHotel/index.aspx" + Request.Url.Query;
            var bll = new BLL.wx_userweixin();
            this.wid = MyCommFun.RequestWid();
            Model.wx_userweixin uWeiXinModel = bll.GetModel(wid);
            OAuth2BaseProc(uWeiXinModel, "index", thisUrl);

            if (!Page.IsPostBack)
            {

                BLL.wx_hotel_dingdan dingdanbll = new BLL.wx_hotel_dingdan();
                DataSet dr = dingdanbll.GetList(openid, hotelid);
                if (dr.Tables[0].Rows.Count > 0)
                {
                    dingdannum = dr.Tables[0].Rows.Count;
                    numdingdan = "  <ul class=\"round\">";
                    numdingdan += "<li><a href=\"hotel_order.aspx?openid=" + openid + "&hotelid=" + hotelid + "\">  ";
                    numdingdan += "<span>我的订单<em class=\"ok\">" + dr.Tables[0].Rows.Count + "</em></span></a></li>";
                    numdingdan += " </ul>";
                }
                else
                {
                    numdingdan = "";
                }


                if (hotelid != 0)
                {
                    hoteList(hotelid);
                }
            }
        }

        public void hoteList(int hotelid)
        {


            hotel = hotelbll.GetModel(hotelid);
            if (hotel != null)
            {
                //listMode
                shuoming = hotel.orderRemark;
                dizhi = hotel.hotelAddress;
                xplace = hotel.xplace.ToString();
                yplace = hotel.yplace.ToString();
                image = hotel.topPic;
                tel = hotel.hotelPhone;
                listMode = hotel.listMode;
                yuding += " <li class=\"title\"><span class=\"none\">" + hotel.hotelName.ToString() + "</span></li>";

                if (!hotel.listMode)
                {
                    yuding += "<li class=\"biaotou\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
                    yuding += "<tr><td>类型</td><td class=\"yuanjia\">原价</td><td class=\"youhuijia\">优惠价</td></tr></table></li>";
                }
               

                var list = roombll.GetModelList(" hotelid= "+hotelid+" and Status=4");
                if (list.Count > 0)
                {

                    if (hotel.listMode == false)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {

                            yuding += "<li class=\"dandanb\"><a href=\"hotel_form.aspx?roomid=" + list[i].id + "&hotelid=" + hotel.id.ToString() + "&openid=" + openid + "\"><span>";
                            yuding += "<table class=\"jiagebiao\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
                            yuding += "<tr><td>" + list[i].roomType  + "";
                            yuding += " <p class='break-word'>" + list[i].indroduce  + "</b></td>";
                            yuding += " <td class=\"yuanjia\">￥" + list[i].roomPrice  + "</td><td class=\"youhuijia\">￥" + list[i].salePrice   + "</td>";
                            yuding += "</tr></table></span></a></li>";
                        }
                    }

                    else
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            yuding += "   <li class=\"dandanb\"><a href=\"hotel_form.aspx?roomid=" + list[i].id
                                + "&hotelid=" + hotel.id.ToString() + "&openid=" + openid + "\"><span>";
                            yuding += " <table class=\"jiagebiao\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
                            yuding += " <tr>";
                            yuding += " <td><div>" + list[i].roomType + "</div>";

                            BLL.wx_hotel_roompic roompic = new BLL.wx_hotel_roompic();
                            DataSet pic = roompic.GetList(list[i].id);
                            if (pic.Tables[0].Rows.Count > 0)
                            {
                                yuding += " <div><img src=\"" + pic.Tables[0].Rows[0]["roompic"] + "\" class=\"showimg\">";
                            }
                            else
                            {
                                yuding += " <div><img src=\"\" class=\"showimg\">";
                            }


                            yuding += " <p class='break-word'>" + list[i].indroduce + "</p>";
                            yuding += " <p>原价：<a class=\"yuanjia\">￥" + list[i].roomPrice + "</a></p>";
                            yuding += " <p>优惠价：<a class=\"youhuijia\">￥" + list[i].salePrice + "</a></p></td>";
                            yuding += " </tr>";
                            yuding += " </table>";
                            yuding += " </span></a>";
                            yuding += " </li>";
                        }
                    }
                }

            }

        }
    }
}