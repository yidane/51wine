using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Infrastructure.DomainDataAccess.IdentifyingCode.Repository
{
    using System.Data;
    using System.Data.Entity;
    using System.Data.SqlClient;

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
    }
}
