using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.DomainModules.WeChat.DTOS
{
    public class UserStatisticsDTO
    {
        public string ref_date { get; set; }
        public int user_source { get; set; }
        public string user_sourceDesc { get; set; }
        public int new_user { get; set; }
        public int cancel_user { get; set; }
    }
}
