using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travel.Presentation.WebPlugin.UserStatistics
{
    public class UserStatisticsInfo
    {
        public string date { get; set; }
        public int user_source { get; set; }
        public string user_sourceDesc { get; set; }
        public int new_user { get; set; }
        public int cancel_user { get; set; }
        public int cumulate_user { get; set; }
        public int netgain_user { get; set; }
    }
}