using System;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeiXinPF.Application.DomainModules.Hotel;
using WeiXinPF.Application.DomainModules.Hotel.DTOS;
using WeiXinPF.Application.DomainModules.IdentifyingCode.Service;

namespace Application.DomainModules.UnitTest
{
    [TestClass]
    public class HotelUnitTest
    {
        [TestMethod]
        public void SaveTuidan_ReturnNull_WithID()
        {
            var hotelService = new HotelService();
            int wid = 1;
            TuidanDto tuidanDto = new TuidanDto()
            {
                dingdanid=1,
                hotelid=1,
                openid="test",
                wid= wid,
                roomid=1,
                refundTime=DateTime.Now,
                refundAmount=123.12,
                operateUser= wid,
                remarks="什么原因也一样"
            };
            hotelService.AddTuidan(tuidanDto);
            Assert.IsTrue(tuidanDto.id>0);

            var model = hotelService.GetModel(tuidanDto.id);
            Assert.IsNotNull(model);
            Assert.AreEqual(model.dingdanid,tuidanDto.dingdanid);
        }

        [TestMethod]
        public void AfterPaySuccess_ReturnTrue()
        {
            using (var scope = new TransactionScope())
            {
                string no = "H20151025221857350873926";
                new WeiXinPF.BLL.wx_hotel_dingdan().PaySuccess(no);
               var isModifySuccess = IdentifyingCodeService.ModifyIdentifyingCodeInfoStatus(no, 1);
                Assert.IsTrue(isModifySuccess);
                if (isModifySuccess)
                {
                    scope.Complete();
                }
            }
          

        }
    }
}
