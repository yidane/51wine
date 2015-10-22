using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WeiXinPF.WeiXinComm.UnitTest
{
    [TestClass]
    public class EncryptionManagerTest
    {
        [TestMethod]
        public void AESEncryptTest()
        {
            var text = Guid.NewGuid().ToString();
            var password = "yidane";
            var vi = "123";

            var en = EncryptionManager.AESEncrypt(text, password, vi);

            var cn = EncryptionManager.AESDecrypt(en, password, vi);

            Assert.IsTrue(string.Equals(text, cn));
        }
    }
}
