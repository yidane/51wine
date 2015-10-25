using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Infrastructure.DomainDataAccess.IdentifyingCode.Repository
{
    using System.Data;
    using System.Data.Entity;
    using System.Data.SqlClient;

    using WeiXinPF.Common;
    using WeiXinPF.DBUtility;
    using WeiXinPF.Infrastructure.DomainDataAccess.IdentifyingCode.DTO;

    public class IdentifyingCodeRepository: EFRepository<IdentifyingCodeInfo>
    {
        public IdentifyingCodeRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public IdentifyingCodeRepository()
            : base(new WXDBContext())
        {
        }

        public bool AddIdentifyingCode(IdentifyingCodeInfo code)
        {
            var sqlParams = new List<SqlParameter>();

            sqlParams.Add(new SqlParameter("@IdentifyingCodeId", SqlDbType.UniqueIdentifier){Value = code.IdentifyingCodeId});
            sqlParams.Add(new SqlParameter("@ModuleName", SqlDbType.NVarChar, 50){Value = code.ModuleName});
            sqlParams.Add(new SqlParameter("@OrderId", SqlDbType.NVarChar, 50){Value = code.OrderId});
            sqlParams.Add(new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50){Value = code.OrderCode});
            sqlParams.Add(new SqlParameter("@ProductId", SqlDbType.NVarChar, 50){Value = code.ProductId});
            sqlParams.Add(new SqlParameter("@ProductCode", SqlDbType.NVarChar, 50){Value = code.ProductCode});
            sqlParams.Add(new SqlParameter("@IdentifyingCode", SqlDbType.NVarChar, 100){Value = code.IdentifyingCode});
            sqlParams.Add(new SqlParameter("@CreateTime", SqlDbType.DateTime){Value = code.CreateTime});
            sqlParams.Add(new SqlParameter("@ModifyTime", SqlDbType.DateTime){Value = code.ModifyTime});
            sqlParams.Add(new SqlParameter("@STATUS", SqlDbType.Int){Value = code.Status});
            sqlParams.Add(new SqlParameter("@Wid", SqlDbType.Int){Value = code.Wid});
            sqlParams.Add(new SqlParameter("@ShopId", SqlDbType.NVarChar, 50){Value = code.ShopId});

            var strSql = @"USP_Verification_AddIdentifyingCode @IdentifyingCodeId, @ModuleName, @OrderId, @OrderCode, @ProductId, @ProductCode, @IdentifyingCode, @CreateTime, @ModifyTime, @STATUS, @Wid, @ShopId";
            return this.Context.Database.SqlQuery<int>(strSql, sqlParams.ToArray()).First() == 1;
        }

        public IList<IdentifyingCodeDetailSearchDTO> GetIdentifyingCodeDetailById(IdentifyingCodeInfo code)
        {
            var strSql = new StringBuilder();

            if (code == null)
            {
                return null;
            }

            if (code.ModuleName.Equals("restaurant"))
            {
                strSql.Append("select a.id AS ProductId,b.OrderId AS OrderId,a.cpName as ProductName,c.price as Price,b.status as Status FROM wx_diancai_caipin_manage a INNER join ");
                strSql.Append("(select IdentifyingCodeId,CAST(ProductId AS INT) AS ProductId,CAST(OrderId AS INT) AS OrderId,");
                strSql.Append("Status FROM [wx_Verification_IdentifyingCodeInfo] WHERE IdentifyingCodeId='" + code.IdentifyingCodeId + "') as b");
                strSql.Append(" ON a.id=b.ProductId INNER JOIN (SELECT price,caiId,dingId FROM dbo.wx_diancai_dingdan_caiping)c");
                strSql.Append(" ON b.ProductId= c.caiId AND c.dingId= b.OrderId");
            }else if (code.ModuleName.Equals("hotel"))
            {
                strSql.Append("select a.id AS OrderId,a.roomid AS ProductId,a.roomType as ProductName,a.price as Price,b.status as Status FROM dbo.wx_hotel_dingdan a INNER join ");
                strSql.Append("(select IdentifyingCodeId,CAST(OrderId AS INT) AS OrderId,Status FROM [wx_Verification_IdentifyingCodeInfo] ");
                strSql.Append("WHERE IdentifyingCodeId='" + code.IdentifyingCodeId +"') as b ON a.id=b.OrderId ");
            }

            using (var db = new WXDBContext())
            {
                return db.Database.SqlQuery<IdentifyingCodeDetailSearchDTO>(strSql.ToString()).ToList();
            }
        }

        public IList<OrderDetailDTO> GetOrderDetail(IdentifyingCodeInfo code, string condition)
        {
            var strProc = "USP_Verification_GetCredentialList @ShopId, @ModuleName, @Condition";

            var sqlParams = new List<SqlParameter>();

            sqlParams.Add(new SqlParameter("@ShopId", SqlDbType.Int) { Value = code.ShopId });
            sqlParams.Add(new SqlParameter("@ModuleName", SqlDbType.VarChar, 50) { Value = code.ModuleName });
            sqlParams.Add(new SqlParameter("@Condition", SqlDbType.VarChar) { Value = condition });

            return DbHelperSQL.Query(strProc, sqlParams.ToArray()).Tables[0].ToObject<OrderDetailDTO>().ToList();
        }

        public IList<IdentifyingCodeDTO> GetIdentifyingCodeDTO(string moduleName, int orderId)
        {
            var strProc = "USP_Verification_GetCredentialDetailList @OrderId, @ModuleName";

            var sqlParams = new List<SqlParameter>();

            sqlParams.Add(new SqlParameter("@OrderId", SqlDbType.Int) { Value = orderId });
            sqlParams.Add(new SqlParameter("@ModuleName", SqlDbType.VarChar, 50) { Value = moduleName });

            return DbHelperSQL.Query(strProc, sqlParams.ToArray()).Tables[0].ToObject<IdentifyingCodeDTO>().ToList();
        }
    }
}
