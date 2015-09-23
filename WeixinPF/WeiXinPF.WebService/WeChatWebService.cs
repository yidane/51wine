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
    public class WeChatWebService:BaseWebService
    {
        [WebMethod]
        public void WeChatConfigInit(int wid,string url)
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



     
    }
}
