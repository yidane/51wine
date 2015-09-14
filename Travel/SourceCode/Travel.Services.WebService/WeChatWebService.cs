using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Travel.Application.DomainModules.WeChat;
using Travel.Application.DomainModules.WeChat.DTOS;
using Travel.Infrastructure.CommonFunctions.Ajax;
using Travel.Infrastructure.WeiXin.Common.OAuth2;

namespace Travel.Services.WebService
{
    public class WeChatWebService : BaseWebService
    {
        [WebMethod]
        public void WeChatConfigInit(string url)
        {
            var signatureDTO = new WeChatService().CreateSignatureDto(url);
            Context.Response.Write(AjaxResult.Success(signatureDTO));
        }

        [WebMethod]
        public void WeChatCardInit()
        {
            var cardTicketDTO = new WeChatService().CreateCardTicket();
            Context.Response.Write(AjaxResult.Success(cardTicketDTO));
        }

        [WebMethod]
        public void GetAccessToken(string code)
        {
            if (code == "123")
            {
                Context.Response.Write(AjaxResult.Success(new AccessTokenDTO(new AccessTokenResult() { openid = "12345" })));
            }
            else
            {
                var result = new WeChatService().GetAccessToken(code);
                Context.Response.Write(AjaxResult.Success(result));
            }
        }

        [WebMethod]
        public void GetUserAnalysis(DateTime beginDate, DateTime endDate)
        {
            var userStatisticsList = new WeChatService().GetUserStatistics(beginDate, endDate);
            if (userStatisticsList.Count > 0)
            {
                Context.Response.Write(AjaxResult.Success(userStatisticsList));
            }
        }
    }
}