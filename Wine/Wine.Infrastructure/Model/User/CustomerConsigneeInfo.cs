using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wine.Infrastructure.Model.User
{
    /// <summary>
    /// 顾客配送地址
    /// </summary>
    public class CustomerConsigneeInfo
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string ConsigneeUserName { get; set; }

        /// <summary>
        /// 收货人电话
        /// </summary>
        public string ConsigneeMobile { get; set; }

        /// <summary>
        ///配送区域
        /// </summary>
        public int Region { get; set; }

        /// <summary>
        /// 配送详细地址
        /// </summary>
        public string Adress { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
    }
}