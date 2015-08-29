﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travel.Application.OrderUnitTest
{
    using NUnit.Framework;

    using Travel.Application.DomainModules.Order.Core;
    using Travel.Application.DomainModules.Order.Entity;
    using Travel.Infrastructure.WeiXin.Advanced.Pay.Model;

    [TestFixture]
    public class OrderUnitTest
    {
        public OrderRequestEntity OrderRequest;

        public PaymentNotify PaymentNotify;

        [SetUp]
        public void Init()
        {
            this.OrderRequest = new OrderRequestEntity()
                               {
                                   OpenId = "obzTswxzFzzzdWdAKf2mWx3CrpXk",
                                   TicketCategory = "956BFF5E-AA6A-454E-8F46-BD4175235C9E",
                                   TicketName = "布尔津测试门票",
                                   Count = 1,
                                   CouponId = string.Empty,
                                   ContactPersonName = "龚博",
                                   MobilePhoneNumber = "11111",
                                   IdentityCardNumber = "21212121212121"
                               };

            this.PaymentNotify = new PaymentNotify()
                                     {
                                         appid = "wx2421b1c4370ec43b",
                                         attach = "测试",
                                         bank_type = "CFT",
                                         fee_type = "CNY",
                                         is_subscribe = "Y",
                                         mch_id = "100000100",
                                         nonce_str = "5d2b6c2a8db53831f7eda20af46e531c",
                                         openid = "obzTswxzFzzzdWdAKf2mWx3CrpXk",
                                         out_trade_no = "C2015082920020045701619",
                                         result_code = "SUCCESS",
                                         return_code = "SUCCESS",
                                         sign = "B552ED6B279343CB493C5DD0D78AB241",
                                         time_end = "20150829131540",
                                         total_fee = 260,
                                         trade_type = "JSAPI",
                                         transaction_id = "1409811653"
                                     };
        }

        [Test]
        public void CreateOrderCode_CreateOrder_ReturnUnRepeatOrderCode()
        {
            var otaOrder = new OTAOrder(this.OrderRequest);

            otaOrder.CreateOrderMain();
        }

        [Test]
        public void GetPaymentNotify_PaymentNotify_ReturnVoid()
        {
            var wxPay = new WXPaymentOperate();

            wxPay.GetPaymentCompleteInfomation(this.PaymentNotify);
        }

        [Test]
        public void fdfd()
        {
            

            
            var sbv = string.Format("{{'return_code':'{0}', 'return_msg':'{1}'}}", "FALL", "参数格式校验错误或签名失败");
            
        }
    }
}