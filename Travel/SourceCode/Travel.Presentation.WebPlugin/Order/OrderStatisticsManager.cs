using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PetroChina.Riped.DBAccess;
using System.Linq;
using System.Web;
using Travel.Infrastructure.CommonFunctions;

namespace Travel.Presentation.WebPlugin.Order
{
    public class OrderStatisticsManager
    {
        public OrderStatistics Search(DateTime beginDate, DateTime endDate, string orderStatus, float beginTotalPrice, float endTotalPrice, int pageSize, int pageIndex)
        {
            var rtnOrderStatistics = new OrderStatistics() { OrderList = new List<OrderEntity>() };
            var totalRows = new SqlParameter() { ParameterName = "@TotalRows", Direction = ParameterDirection.Output };
            var sqlparams = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName = "@BeginDate",Value = beginDate},
                    new SqlParameter(){ParameterName = "@EndDate",Value = endDate},
                    new SqlParameter(){ParameterName = "@OrderStatus",Value = orderStatus},
                    new SqlParameter(){ParameterName = "@BeginTotalPrice",Value = beginTotalPrice},
                    new SqlParameter(){ParameterName = "@EndTotalPrice",Value = endTotalPrice},
                    new SqlParameter(){ParameterName = "@PageSize",Value = pageSize},
                    new SqlParameter(){ParameterName = "@PageIndex",Value = pageIndex},
                    totalRows
                };

            rtnOrderStatistics.Total = totalRows.Value == DBNull.Value ? 0 : (int)totalRows.Value;

            var result = new SqlHelper().ExecuteDataTable(WebConfigureHelper.ConnectionStrings.DbConnection, CommandType.StoredProcedure, "", sqlparams.ToArray());

            if (result != null && result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    var rtnOrderEntity = new OrderEntity
                        {
                            AppID = row.Field<string>("AppID"),
                            ContactPersonName = row.Field<string>("ContactPersonName"),
                            IdentityCardNumber = row.Field<string>("IdentityCardNumber"),
                            MobilePhoneNumber = row.Field<string>("MobilePhoneNumber"),
                            OrderStatus = row.Field<string>("OrderStatus"),
                            TicketCount = row.Field<int>("TicketCount"),
                            TicketPrice = row.Field<float>("TicketPrice"),
                            TotalPrice = row.Field<float>("TotalPrice"),
                            TradeNo = row.Field<string>("TradeNo"),
                            TradeTime = row.Field<DateTime>("TradeTime"),
                            WeChatTradeNo = row.Field<string>("WeChatTradeNo")
                        };

                    rtnOrderStatistics.OrderList.Add(rtnOrderEntity);
                }
            }

            return rtnOrderStatistics;
        }

        public OrderStatistics Search(SearchParameter searchParameter)
        {
            return Search(searchParameter.BeginDate,
                                    searchParameter.EndDate,
                                    searchParameter.OrderStatus,
                                    searchParameter.BeginTotalPrice,
                                    searchParameter.EndTotalPrice,
                                    searchParameter.PageSize,
                                    searchParameter.PageIndex);
        }
    }

    public class OrderStatistics
    {
        public List<OrderEntity> OrderList { get; set; }
        public int Total { get; set; }
    }

    public class SearchParameter
    {
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string OrderStatus { get; set; }
        public float BeginTotalPrice { get; set; }
        public float EndTotalPrice { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}