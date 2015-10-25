using System;

namespace WeiXinPF.Application.DomainModules.Hotel.DTOS
{
    public class TuidanDto
    {
        public int id { get; set; }
        
        public int dingdanid { get; set; }
        public int hotelid { get; set; }
        public string openid { get; set; }
        public int wid { get; set; }
        public int roomid { get; set; }
        public DateTime refundTime { get; set; }
        public double refundAmount { get; set; }
        public int operateUser { get; set; } 
        public string remarks { get; set; }
    }
}