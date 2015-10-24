using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Model
{
    /// <summary>
    /// 酒店管理员
    /// </summary>
    public class wx_hotel_admin
    {
        public int Id { get; set; }
        public int ManagerId { get; set; }
        public int HotelId { get; set; }
    }
}
