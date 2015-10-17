using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Model
{
    public class wx_diancai_credentials_detail
    {
        public int id { get; set; }
        public string orderNumber { get; set; }
        public string payStatus { get; set; }
        public DateTime modifyTime { get; set; }
        public string customerName { get; set; }
        public double payAmount { get; set; }
    }
}
