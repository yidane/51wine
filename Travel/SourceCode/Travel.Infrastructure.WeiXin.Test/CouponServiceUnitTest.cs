using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Travel.Application.DomainModules.Coupon;
using Travel.Application.DomainModules.Coupon.DTOS;
using Travel.Infrastructure.WeiXin.Extra;
using Travel.Application.DomainModules.WeChat.DTOS;
using System.Collections.Generic;
using System.Threading;

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
            var service = new CouponService();
            var user = new UserInfoDTO()
            {
                openid = openId,

                nickname = "JX",
                city = "北京"
            };
            CouponDTO couponDto = service.GetRandomCoupon(user);
            Assert.IsNotNull(couponDto);

            ////第二次优惠券已发完
            //couponDto = service.GetRandomCoupon(user);
            //Assert.IsNull(couponDto);
        }

        /// <summary>
        /// 定义委托
        /// </summary>
        /// <param name="user">用户</param>
        delegate void MakeStaticDelegate(string user);

        /// <summary>
        /// 这里是测试静态方法
        /// </summary>
        /// <param name="user">用户</param>
        private static void MakeStaticTest(string user)
        {
            GetCoupon_ReturnRandomCoupon100();
        }

        /// <summary>
        /// 这里是模拟多用户同时点击并发
        /// </summary>
        public static void DoTest()
        {
            // 模拟3个用户的并发操作
            MakeStaticDelegate makeStaticDelegate1 = new MakeStaticDelegate(MakeStaticTest);
            makeStaticDelegate1.BeginInvoke("user1", null, null);
            MakeStaticDelegate makeStaticDelegate2 = new MakeStaticDelegate(MakeStaticTest);
            makeStaticDelegate2.BeginInvoke("user2", null, null);
            MakeStaticDelegate makeStaticDelegate3 = new MakeStaticDelegate(MakeStaticTest);
            makeStaticDelegate3.BeginInvoke("user3", null, null);
            System.Console.ReadLine();
        }


        static void GetCoupon_ReturnRandomCoupon100()
        {
            int i = 0,count=100;
            var service = new CouponService();
            while (i < count)
            {
                string openId = Guid.NewGuid().ToString().Replace("-", "");
             
                var user = new UserInfoDTO()
                {
                    openid = openId,
                    nickname = "JX",
                    city = "北京"
                };
                CouponDTO couponDto = service.GetRandomCoupon(user);
                i++;
            }
        }

        [TestMethod]
        public void GetCoupon_ReturnRandomCoupon()
        {

             GetCoupon_ReturnRandomCoupon100();
            //for (int i = 0; i < 3; i++)
            //{
            //    Thread t = new Thread(GetCoupon_ReturnRandomCoupon100);
            //    t.Start(i);
            //   // Interlocked.Increment(ref threadsRunning);
            //}
            // ThreadPool.QueueUserWorkItem(new WaitCallback(GetCoupon_ReturnRandomCoupon100),null);
            //ThreadPool.QueueUserWorkItem(new WaitCallback(GetCoupon_ReturnRandomCoupon100),null);
            //ThreadPool.QueueUserWorkItem(new WaitCallback(GetCoupon_ReturnRandomCoupon100),null);
            //Thread thread1 = new Thread(GetCoupon_ReturnRandomCoupon100);
            //thread1.Start();

            //Thread thread2 = new Thread(GetCoupon_ReturnRandomCoupon100);
            //thread2.Start();

            //Thread thread3 = new Thread(GetCoupon_ReturnRandomCoupon100);
            //thread3.Start();

        }

        [TestMethod]
        public void GetCouponList_ReturnList_ByUser()
        {
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

            Assert.IsTrue(dto.ExpiredCoupons.Count > 0);
            Assert.IsTrue(dto.UnExpiredCoupons.Count > 0);

        }



      

    }


}
