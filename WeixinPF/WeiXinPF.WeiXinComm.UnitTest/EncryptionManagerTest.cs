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
            var password = "123123123123123123123123123123123";
            var vi = EncryptionManager.CreateIV();

            var en = EncryptionManager.AESEncrypt(text, password, vi);

            var cn = EncryptionManager.AESDecrypt(en, password, vi);

            Assert.IsTrue(string.Equals(text, cn));
        }

        [TestMethod]
        public void DESEncryptTest()
        {
            var beforeText = "x6i0CG/Pgcsj/cqDTk4FyrChEdSoml31MqFFM8bV5/LYhcefC2rlDlse0R4FywwHQtB5UL0PhEAJwgojXAYFehIb76NGv6kc8QIJfABCnnald56zwDCa6Tf8G5dUH%2B2ye3T329JZJo/Hf4mNUh4Lqwi2ZR37Qxh6ytLpGie1kMc2CpXC803WTThPbZtW8I7nV6vF3evtaITLGRYQdJZ%2BKxvbh6aB88O7eqg3bUxySEUkacmIcaBYb/%2BEjLxPAYSLfjFKWtyUI9fgBxFVUsEwaXWpayOb3dWEDJD6DyANTR9WP7AvNjV9XKBajALQmLCIpdPpWbp9Vj2Uz19CJlNRyk3U%2B4pc6gaM10h2ML08R3AEooxrVM3AQxT9SU91FO0RyAWt7O2ZxDB8QcExKF5Y4JA3psb9ZnShNilnR7ZTjAl9dQpzU4uKjigEEWlpwDmglFp2ZwGPRrsbz%2BcX75Js2p%2BdMkyBA5zDmvdZaXWIHAaXYQ%2BwvgGQy3NbZOqFpx3WaFhbgZvkr7x93LN/dQRhjyA0ZA9wNShFv%2BMaSGTgpuTFUc5rp5BkutBszS4EurF3";
            var ticket = "HXR0TV8P28R64XFL";
            var afterText = EncryptionManager.AESDecrypt(beforeText.Replace("%2B", "+"), "yidane", ticket);

        }
    }
}
