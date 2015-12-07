using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeiXinPF.Application.DomainModules.Photo;
using WeiXinPF.Application.DomainModules.Photo.DTOS;

namespace Application.DomainModules.UnitTest
{
    using WeiXinPF.Application.DomainModules.IdentifyingCode.Service;
    using WeiXinPF.Infrastructure.DomainDataAccess;
    using WeiXinPF.Infrastructure.DomainDataAccess.IdentifyingCode;
    using WeiXinPF.Infrastructure.DomainDataAccess.IdentifyingCode.Repository;
    using WeiXinPF.Infrastructure.DomainDataAccess.Payment;

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

        [TestMethod]
        public void InsertIdentifyCode_Verification_ReturnTure()
        {
            var icode = new IdentifyingCodeInfo()
                            {
                                IdentifyingCodeId = Guid.NewGuid(),
                                CreateTime = DateTime.Now,
                                IdentifyingCode = "1256978741253",
                                ModifyTime = DateTime.Now,
                                ModuleName = "HotelTest",
                                OrderCode = "H201510221656123456789",
                                OrderId = "22222",
                                ProductCode = "大床房",
                                ProductId = "大床房",
                                Status = 0,
                                ShopId = "12",
                                Wid = 12
                            };

            var service = IdentifyingCodeService.AddIdentifyingCode(icode);

            //Assert.IsTrue(result);
        }

        [TestMethod]
        public void AfterPaymentProcess_HotelOrderManage_ReturnSuccessProcess()
        {
            //var isSuccess = IdentifyingCodeService.ModifyIdentifyingCodeInfoStatus("H20151025175559136498137", 1);
        }

        [TestMethod]
        public void SearchIdentifyingCodeDetailForSearch_IdentifyingCode_ReturnDTO()
        {
            var db = new WXDBContext();
            var obj = new IdentifyingCodeInfo() { ModuleName = "restaurant", IdentifyingCodeId = Guid.Parse("15EB49FB-B41D-4101-B882-29DDF746F0B0") };
            var result = new IdentifyingCodeRepository(db).GetIdentifyingCodeDetailById(obj);
        }

        [TestMethod]
        public void GetOrderDetailForSearch_IdentifyingCode_ReturnDTO()
        {
            var result = IdentifyingCodeService.GetOrderDetail(8, "restaurant", "");
        }
    }
}
