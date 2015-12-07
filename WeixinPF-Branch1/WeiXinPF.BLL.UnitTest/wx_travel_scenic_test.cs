using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeiXinPF.Model;

namespace WeiXinPF.BLL.UnitTest
{
    [TestClass]
    public class wx_travel_scenic_test
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void Add()
        {
            BLL.wx_travel_scenic bll = new BLL.wx_travel_scenic();
            Model.wx_travel_scenic model=new Model.wx_travel_scenic();
            model.Name = "qweqwe";
         model.Description= "qweqwe";
         model.TemplateId= 1;
         model.FirstBgImg= "qweqwe";
         model.IdentifyImg= "qweqwe";
         model.DisplayAction= "qweqwe";
         model.SecondBgImg= "qweqwe";
         model.AutoDisplayNextPage= true;
         model.Delay= 1000;
         model.AudioPath= "qweqwe";
         model.AutoPlayAudio = true;
         model.AudioLoop = true;
         model.VideoPath = "123123123123";
         model.AutoPlayVideo = true;
         model.extInt1= 1;
         model.extInt2= 1;
         model.extStr1= "qweqwe";
         model.extStr2 = "qweqwe";
            int result = bll.Add(model);

            Assert.AreNotEqual(result,0);
        }
        [TestMethod]
        public void GetModel()
        {
            BLL.wx_travel_scenic bll = new BLL.wx_travel_scenic();
            var model = bll.GetModel(1);
        }
    }
}
