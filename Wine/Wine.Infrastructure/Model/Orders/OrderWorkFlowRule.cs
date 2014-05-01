using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wine.Infrastructure.Model.Orders
{
    /// <summary>
    /// 订单流程配置
    /// </summary>
    public class OrderWorkFlowRule
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public int OrderType { get; set; }
        /// <summary>
        /// 流程名称
        /// </summary>
        public string WorkFlowName { get; set; }
        /// <summary>
        /// 流程排序
        /// </summary>
        public int WorkFlowSort { get; set; }
    }
}
