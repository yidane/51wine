using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Travel.Infrastructure.OTAWebService.Request;

namespace Travel.Infrastructure.OTAWebService.Test
{
    [TestClass]
    public class OTAWebServiceUnitTest
    {
        private string OrderNo = "C2015090420320750851515";

        [TestMethod]
        public void TestSignture()
        {
            var str = "yidane";
            var str1 = "yinsiwen";
            var str2 = "nb";

            //var str = "";
            //var str1 = "";
            //var str2 = "";

            var mySign = OTAConfigManager.GetSignature(OTAConfigManager.MerchantCode, OTAConfigManager.MerchantKey, str, str1, str2);
            var testSign = new OTAServiceManager().TestSignture(str, str1, str2, string.Empty);

            Assert.IsTrue(string.Equals(mySign, testSign));
        }

        [TestMethod]
        public void GetAccountInfoTest()
        {
            var result = new OTAServiceManager().GetAccountInfo();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void GetQuotaInfo()
        {
            DateTime date = DateTime.Now;
            var result = new OTAServiceManager().GetQuotaInfo(date);
            Assert.IsTrue(!string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void GetProductsTest()
        {
            var request = new GetProductRequest(DateTime.Now.Date.AddDays(1));
            var result = new OTAServiceManager().GetProducts(request);
            Assert.IsTrue(result.IsTrue);
        }

        [TestMethod]
        public void OrderOccupiesTest()
        {
            OrderOccupiesRequest request = new OrderOccupiesRequest();
            PostOrder postOrder = new PostOrder();
            postOrder.Ptime = DateTime.Now.ToString();
            postOrder.Edittype = null;

            Order order = new Order();
            order.OrderNO = OrderNo;
            order.LinkName = "测试";
            order.LinkPhone = "18910063001";
            order.LinkICNO = "";
            order.CreateTime = DateTime.Now.ToString();
            postOrder.Order = order;


            postOrder.Details = new List<Detail>()
                {
                    new Detail()
                        {
                            OrderNO = OrderNo,
                            ItemID = "afee205e-5e63-4020-a26d-d78f3211483f",
                            ProductCode = "berj001",
                            ProductID = 57882,
                            ProductName = "布尔津测试门票",
                            ProductPrice = 100,
                            ProductCount = 1,
                            ProductSDate = "2015-09-01",
                            ProductEDate = "2015-09-01",
                            ProductPackID = 216
                        }
                };

            request.postOrder = postOrder;

            var result = new OTAServiceManager().OrderOccupies(request);

            Assert.IsTrue(result.IsTrue);
        }

        [TestMethod]
        public void OrderRelease()
        {
            //先订单占用
            OrderOccupiesRequest request = new OrderOccupiesRequest();
            PostOrder postOrder = new PostOrder();
            postOrder.Ptime = DateTime.Now.ToString();
            postOrder.Edittype = null;

            Order order = new Order();
            order.OrderNO = OrderNo;
            order.LinkName = "测试";
            order.LinkPhone = "18910063051";
            order.LinkICNO = "";
            order.CreateTime = DateTime.Now.ToString();
            postOrder.Order = order;


            postOrder.Details = new List<Detail>()
                {
                    new Detail()
                        {
                            OrderNO = OrderNo,
                            ItemID = "1",
                            ProductCode = "berj001",
                            ProductID = 57882,
                            ProductName = "布尔津测试门票",
                            ProductPrice = 100,
                            ProductCount = 1,
                            ProductSDate = "2015-08-25",
                            ProductEDate = "2015-08-25",
                            ProductPackID = 216
                        }
                };

            request.postOrder = postOrder;

            var result0 = new OTAServiceManager().OrderOccupies(request);

            var orderReleaseRequest = new OrderReleaseRequest()
                {
                    OtaOrderNO = OrderNo
                };

            var result1 = new OTAServiceManager().OrderRelease(orderReleaseRequest);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void OrderFinishTest()
        {
            var request = new OrderFinishRequest()
                {
                    OtaOrderNO = OrderNo
                };

            var result = new OTAServiceManager().OrderFinish(request);

            Assert.IsTrue(result.IsTrue);
        }

        [TestMethod]
        public void GetAllOrderStatus()
        {
            var request = new GetAllOrderStatusRequest
                {
                    PostOrder = new List<OrderNoCode>()
                        {
                            new OrderNoCode() {OrderCode = "466015815025"},
                            //new OrderNoCode(){OrderCode = "C20150825168858961"}
                        }
                };

            var result = new OTAServiceManager().GetAllOrderStatus(request);
            Assert.IsTrue(result.IsTrue);
        }

        [TestMethod]
        public void OrderReleaseTest()
        {
            OrderReleaseRequest request = new OrderReleaseRequest();
        }

        [TestMethod]
        public void ChangeOrderEditTest()
        {
            var request = new ChangeOrderEditRequest();
            var editPostOrder = new EditPostOrder
                {
                    Ptime = DateTime.Now.ToString(),
                    Edittype = "2",
                    Order = new EditOrder() { OrderNo = OrderNo },
                    Details = new List<EditOrderDetail>()
                {
                            new EditOrderDetail() {Starttime = "2015-08-25", ProductCode = "485083175822"}
                        }
                };
            request.PostOrder = editPostOrder;

            var result = new OTAServiceManager().ChangeOrderEdit(request);

            Assert.IsTrue(result.IsTrue);
        }
    }
}