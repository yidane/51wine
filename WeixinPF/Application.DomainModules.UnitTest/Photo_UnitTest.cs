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
        public void Add_ReturnNull_Withwid()
        {
            int wid = 36;
            var service = new PhotoService();

            var dtos = service.GetList(wid);
            Assert.IsNotNull(dtos);
        }

        [TestMethod]
        public void GetList_ReturnPhotoDTO_Withwid()
        {
            int wid = 36;
            var service = new PhotoService();

            var dtos = service.GetList(wid);
            Assert.IsNotNull(dtos);
        }
    }
}
