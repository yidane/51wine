using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.WeiXin.Statistics
{
    public class UserStatisticsInfo
    {
        public string ref_date { get; set; }
        public int user_source { get; set; }
        public string user_sourceDesc
        {
            get
            {
                switch (user_source)
                {
                    case 0:
                        return "其他";//（包括带参数二维码）
                    case 3:
                        return "二维";
                    case 17:
                        return "名片分享";
                    case 35:
                        return "搜号码";//（即微信添加朋友页的搜索）
                    case 39:
                        return "查询微信公众帐号";
                    case 43:
                        return "图文页右上角菜单";
                    default:
                        return string.Empty;
                }
            }
        }
        public int new_user { get; set; }
        public int cancel_user { get; set; }
        //public int cumulate_user { get; set; }
    }

    public class StatisticsResult
    {
        public List<UserStatisticsInfo> list { get; set; }
    }
}
