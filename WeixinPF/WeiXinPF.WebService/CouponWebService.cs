using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using WeiXinPF.Application.DomainModules.Coupon;
using WeiXinPF.Common;
using WeiXinPF.Application.DomainModules.Coupon.DTOS;
using System.Configuration;

namespace WeiXinPF.WebService
{

    public class CouponWebService : BaseWebService
    {
        /// <summary>
        /// 获取基本信息
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="wid"></param>
        [WebMethod(EnableSession = true)]
        public void GetBaseCouponInfo(int aid, int wid, string code)
        {
            OAuthUserInfo user = null;
            try
            {


                var service = new CouponService();

                if (Session["User"] == null)
                {
                    user = GetUser(wid, "coupon");
                    if (user != null)
                    {
                        Session["User"] = user;
                    }

                }
                else
                {
                    user = Session["User"] as OAuthUserInfo;
                     
                }
                var info = service.GetCouponBaseInfo(aid,user);
                if (info != null)
                {
                    BLL.wx_dzpAwardItem itemBll = new BLL.wx_dzpAwardItem();
                    var itemlist = itemBll.GetModelList("actId=" + aid);//该活动的所有奖项信息
                    //获取当前对象实体
                    BLL.wx_userweixin bll = new BLL.wx_userweixin();
                    Model.wx_userweixin uWeiXinModel = bll.GetModel(wid);

                    //初始话jsskd
                    var signatureDto = jssdkInit(uWeiXinModel);
                    signatureDto.fxContent = info.brief;
                    signatureDto.fxImg =  info.beginPic;
                    signatureDto.fxTitle = info.actName;
                    var data = new
                    {
                        info = info,
                        itemlist = itemlist,
                        signature = signatureDto,
                        user = user
                    };
                    Context.Response.Write(AjaxResult.Success(data));
                }
                else
                {
                    throw new Exception("参数异常");
                }



            }
            catch (JsonException jsEx)
            {

                Context.Response.Write(AjaxResult.Error(jsEx.Message, jsEx.ErrorType));
            }
            catch (Exception ex)
            {
               
                Context.Response.Write(AjaxResult.Error(ex.Message));
            }
        }

        /// <summary>
        /// 获取随机优惠券
        /// </summary>
        [WebMethod(EnableSession = true)]
        public void GetRandomCoupon(int aid, int wid,string openid)
        {

            OAuthUserInfo user = null;
            try
            {
                var thisOpenid = string.Empty;
                if (Session["User"] == null)
                {
                    thisOpenid = openid;


                }
                else
                {
                    user = Session["User"] as OAuthUserInfo;
                    thisOpenid = user.openid;
                }

                if (Session["openid"] == null)
                {
                    
                    if (!string.IsNullOrEmpty(thisOpenid))
                    {
                        Session["openid"] = thisOpenid;
                    }

                }
                



                var service = new CouponService();

                var couponDto = service.GetRandomCoupon(thisOpenid, aid, wid);
                var data = new
                {
                    coupon = couponDto,
                    user = user
                };
                Context.Response.Write(AjaxResult.Success(data));


            }
            catch (JsonException jsEx)
            {
                CouponPrizeDTO dto = null;
                //可以摇时显示没奖时候消息
                var arr = new[] { "nomore", "notimes" };
                if (arr.Contains(jsEx.ErrorType))
                {
                    string jpname = ConfigurationManager.AppSettings["shakeLuckyMoney_defaultNulljpname"];
                    string jxname = ConfigurationManager.AppSettings["shakeLuckyMoney_defaultNulljxname"];
                    dto = new CouponPrizeDTO()
                    {
                        jpname = jpname,
                        jxname = jxname,
                        getTime=DateTime.Now.ToString("yy-MM-dd HH-mm-ss")

                    };
                }

                var data = new
                {
                    coupon = dto,
                    user = user
                };
                var result = AjaxResult.Error(jsEx.Message, jsEx.ErrorType);
                result.Data = data;
                Context.Response.Write(result);
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
        /// 我的优惠券列表
        /// </summary>
        /// <param name="access_code"></param>
        [WebMethod(EnableSession = true)]
        public void GetCouponList(string code,int wid)
        {
            string openid = string.Empty;
            try
            {


                var service = new CouponService();

                if (Session["openid"] == null)
                {
                    BLL.wx_userweixin bll = new BLL.wx_userweixin();
                    Model.wx_userweixin wxModel = bll.GetModel(wid);
                    openid = OAuth2BaseProc(wxModel, "coupon", code);
                    if (!string.IsNullOrEmpty(openid))
                    {
                        Session["openid"] = openid;
                    }

                }
                else
                {
                    openid = Session["openid"] as string;

                }
                CouponListDTO dto = service.GetCouponList( wid, openid);
                if (dto != null)
                {
                    var data = new
                    {
                        lists = dto,
                        openid = openid
                    };
                    Context.Response.Write(AjaxResult.Success(data));
                }
                else
                {
                    throw new Exception("参数异常");
                }



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
        /// 获取优惠券详细
        /// </summary>
        /// <param name="access_code"></param>
        /// <param name="couponId"></param>
        [WebMethod]
        public void GetCoupon(string openId,int  id)
        {
            try
            {


              
                var service = new CouponService();
           
                var coupon = service.GetCouponDetail(openId,id);
                var data = new
                {
                    coupon = coupon
                    // user=user
                };
                Context.Response.Write(AjaxResult.Success(data));


            }
            catch (Exception ex)
            {
                Context.Response.Write(AjaxResult.Error(ex.Message));
            }
        }

        
    }
}
