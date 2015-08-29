using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;
using Travel.Application.DomainModules.Coupon;
using Travel.Application.DomainModules.Coupon.DTOS;
using Travel.Application.DomainModules.WeChat;
using Travel.Application.DomainModules.WeChat.DTOS;
using Travel.Infrastructure.CommonFunctions.Ajax;
using Travel.Infrastructure.WeiXin.Extra;

namespace Travel.Services.WebService
{
    public class CouponWebService : BaseWebService
    {
        /// <summary>
        /// 获取随机优惠券
        /// </summary>
        [WebMethod]
        public void GetRandomCoupon(string access_code)
        {


            try
            {
                //string strWXUser=Context.Request.QueryString[""];
                UserInfoDTO user = null;
                if (!string.IsNullOrEmpty(access_code))
                {
                    var weChatService = new WeChatService();
                    var tokenResult = weChatService.GetAccessToken(access_code);
                    user = weChatService.GetUserInfo(tokenResult.openid);
                }
                else
                {
                    var url = "http://wx.qlogo.cn/mmopen/g3MonUZtNHkdmzicIlibx6iaFqAc56vxLSUfpb6n5WKSYVY0ChQKkiaJSgQ1dZuTOgvLLrhJbERQQ4eMsv84eavHiaiceqxibJxCfHe/0";
                    url = url.Substring(0, url.LastIndexOf("/") ) + "/64";
                    user = new UserInfoDTO() {
                        openid="testid",
                        nickname="金小西",
                        headimgurl= url
                    };
                }


                //string openId = "testOpenId";
                //var user = new WxUserInfo()
                //{
                //    OpenId = tokenResult.openid,
                //    Username = "JXiaoX",
                //    NickName = "JX",
                //    City = "北京"
                //};


                var service = new CouponService();

                var couponDto = service.GetRandomCoupon(user);
                var data = new
                {
                    coupon = couponDto,
                    user = user
                };
                Context.Response.Write(AjaxResult.Success(data));


            }
            catch (Exception ex)
            {
                Context.Response.Write(AjaxResult.Error(ex.Message));
            }
        }

        /// <summary>
        /// 接收优惠券
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="couponId"></param>
        [WebMethod]
        public void ReceiveCoupon(string openid, string couponId)
        {


            try
            {


               
               

                //var user = weChatService.GetUserInfo(openId);
                var service = new CouponService();
                Guid id;
                if (!Guid.TryParse(couponId, out id))
                {
                    throw new Exception("指定的优惠券不正确!");
                }
                service.AddCouponToUser(openid, id);
                Context.Response.Write(AjaxResult.Success(true));


            }
            catch (Exception ex)
            {
                Context.Response.Write(AjaxResult.Error(ex.Message));
            }
        }


        /// <summary>
        /// 我的优惠券列表
        /// </summary>
        /// <param name="access_code"></param>
        [WebMethod]
        public void GetCouponList(string access_code)
        {
            try
            {
                //string strWXUser=Context.Request.QueryString[""];
                UserInfoDTO user = null;
                if (!string.IsNullOrEmpty(access_code))
                {
                    var weChatService = new WeChatService();
                    var tokenResult = weChatService.GetAccessToken(access_code);
                    user = weChatService.GetUserInfo(tokenResult.openid);
                }
                else
                {
                    var url = "http://wx.qlogo.cn/mmopen/g3MonUZtNHkdmzicIlibx6iaFqAc56vxLSUfpb6n5WKSYVY0ChQKkiaJSgQ1dZuTOgvLLrhJbERQQ4eMsv84eavHiaiceqxibJxCfHe/0";
                    url = url.Substring(0, url.LastIndexOf("/")) + "/64";
                    user = new UserInfoDTO()
                    {
                        openid = "testid",
                        nickname = "金小西",
                        headimgurl = url
                    };
                }               


                var service = new CouponService();

                var dto = service.GetCouponList(user);
                var data = new {
                    lists= dto,
                    user=user
                };
                
                Context.Response.Write(AjaxResult.Success(data));


            }
            catch (Exception ex)
            {
                Context.Response.Write(AjaxResult.Error(ex.Message));
            }
        }

        /// <summary>
        /// 获取优惠券详细
        /// </summary>
        /// <param name="access_code"></param>
        /// <param name="couponId"></param>
        [WebMethod]
        public void GetCoupon(string openId, string couponId)
        {
            try
            {


                //string strWXUser=Context.Request.QueryString[""];
                //UserInfoDTO user = null;
                //if (!string.IsNullOrEmpty(access_code))
                //{
                //    var weChatService = new WeChatService();
                //    var tokenResult = weChatService.GetAccessToken(access_code);
                //    user = weChatService.GetUserInfo(tokenResult.openid);
                //}
                //else
                //{
                //    var url = "http://wx.qlogo.cn/mmopen/g3MonUZtNHkdmzicIlibx6iaFqAc56vxLSUfpb6n5WKSYVY0ChQKkiaJSgQ1dZuTOgvLLrhJbERQQ4eMsv84eavHiaiceqxibJxCfHe/0";
                //    url = url.Substring(0, url.LastIndexOf("/")) + "/64";
                //    user = new UserInfoDTO()
                //    {
                //        openid = "testid",
                //        nickname = "金小西",
                //        headimgurl = url
                //    };
                //}
                //var user = weChatService.GetUserInfo(openId);
                var service = new CouponService();
                Guid id;
                if (!Guid.TryParse(couponId, out id))
                {
                    throw new Exception("指定的优惠券不正确!");
                }
                var coupon = service.GetCoupon(openId, id);
                var data = new {
                    coupon=coupon
                   // user=user
                };
                Context.Response.Write(AjaxResult.Success(data));


            }
            catch (Exception ex)
            {
                Context.Response.Write(AjaxResult.Error(ex.Message));
            }
        }

        [WebMethod]
        public void UseCoupon(string openId, string couponId) {
            try
            {


                
                //var user = weChatService.GetUserInfo(openId);
                var service = new CouponService();
                Guid id;
                if (!Guid.TryParse(couponId, out id))
                {
                    throw new Exception("指定的优惠券不正确!");
                }
                service.UseCoupon(openId, id);
                Context.Response.Write(AjaxResult.Success(true));


            }
            catch (Exception ex)
            {
                Context.Response.Write(AjaxResult.Error(ex.Message));
            }
        }
    }
}
