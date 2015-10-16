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
            const string sql = "UPDATE dbo.wx_diancai_tuidan_manage SET refundStatus=1 WHERE refundCode=@RefundCode";
            SqlParameter[] sqlparams =
                {
                    new SqlParameter(){ParameterName = "@RefundCode",SqlDbType = SqlDbType.NVarChar,Value = refundcode} 
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
            return null;
        }
    }
}
