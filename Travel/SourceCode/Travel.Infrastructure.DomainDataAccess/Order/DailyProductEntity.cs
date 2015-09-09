using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.DomainDataAccess.Order
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class DailyProductEntity
    {
        [Key, Column(Order = 1)]
        public int ProductId { get; set; }

        /// <summary>
        /// 查询日期
        /// </summary>
        [Key, Column(Order = 2)]
        public DateTime SearchDate { get; set; }

        /// <summary>
        /// 产品来源
        /// </summary>
        [Key, Column(Order = 3)]
        public string ProductSource { get; set; }

        /// <summary>
        /// 产品包Id
        /// </summary>
        [Key, Column(Order = 4)]
        public int ProductPackageId { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public string ProductType { get; set; }

        public Guid ProductCategoryId { get; set; }

        public static IEnumerable<DailyProductEntity> DailyProduct { get; set; }

        public static bool IsDailyProductsExists()
        {
            using (var db = new TravelDBContext())
            {
                return db.DailyProduct.Any(item => item.SearchDate.Equals(DateTime.Now.Date));
            }
        }

        public void SetDailyProducts(IEnumerable<DailyProductEntity> dailyProducts)
        {
            var hasNewProductCategory = false;
            if (dailyProducts.Any())
            {
                foreach (var dailyProduct in dailyProducts)
                {
                    if (!ProductCategoryEntity.IsDailyProductExists(dailyProduct))
                    {                    
                        var productCategory = new ProductCategoryEntity()
                                                  {
                                                      ProductCategoryId = Guid.NewGuid(),
                                                      ProductId = dailyProduct.ProductId,
                                                      ProductPackageId = dailyProduct.ProductPackageId,
                                                      ProductSource = dailyProduct.ProductSource,
                                                      ProductName = dailyProduct.ProductName,
                                                      OldProductName = dailyProduct.ProductName,
                                                      ProductType = dailyProduct.ProductType,
                                                      ProductCode = dailyProduct.ProductCode,
                                                      ProductPrice = dailyProduct.ProductPrice,
                                                      ProductDescription = string.Empty,
                                                      IsCombinedProduct = false,
                                                      IsSelfDefinedProduct = false,
                                                      CurrentStatus = "10001",
                                                      FirstSort = "10001",
                                                      SecondSort = string.Empty
                                                  };

                        productCategory.SaveProductCategory();                        
                        dailyProduct.ProductCategoryId = productCategory.ProductCategoryId;
                        hasNewProductCategory = true;
                    }
                }

                if (hasNewProductCategory)
                {
                    ProductCategoryEntity.RefreshProductCategory();
                }

                using (var db = new TravelDBContext())
                {
                    db.DailyProduct.AddRange(dailyProducts);
                    db.SaveChanges();
                }
            }
        }
    }
}
