using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Application.DomainModules.Hotel;
using WeiXinPF.Application.DomainModules.Hotel.DTOS;
using WeiXinPF.Model;
using WeiXinPF.Model.KNSHotel;

namespace WeiXinPF.Web.admin.hotel
{
    public partial class hotel_dingdan_cz : Web.UI.ManagePage
    {
        public int dingdanid = 0;
        BLL.wx_hotel_dingdan dingdanbll = new BLL.wx_hotel_dingdan();
        protected Model.wx_hotel_dingdan dingdan = new Model.wx_hotel_dingdan();
        public string ordername = "";
        public string openid = "";
        public string beizhu = "";
        public int hotelid = 0;
        public string arriveTime = string.Empty;
        public string leaveTime = string.Empty;
        public string Dingdanlist = "";
        public string Dingdanren = "";
        public bool isAdmin = false;//判断是景区还是商户
        public int orderStatus = -1;
        public StatusDict status;
        public TuidanDto tuidan = new TuidanDto();
        public string uName = string.Empty;
        public string roleName = string.Empty;



        protected void Page_Load(object sender, EventArgs e)
        {
            isAdmin = IsWeiXinCode();
            dingdanid = MyCommFun.RequestInt("id");
            hotelid = MyCommFun.RequestInt("hotelid");
            if (!IsPostBack)
            {
                GetOrderList(dingdanid);
                GetUserMsg(dingdan);
                GetOrderStatusMsg(dingdan);
            }
        }

        private void GetOrderStatusMsg(wx_hotel_dingdan dingdan)
        {
            orderStatus = dingdan.orderStatus.Value;
            //支付状态下默认退款金额为订单总额
            if (isAdmin && orderStatus == HotelStatusManager.OrderStatus.Payed.StatusId)
            {



                //总花费
                var dateSpan = dingdan.leaveTime.Value - dingdan.arriveTime.Value;
                var totalPrice = dingdan.price.Value * dingdan.orderNum.Value * dateSpan.Days;
                txtAmount.Text = totalPrice.ToString();
            }
            else if (orderStatus == HotelStatusManager.OrderStatus.Refunding.StatusId
                     || orderStatus == HotelStatusManager.OrderStatus.Refunded.StatusId)
            {
                var hotelService = new HotelService();
                tuidan = hotelService.GetModel(dingdan.id, dingdan.hotelid.Value);
                if (tuidan.operateUser > 0)
                {
                    var manager = new BLL.manager().GetModel(tuidan.operateUser);
                    uName = manager.real_name;
                    roleName = new WeiXinPF.BLL.manager_role().GetTitle(manager.role_id);
                }
            }
        }

        private void GetOrderList(int dingdanid)
        {

            dingdan = dingdanbll.GetModel(dingdanid);
            //            if (dingdan != null)
            //            {
            //                ordername = dingdan.oderName;
            //                openid = dingdan.openid;
            //                beizhu = dingdan.remark;
            //                arriveTime = string.Format("{0:yyyy/MM/dd HH:mm}", dingdan.arriveTime);
            //                leaveTime = string.Format("{0:yyyy/MM/dd HH:mm}", dingdan.leaveTime);
            //            }
            //            else
            //            {
            //                dingdan = new Model.wx_hotel_dingdan();
            //            }

            if (dingdan != null)
            {

                decimal amount = 0;
                var rowcount = 1;
                Dingdanlist += "<tr><th>商品名称</th><th class=\"cc\">购买数量</th><th class=\"cc\">单价</th><th class=\"cc\">入住时间</th><th class=\"cc\">离店时间</th> </tr>";
                for (int i = 0; i < rowcount; i++)
                {
                    arriveTime = string.Format("{0:yyyy/MM/dd HH:mm}", dingdan.arriveTime);
                    leaveTime = string.Format("{0:yyyy/MM/dd HH:mm}", dingdan.leaveTime);
                    Dingdanlist += " <tr><td>" + dingdan.roomType + "</td>";
                    Dingdanlist += "<td class=\"cc\">" + dingdan.orderNum + "</td>";
                    Dingdanlist += "<td class=\"cc\">￥" + dingdan.price + "</td>";
                    Dingdanlist += "<td class=\"cc\">" + arriveTime + "</td>";
                    Dingdanlist += "<td class=\"cc\">" + leaveTime + "</td>";

                    Dingdanlist += "</tr>";
                    //总花费
                    var dateSpan = dingdan.leaveTime.Value - dingdan.arriveTime.Value;
                    var totalPrice = dingdan.price.Value * dingdan.orderNum.Value * dateSpan.Days;

                    amount += totalPrice;
                }


                Dingdanlist += "<tr><td></td><td ></td><td ></td><td ></td><td class=\"rr\">总计：<span class='text-danger total-money'>￥" + amount + "</span></td></tr>";
            }
        }

        private void GetUserMsg(Model.wx_hotel_dingdan manage)
        {

            //订单信息
            if (manage != null)
            {
              var  createTime = string.Format("{0:yyyy/MM/dd HH:mm}", dingdan.createDate);
                var hotel = new BLL.wx_hotels_info().GetModel(manage.hotelid.Value);
                Dingdanren += "<tr> <td>酒店商户或门店：" + hotel.hotelName + "</td></tr>";
                Dingdanren += "<tr> <td>商户或门店编号：" + hotel.HotelCode + "</td></tr>";
                Dingdanren += "<tr><td width=\"70\">订单编号： " + manage.orderNum + "</td></tr>";
                Dingdanren += "<tr> <td>交易日期：" + createTime + "</td></tr>";
                Dingdanren += "<tr><td>预定人：" + manage.oderName + "</td></tr>";
                Dingdanren += "<tr><td>电话：" + manage.tel + "</td></tr>";
                //                dingdanren += "<tr><td>地址：" + manage.address + "</td></tr>";
                //                dingdanren += "<tr><td>备注 ：" + manage.oderRemark + "</td></tr>";

                status = HotelStatusManager.OrderStatus.GetStatusDict(manage.orderStatus.Value);
                Dingdanren += "<tr><td>订单状态：<em  style='width:70px;' class='" + status.CssClass
                    + "'>" + status.StatusName + "</em></td></tr>";


            }
            else
            {
                Dingdanren += "<tr> <td>酒店商户或门店：</td></tr>";
                Dingdanren += "<tr> <td>商户或门店编号：</td></tr>";
                Dingdanren += "<tr><td width=\"70\">订单编号：</td></tr>";
                Dingdanren += "<tr> <td>交易日期：</td></tr>";
                Dingdanren += "<tr><td>预定人：</td></tr>";
                Dingdanren += "<tr><td>电话：</td></tr>";

                Dingdanren += "<tr><td>订单状态：<em  style='width:70px;' class='no'>未处理</em></td></tr>";
            }
        }

        protected void save_groupbase_Click(object sender, EventArgs e)
        {
            dingdanid = MyCommFun.RequestInt("id");
            string status = StatusType.SelectedItem.Value;
            dingdanbll.Update(dingdanid, status);

            AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改酒店状态为" + status + "，主键为" + dingdanid); //记录日志
            JscriptMsg("订单修改成功！", "hotel_dingdan_manage.aspx?hotelid=" + hotelid + "", "Success");
        }

        protected void btnSaveRefund_OnClick(object sender, EventArgs e)
        {
            var wxUserweixin = GetAdminInfo();
            if (wxUserweixin == null)
            {
                throw new Exception("用户不能为空！");
            }
            if (chkIsRefund.Checked)
            {
                double money = MyCommFun.Str2Float(txtAmount.Text);
                var hotelService = new HotelService();
                dingdan = dingdanbll.GetModel(dingdanid);
                var dto = new TuidanDto()
                {

                    dingdanid = dingdan.id,
                    hotelid = dingdan.hotelid.Value,
                    roomid = dingdan.roomid.Value,
                    openid = dingdan.openid,
                    operateUser = wxUserweixin.id,
                    refundAmount = money,
                    refundTime = DateTime.Now,
                    remarks = this.remarks.InnerText

                };
                hotelService.AddTuidan(dto);

                dingdanbll.Update(dingdan.id, HotelStatusManager.OrderStatus.Refunding.StatusId.ToString());

                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改酒店状态为" +
                    HotelStatusManager.OrderStatus.Refunding.StatusName
                    + HotelStatusManager.OrderStatus.Refunding.StatusId + "，主键为" + dingdanid); //记录日志
                JscriptMsg("退款成功！", "hotel_dingdan_manage.aspx?hotelid=" + hotelid + "", "Success");
            }
        }


        protected void btn_completed_OnClick(object sender, EventArgs e)
        {

            dingdan = dingdanbll.GetModel(dingdanid);
            dingdanbll.Update(dingdan.id, HotelStatusManager.OrderStatus.Completed.StatusId.ToString());

            AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改酒店状态为" +
                   HotelStatusManager.OrderStatus.Completed.StatusName
                   + HotelStatusManager.OrderStatus.Completed.StatusId + "，主键为" + dingdanid); //记录日志
            JscriptMsg("修改成功！", "hotel_dingdan_manage.aspx?hotelid=" + hotelid + "", "Success");
        }
    }
}