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

            //合并每天的数据
            for (DateTime date = beginDate.Date; date <= endDate.Date; date = date.AddDays(1))
            {
                rtnUserStatisticsList.Add(new UserStatisticsInfo() { date = date.ToString("yyyy-MM-dd") });
            }

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
                    var date = row.Field<DateTime>("ref_date").ToString("yyyy-MM-dd");
                    var newUserStatistics = rtnUserStatisticsList.FirstOrDefault(item => string.Equals(item.date, date));

                    newUserStatistics.cancel_user += row.Field<int>("cancel_user");
                    newUserStatistics.cumulate_user = row.Field<int>("cumulate_user");
                    newUserStatistics.date = date;
                    newUserStatistics.new_user += row.Field<int>("new_user");
                    newUserStatistics.user_source = row.Field<int>("user_source");
                    rtnUserStatisticsList.RemoveAll(item => string.Equals(item.date, date));
                    rtnUserStatisticsList.Add(newUserStatistics);
                }
            }

            //计算净值，平滑总关注数据曲线
            foreach (UserStatisticsInfo info in rtnUserStatisticsList)
            {
                info.netgain_user = info.new_user - info.cancel_user;
            }

            foreach (UserStatisticsInfo info in rtnUserStatisticsList)
            {
                if (info.cumulate_user == 0)
                {
                    var infoIndex = rtnUserStatisticsList.IndexOf(info);
                    if (infoIndex == 0 && rtnUserStatisticsList.Count > 1)
                    {
                        info.cumulate_user = rtnUserStatisticsList[1].cumulate_user - rtnUserStatisticsList[1].netgain_user;
                    }
                    else if (infoIndex == rtnUserStatisticsList.Count - 1 && rtnUserStatisticsList.Count > 1)
                    {
                        info.cumulate_user = rtnUserStatisticsList[rtnUserStatisticsList.Count - 2].cumulate_user +
                                             rtnUserStatisticsList[rtnUserStatisticsList.Count - 2].netgain_user;
                    }
                    else
                    {
                        var preInfo = rtnUserStatisticsList[infoIndex - 1];
                        var nextInfo = rtnUserStatisticsList[infoIndex + 1];
                        if (preInfo.cumulate_user == 0)
                        {
                            if (nextInfo.cumulate_user == 0)
                            {
                                info.cumulate_user = 0;
                            }
                            else
                            {
                                info.cumulate_user = nextInfo.cumulate_user - nextInfo.netgain_user;
                            }
                        }
                        else
                        {
                            if (nextInfo.cumulate_user == 0)
                            {
                                info.cumulate_user = preInfo.cumulate_user + preInfo.netgain_user;
                            }
                            else
                            {
                                info.cumulate_user = (nextInfo.cumulate_user + preInfo.cumulate_user) / 2;
                            }
                        }
                    }
                }
            }

            return rtnUserStatisticsList;
        }

        public UserStatisricsRadioInfo GetUserStatisricsRadioInfo()
        {
            var result =
                new PetroChina.Riped.DBAccess.SqlHelper().ExecuteDataTable(
                    WebConfigureHelper.ConnectionStrings.DbConnection, CommandType.StoredProcedure,
                    "usp_GetUserStatisticsRatio");

            var rtnUserStatisricsRadioInfo = new UserStatisricsRadioInfo();
            if (result != null && result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                rtnUserStatisricsRadioInfo.DayCancelUser = row.Field<string>("DayCancelUser");
                rtnUserStatisricsRadioInfo.DayCumulateUser = row.Field<string>("DayCumulateUser");
                rtnUserStatisricsRadioInfo.DayNetgainUser = row.Field<string>("DayNetgainUser");
                rtnUserStatisricsRadioInfo.DayNewUser = row.Field<string>("DayNewUser");
                rtnUserStatisricsRadioInfo.MonthCancelUser = row.Field<string>("MonthCancelUser");
                rtnUserStatisricsRadioInfo.MonthCumulateUser = row.Field<string>("MonthCumulateUser");
                rtnUserStatisricsRadioInfo.MonthNetgainUser = row.Field<string>("MonthNetgainUser");
                rtnUserStatisricsRadioInfo.MonthNewUser = row.Field<string>("MonthNewUser");
                rtnUserStatisricsRadioInfo.ThisDayCancelUser = row.Field<string>("ThisDayCancelUser");
                rtnUserStatisricsRadioInfo.ThisDayCumulateUser = row.Field<string>("ThisDayCumulateUser");
                rtnUserStatisricsRadioInfo.ThisDayNetgainUser = row.Field<string>("ThisDayNetgainUser");
                rtnUserStatisricsRadioInfo.ThisDayNewUser = row.Field<string>("ThisDayNewUser");
                rtnUserStatisricsRadioInfo.WeekCancelUser = row.Field<string>("WeekCancelUser");
                rtnUserStatisricsRadioInfo.WeekCumulateUser = row.Field<string>("WeekCumulateUser");
                rtnUserStatisricsRadioInfo.WeekNetgainUser = row.Field<string>("WeekNetgainUser");
                rtnUserStatisricsRadioInfo.WeekNewUser = row.Field<string>("WeekNewUser");
            }

            return rtnUserStatisricsRadioInfo;
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

    public class UserStatisricsRadioInfo
    {
        public string ThisDayNewUser { get; set; }
        public string ThisDayCancelUser { get; set; }
        public string ThisDayNetgainUser { get; set; }
        public string ThisDayCumulateUser { get; set; }
        public string DayNewUser { get; set; }
        public string DayCancelUser { get; set; }
        public string DayNetgainUser { get; set; }
        public string DayCumulateUser { get; set; }
        public string WeekNewUser { get; set; }
        public string WeekCancelUser { get; set; }
        public string WeekNetgainUser { get; set; }
        public string WeekCumulateUser { get; set; }
        public string MonthNewUser { get; set; }
        public string MonthCancelUser { get; set; }
        public string MonthNetgainUser { get; set; }
        public string MonthCumulateUser { get; set; }
    }
}