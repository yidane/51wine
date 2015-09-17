using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PetroChina.Riped.DBAccess;
using System.Linq;
using System.Web;
using Travel.Infrastructure.CommonFunctions;
using System.IO;

namespace Travel.Presentation.WebPlugin.Order
{
    public class OrderStatisticsManager
    {
        public OrderStatistics Search(Guid ticketCategoryId, DateTime beginDate, DateTime endDate, string orderStatus, float beginTotalPrice, float endTotalPrice, int pageSize, int pageIndex)
        {
            var rtnOrderStatistics = new OrderStatistics() { OrderList = new List<OrderEntity>() };
            var totalRows = new SqlParameter() { ParameterName = "@TotalRows", Direction = ParameterDirection.Output, Size = 30, SqlDbType = SqlDbType.Int };
            var sqlparams = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName = "@TicketCategoryID",Value = ticketCategoryId,SqlDbType = SqlDbType.UniqueIdentifier},
                    new SqlParameter(){ParameterName = "@BeginDate",Value = beginDate, SqlDbType = SqlDbType.DateTime},
                    new SqlParameter(){ParameterName = "@EndDate",Value = endDate, SqlDbType = SqlDbType.DateTime},
                    new SqlParameter(){ParameterName = "@OrderStatus",Value =string.IsNullOrEmpty(orderStatus)?"":orderStatus, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ParameterName = "@BeginTotalPrice",Value = beginTotalPrice, SqlDbType = SqlDbType.Decimal},
                    new SqlParameter(){ParameterName = "@EndTotalPrice",Value = endTotalPrice, SqlDbType = SqlDbType.Decimal},
                    new SqlParameter(){ParameterName = "@PageSize",Value = pageSize, SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ParameterName = "@PageIndex",Value = pageIndex, SqlDbType = SqlDbType.Int},
                    totalRows
                };
            var result = new SqlHelper().ExecuteDataTable(WebConfigureHelper.ConnectionStrings.DbConnection, CommandType.StoredProcedure, "usp_OrderSearch", sqlparams.ToArray());
            rtnOrderStatistics.Total = totalRows.Value == DBNull.Value || totalRows.Value == null ? 0 : (int)totalRows.Value;

            if (result != null && result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    var rtnOrderEntity = new OrderEntity();
                    rtnOrderEntity.OpenID = row.Field<string>("OpenID");
                    rtnOrderEntity.ContactPersonName = row.Field<string>("ContactPersonName");
                    rtnOrderEntity.IdentityCardNumber = row.Field<string>("IdentityCardNumber");
                    rtnOrderEntity.MobilePhoneNumber = row.Field<string>("MobilePhoneNumber");
                    rtnOrderEntity.OrderStatus = row.Field<string>("OrderStatus");
                    rtnOrderEntity.TicketCount = row.Field<int>("TicketCount");
                    rtnOrderEntity.TicketPrice = row.Field<decimal>("TicketPrice");
                    rtnOrderEntity.TotalPrice = row.Field<decimal>("TotalPrice");
                    rtnOrderEntity.TradeNo = row.Field<string>("TradeNo");
                    rtnOrderEntity.TradeTime = row.Field<DateTime>("TradeTime");
                    rtnOrderEntity.WeChatTradeNo = row.Field<string>("WeChatTradeNo");
                    rtnOrderEntity.TicketCategoryName = row.Field<string>("TicketCategoryName");

                    rtnOrderStatistics.OrderList.Add(rtnOrderEntity);
                }
            }

            return rtnOrderStatistics;
        }

        public OrderStatistics Search(SearchParameter searchParameter)
        {
            return Search(searchParameter.TicketCategoryID,
                                    searchParameter.BeginDate,
                                    searchParameter.EndDate,
                                    searchParameter.OrderStatus,
                                    searchParameter.BeginTotalPrice,
                                    searchParameter.EndTotalPrice,
                                    searchParameter.PageSize,
                                    searchParameter.PageIndex);
        }

        public List<TicketCategoryEntity> GetTicketCategoryEntityList()
        {
            var rtnTicketCategoryEntityList = new List<TicketCategoryEntity>();
            var result = new SqlHelper().ExecuteDataTable(WebConfigureHelper.ConnectionStrings.DbConnection, CommandType.StoredProcedure, "usp_GetTicketCategoryEntity");

            if (result != null && result.Rows.Count > 0)
            {
                rtnTicketCategoryEntityList.Add(new TicketCategoryEntity()
                    {
                        TicketCategoryID = Guid.Empty,
                        TicketName = "所有类型"
                    });

                foreach (DataRow row in result.Rows)
                {
                    var newTicketCategoryEntity = new TicketCategoryEntity();
                    newTicketCategoryEntity.TicketCategoryID = row.Field<Guid>("ProductCategoryID");
                    newTicketCategoryEntity.TicketName = row.Field<string>("ProductName");
                    //newTicketCategoryEntity.Type = row.Field<string>("Type");

                    rtnTicketCategoryEntityList.Add(newTicketCategoryEntity);
                }
            }

            return rtnTicketCategoryEntityList;
        }

        public List<TicketStatusEntity> GetTicketStatusEntityList()
        {
            var rtnTicketStatusEntityList = new List<TicketStatusEntity>();
            var result = new SqlHelper().ExecuteDataTable(WebConfigureHelper.ConnectionStrings.DbConnection, CommandType.StoredProcedure, "usp_GetAllOrderStatus");

            if (result != null && result.Rows.Count > 0)
            {
                rtnTicketStatusEntityList.Add(new TicketStatusEntity()
                {
                    StatusCode = "yidane",
                    StatusDesc = "所有类型"
                });

                foreach (DataRow row in result.Rows)
                {
                    var newTicketStatusEntity = new TicketStatusEntity();
                    newTicketStatusEntity.StatusCode = row.Field<string>("StatusCode");
                    newTicketStatusEntity.StatusDesc = row.Field<string>("StatusDesc");

                    rtnTicketStatusEntityList.Add(newTicketStatusEntity);
                }
            }

            return rtnTicketStatusEntityList;
        }

        public void DownloadSearchResult(SearchParameter searchParameter)
        {
            var sqlparams = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName = "@TicketCategoryID",Value = searchParameter.TicketCategoryID,SqlDbType = SqlDbType.UniqueIdentifier},
                    new SqlParameter(){ParameterName = "@BeginDate",Value = searchParameter.BeginDate, SqlDbType = SqlDbType.DateTime},
                    new SqlParameter(){ParameterName = "@EndDate",Value = searchParameter.EndDate, SqlDbType = SqlDbType.DateTime},
                    new SqlParameter(){ParameterName = "@OrderStatus",Value =string.IsNullOrEmpty(searchParameter.OrderStatus)?"":searchParameter.OrderStatus, SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ParameterName = "@BeginTotalPrice",Value = searchParameter.BeginTotalPrice, SqlDbType = SqlDbType.Decimal},
                    new SqlParameter(){ParameterName = "@EndTotalPrice",Value = searchParameter.EndTotalPrice, SqlDbType = SqlDbType.Decimal}
                };

            var result = new SqlHelper().ExecuteDataTable(WebConfigureHelper.ConnectionStrings.DbConnection, CommandType.StoredProcedure, "usp_DownloadOrderSearch", sqlparams.ToArray());
            DownloadSearchResult(result);
        }

        public void DownloadSearchResult(DataTable table)
        {
            var stream = new MemoryStream();
            using (var sw = new StreamWriter(stream, System.Text.Encoding.UTF8))
            {
                string data = "序号,";
                //写出列名称
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    data += table.Columns[i].ColumnName.ToString();
                    if (i < table.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);

                //写出各行数据
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    data = (i + 1).ToString() + ",";
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        data += (string.Equals(table.Columns[j].ColumnName, "联系电话") ||
                                        string.Equals(table.Columns[j].ColumnName, "联系人身份证") ||
                                        string.Equals(table.Columns[j].ColumnName, "订单号")
                                    ? "'"
                                    : "") + table.Rows[i][j].ToString();

                        if (j < table.Columns.Count - 1)
                        {
                            data += ",";
                        }
                    }
                    sw.WriteLine(data);
                }
            }

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=OrderResult.csv");
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;

            //response.contenttype指定文件类型 能为application/ms-excel || application/ms-word || application/ms-txt || application/ms-html || 或其他浏览器可直接支持文件 
            HttpContext.Current.Response.ContentType = "application/ms-txt";
            HttpContext.Current.Response.BinaryWrite(stream.ToArray());
        }
    }

    public class OrderStatistics
    {
        public List<OrderEntity> OrderList { get; set; }
        public int Total { get; set; }
    }

    public class SearchParameter
    {
        public Guid TicketCategoryID { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string OrderStatus { get; set; }
        public float BeginTotalPrice { get; set; }
        public float EndTotalPrice { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }

    /// <summary>
    /// 票种类
    /// </summary>
    public class TicketCategoryEntity
    {
        public Guid TicketCategoryID { get; set; }
        public string Type { get; set; }
        public string TicketName { get; set; }
    }

    /// <summary>
    /// 票状态
    /// </summary>
    public class TicketStatusEntity
    {
        public string StatusDesc { get; set; }
        public string StatusCode { get; set; }
    }
}