using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Travel.Application.DomainModules.WeChat;
using Travel.Application.DomainModules.WeChat.DTOS;
using Travel.Infrastructure.CommonFunctions;

namespace Travel.Presentation.WebPlugin.UserStatistics
{
    public class UserStatisticsManager
    {
        //递归次数
        private const int RecursiveTimes = 2;

        public List<UserStatisticsInfo> GetUserStatistics(DateTime beginDate, DateTime endDate, int recursiveTimes = 0)
        {
            beginDate = beginDate.Date;
            endDate = endDate.Date;
            //今天结果不能查询
            if (DateTime.Equals(endDate, DateTime.Now))
                endDate = endDate.AddDays(-1);

            if (beginDate > endDate)
                throw new Exception("beginDate不能大于endDate");

            var result = GetUserStatistics(beginDate, endDate);

            //return result;

            //已存在数据的时间散列域
            var existsDateList = new List<DateTime>();
            if (result != null && result.Count > 0)
            {
                foreach (UserStatisticsInfo userInfo in result)
                {
                    var existsDate = Convert.ToDateTime(userInfo.date);
                    if (!existsDateList.Contains(existsDate))
                        existsDateList.Add(existsDate);
                }
            }
            existsDateList.Sort();

            //
            //existsDateList.Add(new DateTime(2015, 8, 29));
            //existsDateList.Add(new DateTime(2015, 9, 2));
            //existsDateList.Add(new DateTime(2015, 9, 7));

            //不存在数据的时间散列区间域
            var notExistsDataRangeList = new List<DateRange>();

            DateRange range = null;
            //需要检索的时间域，已经是按照时间大小排序
            for (DateTime date = beginDate; date <= endDate; date = date.AddDays(1))
            {
                if (!existsDateList.Contains(date))
                {
                    if (range == null)
                    {
                        range = new DateRange(date);
                    }
                    else
                    {
                        if (!range.AddRange(date))
                        {
                            notExistsDataRangeList.Add(range);

                            range = new DateRange(date);
                        }
                    }
                }
            }

            if (range != null)
                notExistsDataRangeList.Add(range);

            foreach (var dateRange in notExistsDataRangeList)
            {
                dateRange.DownloadData();
            }

            return GetUserStatistics(beginDate, endDate);
        }

        private List<UserStatisticsInfo> GetUserStatistics(DateTime beginDate, DateTime endDate)
        {
            var rtnUserStatisticsList = new List<UserStatisticsInfo>();

            var sqlparams = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName = "@BeginDate",SqlDbType = SqlDbType.Date,Value = beginDate},
                    new SqlParameter(){ParameterName = "@EndDate",SqlDbType = SqlDbType.Date,Value = endDate}
                };

            var result = new PetroChina.Riped.DBAccess.SqlHelper().ExecuteDataTable(WebConfigureHelper.ConnectionStrings.DbConnection, CommandType.StoredProcedure,
                                                                                    "usp_GetUserStatistics",
                                                                                    sqlparams.ToArray());

            if (result != null && result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    var newUserStatistics = new UserStatisticsInfo();
                    newUserStatistics.cancel_user = row.Field<int>("cancel_user");
                    newUserStatistics.cumulate_user = row.Field<int>("cumulate_user");
                    newUserStatistics.date = row.Field<DateTime>("ref_date").ToString("yyyy-MM-dd");
                    newUserStatistics.new_user = row.Field<int>("new_user");
                    newUserStatistics.user_source = row.Field<int>("user_source");

                    rtnUserStatisticsList.Add(newUserStatistics);
                }
            }

            return rtnUserStatisticsList;
        }
    }

    public class DateRange
    {
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateRange(DateTime beginDate)
        {
            BeginDate = beginDate;
            EndDate = DateTime.MinValue;
        }

        private DateRange(DateTime beginDate, DateTime endDate)
        {
            BeginDate = beginDate;
            EndDate = endDate;
        }

        public bool AddRange(DateTime date)
        {
            if (EndDate == DateTime.MinValue)
            {
                EndDate = date;
                return true;
            }

            if (date == EndDate.AddDays(1))
            {
                EndDate = date;
                return true;
            }

            return false;
        }

        public void DownloadData()
        {
            //将时间域按照每七天划分
            var sevenDayRangeList = new List<DateRange>();
            var beginDate = BeginDate;
            var endDate = beginDate.AddDays(6);
            while (endDate <= EndDate)
            {
                sevenDayRangeList.Add(new DateRange(beginDate, endDate));
                beginDate = endDate;
                endDate = endDate.AddDays(6);
            }

            if (EndDate.AddDays(6) > endDate)
            {
                sevenDayRangeList.Add(new DateRange(beginDate, EndDate));
            }

            foreach (var dateRange in sevenDayRangeList)
            {
                dateRange.AddData();
            }
        }

        private void AddData()
        {
            var result = new WeChatService().GetUserStatistics(BeginDate, EndDate);
            if (result != null && result.Count > 0)
            {
                //在一次获取统计数据时候，中间某天无数据，使用初始化的值代替
                for (DateTime date = BeginDate; date <= EndDate; date = date.AddDays(1))
                {
                    if (!result.Any(d => DateTime.Equals(Convert.ToDateTime(d.date), date.Date)))
                    {
                        result.Add(new UserStatisticsDTO() { date = date.ToString("yyyy-MM-dd") });
                    }
                }

                foreach (var userStatisticsDto in result)
                {
                    SaveData(userStatisticsDto);
                }

            }
        }

        private void SaveData(UserStatisticsDTO user)
        {
            var sqlparams = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@Cancel_User", Value = user.cancel_user},
                    new SqlParameter() {ParameterName = "@Cumulate_User", Value = user.cumulate_user},
                    new SqlParameter() {ParameterName = "@Ref_Date", Value = user.date},
                    new SqlParameter() {ParameterName = "@New_User", Value = user.new_user},
                    new SqlParameter() {ParameterName = "@User_Source", Value = user.user_source}
                };

            new PetroChina.Riped.DBAccess.SqlHelper().ExecuteNonQuery(WebConfigureHelper.ConnectionStrings.DbConnection, CommandType.StoredProcedure, "usp_WriteUserStatistics", sqlparams.ToArray());
        }
    }
}