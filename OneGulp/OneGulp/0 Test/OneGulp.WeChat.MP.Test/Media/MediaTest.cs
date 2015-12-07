using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneGulp.WeChat.MP.AdvancedAPIs;

namespace OneGulp.WeChat.MP.Test
{
    [TestClass]
    public class MediaTest : WeChatAppAcount
    {
        [TestMethod]
        public void TestGetMediaCount()
        {
            var result = MediaApi.GetMediaCount(access_token);

        }

        [TestMethod]
        public void TestUploadForeverMedia()
        {
            var filePath = "..\\Media\\1.mp3";
            var result = MediaApi.UploadForeverMedia(access_token, filePath);
        }
    }
}