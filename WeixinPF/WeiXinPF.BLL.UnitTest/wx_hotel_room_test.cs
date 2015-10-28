using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WeiXinPF.BLL.UnitTest
{
    [TestClass]
    public class wx_hotel_room_test
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void GetRoomCode()
        {
            BLL.wx_hotel_room bll = new wx_hotel_room();

            string code = bll.GetRoomCode(1);
             
        }
    }
}
