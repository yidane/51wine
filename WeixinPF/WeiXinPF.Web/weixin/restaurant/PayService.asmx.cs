﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WeiXinPF.Common;

namespace WeiXinPF.Web.weixin.restaurant
{
    /// <summary>
    /// PayService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class PayService : System.Web.Services.WebService
    {
        [WebMethod]
        public void AfterPaySuccess(string prepayid)
        {
            new BLL.wx_diancai_dingdan_manage().PaySuccess(prepayid);
            HttpContext.Current.Response.Write(AjaxResult.Success("支付成功"));
        }
    }
}
