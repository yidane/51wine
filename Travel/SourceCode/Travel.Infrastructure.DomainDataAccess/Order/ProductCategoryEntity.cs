using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.DomainDataAccess.Order
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Migrations;
    using System.Runtime.CompilerServices;

    public class ProductCategoryEntity
    {
        public Guid ProductCategoryId { get; set; }

        [Key, Column(Order = 1)]
        public int ProductId { get; set; }

        /// <summary>
        /// 产品包Id
        /// </summary>
        [Key, Column(Order = 2)]
        public int ProductPackageId { get; set; }

        /// <summary>
        /// 产品来源
        /// </summary>
        [Key, Column(Order = 3)]
        public string ProductSource { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 原产品名称
        /// </summary>
        public string OldProductName { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public string ProductType { get; set; }

        public string ProductCode { get; set; }

        public decimal ProductPrice { get; set; }

        public string ProductDescription { get; set; }

        /// <summary>
        /// 是否为组合类型产品
        /// </summary>
        public bool IsCombinedProduct { get; set; }

        /// <summary>
        /// 是否为自定义商品
        /// </summary>
        public bool IsSelfDefinedProduct { get; set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        public string CurrentStatus { get; set; }

        /// <summary>
        /// 一级排序字段
        /// </summary>
        public string FirstSort { get; set; }

        /// <summary>
        /// 二级排序字段
        /// </summary>
        public string SecondSort { get; set; }

        internal static object _productLock = new object();

        private static IList<ProductCategoryEntity> _productCategory;

        private static DateTime _currentDate = DateTime.Now.Date;
        public static IList<ProductCategoryEntity> ProductCategory {
            get
            {                
                if (_productCategory == null || !_productCategory.Any() || !DateTime.Now.Date.Equals(_currentDate))
                {                   
                    using (var db = new TravelDBContext())
                    {
                        _productCategory = db.ProductCategory.ToList();
                    }

                    if (_productCategory.Any())
                    {
                        _currentDate = DateTime.Now.Date;
                    }                    
                }

                return _productCategory;
            }

            private set
            {
                _productCategory = value;
            }
        }

        public static void RefreshProductCategory()
        {
            using (var db = new TravelDBContext())
            {
                ProductCategory = db.ProductCategory.ToList();
            }
        }

        public void SaveProductCategory()
        {
            using (var db = new TravelDBContext())
            {
                db.ProductCategory.AddOrUpdate(this);
                db.SaveChanges();
            }
        }
    }
}
