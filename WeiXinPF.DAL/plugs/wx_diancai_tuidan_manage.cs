using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WeiXinPF.DBUtility;

namespace WeiXinPF.DAL
{
    public partial class wx_diancai_tuidan_manage
    {
        /// <summary>
        /// 记录退款日志
        /// </summary>
        /// <param name="modelList"></param>
        public void AddRefundModel(List<Model.wx_diancai_tuidan_manage> modelList)
        {
            const string sql = @"INSERT INTO dbo.wx_diancai_tuidan_manage
                                ( shopinfoid ,
                                    openid ,
                                    wid ,
                                    caipinid ,
                                    orderNumber ,
                                    refundCode,
                                    refundTime ,
                                    refundAmount ,
                                    refundStatus ,
                                    createDate
                                )
                        VALUES  ( @ShopInfoID , -- shopinfoid - int
                                    @OpenID , -- openid - varchar(200)
                                    @Wid , -- wid - int
                                    @CaipinID , -- caipinid - int
                                    @RefundCode,
                                    @OrderNumber , -- orderNumber - varchar(200)
                                    null , -- refundTime - datetime
                                    @RefundAmount , -- refundAmount - float
                                    @RefundStatus , -- refundStatus - int
                                    GETDATE()  -- createDate - datetime
                                )";

            if (modelList != null && modelList.Count > 0)
            {
                List<CommandInfo> CommandInfoList = new List<CommandInfo>();
                foreach (Model.wx_diancai_tuidan_manage model in modelList)
                {
                    SqlParameter[] sqlparams =
                                        {
                                            new SqlParameter(){ParameterName = "@ShopInfoID",SqlDbType = SqlDbType.Int,Value = model.shopinfoid},
                                            new SqlParameter(){ParameterName = "OpenID",SqlDbType = SqlDbType.NVarChar,Value = model.openid},
                                            new SqlParameter(){ParameterName = "@Wid",SqlDbType = SqlDbType.Int,Value = model.wid},
                                            new SqlParameter(){ParameterName = "@CaipinID",SqlDbType = SqlDbType.Int,Value = model.caipinid},
                                            new SqlParameter(){ParameterName = "@RefundCode",SqlDbType = SqlDbType.NVarChar,Value = model.refundCode}, 
                                            new SqlParameter(){ParameterName = "@OrderNumber",SqlDbType = SqlDbType.NVarChar,Value = model.orderNumber},
                                            new SqlParameter(){ParameterName = "@RefundAmount",SqlDbType = SqlDbType.Float,Value = model.refundAmount},
                                            new SqlParameter(){ParameterName = "@RefundStatus",SqlDbType = SqlDbType.Int,Value = model.refundStatus}
                                        };

                    CommandInfoList.Add(new CommandInfo() { CommandText = sql, Parameters = sqlparams });
                }

                DbHelperSQL.ExecuteSqlTran(CommandInfoList);
            }
        }

        /// <summary>
        /// 退款成功
        /// </summary>
        /// <param name="refundcode"></param>
        public void Refund(string refundcode)
        {
            const string sql = "UPDATE dbo.wx_diancai_tuidan_manage SET refundStatus=2 WHERE refundCode=@RefundCode";
            SqlParameter[] sqlparams =
                {
                    new SqlParameter(){ParameterName = "@RefundCode",SqlDbType = SqlDbType.NVarChar,Value = refundcode} 
                };

            DbHelperSQL.ExecuteSql(sql, sqlparams);
        }

        /// <summary>
        /// 申请退款
        /// </summary>
        /// <param name="refundId"></param>
        public void Refund(int refundId)
        {
            const string sql = "UPDATE dbo.wx_diancai_tuidan_manage SET refundStatus=2 WHERE id=@RefundID";
            SqlParameter[] sqlparams =
                {
                    new SqlParameter(){ParameterName = "@RefundID",SqlDbType = SqlDbType.Int,Value = refundId} 
                };

            DbHelperSQL.ExecuteSql(sql, sqlparams);
        }

        /// <summary>
        /// 退款完成
        /// </summary>
        /// <param name="refundCode"></param>
        public void RefundComplete(string refundCode)
        {
            const string sql = @"UPDATE dbo.wx_diancai_tuidan_manage SET refundStatus=4 WHERE refundcode=@RefundCode;
                               UPDATE  dbo.wx_Verification_IdentifyingCodeInfo
                                SET     Status = 4
                                WHERE   IdentifyingCodeId IN ( SELECT   caipinid
                                                               FROM     dbo.wx_diancai_tuidan_manage
                                                               WHERE    refundCode = @RefundCode ) ";
            SqlParameter[] sqlparams =
                {
                    new SqlParameter(){ParameterName = "@RefundCode",SqlDbType = SqlDbType.NVarChar,Value = refundCode} 
                };

            DbHelperSQL.ExecuteSql(sql, sqlparams);
        }

        /// <summary>
        /// 不同意退款
        /// </summary>
        /// <param name="refundCode"></param>
        public void RefundFail(string refundCode)
        {
            const string sql = @"UPDATE dbo.wx_diancai_tuidan_manage SET refundStatus=5 WHERE refundcode=@RefundCode;
                                       UPDATE  dbo.wx_Verification_IdentifyingCodeInfo
                                        SET     Status = 5
                                        WHERE   IdentifyingCodeId = ( SELECT    caipinid
                                                                      FROM      dbo.wx_diancai_tuidan_manage
                                                                      WHERE     refundCode = @RefundCode)";
            SqlParameter[] sqlparams =
                {
                    new SqlParameter(){ParameterName = "@RefundCode",SqlDbType = SqlDbType.NVarChar,Value = refundCode} 
                };

            DbHelperSQL.ExecuteSql(sql, sqlparams);
        }

        /// <summary>
        /// 获取退款记录
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public DataSet GetRefundList(string openId)
        {
            const string sql = @"SELECT  distinct tm.createDate ,
                                                ds.hotelName ,
                                                tm.refundCode ,
                                                dm.orderNumber ,
                                                tm.refundAmount/100 as refundAmount,
                                                tm.refundStatus ,
                                                tm.wid ,
                                                tm.openid ,
                                                tm.shopinfoid ,
                                                dm.id AS dingdan
                                        FROM    dbo.wx_diancai_tuidan_manage tm
                                                INNER JOIN dbo.wx_Verification_IdentifyingCodeInfo vi ON tm.caipinid = vi.IdentifyingCodeId
                                                                                                      AND vi.ModuleName = 'restaurant'
                                                LEFT JOIN dbo.wx_diancai_shopinfo ds ON tm.shopinfoid = ds.id
                                                INNER JOIN dbo.wx_diancai_dingdan_manage dm ON vi.OrderId = dm.id
                                        WHERE   dm.openid = @OpenID
                                        ORDER BY tm.CreateDate DESC;";

            SqlParameter[] sqlparams =
                {
                    new SqlParameter(){ParameterName = "@OpenID",SqlDbType = SqlDbType.NVarChar,Value = openId} 
                };

            return DbHelperSQL.Query(sql, sqlparams);
        }

        /// <summary>
        /// 商家查询退单
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="payAmountMin"></param>
        /// <param name="payAmountMax"></param>
        /// <param name="refundNumber"></param>
        /// <param name="orderNumber"></param>
        /// <param name="customerName"></param>
        /// <param name="customerTel"></param>
        /// <param name="refundStatus"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet GetRefundList(int shopId, int pageSize, int pageIndex,
                                                            DateTime beginDate, DateTime endDate, int payAmountMin,
                                                            int payAmountMax, string refundNumber, string orderNumber,
                                                            string customerName, string customerTel, int refundStatus, out int totalCount)
        {
            totalCount = 0;
            var totalParam = new SqlParameter()
                {
                    ParameterName = "@TotalCount",
                    SqlDbType = SqlDbType.Int,
                    Value = totalCount,
                    Direction = ParameterDirection.Output
                };

            var sqlparams = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName = "@ShopID",SqlDbType = SqlDbType.Int,Value = shopId},
                    new SqlParameter(){ParameterName = "@PageSize",SqlDbType = SqlDbType.Int,Value = pageSize},
                    new SqlParameter(){ParameterName = "@PageIndex",SqlDbType = SqlDbType.Int,Value = pageIndex},
                    new SqlParameter(){ParameterName = "@BeginCreateTime",SqlDbType = SqlDbType.DateTime,Value = beginDate},
                    new SqlParameter(){ParameterName = "@EndCreateTime",SqlDbType = SqlDbType.DateTime,Value = endDate},
                    new SqlParameter(){ParameterName = "@PayAmountMin",SqlDbType = SqlDbType.Int,Value = payAmountMin},
                    new SqlParameter(){ParameterName = "@PayAmountMax",SqlDbType = SqlDbType.Int,Value = payAmountMax},
                    new SqlParameter(){ParameterName = "@RefundNumber",SqlDbType = SqlDbType.NVarChar,Value = refundNumber},
                    new SqlParameter(){ParameterName = "@OrderNumber",SqlDbType = SqlDbType.NVarChar,Value = refundNumber},
                    new SqlParameter(){ParameterName = "@CustomerName",SqlDbType = SqlDbType.NVarChar,Value = customerName},
                    new SqlParameter(){ParameterName = "@CustomerTel",SqlDbType = SqlDbType.NVarChar,Value = customerTel},
                    new SqlParameter(){ParameterName = "@RefundStatus",SqlDbType = SqlDbType.Int,Value = refundStatus},
                    totalParam
                };

            if (DateTime.Equals(beginDate, DateTime.MinValue))
                sqlparams[3].Value = null;
            else
                sqlparams[3].Value = beginDate;

            if (DateTime.Equals(endDate, DateTime.MinValue))
                sqlparams[4].Value = null;
            else
                sqlparams[4].Value = endDate;

            var result = DbHelperSQL.RunProcedure("usp_wx_diancai_tuidan_manage_RefundManage", sqlparams.ToArray(), "tuidan");

            totalCount = totalParam.Value != null && totalParam.Value != DBNull.Value
                             ? Convert.ToInt32(totalParam.Value)
                             : 0;

            return result;
        }

        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="refundCode"></param>
        /// <returns></returns>
        public string GetRefundDetail(string refundCode)
        {
            var sqlparams = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName = "@RefundCode",SqlDbType = SqlDbType.NVarChar,Value = refundCode}
                };

            var result = DbHelperSQL.RunProcedure("usp_wx_diancai_tuidan_manage_RefundDetail", sqlparams.ToArray(), "refundDetail");

            var rtnStringBuilder = new StringBuilder();
            if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in result.Tables[0].Rows)
                {
                    if (row[0] != null && row[0] != DBNull.Value)
                        rtnStringBuilder.Append(row[0].ToString());
                }
            }
            return rtnStringBuilder.ToString();
        }

        /// <summary>
        /// 获取退单详细信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="orderId"></param>
        /// <param name="refundCode"></param>
        /// <returns></returns>
        public DataSet GetRefundDetail(int shopId, int orderId, string refundCode)
        {
            var sqlparams = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName = "@ShopID",SqlDbType = SqlDbType.Int,Value = shopId},
                    new SqlParameter(){ParameterName = "@OrderID",SqlDbType = SqlDbType.Int,Value = orderId},
                    new SqlParameter(){ParameterName = "@RefundCode",SqlDbType = SqlDbType.NVarChar,Value = refundCode}
                };

            var result = DbHelperSQL.RunProcedure("usp_wx_diancai_tuidan_manage_Detail", sqlparams.ToArray(), "refundDetail");
            return result;
        }

        public DataSet GetWeChatRefundParams(int shopId, int dingdanId, string refundCode)
        {
            SqlParameter[] sqlparams =
                {
                    new SqlParameter(){ParameterName = "@ShopID",SqlDbType = SqlDbType.Int,Value = shopId},
                    new SqlParameter(){ParameterName = "@OrderID",SqlDbType = SqlDbType.Int,Value = dingdanId},
                    new SqlParameter(){ParameterName = "@RefundCode",SqlDbType = SqlDbType.NVarChar,Value = refundCode} 
                };

            return DbHelperSQL.RunProcedure("usp_wx_diancai_tuidan_manage_WeChatDetail", sqlparams, "WeChatRefundDetail");
        }
    }
}