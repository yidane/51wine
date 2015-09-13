using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeiXinPF.Model;

namespace WeiXinPF.BLL.UnitTest
{
    [TestClass]
    public class wx_travel_scenicDetail_test
    {
        [TestMethod]
        public void GetModel()
        {
            wx_travel_scenicDetail bll = new wx_travel_scenicDetail();
            var result = bll.GetModel(1);

        }

        [TestMethod]
        public void Add()
        {
            wx_travel_scenicDetail bll = new wx_travel_scenicDetail();
            Model.wx_travel_scenicDetail model = new Model.wx_travel_scenicDetail
            {
                ScenicId = 3,
                Name = "ghfghfg",
                Cover = "asdfghfhfghasd",
                BackgroundImage = "fghfgh",
                Digest = "fghfgh",
                Content = "fghfgh",
                Audio = "fghfgh",
                AutoAudio = true,
                LoopAudio = true,
                Video = "fhfgh",
                AutoVideo = true,
                OriginalLink = "asdfghfghasd",
                //Albumses = new List<Model.wx_travel_albums>()
                //{
                //   new wx_travel_albums(){ThumbPath ="asdasdasdasdasdasd" },
                //    new wx_travel_albums(){ThumbPath ="ffffgfggfg" }
                //}


            };
            bll.Add(model);

        }
    }
}
