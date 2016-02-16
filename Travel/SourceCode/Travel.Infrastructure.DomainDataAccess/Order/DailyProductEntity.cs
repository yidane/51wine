using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.DomainDataAccess.Order
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Travel.Infrastructure.OTAWebService;
    using Travel.Infrastructure.OTAWebService.Response;

    using OTARequest = Travel.Infrastructure.OTAWebService.Request;
    using System.IO;
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

        private static object dailyProductLock = new object();
        private static IList<DailyProductEntity> _dailyProducts;
        private static DateTime _currentDate = DateTime.Now.Date;
        public static IList<DailyProductEntity> DailyProduct { 
            get 
            {
                if (_dailyProducts == null || !_dailyProducts.Any() || !DateTime.Now.Date.Equals(_currentDate))
                {
                    // 控制并发锁，当用户同时操作时，有可能向数据库写入相同数据
                    lock (dailyProductLock)
                    {
                        if (!IsDailyProductsExists())
                        {
                            var request = new OTARequest.GetProductRequest(DateTime.Now.Date);

                            try
                            {
                                var dailyProductsResponse = new OTAServiceManager().GetProducts(request);

                                if (dailyProductsResponse.IsTrue)
                                {
                                    var dailyProducts = dailyProductsResponse.ResultData.Select(item => new DailyProductEntity()
                                    {
                                        ProductId = int.Parse(item.ProductID),
                                        SearchDate = DateTime.Now.Date,
                                        ProductSource = "KNSMP",
                                        ProductPackageId = item.ProductPackID,
                                        ProductCode = item.ProductCode,
                                        ProductName = item.ProductName,
                                        ProductPrice = item.ProductPrice,
                                        ProductType = item.ProductType
                                    }).ToList();
                                    SetDailyProducts(dailyProducts);
                                    _dailyProducts = dailyProducts;
                                    _currentDate = DateTime.Now.Date;
                                }
                                else
                                {
                                    _dailyProducts = new List<DailyProductEntity>();
                                }
                            }
                            catch (Exception ex)
                            {
                                _dailyProducts = new List<DailyProductEntity>();
                                WriteLog(ex.Message);
                            }
                        }
                        // 避免在并发时，第一个用户重新获取数据后，其它用户还需获取数据
                        else if (_dailyProducts == null || !_dailyProducts.Any() || !DateTime.Now.Date.Equals(_currentDate))
                        {
                            using (var db = new TravelDBContext())
                            {
                                var today = DateTime.Now.Date;
                                var tomorrow = today.AddDays(1);
                                _dailyProducts = db.DailyProduct.Where(item => item.SearchDate >= today && item.SearchDate < tomorrow).ToList();
                            }
                        }
                    }                                        
                }

                return _dailyProducts;
            } 
        }

        public static bool IsDailyProductsExists()
        {
            if (_dailyProducts != null && _dailyProducts.Any() && _currentDate.Equals(DateTime.Now.Date))
            {
                return true;
            }
            else
            {
                using (var db = new TravelDBContext())
                {
                    var today = DateTime.Now.Date;
                    var tomorrow = today.AddDays(1);
                    return db.DailyProduct.Any(item => item.SearchDate >= today && item.SearchDate < tomorrow);
                }
            }            
        }

        public static void SetDailyProducts(IList<DailyProductEntity> dailyProducts)
        {
            if (dailyProducts.Any())
            {
                var products = ProductCategoryEntity.ProductCategory;

                foreach (var dailyProduct in dailyProducts)
                {
                    var product = products.FirstOrDefault(item => item.ProductId.Equals(dailyProduct.ProductId)
                                                                  &&
                                                                  item.ProductPackageId.Equals(
                                                                      dailyProduct.ProductPackageId)
                                                                  &&
                                                                  item.ProductSource.Equals(dailyProduct.ProductSource));
                    if (product == null)
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
                        products.Add(productCategory);
                        dailyProduct.ProductCategoryId = productCategory.ProductCategoryId;
                    }
                    else
                    {
                        dailyProduct.ProductCategoryId =product.ProductCategoryId;
                    }
                }

                using (var db = new TravelDBContext())
                {
                    db.DailyProduct.AddRange(dailyProducts);
                    db.SaveChanges();
                }
            }
        }

        protected static void WriteLog(string content)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "log";
            if (!Directory.Exists(path))//如果日志目录不存在就创建
            {
                Directory.CreateDirectory(path);
            }

            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");//获取当前系统时间
            string filename = path + "/" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";//用日期对日志文件命名

            //创建或打开日志文件，向日志文件末尾追加记录
            StreamWriter mySw = File.AppendText(filename);

            mySw.WriteLine(time + "   |" + content + Environment.NewLine);

            //关闭日志文件
            mySw.Close();
        }
    }
}
