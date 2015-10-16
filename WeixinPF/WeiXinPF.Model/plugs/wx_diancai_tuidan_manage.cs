using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Model
{
    [Serializable]
    public class wx_diancai_tuidan_manage
    {
        public int id { get; set; }
        public int shopinfoid { get; set; }
        public string openid { get; set; }
        public int wid { get; set; }
        public int caipinid { get; set; }
        public string orderNumber { get; set; }
        public string refundCode { get; set; }
        public DateTime? refundTime { get; set; }
        public float refundAmount { get; set; }
        public int refundStatus { get; set; }
        public DateTime createDate { get; set; }
    }
}
