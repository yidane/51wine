using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Application.DomainModules.IdentifyingCode.Service;
using WeiXinPF.Infrastructure.DomainDataAccess;
using WeiXinPF.Infrastructure.DomainDataAccess.IdentifyingCode;
using WeiXinPF.Infrastructure.DomainDataAccess.Payment;
using WeiXinPF.Model;
using WeiXinPF.Model.KNSHotel;

namespace WeiXinPF.Web.weixin.KNSHotel
{
    public partial class hotel_order_xianshi : WeiXinPage
    {
        public int hotelid = 0;
        public string openid = "";
        public string image = "";

        public string order = "";
        public int numdingdan = 0;
        public string dingdanid = "";
        BLL.wx_hotel_dingdan dingdanbll = new BLL.wx_hotel_dingdan();
        Model.wx_hotel_dingdan dingdan = new Model.wx_hotel_dingdan();
        BLL.wx_hotel_room roombll=new BLL.wx_hotel_room();
        Model.wx_hotel_room room = new wx_hotel_room();
        public string createtime = "";
        public string zhuangtai = "";
        public string roomtype = "";
        public decimal yuanjia = 0;
        public decimal price = 0;
        public decimal jiesheng = 0;
        public int roomid = 0;
        public string truename = "";
        public string tel = "";
        public string rztime = "";
        public string ldtime = "";
        public string  num = "";
        public string beizhu = "";
        public int BtnShowStatus = 0;//默认0什么都没，1只显示删除按钮,2微信支付
        public bool isShowQRCode = false;
        public string OrderNumber;
        public decimal PayAmount;
        public string AlertOrderMsg=String.Empty;
        public string VerificationCode = String.Empty;
        public string jieshao = String.Empty;
        public string address = "";
        public string xplace = "";
        public string yplace = "";
        public bool isShowContent = false; 
        public string UseInstruction = string.Empty;
        public string RefundRule = string.Empty;
        public string IdentityNumber = string.Empty;

        public decimal totalyuanjia = 0;
        public decimal totaljiesheng = 0;
        public decimal totalPrice = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            hotelid = MyCommFun.RequestInt("hotelid");
            openid = MyCommFun.QueryString("openid");
            dingdanid = MyCommFun.QueryString("dingdanid");
            roomid = MyCommFun.RequestInt("roomid");

            if (!IsPostBack)
            {

                BLL.wx_hotels_info infobll = new BLL.wx_hotels_info();
                Model.wx_hotels_info info = new Model.wx_hotels_info();
                info = infobll.GetModel(hotelid);
                
                image = info.topPic;
                jieshao = info.hotelIntroduct;
                address = info.hotelAddress;
                xplace = info.xplace.ToString();
                yplace = info.yplace.ToString();

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
                this.dingdanidnum.Value = dingdanid;
             

                getdingdan(dingdanid);
            }

        }

        public void getdingdan(string dingdanid)
        {
            int id = Convert.ToInt32(dingdanid);
            dingdan = dingdanbll.GetModel(id);
            if (dingdan != null)
            {
                if (dingdan.roomid != null)
                {
                    room = roombll.GetModel(dingdan.roomid.Value);
                    if (room!=null)
                    {
                        UseInstruction = room.UseInstruction;
                        RefundRule = room.RefundRule;
                    }
                }
                createtime = dingdan.orderTime.ToString();

                if (dingdan.orderStatus != null)
                {
                    var orderStatus = dingdan.orderStatus.Value;
                    var status = HotelStatusManager.OrderStatus.GetStatusDict(orderStatus);
                    zhuangtai = "<em class=\"" + status.CssClass + "\">" + status.StatusName + "</em></span>";

                    //判断按钮
                    ShowBtnStatus(orderStatus);
                    ShowQRCode(orderStatus);
                    ShowAlertMsg(orderStatus);
                    ShowContent(orderStatus);

                    GetVerificationCode(dingdan);
                }
 
//
//                if (dingdan.orderStatus == 0)
//                {
//                    zhuangtai = "<em class=\"no\">待确认</em>";
//
//                }
//                else if (dingdan.orderStatus == 1)
//                {
//                    zhuangtai = "<em class=\"ok\">已确认</em>";
//                }
//                else if (dingdan.orderStatus == 2)
//                {
//                    zhuangtai = "<em class=\"fail\">已拒绝</em>";
//                }
//                else if (dingdan.orderStatus == 3)
//                {
//                    zhuangtai = "<em class=\"fail\">已付款</em>";
//                }
//                else
//                {
//                    return;
//                }

                truename = dingdan.oderName;
                tel = dingdan.tel;
                if (dingdan.arriveTime != null) rztime = dingdan.arriveTime.Value.ToString("yyyy/MM/dd");
                roomtype = dingdan.roomType;
                if (dingdan.leaveTime != null) ldtime = dingdan.leaveTime.Value.ToString("yyyy/MM/dd");
                num = dingdan.orderNum.ToString();
                yuanjia = Convert.ToDecimal(dingdan.yuanjia);
                price = Convert.ToDecimal(dingdan.price);
                jiesheng = (yuanjia - price) * Convert.ToDecimal(dingdan.orderNum);

                beizhu = dingdan.remark;

                OrderNumber = dingdan.OrderNumber;
                IdentityNumber = dingdan.IdentityNumber;
                if (dingdan.price != null) PayAmount = dingdan.price.Value;


                //总花费
                var dateSpan = dingdan.leaveTime.Value - dingdan.arriveTime.Value;
                this.totalPrice = dingdan.price.Value * dingdan.orderNum.Value * dateSpan.Days;
                this.totalyuanjia = dingdan.yuanjia.Value * dingdan.orderNum.Value * dateSpan.Days;
                this.totaljiesheng = totalyuanjia - totalPrice;

            }
        }

        /// <summary>
        /// 显示详情
        /// </summary>
        /// <param name="orderStatus"></param>
        private void ShowContent(int orderStatus)
        {
            if (orderStatus == HotelStatusManager.OrderStatus.Payed.StatusId)
            {
                isShowContent = true;
            }
            
        }

        /// <summary>
        /// 获取验证码
        /// todo:添加功能
        /// </summary>
        /// <param name="wxHotelDingdan"></param>
        private void GetVerificationCode(wx_hotel_dingdan wxHotelDingdan)
        {
            if (wxHotelDingdan.orderStatus == HotelStatusManager.OrderStatus.Payed.StatusId)
            {
                var wxHotelsInfo = new BLL.wx_hotels_info().GetModel(wxHotelDingdan.hotelid.Value);
                var list=  IdentifyingCodeService.GetIdentifyingCodeInfoByOrderId
                    (wxHotelDingdan.hotelid.Value, "hotel",
                    wxHotelDingdan.id.ToString(), wxHotelsInfo.wid.Value);
                foreach (var code in list)
                {
                    //根据验证码状态， 显示在界面的状态
                    int showStatus = 0;
                    switch (code.Status)
                    {
                        case 0:
                        case 1:
                            showStatus = 1;
                            break;
                        case 2:
                            showStatus = 2;
                            break;
                        case 3:
                        case 4:
                            showStatus = 3;
                            break;

                    }
                    VerificationCode += string.Format(@"<div class='swiper-slide'>
                                  <input type ='hidden' value='{0}' status='{1}' />
                                   </div>", code.IdentifyingCode, showStatus);
                }

                 
            }
            
            
        }

        private void ShowAlertMsg(int orderStatus)
        {
            if (orderStatus == HotelStatusManager.OrderStatus.Refused.StatusId)
            {
                AlertOrderMsg = @"
        <div class='alert alert-warning' role='alert'>
      <strong> 抱歉!</strong> 您选购的房型商家已确认无房！欢迎您重新订购。
         </div>
     ";
            }
            else if (orderStatus == HotelStatusManager.OrderStatus.Accepted.StatusId)
            {
                AlertOrderMsg = @"
        <div class='alert alert-warning' role='alert'>
      <strong> 特别提醒!</strong> 订单只为您保留1个小时，请您尽快支付！
         </div>
     ";
            }
            else if (orderStatus == HotelStatusManager.OrderStatus.Payed.StatusId)
            {
                AlertOrderMsg = @"
        <div class='alert alert-warning' role='alert'>
      <strong> 特别提醒!</strong> 抱歉，由于景区酒店房源紧张，已付款订单不能申请退款，需电话联系酒店工作人员解决。

         </div>
     ";
            }
        }

        /// <summary>
        /// 根据订单状态显示二维码
        /// </summary>
        /// <param name="orderStatus"></param>
        private void ShowQRCode(int orderStatus)
        {
            if (orderStatus== HotelStatusManager.OrderStatus.Payed.StatusId)
            {
                isShowQRCode = true;
            }
        }

        /// <summary>
        /// 根据订单状态显示不同按钮
        /// </summary>
        /// <param name="orderStatus"></param>
        public void ShowBtnStatus(int orderStatus)
        {
            if (orderStatus == HotelStatusManager.OrderStatus.Pending.StatusId
                || orderStatus == HotelStatusManager.OrderStatus.Refused.StatusId)
            {
                BtnShowStatus = 1;
            }
            else if (orderStatus == HotelStatusManager.OrderStatus.Accepted.StatusId )
            {
                BtnShowStatus = 2;
            }
            else
            {
                BtnShowStatus = 0;
            }
        }
    }
}