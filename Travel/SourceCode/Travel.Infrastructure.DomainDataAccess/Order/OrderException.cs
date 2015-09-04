using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.DomainDataAccess.Order
{
    public class OrderException :Exception
    {
        public OrderException(string message)
            : base(message)
        {
        }
    }

    public class OrderOperateFailException : OrderException
    {
        public OrderOperationStep OperationStep;

        public IDictionary<string, object> param;

        public string ErrorCode;
        public OrderOperateFailException(string message, OrderOperationStep operationStep)
            : base(message)
        {
            this.OperationStep = operationStep;
            this.ErrorCode = string.Empty;
        }

        public OrderOperateFailException(string message, OrderOperationStep operationStep, string errorCode)
            : base(message)
        {
            this.OperationStep = operationStep;
            this.ErrorCode = errorCode;
        }
    }

    public class OrderPaymentFailException : OrderException
    {
        public OrderPaymentStep PaymentStep;
        public IDictionary<string, object> param;

        public string ErrorCode;
        public OrderPaymentFailException(string message, OrderPaymentStep paymentStep)
            : base(message)
        {
            this.PaymentStep = paymentStep;
            this.ErrorCode = string.Empty;
        }

        public OrderPaymentFailException(string message, OrderPaymentStep paymentStep, string errorCode)
            : base(message)
        {
            this.PaymentStep = paymentStep;
            this.ErrorCode = errorCode;
        }
    }

    public enum OrderOperationStep
    {
        /// <summary>
        /// 获取当日可销售的门票
        /// </summary>
        GetDailyTicket,

        /// <summary>
        /// 锁定景区订单
        /// </summary>
        OrderOccupy,

        /// <summary>
        /// 创建本地订单
        /// </summary>
        OrderCreate,

        /// <summary>
        /// 完结景区订单
        /// </summary>
        OrderFinish,

        /// <summary>
        /// 修改景区订单
        /// </summary>
        OrderChange,

        /// <summary>
        /// 获取景区订单状态
        /// </summary>
        GetOrderStatus
    }

    public enum OrderPaymentStep
    {
        /// <summary>
        /// 统一下单
        /// </summary>
        UnifiedOrder,

        /// <summary>
        /// 支付结果通知
        /// </summary>
        PaymentResultNotify,

        /// <summary>
        /// 申请退款
        /// </summary>
        ApplyRefund,

        /// <summary>
        /// 查询退款
        /// </summary>
        RefundQuery
    }
}
