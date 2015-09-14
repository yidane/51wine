using System;
using System.IO;
using System.Net;
using System.Security.Policy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Travel.Infrastructure.WeiXin.Common;
using Travel.Infrastructure.WeiXin.Common.Ticket;
using Travel.Infrastructure.WeiXin.Statistics;
using Travel.Infrastructure.WeiXin.User;

namespace Travel.Infrastructure.WeiXin.Test
{
    [TestClass]
    public class WeChatUnitTest
    {
        [TestMethod]
        public void TestCreateSignature()
        {
            var weChatAccountManager = WeChatAccountManager.CreateDefaultInstance();
            var jsapiTicket = new ConfigTicket(new Credential(weChatAccountManager));

            var timestamp = jsapiTicket.CreateTimeStamp();
            var randomString = jsapiTicket.CreateRandomString();
            var ticket = jsapiTicket.CreateTicket();
            var url = "http://www.baidu.com";
            var signature = jsapiTicket.CreateSignature(ticket, randomString, timestamp, url);

            Assert.IsTrue(!string.IsNullOrEmpty(signature));
        }

        [TestMethod]
        public void TestSHA1()
        {
            var weChatAccountManager = WeChatAccountManager.CreateDefaultInstance();
            var jsapiTicket = new ConfigTicket(new Credential(weChatAccountManager));

            var timestamp = "1414587457";
            var randomString = "Wm3WZYTPz0wzccnW";
            var ticket = "sM4AOVdWfPE4DxkXGEs8VMCPGGVi4C3VM0P37wVUCFvkVAy_90u5h9nbSlYy3-Sl-HhTdfl2fzFy1AOcHKP7qg";
            var url = "http://mp.weixin.qq.com?params=value";
            var signature = jsapiTicket.CreateSignature(ticket, randomString, timestamp, url);

            Assert.IsTrue(string.Equals(signature, "0f9de62fce790f9a083d5c99e95740ceb90c27ed"));
        }

        [TestMethod]
        public void TestPostMessage()
        {
            var body = "123";
            var url = "http://localhost:23910/Message/MessageHelper.aspx";
            var request = WebRequest.Create(url);
            request.Method = "POST";
            var sw = new StreamWriter(request.GetRequestStream());
            sw.WriteLine(body);
            sw.Flush();
            sw.Close();
            using (var rep = request.GetResponse())
            {
                var sm = rep.GetResponseStream();
                StreamReader streamReader = new StreamReader(sm);
                var content = streamReader.ReadToEnd();
                Assert.IsTrue(string.Equals(content, "尹思雯"));
            }
        }

        [TestMethod]
        public void TestGetUserInfo()
        {
            var openId = "obzTsw3fdXUtoEsM9Du9LveNp5eE";
            var result = new UserInfoHelper().GetUserInfoByOpenID(openId);

            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void TestUserStatistics()
        {
            var beginDate = new DateTime(2015, 9, 1);
            var endDate = new DateTime(2015, 9, 6);
            var userList = new UserStatistics().GetUserStatistics(beginDate, endDate);

            Assert.IsTrue(userList != null && userList.list != null && userList.list.Count > 0);
        }
    }
}