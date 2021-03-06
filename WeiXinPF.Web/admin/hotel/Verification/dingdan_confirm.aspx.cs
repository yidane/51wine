﻿using Microsoft.JScript;
using WeiXinPF.Model.KNSHotel;
using WeiXinPF.Web.weixin.restaurant;

namespace WeiXinPF.Web.admin.hotel.Verification
{
    using System;

    using WeiXinPF.Application.DomainModules.IdentifyingCode.Service;
    using WeiXinPF.Common;

    public partial class dingdan_confirm : Web.UI.ManagePage
    {
        public int hotelid = 0;
        public string Dingdanlist = "";
        public string dingdanren = "";

        public int wid = 0;

        public const string ModuleName = "hotel";

        protected void Page_Load(object sender, EventArgs e)
        {
            hotelid =  this.GetHotelId();
            wid = this.GetWeiXinCode().id;
            confirmnumber.CausesValidation = true;
        }


        #region 验证订单
        protected void confirm_dingdan_Click(object sender, EventArgs e)
        {
            var number = this.confirmnumber.Text.Trim();

            var identifyingCode = IdentifyingCodeService.GetConfirmIdentifyingCodeInfo(this.hotelid, number, ModuleName, wid);

            if (identifyingCode != null)
            {
                var order = new BLL.wx_hotel_dingdan().GetModel(int.Parse(identifyingCode.OrderId));

                if (order != null)
                {
                    if (order.orderStatus.Value.Equals(HotelStatusManager.OrderStatus.Refunded.StatusId)
                        || order.orderStatus.Value.Equals(HotelStatusManager.OrderStatus.Refunding.StatusId)
                        || order.orderStatus.Value.Equals(HotelStatusManager.OrderStatus.Completed))
                    {
                        this.Response.Write(
                            "<script language='javascript' type='text/javascript'>alert('该订单已完成或进行退单处理，不能进行验证！')</script>");
                    }
                    else
                    {
                        if (identifyingCode.Status != 1)
                        {
                            if (identifyingCode.Status == 0)
                            {
                                this.Response.Write("<script language='javascript' type='text/javascript'>alert('该商品未付款！')</script>");
                            }
                            else
                            {
                                this.Response.Write("<script language='javascript' type='text/javascript'>alert('该商品已消费或者退单，请确认！')</script>");

                            }
                        }
                        else
                        {
                            this.Response.Redirect("commodity_detail.aspx?cid=" + identifyingCode.IdentifyingCodeId + "&shopid=" + identifyingCode.ShopId + "&id=" + identifyingCode.OrderId);
                        }
                    }
                }
                else
                {
                    this.Response.Write("<script language='javascript' type='text/javascript'>alert('该订单不存在或未付款，请确认！')</script>");
                }
            }
            else
            {
                this.Response.Write("<script language='javascript' type='text/javascript'>alert('该订单不存在或未付款，请确认！')</script>");
            }            
        }

        protected void confirmnumber_Validating()
        {

        }
        #endregion
    }
}