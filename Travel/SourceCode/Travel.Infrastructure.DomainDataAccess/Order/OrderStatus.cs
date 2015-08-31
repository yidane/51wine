using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.DomainDataAccess.Order
{
    public class OrderStatus
    {
        /// <summary>
        /// 购票的详情类型
        /// </summary>
        public const string OrderDetailCategory_Create = "{CE7B7E52-3811-44B6-AF9A-7562E0A773D2}";

        /// <summary>
        /// 退票的详情类型
        /// </summary>
        public const string OrderDetailCategory_Refund = "{2A09F347-480A-4AF3-95AE-480984262DD0}";

        /// <summary>
        /// 初始订单状态
        /// </summary>
        public const string OrderStatus_Init = "OS20001";
        public const string OrderStatus_WaitPay = "OS20002";
        public const string OrderStatus_PayComplete = "OS20003";
        public const string OrderStatus_WaitRefund = "OS20004";
        public const string OrderStatus_WaitUse = "OS20005";
        public const string DateTicketStatus_Init = "DTS10001";
        public const string DateTicketStatus_Lock = "DTS10002";
        public const string DateTicketStatus_PayComplete = "DTS10003";
        public const string TicketStatus_Init = "TS30001";

        /// <summary>
        /// 待付款
        /// </summary>
        public const string TicketStatus_WaitPay = "TS30006";


        /// <summary>
        /// 已付款
        /// </summary>
        public const string TicketStatus_PayComplete = "TS30002";

        /// <summary>
        /// 待使用
        /// </summary>
        public const string TicketStatus_WaitUse = "TS30003";

        /// <summary>
        /// 退票审核中
        /// </summary>
        public const string TicketStatus_Refund_Audit = "TS30004";

        /// <summary>
        /// 退款处理中
        /// </summary>
        public const string TicketStatus_Refund_RefundPayProcessing = "TS30005";

        /// <summary>
        /// 退票完成
        /// </summary>
        public const string TicketStatus_Refund_Complete = "TS30007";

        /// <summary>
        /// 退票队列初始状态
        /// </summary>
        public const string RefundOrderQueueStatus_Init = "ROQS40001";

        /// <summary>
        /// 退票单初始状态
        /// </summary>
        public const string RefundOrderStatus_Init = "ROS50001";
    }
}
