using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Model
{
    /// <summary>
    /// 酒店房间审核
    /// </summary>
    public class wx_hotel_room_manage
    {
        public int Id { get; set; }
	    public int RoomId { get; set; }
        public int Operator { get; set; }
        public string OperateName { get; set; }
        public DateTime OprateTime { get; set; }
        public string Comment { get; set; }
    }
}
