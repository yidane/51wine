using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Travel.Application.DomainModules.User.DTO;
using Travel.Infrastructure.DomainDataAccess.User;

namespace Travel.Application.DomainModules.User
{
    public class UserService
    {
        public UserContractInfo GetUserContractInfo(string openId)
        {
            var contract = UserContracts.Get(openId);
            if (contract != null)
            {
                return new UserContractInfo()
                    {
                        OpenID = contract.OpenID,
                        UserName = contract.UserName,
                        Mobile = contract.Mobile,
                        IdCard = contract.IdCard
                    };
            }
            return null;
        }

        public void SaveContractInfo(UserContractInfo userContractInfo)
        {
            if (userContractInfo == null)
                return;

            var userContracts = new UserContracts()
                {
                    OpenID = userContractInfo.OpenID,
                    UserName = userContractInfo.UserName,
                    Mobile = userContractInfo.Mobile,
                    IdCard = userContractInfo.IdCard
                };

            userContracts.SaveNew();
        }
    }
}
