using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WeiXinPF.BLL.UnitTest
{
    using System.Runtime.InteropServices.ComTypes;
    using System.Transactions;

    /// <summary>
    /// wx_diancai_dingdan_caiping_test 的摘要说明
    /// </summary>
    [TestClass]
    public class wx_diancai_dingdan_caiping_test
    {
        public wx_diancai_dingdan_caiping_test()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性:
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: 在此处添加测试逻辑
            //
        }

        [TestMethod]
        public void TestChangeCommodity()
        {
            wx_diancai_dingdan_manage bll = new wx_diancai_dingdan_manage();
            string ccode = "4978729704708228";

            bll.UpdateCommoditystatus(ccode,1);
        }

        [TestMethod]
        public void AfterPaymentProcess_diancaiOrderManage_ReturnSuccessProcess()
        {
            var wid = "11111";

            // 订单id
            var orderid = "7";
            var result = false;

            if (!string.IsNullOrEmpty(wid) && !string.IsNullOrEmpty(orderid))
            {
                var bll = new BLL.wx_diancai_dingdan_manage();
                var order = bll.GetModel(int.Parse(orderid));

                if (order != null)
                {
                    order.wid = wid;
                    order.payStatus = 1;
                    order.oderTime = DateTime.Now;

                    try
                    {
                        using (var scope = new TransactionScope())
                        {
                            bll.Update(order);
                            bll.UpdateCommodityStatusByOrderId(orderid, "1");

                            scope.Complete();
                        }
                    }
                    catch (Exception)
                    {
                        result= false;
                    }
                }
                else
                {
                    result= false;
                }
                result = true;
            }
            else
            {
                result= false;
            }

            Assert.IsTrue(result);
        }
    }
}
