using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Travel.Application.DomainModules.User;
using Travel.Application.DomainModules.User.DTO;
using Travel.Infrastructure.CommonFunctions.Ajax;

namespace Travel.Services.WebService
{
    public class UserWebService : BaseWebService
    {
        [WebMethod(EnableSession = true)]
        public void GetContract(string code)
        {
            try
            {
                var openId = GetOpenIDByCodeID(code);
                var contract = new UserService().GetUserContractInfo(openId);

                var result = new
                    {
                        HasValue = contract != null,
                        Value = contract
                    };

                Context.Response.Write(AjaxResult.Success(result));
            }
            catch (Exception exception)
            {
                Context.Response.Write(AjaxResult.Error(exception.Message));
            }
        }

        [WebMethod(EnableSession = true)]
        public void SaveContract(string code, string userName, string mobile, string idCard)
        {
            try
            {
                if (!(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(idCard)))
                {
                    var openId = GetOpenIDByCodeID(code);
                    if (!string.IsNullOrEmpty(openId))
                    {
                        var contractInfo = new UserContractInfo()
                        {
                            OpenID = openId,
                            UserName = userName,
                            Mobile = mobile,
                            IdCard = idCard
                        };

                        new UserService().SaveContractInfo(contractInfo);
                    }
                }
            }
            catch (Exception)
            {
                //不保证每次都需要保存成功
            }
        }
    }
}