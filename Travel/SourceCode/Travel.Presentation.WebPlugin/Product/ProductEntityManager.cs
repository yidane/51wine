using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PetroChina.Riped.DBAccess;
using Travel.Infrastructure.CommonFunctions;

namespace Travel.Presentation.WebPlugin.Product
{
    public class ProductEntityManager
    {
        public ProductEntity InsertOrUpdate(ProductEntity entity, bool isAdd)
        {
            var sqlparams = new List<SqlParameter>()
                {
//@IsAdd BIT ,
//@ProductId INT ,
//@ProductPackageId INT ,
//@ProductSource NVARCHAR(128) ,
//@ProductCategoryId UNIQUEIDENTIFIER ,
//@ProductName NVARCHAR(500) ,
//@OldProductName NVARCHAR(500) ,
//@ProductType NVARCHAR(500) ,
//@ProductCode NVARCHAR(500) ,
//@ProductPrice DECIMAL ,
//@ProductDescription NVARCHAR(500) ,
//@IsCombinedProduct BIT ,
//@IsSelfDefinedProduct BIT ,
//@CurrentStatus NVARCHAR(500) ,
//@FirstSort NVARCHAR(500) ,
//@SecondSort NVARCHAR(500)
                    new SqlParameter(){ParameterName = "@IsAdd",Value = isAdd,SqlDbType = SqlDbType.Bit},
                    new SqlParameter(){ParameterName = "@ProductId",Value = entity.ProductId,SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ParameterName = "@ProductPackageId",Value = entity.ProductPackageId,SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ParameterName = "@ProductSource",Value = entity.ProductSource,SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ParameterName = "@ProductCategoryId",Value = entity.ProductCategoryId,SqlDbType = SqlDbType.UniqueIdentifier},
                    new SqlParameter(){ParameterName = "@ProductName",Value = entity.ProductName,SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ParameterName = "@OldProductName",Value = entity.OldProductName,SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ParameterName = "@ProductType",Value = entity.ProductType,SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ParameterName = "@ProductCode",Value = entity.ProductCode,SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ParameterName = "@ProductPrice",Value = entity.ProductPrice,SqlDbType = SqlDbType.Decimal},
                    new SqlParameter(){ParameterName = "@ProductDescription",Value = entity.ProductDescription,SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ParameterName = "@IsCombinedProduct",Value = entity.IsCombinedProduct,SqlDbType = SqlDbType.Bit},
                    new SqlParameter(){ParameterName = "@IsSelfDefinedProduct",Value = entity.IsSelfDefinedProduct,SqlDbType = SqlDbType.Bit},
                    new SqlParameter(){ParameterName = "@CurrentStatus",Value = entity.CurrentStatus,SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ParameterName = "@FirstSort",Value = entity.FirstSort,SqlDbType = SqlDbType.NVarChar},
                    new SqlParameter(){ParameterName = "@SecondSort",Value = entity.SecondSort,SqlDbType = SqlDbType.NVarChar}
                };

            var result = new SqlHelper().ExecuteDataTable(WebConfigureHelper.ConnectionStrings.DbConnection, CommandType.StoredProcedure, "usp_InsertOrUpdate_ProductCategoryEntity", sqlparams.ToArray());

            if (result != null && result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                var newProductEntity = new ProductEntity();

                newProductEntity.CurrentStatus = row.Field<string>("CurrentStatus");
                newProductEntity.FirstSort = row.Field<string>("FirstSort");
                newProductEntity.IsCombinedProduct = row.Field<bool>("IsCombinedProduct");
                newProductEntity.IsSelfDefinedProduct = row.Field<bool>("IsSelfDefinedProduct");
                newProductEntity.OldProductName = row.Field<string>("OldProductName");
                newProductEntity.ProductCategoryId = row.Field<Guid>("ProductCategoryId");
                newProductEntity.ProductCode = row.Field<string>("ProductCode");
                newProductEntity.ProductDescription = row.Field<string>("ProductDescription");
                newProductEntity.ProductId = row.Field<int>("ProductId");
                newProductEntity.ProductName = row.Field<string>("ProductName");
                newProductEntity.ProductPackageId = row.Field<int>("ProductPackageId");
                newProductEntity.ProductPrice = row.Field<decimal>("ProductPrice");
                newProductEntity.ProductSource = row.Field<string>("ProductSource");
                newProductEntity.ProductType = row.Field<string>("ProductType");
                newProductEntity.SecondSort = row.Field<string>("SecondSort");

                return newProductEntity;
            }

            return null;
        }

        public bool DeleteEntity(int productId, int productPackageId, string productSource)
        {
            var sqlparams = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName = "@ProductId",Value = productId,SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ParameterName = "@ProductPackageId",Value = productPackageId,SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ParameterName = "@ProductSource",Value = productSource,SqlDbType = SqlDbType.NVarChar}
                };

            new SqlHelper().ExecuteNonQuery(WebConfigureHelper.ConnectionStrings.DbConnection, CommandType.StoredProcedure, "usp_DeleteProductEntity", sqlparams.ToArray());

            return true;
        }

        public List<ProductEntity> GetProductEntityList()
        {
            var rtnProductEntityList = new List<ProductEntity>();
            var result = new SqlHelper().ExecuteDataTable(WebConfigureHelper.ConnectionStrings.DbConnection, CommandType.StoredProcedure, "usp_GetProductCategoryEntityList");
            if (result != null && result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    var newProductEntity = new ProductEntity();

                    newProductEntity.CurrentStatus = row.Field<string>("CurrentStatus");
                    newProductEntity.FirstSort = row.Field<string>("FirstSort");
                    newProductEntity.IsCombinedProduct = row.Field<bool>("IsCombinedProduct");
                    newProductEntity.IsSelfDefinedProduct = row.Field<bool>("IsSelfDefinedProduct");
                    newProductEntity.OldProductName = row.Field<string>("OldProductName");
                    newProductEntity.ProductCategoryId = row.Field<Guid>("ProductCategoryId");
                    newProductEntity.ProductCode = row.Field<string>("ProductCode");
                    newProductEntity.ProductDescription = row.Field<string>("ProductDescription");
                    newProductEntity.ProductId = row.Field<int>("ProductId");
                    newProductEntity.ProductName = row.Field<string>("ProductName");
                    newProductEntity.ProductPackageId = row.Field<int>("ProductPackageId");
                    newProductEntity.ProductPrice = row.Field<decimal>("ProductPrice");
                    newProductEntity.ProductSource = row.Field<string>("ProductSource");
                    newProductEntity.ProductType = row.Field<string>("ProductType");
                    newProductEntity.SecondSort = row.Field<string>("SecondSort");

                    rtnProductEntityList.Add(newProductEntity);
                }
            }

            return rtnProductEntityList;
        }

        public ProductEntity GetProductEntity(int productId, int productPackageId, string productSource)
        {
            var sqlparams = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName = "@ProductId",Value = productId,SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ParameterName = "@ProductPackageId",Value = productPackageId,SqlDbType = SqlDbType.Int},
                    new SqlParameter(){ParameterName = "@ProductSource",Value = productSource,SqlDbType = SqlDbType.NVarChar}
                };

            var result = new SqlHelper().ExecuteDataTable(WebConfigureHelper.ConnectionStrings.DbConnection, CommandType.StoredProcedure, "usp_GetProductEntity", sqlparams.ToArray());
            if (result != null && result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                var newProductEntity = new ProductEntity();

                newProductEntity.CurrentStatus = row.Field<string>("CurrentStatus");
                newProductEntity.FirstSort = row.Field<string>("FirstSort");
                newProductEntity.IsCombinedProduct = row.Field<bool>("IsCombinedProduct");
                newProductEntity.IsSelfDefinedProduct = row.Field<bool>("IsSelfDefinedProduct");
                newProductEntity.OldProductName = row.Field<string>("OldProductName");
                newProductEntity.ProductCategoryId = row.Field<Guid>("ProductCategoryId");
                newProductEntity.ProductCode = row.Field<string>("ProductCode");
                newProductEntity.ProductDescription = row.Field<string>("ProductDescription");
                newProductEntity.ProductId = row.Field<int>("ProductId");
                newProductEntity.ProductName = row.Field<string>("ProductName");
                newProductEntity.ProductPackageId = row.Field<int>("ProductPackageId");
                newProductEntity.ProductPrice = row.Field<decimal>("ProductPrice");
                newProductEntity.ProductSource = row.Field<string>("ProductSource");
                newProductEntity.ProductType = row.Field<string>("ProductType");
                newProductEntity.SecondSort = row.Field<string>("SecondSort");

                return newProductEntity;
            }

            return null;
        }
    }
}