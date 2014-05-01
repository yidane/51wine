using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wine.Data;

namespace WineUnitTestProject
{
    [TestClass]
    public class WineMagagerUnitTest
    {
        [TestMethod]
        public void DownloadData()
        {
            try
            {
                WineManager oWineManager = new WineManager();
                oWineManager.DownloadData();
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.IsFalse(true, ex.Message);
            }
        }
    }
}
