using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Travel.Application.DomainModules.Coupon;
using Travel.Application.DomainModules.Coupon.DTOS;
using Travel.Infrastructure.WeiXin.Extra;
using Travel.Application.DomainModules.WeChat.DTOS;
using System.Collections.Generic;

namespace Travel.Infrastructure.WeiXin.Test
{
    [TestClass]
    public class CouponServiceUnitTest
    {
        [TestMethod]
        public void GetCoupon_ReturnRandomCoupon_ByOpenID()
        {
            //随机数量是1
            string openId = "obzTsw3oHcJPbZ8X3mCUnGuCFDOY";
            var service= new CouponService();
            var user = new UserInfoDTO() {
                openid=openId,
             
                nickname="JX",
                city="北京"
            };
            CouponDTO couponDto= service.GetRandomCoupon(user);
            Assert.IsNotNull(couponDto);

            ////第二次优惠券已发完
            //couponDto = service.GetRandomCoupon(user);
            //Assert.IsNull(couponDto);
        }

        [TestMethod]
        public void GetCouponList_ReturnList_ByUser() {
            //随机数量是1
            string openId = "testid";
            var service = new CouponService();
            var user = new UserInfoDTO()
            {
                openid = openId,

                nickname = "JX",
                city = "北京"
            };
            CouponListDTO dto = service.GetCouponList(user);
            Assert.IsNotNull(dto);

            Assert.IsTrue(dto.ExpiredCoupons.Count>0);
            Assert.IsTrue(dto.UnExpiredCoupons.Count > 0);

        }

    }
}
