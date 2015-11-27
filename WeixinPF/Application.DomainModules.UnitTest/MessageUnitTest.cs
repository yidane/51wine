using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeiXinPF.Application.DomainModules.Message;
using WeiXinPF.Application.DomainModules.Message.Dtos;
using WeiXinPF.BLL;
using WeiXinPF.Model;
using WeiXinPF.Model.message;

namespace Application.DomainModules.UnitTest
{
    [TestClass]
    public class MessageUnitTest
    {
        readonly IShortMsgService _shortMsgService = new ShortMsgService();
        [TestMethod]
        public void SendMsg_WithOrderANdHotel()
        {
            
            WeiXinPF.Model.wx_hotel_dingdan order=new WeiXinPF.Model.wx_hotel_dingdan()
            {
                id = 10,
                hotelid = 2,
                OrderNumber = "H20151021104424303340917",
                arriveTime = DateTime.Now.AddDays(-1),
                leaveTime = DateTime.Now,
                roomType = "豪华标准间",
                orderNum = 2,
                openid = "obzTsw5qxlbwGYYZJC9b-91J-X1Y"

            };
            WeiXinPF.Model.wx_hotels_info hotel=new WeiXinPF.Model.wx_hotels_info()
            {
                id = 2,
                hotelName = "喀纳斯山庄"
            };
            var sendMsg = SendMsg(order,hotel);
            Assert.IsTrue(sendMsg);
        }


        /// <summary>
        /// 发送消息给酒店
        /// </summary>
        /// <param name="order"></param>
        /// <param name="hotel"></param>
        private bool SendMsg(WeiXinPF.Model.wx_hotel_dingdan order, WeiXinPF.Model.wx_hotels_info hotel)
        {
            bool result = false;
            WeiXinPF.BLL.wx_hotel_admin dBll = new WeiXinPF.BLL.wx_hotel_admin();
            WeiXinPF.Model.wx_hotel_admin hotelAdmin = null;
            var users = dBll.GetModelList(String.Format(" HotelId={0}", order.hotelid));
            hotelAdmin = users.FirstOrDefault();
            if (hotelAdmin != null)
            {
                var msg = new ShortMsgDto()
                {
                    Title = "订单管理",
                    Content = String.Format("订单编号为{0}的订单需订购{1}至{2}{3}{4}间，请您确认是否接受！",
                        order.OrderNumber, order.arriveTime.Value.ToString("yyyy/MM/dd"),
                        order.leaveTime.Value.ToString("yyyy/MM/dd"),
                        order.roomType, order.orderNum
                        ),
                    Type = "HotelOrder",
                    MenuType = "hotel_room",
                    IsShowButton = true,
                    ButtonText = "马上去处理",
                    ButtonUrl = String.Format("/admin/hotel/hotel_dingdan_cz.aspx?id={0}&hotelid={1}",
                        order.id, order.hotelid),
                    ButtonMutipleUrl = "/admin/hotel/hotel_dingdan_manage.aspx",
                    FromUserId = order.openid,
                    ToUserId = hotelAdmin.ManagerId.ToString(),
                    MsgToUserType = MsgUserType.Hotel,
                    MsgFromUserType = MsgUserType.WeChatCustomer
                };
                _shortMsgService.SendMsg(msg);
                result = true;
            }

            return result;
        }
    }
}
