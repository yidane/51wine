using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travel.Presentation.WebPlugin.Product
{
    public class ProductEntity
    {
        public int ProductId { get; set; }
        public int ProductPackageId { get; set; }
        public string ProductSource { get; set; }
        public Guid ProductCategoryId { get; set; }
        public string ProductName { get; set; }
        public string OldProductName { get; set; }
        public string ProductType { get; set; }
        public string ProductCode { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public bool IsCombinedProduct { get; set; }
        public bool IsSelfDefinedProduct { get; set; }
        public string CurrentStatus { get; set; }
        public string FirstSort { get; set; }
        public string SecondSort { get; set; }
    }
}