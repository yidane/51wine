using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wine.Infrastructure.Model.User
{
    /// <summary>
    /// 顾客信息
    /// </summary>
    public class Customer
    {
        public int CustomerID { get; set; }
        public string LoginName { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public DateTime RegisterTime { get; set; }
    }
}