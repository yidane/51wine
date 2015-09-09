using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using WeiXinPF.Application.DomainModules.Coupon;
using WeiXinPF.Common;

namespace WeiXinPF.WebService
{

    public class CouponWebService : BaseWebService
    {
        /// <summary>
        /// 获取基本信息
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="wid"></param>
        [WebMethod]
        public void GetBaseCouponInfo(int aid,int wid) {
            try
            {

               
                var service = new CouponService();

                var info = service.GetCouponBaseInfo( aid);
                
                Context.Response.Write(AjaxResult.Success(info));


            }
            catch (JsonException jsEx)
            {

                Context.Response.Write(AjaxResult.Error(jsEx.Message, jsEx.ErrorType));
            }
            catch (Exception ex)
            {
                var s = "";
                if (ex.InnerException != null)
                {
                    s = ex.InnerException.Message;
                }
                Context.Response.Write(AjaxResult.Error(s));
            }
        }

        /// <summary>
        /// 获取随机优惠券
        /// </summary>
        [WebMethod(EnableSession = true)]
        public void GetRandomCoupon(int aid, int wid,string code)
        {


            try
            {
                
                var user = GetUser(wid, "coupon");


                var service = new CouponService();

                var couponDto = service.GetRandomCoupon(user,aid,wid);
                var data = new
                {
                    coupon = couponDto,
                    user = user
                };
                Context.Response.Write(AjaxResult.Success(data));


            }
            catch (JsonException jsEx)
            {
                
                Context.Response.Write(AjaxResult.Error(jsEx.Message,jsEx.ErrorType));
            }
            catch (Exception ex)
            {
                var s = "";
                if (ex.InnerException != null)
                {
                    s = ex.InnerException.Message;
                }
                Context.Response.Write(AjaxResult.Error(s));
            }
        }

       


//        /// <summary>
//        /// 我的优惠券列表
//        /// </summary>
//        /// <param name="access_code"></param>
//        [WebMethod(EnableSession = true)]
//        public void GetCouponList(string access_code)
//        {
//            try
//            {
//                //string strWXUser=Context.Request.QueryString[""];
//                UserInfoDTO user = null;
//                if (!string.IsNullOrEmpty(access_code))
//                {
//                    var openId = GetOpenIDByCodeID(access_code);
//                    var weChatService = new WeChatService();

//                    user = weChatService.GetUserInfo(openId);
//                }
//#if DEBUG

//                else
//                {
//                    var url = "http://wx.qlogo.cn/mmopen/g3MonUZtNHkdmzicIlibx6iaFqAc56vxLSUfpb6n5WKSYVY0ChQKkiaJSgQ1dZuTOgvLLrhJbERQQ4eMsv84eavHiaiceqxibJxCfHe/0";
//                    url = url.Substring(0, url.LastIndexOf("/")) + "/64";
//                    user = new UserInfoDTO()
//                    {
//                        openid = "obzTsw_PZU5Q5NZqixFi6lB2YHkI",
//                        nickname = "金小西",
//                        headimgurl = url
//                    };
//                }
//#endif


//                var service = new CouponService();

//                var dto = service.GetCouponList(user);
//                var data = new
//                {
//                    lists = dto,
//                    user = user
//                };

//                Context.Response.Write(AjaxResult.Success(data));


//            }
//            catch (Exception ex)
//            {
//                Context.Response.Write(AjaxResult.Error(ex.Message));
//            }
//        }

//        /// <summary>
//        /// 获取优惠券详细
//        /// </summary>
//        /// <param name="access_code"></param>
//        /// <param name="couponId"></param>
//        [WebMethod]
//        public void GetCoupon(string openId, string couponUsageId)
//        {
//            try
//            {

//                var service = new CouponService();
//                Guid id;
//                if (!Guid.TryParse(couponUsageId, out id))
//                {
//                    throw new Exception("指定的优惠券不正确!");
//                }
//                var coupon = service.GetCoupon(openId, id);
//                var data = new
//                {
//                    coupon = coupon
//                    // user=user
//                };
//                Context.Response.Write(AjaxResult.Success(data));


//            }
//            catch (Exception ex)
//            {
//                Context.Response.Write(AjaxResult.Error(ex.Message));
//            }
//        }


//        /// <summary>
//        /// 使用优惠券
//        /// </summary>
//        /// <param name="openId"></param>
//        /// <param name="couponUsageId"></param>
//        [WebMethod]
//        public void UseCoupon(string openId, string couponUsageId)
//        {
//            try
//            {



//                //var user = weChatService.GetUserInfo(openId);
//                var service = new CouponService();
//                Guid id;
//                if (!Guid.TryParse(couponUsageId, out id))
//                {
//                    throw new Exception("指定的优惠券不正确!");
//                }
//                service.UseCoupon(openId, id);
//                Context.Response.Write(AjaxResult.Success(true));


//            }
//            catch (Exception ex)
//            {
//                Context.Response.Write(AjaxResult.Error(ex.Message));
//            }
//        }
    }
}
