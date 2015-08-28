using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.DomainDataAccess.User
{
    public class UserGroup
    {
        [Key]
        
        public int groupId { get; set; }

        public string groupName { get; set; }
    }
}
