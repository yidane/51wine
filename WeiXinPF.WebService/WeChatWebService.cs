using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;
using OneGulp.WeChat.MP.Helpers;
using WeiXinPF.Common;

namespace WeiXinPF.WebService
{
    public class WeChatWebService : BaseWebService
    {
        [WebMethod]
        public void WeChatConfigInit(int wid, string url)
        {
            try
            {
                //获取当前对象实体
                BLL.wx_userweixin bll = new BLL.wx_userweixin();
                Model.wx_userweixin uWeiXinModel = bll.GetModel(wid);

                //初始话jsskd
                var signatureDto = jssdkInit(uWeiXinModel, url);
                Context.Response.Write(AjaxResult.Success(signatureDto));
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
        /// 获取OpenID
        /// </summary>
        /// <param name="code">assess_code</param>
        /// <param name="wid">微信号唯一Id</param>
        /// <param name="url">当前页面地址</param>
        [WebMethod(EnableSession = true)]
        public void GetOpenId(string code, int wid, string url)
        {
            string openid = string.Empty;
            //try
            //{
            //    if (Session["openid"] == null)
            //    {
            //        BLL.wx_userweixin bll = new BLL.wx_userweixin();
            //        Model.wx_userweixin wxModel = bll.GetModel(wid);
            //        openid = OAuth2BaseProc(wxModel, "coupon", code, url);
            //        if (!string.IsNullOrEmpty(openid))
            //        {
            //            Session["openid"] = openid;
            //        }

            //    }
            //    else
            //    {
            //        openid = Session["openid"] as string;

            //    }
            //    CouponListDTO dto = service.GetCouponList(wid, openid);
            //    if (dto != null)
            //    {
            //        var data = new
            //        {
            //            lists = dto,
            //            openid = openid
            //        };
            //        Context.Response.Write(AjaxResult.Success(data));
            //    }
            //    else
            //    {
            //        throw new Exception("参数异常");
            //    }

            //}
            //catch (JsonException jsEx)
            //{

            //    Context.Response.Write(AjaxResult.Error(jsEx.Message, jsEx.ErrorType));
            //}
            //catch (Exception ex)
            //{
            //    var s = "";
            //    if (ex.InnerException != null)
            //    {
            //        s = ex.InnerException.Message;
            //    }
            //    Context.Response.Write(AjaxResult.Error(s));
            //}
        }
    }
}
