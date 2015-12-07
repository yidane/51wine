using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;

namespace Travel.Application.OrderUnitTest
{
    using Travel.Infrastructure.DomainDataAccess.Order;

    [TestFixture]
    public class ClassTest
    {
        [Test]
        public void DaliyProduct_SetDaliyProduct()
        {
            var dailyProduct = DailyProductEntity.DailyProduct;
        }
    }
}