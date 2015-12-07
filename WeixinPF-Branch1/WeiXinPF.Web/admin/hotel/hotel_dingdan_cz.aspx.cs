using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.MobileControls;
using System.Web.UI.WebControls;
using OneGulp.WeChat.MP.AdvancedAPIs;
using OneGulp.WeChat.MP.TenPayLibV3;
using Travel.Infrastructure.WeiXin.Advanced.Pay.Model;
using WeiXinPF.Application.DomainModules.Hotel;
using WeiXinPF.Application.DomainModules.Hotel.DTOS;
using WeiXinPF.Application.DomainModules.IdentifyingCode.Service;
using WeiXinPF.Infrastructure.DomainDataAccess.IdentifyingCode.DTO;
using WeiXinPF.Model;
using WeiXinPF.Model.KNSHotel;
using WeiXinPF.WeiXinComm;

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
        public string ordermsg = string.Empty;
        private IList<IdentifyingCodeDTO> listCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            var adminInfo = GetAdminInfo();
            if (adminInfo != null)
            {
                isAdmin = adminInfo.role_id == 13;//景区管理员
            }
            dingdanid = MyCommFun.RequestInt("id");
            hotelid = MyCommFun.RequestInt("hotelid");
            if (Request.Form["__EVENTTARGET"] == "btn_completed")
            {
                // Fire event
                btn_completed_OnClick(this, new EventArgs());
            }
            listCode = IdentifyingCodeService.GetIdentifyingCodeDTO("hotel", dingdanid);
            GetData(dingdanid);

            if (!IsPostBack)
            {
                GetOrderStatusMsg(dingdan);
            }

        }

        private void GetData(int dingdanid)
        {
            dingdan = dingdanbll.GetModel(dingdanid);
            GetOrderList(dingdanid);
            GetUserMsg(dingdan);
            //            GetOrderStatusMsg(dingdan);
        }

        /// <summary>
        /// 获取订单状态
        /// </summary>
        /// <param name="dingdan"></param>
        private void GetOrderStatusMsg(wx_hotel_dingdan dingdan)
        {
            orderStatus = dingdan.orderStatus.Value;
            //支付状态下默认退款金额为订单总额
            if (orderStatus == HotelStatusManager.OrderStatus.Payed.StatusId)
            {
                var result = GetPrice(dingdan);

                txtAmount.Text = result.ToString();
                if (isAdmin)
                {
                    hidConfirmStr.Value = "确定执行【退款】操作吗？";

                    if (listCode != null && listCode.Any(c => c.Status == "已使用"))//订单中有验证码已使用
                    {
                        hidConfirmStr.Value = string.Format("{0}，{1}", "订单中有验证码已使用", hidConfirmStr.Value);
                    }
                }
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

        /// <summary>
        /// 获取最大可退价格
        /// </summary>
        /// <param name="dingdan"></param>
        private decimal GetPrice(wx_hotel_dingdan dingdan)
        {
            decimal result = 0;

            var count = GetCodeCount(dingdan);
            //总花费
            var dateSpan = dingdan.leaveTime.Value - dingdan.arriveTime.Value;
            //            var totalPrice = dingdan.price.Value * (dingdan.orderNum.Value - count) * dateSpan.Days;
            var totalPrice = dingdan.price.Value * (dingdan.orderNum.Value) * dateSpan.Days;
            result = totalPrice;
            return result;
        }

        /// <summary>
        /// 获取验证码剩余数量
        /// </summary>
        /// <returns></returns>
        private int GetCodeCount(wx_hotel_dingdan dingdan)
        {
            var count = 0;

            var wxHotelsInfo = new BLL.wx_hotels_info().GetModel(dingdan.hotelid.Value);
            var listCodes = IdentifyingCodeService.GetIdentifyingCodeInfoByOrderId
               (dingdan.hotelid.Value, "hotel",
               dingdan.id.ToString(), wxHotelsInfo.wid.Value);

            //查询状态为已使用的
            var usedCode = listCodes.Where(t => t.Status == 2);

            if (usedCode.Any())
            {
                count = dingdan.orderNum.Value - usedCode.Count();


            }
            else
            {
                count = dingdan.orderNum.Value;
            }

            if (count <= 0)
            {
                //                ordermsg = "房间已全部入住";
                //                ordermsg = string.Format(@"  <div class='alert alert-warning' role='alert'>
                //      <strong> 提示!</strong>  {0}
                //         </div>", ordermsg);
            }
            else
            {

            }
            return count;
        }

        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="dingdanid"></param>
        private void GetOrderList(int dingdanid)
        {

 
            if (dingdan != null)
            {
                if (listCode!=null&& listCode.Any())
                {
                    decimal amount = 0;
                    arriveTime = string.Format("{0:yyyy/MM/dd HH:mm}", dingdan.arriveTime);
                    leaveTime = string.Format("{0:yyyy/MM/dd HH:mm}", dingdan.leaveTime);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<tr><th>商品名称</th><th class=\"cc\">单价</th><th class=\"cc\">验证码</th><th class=\"cc\">是否验证</th><th class=\"cc\">入住时间</th><th class=\"cc\">离店时间</th> </tr>");
                    foreach (var code in listCode)
                    {
                        var isUserd = "未验证";
                        if (code.Status== "已使用")
                        {
                            isUserd = "已验证";
                        }
                        var codeCount = code.IdentifyingCode.Length - 4;
                        var icode = string.Format("****************{0}",
                            code.IdentifyingCode.Substring(codeCount, 4));
                        sb.Append(string.Format(" <tr><td>{0}</td>", dingdan.roomType));
                        sb.Append(string.Format("<td class=\"cc\">￥{0}</td>", dingdan.price));
                        sb.Append(string.Format("<td class=\"cc\">{0}</td>", icode));
                        sb.Append(string.Format("<td class=\"cc\">{0}</td>", isUserd));

                        sb.Append(string.Format("<td class=\"cc\">{0}</td>", arriveTime));
                        sb.Append(string.Format("<td class=\"cc\">{0}</td>", leaveTime));

                        sb.Append("</tr>");
                       
                    }
                    //总花费
                    var dateSpan = dingdan.leaveTime.Value - dingdan.arriveTime.Value;
                    var totalPrice = dingdan.price.Value * dingdan.orderNum.Value * dateSpan.Days;

                    amount += totalPrice;

                    sb.AppendFormat("<tr><td></td><td ></td><td ></td><td ></td><td ></td><td class=\"rr\">总计：<span class='text-danger total-money'>￥{0}</span></td></tr>", amount);
                    Dingdanlist = sb.ToString();
                }
                
            }
        }

        private void GetUserMsg(Model.wx_hotel_dingdan manage)
        {

            //订单信息
            if (manage != null)
            {
                var createTime = string.Format("{0:yyyy/MM/dd HH:mm}", dingdan.createDate);
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
            JscriptMsg("状态修改成功！", "hotel_dingdan_manage.aspx?hotelid=" + hotelid + "", "Success");
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

                var hotel = new BLL.wx_hotels_info().GetModel(dingdan.hotelid.Value);

                using (var scope = new TransactionScope())
                {
                    var dto = new TuidanDto()
                    {

                        dingdanid = dingdan.id,
                        hotelid = dingdan.hotelid.Value,
                        roomid = dingdan.roomid.Value,
                        openid = dingdan.openid,
                        wid = hotel.wid.Value,
                        operateUser = wxUserweixin.id,
                        refundAmount = money,
                        refundTime = DateTime.Now,
                        remarks = this.remarks.InnerText,
                        refundCode = "HT" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + Utils.Number(5)

                    };
                    hotelService.AddTuidan(dto);

                    string return_msg = String.Empty;

                    if (WeChatRefund(dingdan, dto, hotel.wid.Value, out return_msg))//
                    {
                        new BLL.wx_hotel_dingdan().RefundComplete(dingdan.OrderNumber);

                        AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改酒店状态为" +
                    HotelStatusManager.OrderStatus.Refunding.StatusName
                    + HotelStatusManager.OrderStatus.Refunding.StatusId + "，主键为" + dingdanid); //记录日志
                        JscriptMsg("退款成功！", "hotel_dingdan_manage.aspx?hotelid=" + hotelid + "", "Success");
                    }
                    else
                    {
                        Response.Write(return_msg);
                        GetData(dingdanid);
                    }
                    //                    dingdanbll.Update(dingdan.id, HotelStatusManager.OrderStatus.Refunding.StatusId.ToString());



                    scope.Complete();
                }










            }
        }

        /// <summary>
        /// 微信退单
        /// </summary>
        /// <param name="dingdan"></param>
        /// <param name="dto"></param>
        /// <param name="returnMsg"></param>
        /// <returns></returns>
        private bool WeChatRefund(wx_hotel_dingdan dingdan, TuidanDto dto, int wid, out string returnMsg)
        {
            bool result = false;
            returnMsg = null;

            var refundResult = dingdanbll.GetWeChatRefundParams(wid, dingdan.hotelid.Value, dingdan.id, dto.refundCode);

            //使用系统订单号退单
            if (refundResult != null && refundResult.Tables.Count > 0 && refundResult.Tables[0].Rows.Count > 0)
            {
                var orderNumber = refundResult.Tables[0].Rows[0]["orderNumber"].ToString();
                var transaction_id = refundResult.Tables[0].Rows[0]["transaction_id"].ToString();
                var refundAmount = Convert.ToInt32(refundResult.Tables[0].Rows[0]["refundAmount"]);
                var payAmount = Convert.ToInt32(refundResult.Tables[0].Rows[0]["payAmount"]);


                var wxModel = new BLL.wx_userweixin().GetModel(wid);
                var payInfo = new BLL.wx_payment_wxpay().GetModelByWid(wid);

                var requestHandler = new RequestHandler(null);
                requestHandler.SetParameter("out_trade_no", orderNumber);
                //requestHandler.SetParameter("transaction_id", transaction_id);
                requestHandler.SetParameter("out_refund_no", dto.refundCode);
                requestHandler.SetParameter("appid", wxModel.AppId);
                requestHandler.SetParameter("mch_id", payInfo.mch_id);//商户号
                requestHandler.SetParameter("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));

                //退款金额
                if (PayHelper.IsDebug)
                {
                    requestHandler.SetParameter("total_fee", (payAmount).ToString());
                    requestHandler.SetParameter("refund_fee", (refundAmount).ToString());
                }
                else
                {
                    requestHandler.SetParameter("total_fee", (payAmount * 100).ToString());
                    requestHandler.SetParameter("refund_fee", (refundAmount * 100).ToString());
                }

                requestHandler.SetParameter("op_user_id", wxModel.AppId);
                requestHandler.SetParameter("sign", requestHandler.CreateMd5Sign("key", payInfo.paykey));

                var refundInfo = TenPayV3.Refund(requestHandler.ParseXML(), string.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, payInfo.certInfoPath), payInfo.cerInfoPwd);
                var refundOrderResponse = new RefundOrderResponse(refundInfo);

                result = refundOrderResponse.IsSuccess;
                returnMsg = refundOrderResponse.return_msg;
            }

            return result;
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



        protected void btn_return_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("hotel_dingdan_manage.aspx?hotelid=" + hotelid);
        }
    }
}