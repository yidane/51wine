using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Collections.Generic;

namespace OneGulp.WeChat.MP.Test
{
    [TestClass]
    public class CommonAPITest
    {
        string appId = "wxdd6127bdb5e7611c";
        string appSecret = "78fb32f17d30a6ade836319283ccf118";

        [TestMethod]
        public void GetTokenTest()
        {
            var result = CommonAPIs.CommonApi.GetToken(appId, appSecret);

            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void GetTokenCacheTest()
        {
            List<string> tokenCache = new List<string>();

            var result = CommonAPIs.CommonApi.GetToken(appId, appSecret);
            tokenCache.Add(result.access_token);

            for (int i = 0; i < 20; i++)
            {
                var newResult = CommonAPIs.CommonApi.GetToken(appId, appSecret);
                tokenCache.Add(newResult.access_token);
                //Assert.IsTrue(string.Equals(result.access_token, newResult.access_token));
                //result = newResult;
                //Thread.Sleep(4000);
            }



            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void GetTicketTest()
        {
            var result = CommonAPIs.CommonApi.GetTicket(appId, appSecret);

            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void GetTicketCacheTest()
        {
            var result = CommonAPIs.CommonApi.GetTicket(appId, appSecret);
            for (int i = 0; i < 20; i++)
            {
                var newResult = CommonAPIs.CommonApi.GetTicket(appId, appSecret);

                Assert.IsTrue(string.Equals(result.ticket, newResult.ticket));
                result = newResult;
                Thread.Sleep(4000);
            }

            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void GetUserInfo()
        {
            var openId = "obzTsw3fdXUtoEsM9Du9LveNp5eE";
            var token = CommonAPIs.CommonApi.GetToken(appId, appSecret);
            var userInfo = CommonAPIs.CommonApi.GetUserInfo(token.access_token, openId);

            Assert.IsTrue(userInfo != null);
        }
    }
}
