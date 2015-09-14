using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeiXinPF.Application.DomainModules.Photo;
using WeiXinPF.Application.DomainModules.Photo.DTOS;

namespace Application.DomainModules.UnitTest
{
    [TestClass]
    public class PhotoUnitTest
    {

        [TestMethod]
        public void AddAndModify_ReturnNull_Withwid()
        {
             
            var service = new PhotoService();
            var dto= new photoActionDTO()
            {
                wid = 36,
                beginDate = "2015-09-01",
                endDate = DateTime.Now.AddDays(5).ToString(),
                actContent = "test20151",
                brief = "jianjie",
                isAllowSharing = true
            };
              service.Add( dto);
            Assert.IsTrue(dto.id>0);

            photoActionDTO model =  service.GetModel(dto.id);
            Assert.AreEqual(dto.wid,model.wid);

            dto.brief = "test间接";
            service.Modify(dto);
            Assert.IsNotNull(dto);

            model = service.GetModel(dto.id);
            Assert.AreEqual(dto.brief,model.brief);


        }

        [TestMethod]
        public void GetList_ReturnPhotoDTO_Withwid()
        {
            int wid = 36;
            var service = new PhotoService();

            var dtos = service.GetList(1,10, "", wid);
            Assert.IsNotNull(dtos);
            Assert.IsTrue(dtos.list.Count>0);

            dtos = service.GetList(1, 10, "test", wid);
            Assert.IsNotNull(dtos);
            Assert.IsTrue(dtos.list.Count > 0);

        }
    }
}
