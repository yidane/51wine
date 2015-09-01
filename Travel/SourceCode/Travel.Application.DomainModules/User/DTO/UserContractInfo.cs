using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.DomainModules.User.DTO
{
    public class UserContractInfo
    {
        public string OpenID { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string IdCard { get; set; }
    }
}
