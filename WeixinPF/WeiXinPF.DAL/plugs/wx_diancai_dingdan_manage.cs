using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Data.SqlClient;
using WeiXinPF.DBUtility;
using WeiXinPF.Common;//Please add references
namespace WeiXinPF.DAL
{
    /// <summary>
    /// 数据访问类:wx_diancai_dingdan_manage
    /// </summary>
    public partial class wx_diancai_dingdan_manage
    {
        public wx_diancai_dingdan_manage()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "wx_diancai_dingdan_manage");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from wx_diancai_dingdan_manage");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(WeiXinPF.Model.wx_diancai_dingdan_manage model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into wx_diancai_dingdan_manage(");
            strSql.Append("shopinfoid,openid,wid,orderNumber,deskNumber,customerName,customerTel,address,oderTime,oderRemark,payAmount,payStatus,createDate)");
            strSql.Append(" values (");
            strSql.Append("@shopinfoid,@openid,@wid,@orderNumber,@deskNumber,@customerName,@customerTel,@address,@oderTime,@oderRemark,@payAmount,@payStatus,@createDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@shopinfoid", SqlDbType.Int,4),
					new SqlParameter("@openid", SqlDbType.VarChar,200),
					new SqlParameter("@wid", SqlDbType.Int),
					new SqlParameter("@orderNumber", SqlDbType.VarChar,200),
					new SqlParameter("@deskNumber", SqlDbType.VarChar,200),
					new SqlParameter("@customerName", SqlDbType.VarChar,200),
					new SqlParameter("@customerTel", SqlDbType.VarChar,200),
					new SqlParameter("@address", SqlDbType.VarChar,300),
					new SqlParameter("@oderTime", SqlDbType.DateTime),
					new SqlParameter("@oderRemark", SqlDbType.VarChar,300),
					new SqlParameter("@payAmount", SqlDbType.Float,8),
					new SqlParameter("@payStatus", SqlDbType.Int,4),
					new SqlParameter("@createDate", SqlDbType.DateTime)};
            parameters[0].Value = model.shopinfoid;
            parameters[1].Value = model.openid;
            parameters[2].Value = model.wid;
            parameters[3].Value = model.orderNumber;
            parameters[4].Value = model.deskNumber;
            parameters[5].Value = model.customerName;
            parameters[6].Value = model.customerTel;
            parameters[7].Value = model.address;
            parameters[8].Value = model.oderTime;
            parameters[9].Value = model.oderRemark;
            parameters[10].Value = model.payAmount;
            parameters[11].Value = model.payStatus;
            parameters[12].Value = model.createDate;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(WeiXinPF.Model.wx_diancai_dingdan_manage model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update wx_diancai_dingdan_manage set ");
            strSql.Append("shopinfoid=@shopinfoid,");
            strSql.Append("openid=@openid,");
            strSql.Append("wid=@wid,");
            strSql.Append("orderNumber=@orderNumber,");
            strSql.Append("deskNumber=@deskNumber,");
            strSql.Append("customerName=@customerName,");
            strSql.Append("customerTel=@customerTel,");
            strSql.Append("address=@address,");
            strSql.Append("oderTime=@oderTime,");
            strSql.Append("oderRemark=@oderRemark,");
            strSql.Append("payAmount=@payAmount,");
            strSql.Append("payStatus=@payStatus,");
            strSql.Append("createDate=@createDate");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@shopinfoid", SqlDbType.Int,4),
					new SqlParameter("@openid", SqlDbType.VarChar,200),
					new SqlParameter("@wid", SqlDbType.Int),
					new SqlParameter("@orderNumber", SqlDbType.VarChar,200),
					new SqlParameter("@deskNumber", SqlDbType.VarChar,200),
					new SqlParameter("@customerName", SqlDbType.VarChar,200),
					new SqlParameter("@customerTel", SqlDbType.VarChar,200),
					new SqlParameter("@address", SqlDbType.VarChar,300),
					new SqlParameter("@oderTime", SqlDbType.DateTime),
					new SqlParameter("@oderRemark", SqlDbType.VarChar,300),
					new SqlParameter("@payAmount", SqlDbType.Float,8),
					new SqlParameter("@payStatus", SqlDbType.Int,4),
					new SqlParameter("@createDate", SqlDbType.DateTime),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.shopinfoid;
            parameters[1].Value = model.openid;
            parameters[2].Value = model.wid;
            parameters[3].Value = model.orderNumber;
            parameters[4].Value = model.deskNumber;
            parameters[5].Value = model.customerName;
            parameters[6].Value = model.customerTel;
            parameters[7].Value = model.address;
            parameters[8].Value = model.oderTime;
            parameters[9].Value = model.oderRemark;
            parameters[10].Value = model.payAmount;
            parameters[11].Value = model.payStatus;
            parameters[12].Value = model.createDate;
            parameters[13].Value = model.id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from wx_diancai_dingdan_manage ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from wx_diancai_dingdan_manage ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public WeiXinPF.Model.wx_diancai_dingdan_manage GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,shopinfoid,openid,wid,orderNumber,deskNumber,customerName,customerTel,address,oderTime,oderRemark,payAmount,payStatus,createDate from wx_diancai_dingdan_manage ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            WeiXinPF.Model.wx_diancai_dingdan_manage model = new WeiXinPF.Model.wx_diancai_dingdan_manage();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public WeiXinPF.Model.wx_diancai_dingdan_manage DataRowToModel(DataRow row)
        {
            WeiXinPF.Model.wx_diancai_dingdan_manage model = new WeiXinPF.Model.wx_diancai_dingdan_manage();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["shopinfoid"] != null && row["shopinfoid"].ToString() != "")
                {
                    model.shopinfoid = int.Parse(row["shopinfoid"].ToString());
                }
                if (row["openid"] != null)
                {
                    model.openid = row["openid"].ToString();
                }
                if (row["wid"] != null && row["wid"].ToString() != "")
                {
                    model.wid = int.Parse(row["wid"].ToString());
                }
                if (row["orderNumber"] != null)
                {
                    model.orderNumber = row["orderNumber"].ToString();
                }
                if (row["deskNumber"] != null)
                {
                    model.deskNumber = row["deskNumber"].ToString();
                }
                if (row["customerName"] != null)
                {
                    model.customerName = row["customerName"].ToString();
                }
                if (row["customerTel"] != null)
                {
                    model.customerTel = row["customerTel"].ToString();
                }
                if (row["address"] != null)
                {
                    model.address = row["address"].ToString();
                }
                if (row["oderTime"] != null && row["oderTime"].ToString() != "")
                {
                    model.oderTime = DateTime.Parse(row["oderTime"].ToString());
                }
                if (row["oderRemark"] != null)
                {
                    model.oderRemark = row["oderRemark"].ToString();
                }
                if (row["payAmount"] != null && row["payAmount"].ToString() != "")
                {
                    model.payAmount = decimal.Parse(row["payAmount"].ToString());
                }
                if (row["payStatus"] != null && row["payStatus"].ToString() != "")
                {
                    model.payStatus = int.Parse(row["payStatus"].ToString());
                }
                if (row["createDate"] != null && row["createDate"].ToString() != "")
                {
                    model.createDate = DateTime.Parse(row["createDate"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,shopinfoid,openid,wid,orderNumber,deskNumber,customerName,customerTel,address,oderTime,oderRemark,payAmount,payStatus,createDate ");
            strSql.Append(" FROM wx_diancai_dingdan_manage ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得商品列表
        /// </summary>
        public DataSet GetCommodityList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.id as id,shopinfoid,openid,wid,orderNumber,deskNumber,customerName,customerTel,address,oderTime,oderRemark,payAmount,payStatus,createDate,b.id AS cid  FROM wx_diancai_dingdan_manage AS a ");
            strSql.Append(" right join (select * from wx_diancai_dingdan_commodity where status=1 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            strSql.Append(" ) as b on a.id=b.dingId ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,shopinfoid,openid,wid,orderNumber,deskNumber,customerName,customerTel,address,oderTime,oderRemark,payAmount,payStatus,createDate ");
            strSql.Append(" FROM wx_diancai_dingdan_manage ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM wx_diancai_dingdan_manage ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from wx_diancai_dingdan_manage T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "wx_diancai_dingdan_manage";
            parameters[1].Value = "id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,shopinfoid,openid,wid,orderNumber,deskNumber,customerName,customerTel,address,oderTime,oderRemark,payAmount,payStatus,'' as payStatusStr,createDate ");
            strSql.Append(" FROM wx_diancai_dingdan_manage ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        public DataSet GetOrderList(int shopId, int pageSize, int pageIndex, DateTime beginDate, DateTime endDate, int payAmountMin,
                                    int payAmountMax, string orderNumber, string customerName, string customerTel, out int totalCount)
        {
            var totalParam = new SqlParameter()
                {
                    ParameterName = "@TotalCount",
                    SqlDbType = SqlDbType.Int,
                    Value = shopId,
                    Direction = ParameterDirection.Output
                };
            var sqlparams = new List<SqlParameter>
                {
                    new SqlParameter() {ParameterName = "@ShopID", SqlDbType = SqlDbType.Int, Value = shopId},
                    new SqlParameter() {ParameterName = "@PageSize", SqlDbType = SqlDbType.Int, Value = pageSize},
                    new SqlParameter() {ParameterName = "@PageIndex", SqlDbType = SqlDbType.Int, Value = pageIndex},
                    new SqlParameter() {ParameterName = "@BeginCreateTime", SqlDbType = SqlDbType.DateTime},
                    new SqlParameter() {ParameterName = "@EndCreateTime", SqlDbType = SqlDbType.DateTime},
                    new SqlParameter() {ParameterName = "@PayAmountMin", SqlDbType = SqlDbType.Int, Value = payAmountMin},
                    new SqlParameter() {ParameterName = "@PayAmountMax", SqlDbType = SqlDbType.Int, Value = payAmountMax},
                    new SqlParameter() {ParameterName = "@OrderNumber", SqlDbType = SqlDbType.NVarChar, Value = orderNumber},
                    new SqlParameter() {ParameterName = "@CustomerName", SqlDbType = SqlDbType.NVarChar, Value = customerName},
                    new SqlParameter() {ParameterName = "@CustomerTel", SqlDbType = SqlDbType.NVarChar, Value = customerTel},
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

            var result = DbHelperSQL.RunProcedure("usp_wx_diancai_dingdan_manage_OrderManage", sqlparams.ToArray(), "wx_diancai_dingdan_manage");

            totalCount = totalParam.Value == DBNull.Value ? 0 : Convert.ToInt32(totalParam.Value);

            return result;
        }

        public string GetOrderCaipinDetail(int orderId)
        {
            var sqlparams = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName = "@OrderID",SqlDbType = SqlDbType.Int,Value = orderId}
                };

            var result = DbHelperSQL.RunProcedure("usp_wx_diancai_dingdan_manage_OrderDetail", sqlparams.ToArray(), "orderDetail");

            var resultString = new StringBuilder();
            if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in result.Tables[0].Rows)
                {
                    if (row[0] != null && row[0] != DBNull.Value)
                    {
                        resultString.Append(row[0].ToString() + Environment.NewLine);
                    }
                }
            }

            return resultString.ToString();
        }

        public DataSet GetCredentialsList(int shopid, string condition, string moduleName, out double totalAmount)
        {
            var strProc = "USP_Verification_GetCredentialList @ShopId, @ModuleName, @Condition";

            var sqlParams = new List<SqlParameter>();

            sqlParams.Add(new SqlParameter("@ShopId", SqlDbType.Int) { Value = shopid });
            sqlParams.Add(new SqlParameter("@ModuleName", SqlDbType.VarChar, 50) { Value = moduleName });
            sqlParams.Add(new SqlParameter("@Condition", SqlDbType.VarChar) { Value = condition });

            var ds = DbHelperSQL.Query(strProc, sqlParams.ToArray());
            DataTable dt = ds.Tables[0];
            totalAmount = 0.0;
            for (int row = 0; row < dt.Rows.Count; row++)
                totalAmount += Convert.ToDouble(dt.Rows[row]["payAmount"].ToString());
            return ds;
        }

        public DataSet GetCredentialsCommodityList(int dingId, string moduleName)
        {
            var strProc = "USP_Verification_GetCredentialDetailList @OrderId, @ModuleName";

            var sqlParams = new List<SqlParameter>();

            sqlParams.Add(new SqlParameter("@OrderId", SqlDbType.Int) { Value = dingId });
            sqlParams.Add(new SqlParameter("@ModuleName", SqlDbType.VarChar, 50) { Value = moduleName });

            return DbHelperSQL.Query(strProc, sqlParams.ToArray());
        }

        public DataSet GetPayList(string openid)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select id,shopinfoid,openid,wid,orderNumber,deskNumber,customerName,customerTel,address,oderTime,oderRemark,payAmount,payStatus,createDate ");
            //strSql.Append(" FROM wx_diancai_dingdan_manage ");

            strSql.Append(@"SELECT  d.id ,
                                            shopinfoid ,
                                            s.hotelName ,
                                            openid ,
                                            d.wid ,
                                            t.orderNumber ,
                                            deskNumber ,
                                            customerName ,
                                            customerTel ,
                                            d.address ,
                                            CONVERT(VARCHAR(10), oderTime, 120) AS oderTime ,
                                            oderRemark ,
                                            payAmount ,
                                            payStatus ,
                                            CONVERT(VARCHAR(10), d.createDate, 120) AS CreateDate ,
		                                      t.OrderCount
                                    FROM    ( SELECT    orderNumber ,
                                                        COUNT(1) AS OrderCount
                                              FROM      dbo.wx_diancai_dingdan_manage dm
                                                        INNER JOIN dbo.wx_Verification_IdentifyingCodeInfo vi ON dm.orderNumber = vi.OrderCode
                                                                                                  AND vi.ModuleName = 'restaurant'
                                              WHERE     openid = @OpenID
                                              GROUP BY  orderNumber
                                            ) t
                                            INNER JOIN wx_diancai_dingdan_manage d ON d.orderNumber = t.orderNumber
                                            LEFT JOIN wx_diancai_shopinfo s ON d.shopinfoid = s.id
                                    WHERE   payStatus > 0
                                    ORDER BY d.createDate DESC");
            SqlParameter[] sqlparams =
                {
                    new SqlParameter(){ParameterName = "@OpenID",SqlDbType = SqlDbType.NVarChar,Value = openid}
                };

            return DbHelperSQL.Query(strSql.ToString(), sqlparams);
        }

        public DataSet GetMyOrderInShop(string openid, int shopid)
        {
            var strSql = new StringBuilder();

            strSql.Append(@"SELECT  d.id ,
                                        shopinfoid ,
                                        s.hotelName ,
                                        openid ,
                                        d.wid ,
                                        orderNumber ,
                                        deskNumber ,
                                        customerName ,
                                        customerTel ,
                                        d.address ,
                                        oderTime ,
                                        oderRemark ,
                                        payAmount ,
                                        payStatus ,
                                        d.createDate
                                FROM    wx_diancai_dingdan_manage d
                                        LEFT JOIN wx_diancai_shopinfo s ON d.shopinfoid = s.id
                                WHERE payStatus>0 AND d.shopinfoid = @ShopID AND d.openid = @OpenID
                                Order By d.createDate DESC");

            SqlParameter[] sqlparams =
                {
                    new SqlParameter(){ParameterName = "@OpenID",SqlDbType = SqlDbType.NVarChar,Value = openid},
                    new SqlParameter(){ParameterName = "@ShopID",SqlDbType = SqlDbType.Int,Value = shopid}
                };

            return DbHelperSQL.Query(strSql.ToString(), sqlparams);
        }

        public DataSet GetDingdanRefundDetail(int shopid, int dingdanid, string openid, int caiid)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  vi.IdentifyingCodeId ,
                                            ds.hotelName ,
                                            dm.openid ,
                                            dm.orderNumber ,
                                            vi.IdentifyingCode ,
                                            cm.cpName ,
                                            dc.price ,
                                            vi.ProductId ,
                                            vi.IdentifyingCode ,
                                            dc.price ,
                                            vi.Status
                                    FROM    dbo.wx_diancai_dingdan_manage dm
                                            INNER JOIN dbo.wx_diancai_dingdan_caiping dc ON dm.id = dc.dingId
                                            LEFT JOIN dbo.wx_diancai_caipin_manage cm ON dc.caiId = cm.id
                                            INNER JOIN dbo.wx_Verification_IdentifyingCodeInfo vi ON vi.OrderId = dm.id
                                                                                                  AND vi.ModuleName = 'restaurant'
                                                                                                  AND cm.id = vi.ProductId
                                            LEFT JOIN dbo.wx_diancai_shopinfo ds ON dm.shopinfoid = ds.id
                                    WHERE   vi.Status = 1  
                                       AND vi.OrderID = @DindanID
		                                AND dm.openid=@OpenID
		                                AND vi.ProductID=@CaiID
		                                AND dm.shopinfoid=@ShopInfoID");
            SqlParameter[] sqlparams =
                {
                    new SqlParameter(){ParameterName = "@DindanID",SqlDbType = SqlDbType.Int,Value = dingdanid},
                    new SqlParameter(){ParameterName = "@OpenID",SqlDbType = SqlDbType.NVarChar,Value = openid},
                    new SqlParameter(){ParameterName = "@CaiID",SqlDbType = SqlDbType.Int,Value = caiid},
                    new SqlParameter(){ParameterName = "@ShopInfoID",SqlDbType = SqlDbType.Int,Value = shopid}
                };

            return DbHelperSQL.Query(strSql.ToString(), sqlparams);
        }

        private DataTable GetDingdanNoUsedCount(string openId, int orderId, int caipinId)
        {
            const string sql = @"DECLARE @NoUseCaipinCount INT;
                                    DECLARE @NoUseAllCount INT;

                                    SELECT  @NoUseCaipinCount = COUNT(1)
                                    FROM    dbo.wx_diancai_dingdan_manage m
                                            INNER JOIN dbo.wx_Verification_IdentifyingCodeInfo vi ON m.id = vi.OrderId AND vi.ModuleName='restaurant'
                                    WHERE   vi.status = 1
                                            AND m.id = @OrderID
                                            AND m.openid = @OpenID
                                            AND vi.ProductId = @CaiPinID;

                                    SELECT  @NoUseAllCount = COUNT(1)
                                    FROM    dbo.wx_diancai_dingdan_manage m
                                            INNER JOIN dbo.wx_Verification_IdentifyingCodeInfo vi ON m.id = vi.OrderId AND vi.ModuleName='restaurant'
                                    WHERE   vi.status = 1
                                            AND m.id = @OrderID
                                            AND m.openid = @OpenID;

                                    SELECT  @NoUseCaipinCount AS NoUseCaipinCount ,
                                            @NoUseAllCount AS NoUseAllCount";
            SqlParameter[] sqlparams =
            {
                    new SqlParameter(){ParameterName = "@OrderID",SqlDbType = SqlDbType.Int,Value = orderId},
                    new SqlParameter(){ParameterName = "@OpenID",SqlDbType = SqlDbType.NVarChar,Value = openId},
                    new SqlParameter(){ParameterName = "@CaiPinID",SqlDbType = SqlDbType.Int,Value = caipinId} 
                };

            var result = DbHelperSQL.Query(sql, sqlparams);

            return result.Tables.Count > 0 ? result.Tables[0] : null;
        }

        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="shopinfiId"></param>
        /// <param name="openid"></param>
        /// <param name="wid"></param>
        /// <param name="refundAmount"></param>
        /// <param name="dingdanid"></param>
        /// <param name="caiid"></param>
        /// <param name="caipinIdList"></param>
        /// <returns></returns>
        public bool RefundDiancai(int shopinfiId, string openid, int wid, int refundAmount, int dingdanid, int caiid, List<Guid> caipinIdList)
        {
            if (caipinIdList == null || caipinIdList.Count == 0)
                return true;
            //启用事务
            //1、修改菜品状态
            //2、写入待退款记录
            //3、修改订单状态，看是全部退款还是部分退款


            var commandInfoList = new List<CommandInfo>();
            //获取此订单下此类菜品尚未使用的菜品数量
            var noUseCaiDataTable = GetDingdanNoUsedCount(openid, dingdanid, caiid);
            var noUseCaiCount = 0;
            var noUseCaiAllCount = 0;
            if (noUseCaiDataTable == null || noUseCaiDataTable.Rows.Count == 0)
                throw new Exception("无此订单");

            noUseCaiCount = Convert.ToInt32(noUseCaiDataTable.Rows[0]["NoUseCaipinCount"].ToString());
            noUseCaiAllCount = Convert.ToInt32(noUseCaiDataTable.Rows[0]["NoUseAllCount"].ToString());

            if (noUseCaiCount == 0)
                return true;

            //修改菜品状态
            var refundCaiId = new StringBuilder();
            for (int index = 0; index < caipinIdList.Count; index++)
            {
                refundCaiId.AppendFormat(index == caipinIdList.Count - 1 ? "'{0}'" : "'{0}',", caipinIdList[index].ToString());
            }

            string caipinModifySql = string.Format("UPDATE dbo.wx_Verification_IdentifyingCodeInfo SET Status=3,ModifyTime=GETDATE() WHERE OrderId=@DingdanId AND ProductId=@CaiID AND IdentifyingCodeId IN ({0});", refundCaiId.ToString());
            SqlParameter[] caipinModifysqlparams =
                {
                    new SqlParameter(){ParameterName = "@DingdanId",SqlDbType = SqlDbType.Int,Value = dingdanid},
                    new SqlParameter(){ParameterName = "@CaiID",SqlDbType = SqlDbType.Int,Value = caiid},
                };

            var caipinModifyCommand = new CommandInfo() { CommandText = caipinModifySql, Parameters = caipinModifysqlparams };
            commandInfoList.Add(caipinModifyCommand);

            var currentDate = DateTime.Now;
            //写入待退款记录
            const string insertTuidanSql = @"INSERT INTO dbo.wx_diancai_tuidan_manage
                                                                ( shopinfoid ,
                                                                  openid ,
                                                                  wid ,
                                                                  dingdanid,
                                                                  caipinid ,
                                                                  refundCode ,
                                                                  refundTime ,
                                                                  refundAmount ,
                                                                  refundStatus ,
                                                                  createDate
                                                                )
                                                        VALUES  ( @ShopInfoID ,
                                                                  @OpenID ,
                                                                  @Wid ,
                                                                  @DingdanID,
                                                                  @CaiPinID ,
                                                                  @RefundCode ,
                                                                  null ,
                                                                  @RefundAmount ,
                                                                  3 ,
                                                                  @CurrentDate
                                                                )";

            foreach (var id in caipinIdList)
            {
                SqlParameter[] insertTuidanParams =
                {
                    new SqlParameter(){ParameterName = "@ShopInfoID",SqlDbType = SqlDbType.Int,Value = shopinfiId},
                    new SqlParameter(){ParameterName = "@OpenID",SqlDbType = SqlDbType.NVarChar,Value = openid},
                    new SqlParameter(){ParameterName = "@Wid",SqlDbType = SqlDbType.Int,Value = wid},
                    new SqlParameter(){ParameterName = "@DingdanID",SqlDbType = SqlDbType.Int,Value = dingdanid},
                    new SqlParameter(){ParameterName = "@CaiPinID",SqlDbType = SqlDbType.UniqueIdentifier,Value = id},
                    new SqlParameter(){ParameterName = "@RefundCode",SqlDbType = SqlDbType.NVarChar,Value = "T"+Utils.Number(13)}, 
                    new SqlParameter(){ParameterName = "@RefundAmount",SqlDbType = SqlDbType.Int,Value = refundAmount},
                    new SqlParameter(){ParameterName = "@CurrentDate",SqlDbType = SqlDbType.DateTime,Value = currentDate}
                };

                commandInfoList.Add(new CommandInfo() { CommandText = insertTuidanSql, Parameters = insertTuidanParams });
            }

            //修改订单状态
            const string updateDingdanStatus = @"UPDATE dbo.wx_diancai_dingdan_manage SET payStatus=@PayStatus WHERE id=@DingdanID AND openid=@OpenID";
            var payStatus = caipinIdList.Count == noUseCaiAllCount ? 3 : 2;
            SqlParameter[] updateDingdanStatusParams =
                    {
                        new SqlParameter(){ParameterName = "@PayStatus",SqlDbType = SqlDbType.Int,Value = payStatus},
                        new SqlParameter(){ParameterName = "@DingdanID",SqlDbType = SqlDbType.Int,Value = dingdanid},
                        new SqlParameter(){ParameterName = "@OpenID",SqlDbType = SqlDbType.NVarChar,Value = openid}, 
                    };

            var updateDingdanCommand = new CommandInfo() { CommandText = updateDingdanStatus, Parameters = updateDingdanStatusParams };
            commandInfoList.Add(updateDingdanCommand);

            var result = DbHelperSQL.ExecuteSqlTran(commandInfoList);

            return result > 0;
        }


        /// <summary>
        /// 支付完成
        /// </summary>
        /// <param name="orderNumber"></param>
        public void PaySuccess(string orderNumber)
        {
            //状态为2表示退款中
            const string sql = "UPDATE dbo.wx_diancai_dingdan_manage SET payStatus=1 WHERE orderNumber=@OrderNumber";
            SqlParameter[] sqlparams =
                {
                    new SqlParameter() {ParameterName = "@OrderNumber", SqlDbType = SqlDbType.NVarChar, Value = orderNumber}
                };

            DbHelperSQL.ExecuteSql(sql, sqlparams);
        }


        public bool Update(int id, decimal payAmount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  wx_diancai_dingdan_manage set payAmount=" + payAmount + "  ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public DataSet Getcaopin(string dingdan)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select aa.id,aa.cpName as cpName,bb.price as price,bb.num as num,bb.totpric as totpric  from wx_diancai_caipin_manage  as aa ");
            strSql.Append(" right join (select * from wx_diancai_dingdan_caiping where dingId='" + dingdan + "') as bb on aa.id=bb.caiId ");

            return DbHelperSQL.Query(strSql.ToString());
        }

        public void AfterVerification(int wid, int shopId, int orderID)
        {
            SqlParameter[] sqlparams =
                {
                    new SqlParameter(){ParameterName = "@wid",SqlDbType = SqlDbType.Int,Value = wid}, 
                    new SqlParameter(){ParameterName = "@ShopID",SqlDbType = SqlDbType.Int,Value = shopId}, 
                    new SqlParameter(){ParameterName = "@OrderID",SqlDbType = SqlDbType.Int,Value = orderID},
                };

            DbHelperSQL.RunProcedure("usp_diancai_AfterVerification", sqlparams);
        }


        public DataSet Getcaopin(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select aa.id,aa.cpName as cpName,bb.price as price,bb.num as num,bb.totpric as totpric  from wx_diancai_caipin_manage  as aa ");
            strSql.Append(" right join (select * from wx_diancai_dingdan_caiping where dingId='" + id + "') as bb on aa.id=bb.caiId ");

            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet Getcommodity(string cid)
        {
            var strSql = new StringBuilder();

            strSql.Append("select a.id,a.cpName as cpName,c.price as price,b.status as status FROM wx_diancai_caipin_manage a INNER join ");
            strSql.Append("(select IdentifyingCodeId,CAST(ProductId AS INT) AS ProductId,CAST(OrderId AS INT) AS OrderId,");
            strSql.Append("Status FROM [wx_Verification_IdentifyingCodeInfo] WHERE IdentifyingCodeId='" + cid + "') as b");
            strSql.Append(" ON a.id=b.ProductId INNER JOIN (SELECT price,caiId,dingId FROM dbo.wx_diancai_dingdan_caiping)c");
            strSql.Append(" ON b.ProductId= c.caiId AND c.dingId= b.OrderId");

            //strSql.Append(" select aa.id,aa.cpName as cpName,bb.price as price,bb.status as status from wx_diancai_caipin_manage  as aa ");
            //strSql.Append(" right join (select * from wx_diancai_dingdan_commodity where id='" + cid + "') as bb on aa.id=bb.caiId ");

            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetcommodityTable(string did)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select a.id,a.cpName as cpName,c.price as price,b.status as status FROM wx_diancai_caipin_manage a INNER join ");
            strSql.Append("(select IdentifyingCodeId,CAST(ProductId AS INT) AS ProductId,CAST(OrderId AS INT) AS OrderId,");
            strSql.Append("Status FROM [wx_Verification_IdentifyingCodeInfo] WHERE OrderId='" + did + "') as b");
            strSql.Append(" ON a.id=b.ProductId INNER JOIN (SELECT price,caiId,dingId FROM dbo.wx_diancai_dingdan_caiping)c");
            strSql.Append(" ON b.ProductId= c.caiId AND c.dingId= b.OrderId");

            //strSql.Append(" select aa.id,aa.cpName as cpName,bb.price as price,bb.status from wx_diancai_caipin_manage  as aa ");
            //strSql.Append(" right join (select * from wx_diancai_dingdan_commodity where dingId='" + did + "') as bb on aa.id=bb.caiId ");

            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataSet GetOrderDetail(int orderId)
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.Append(@"SELECT  d.customerName ,
                                                d.customerTel ,
                                                d.orderNumber ,
                                                d.id AS OrderID ,
                                                d.payAmount ,
                                                d.oderTime ,
                                                s.hotelName ,
                                                s.dcRename ,
                                                s.xplace ,
                                                s.yplace ,
                                                s.tel ,
                                                s.hoteltimeBegin ,
                                                s.hoteltimeEnd ,
                                                s.hoteltimeBegin1 ,
                                                s.hoteltimeEnd1 ,
                                                s.hoteltimeBegin2 ,
                                                s.hoteltimeEnd2
                                        FROM    dbo.wx_diancai_dingdan_manage d
                                                LEFT JOIN dbo.wx_diancai_shopinfo s ON d.shopinfoid = s.id
                                        WHERE   d.id = @OrderID;

                                            SELECT distinct  cm.cpName ,
                                                        vi.ProductId ,
                                                        vi.IdentifyingCode ,
                                                        dc.price ,
                                                        vi.Status
                                                FROM    dbo.wx_diancai_dingdan_manage dm
                                                        INNER JOIN dbo.wx_diancai_dingdan_caiping dc ON dm.id = dc.dingId
                                                        INNER JOIN dbo.wx_Verification_IdentifyingCodeInfo vi ON vi.OrderId = dm.id
                                                        LEFT JOIN dbo.wx_diancai_caipin_manage cm ON vi.ProductId = cm.id
                                                WHERE   vi.ModuleName = 'restaurant'
                                                        AND dm.id = @OrderID");

            SqlParameter[] sqlparams =
                {
                    new SqlParameter(){ParameterName = "@OrderID",SqlDbType = SqlDbType.Int,SqlValue = orderId} 
                };

            return DbHelperSQL.Query(sqlBuilder.ToString(), sqlparams);
        }

        /// <summary>
        /// 获取订单菜品详情
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="caipinId"></param>
        /// <returns></returns>
        public DataSet GetOrderCaiPinDetail(int orderId, int caipinId)
        {
            return null;
        }


        public WeiXinPF.Model.wx_diancai_dingdan_manage GetModeldingdan(string dingdan)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,shopinfoid,openid,wid,orderNumber,deskNumber,customerName,customerTel,address,oderTime,oderRemark,payAmount,payStatus,createDate from wx_diancai_dingdan_manage ");
            strSql.Append(" where id=@dingdan");
            SqlParameter[] parameters = {
					new SqlParameter("@dingdan", SqlDbType.NVarChar,200)
			};
            parameters[0].Value = dingdan;

            WeiXinPF.Model.wx_diancai_dingdan_manage model = new WeiXinPF.Model.wx_diancai_dingdan_manage();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        public WeiXinPF.Model.wx_diancai_dingdan_manage GetModeldingdan(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,shopinfoid,openid,wid,orderNumber,deskNumber,customerName,customerTel,address,oderTime,oderRemark,payAmount,payStatus,createDate from wx_diancai_dingdan_manage ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            WeiXinPF.Model.wx_diancai_dingdan_manage model = new WeiXinPF.Model.wx_diancai_dingdan_manage();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        public DataSet GetListshop(int shopid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,shopinfoid,openid,wid,orderNumber,deskNumber,customerName,customerTel,address,oderTime,oderRemark,payAmount,payStatus,createDate ");
            strSql.Append(" FROM wx_diancai_dingdan_manage where shopinfoid='" + shopid + "' order by  createDate desc,id desc  ");

            return DbHelperSQL.Query(strSql.ToString());
        }


        public bool Updatestatus(string id, string state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  wx_diancai_dingdan_manage set payStatus='" + state + "'  ");

            strSql.Append(" where id='" + id + "' ");

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateCommoditystatus(string cid, string status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  wx_diancai_dingdan_commodity set status='" + status + "',modifytime=getdate()  ");

            strSql.Append(" where id='" + cid + "' ");

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateCommodityStatusByOrderCode(string orderCode, string status)
        {
            var strSql = new StringBuilder();
            strSql.Append("update  wx_Verification_IdentifyingCodeInfo set status='" + status + "',modifytime=getdate()  ");

            strSql.Append(" where OrderCode='" + orderCode + "'");

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateCommoditystatus(string ccode, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  wx_diancai_dingdan_commodity set status='" + status + "',modifytime=getdate()  ");

            strSql.Append(" where identifyingcode='" + ccode + "' ");

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(string dingdan)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from wx_diancai_dingdan_manage ");
            strSql.Append(" where   id='" + dingdan + "' ");


            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        #endregion  ExtensionMethod
    }
}

