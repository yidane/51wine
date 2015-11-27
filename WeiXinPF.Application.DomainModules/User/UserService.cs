using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeiXinPF.Infrastructure.DomainDataAccess.User;

namespace WeiXinPF.Application.DomainModules.User
{
    public class UserService
    {
        public UserInfoEntity Get(string openid)
        {
            return UserInfoEntity.GetModel(openid);

        }
    }
}
