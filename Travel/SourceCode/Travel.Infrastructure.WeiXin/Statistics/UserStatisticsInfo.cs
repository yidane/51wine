using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.WeiXin.Statistics
{
    /// <summary>
    /// 用户关注变化
    /// </summary>
    public class UserSummaryInfo
    {
        public DateTime ref_date { get; set; }
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
    }

    /// <summary>
    /// 用户实时统计
    /// </summary>
    public class UserCumulateInfo
    {
        public DateTime ref_date { get; set; }
        public int cumulate_user { get; set; }
    }

    public class StatisticsResult
    {
        public List<UserSummaryInfo> SummaryList { get; set; }
        public List<UserCumulateInfo> CumulateList { get; set; }

        public bool HasValue()
        {
            return SummaryList != null && SummaryList.Count > 0 && CumulateList != null && CumulateList.Count > 0;
        }
    }

    public class StatisticsRequestResult<T> where T : class
    {
        public List<T> list { get; set; }
    }
}
