using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Travel.Infrastructure.WeiXin.Common;
using Travel.Infrastructure.WeiXin.Config;

namespace Travel.Infrastructure.WeiXin.Statistics
{
    public class UserStatistics
    {
        /// <summary>
        /// 获取某时间区间内用户关注统计
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public StatisticsResult GetUserStatistics(DateTime beginDate, DateTime endDate)
        {
            var timeSpan = endDate - beginDate;
            if (timeSpan.TotalDays > 7 || timeSpan.TotalDays < 0)
                throw new Exception("起始时间和结束时间范围最大间隔为7天");

            var credential = new Credential();
            var postData = new
                {
                    begin_date = beginDate.ToString("yyyy-MM-dd"),
                    end_date = endDate.ToString("yyyy-MM-dd")
                };

            var userList = Extra.HttpHelper.Post<StatisticsResult>(string.Format("{0}?access_token={1}", WeChatUrlConfigManager.StatisticsManager.GetUserCumulateUrl, credential.AccessToken), JsonConvert.SerializeObject(postData), null, null);

            return userList;
        }

        /// <summary>
        /// 获取最近一周用户关注统计
        /// </summary>
        /// <returns></returns>
        public StatisticsResult GetUserStatistics()
        {
            var currentDate = DateTime.Now;
            var beginDate = currentDate.AddDays(-7);
            return GetUserStatistics(beginDate, currentDate);
        }
    }
}
