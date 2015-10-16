using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Model
{
    /// <summary>
    /// 餐饮管理员表
    /// </summary>
    public class wx_diancai_admin
    {
        public int Id { get; set; }
        public int ManagerId { get; set; }
        public int ShopId { get; set; }
    }
}
