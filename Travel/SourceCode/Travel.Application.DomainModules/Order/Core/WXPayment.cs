using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.DomainModules.Order.Core
{
    using Travel.Infrastructure.DomainDataAccess.Order;
    using Travel.Infrastructure.WeiXin.Advanced.Pay.Model;

    /// <summary>
    /// The wx payment.
    /// </summary>
    public class WXPayment : Payment
    {        
        public WXPayment(object paymentResponse)
            : base(paymentResponse)
        {
            this.PaymentOperate = new WXPaymentOperate();
            this.PrePayment += this.WXPayment_PrePayment;
            this.PaymentComplete += this.WXPayment_PaymentComplete;
            this.AfterPaymentComplete += this.WXPayment_AfterPaymentComplete;
        }

        public WXPayment(OrderEntity order)
            : base(order)
        {

        }

        /// <summary>
        /// 收到微信支付通知后，判断订单是否已处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WXPayment_PrePayment(object sender, EventArgs e)
        {
            if (this.IsPaymentNotifyParamtersCorrect())
            {
                if (this.OrderObj != null)
                {
                    if (this.OrderObj.OrderStatus.Equals(Order.OrderStatus_PayComplete))
                    {
                        this.IsPaymentInfoProcessed = true;
                    }
                    else if (this.OrderObj.OrderStatus.Equals(Order.OrderStatus_WaitPay))
                    {
                        this.IsPaymentInfoProcessed = false;
                    }
                    else
                    {
                        throw new InvalidOperationException("prepayment");
                    }
                }
                else
                {
                    throw new ArgumentNullException("order");
                }
            }
            else
            {
                throw new ArgumentNullException("prepayment");
            }
        }

        /// <summary>
        /// 验证微信支付通知的参数是否正确
        /// </summary>
        /// <returns></returns>
        private bool IsPaymentNotifyParamtersCorrect()
        {
            // todo:通过PaymentResponse完成微信通知参数的验证
            return true;
        }


        /// <summary>
        /// 异步接收微信支付通知并完成订单处理后，给微信回执
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WXPayment_PaymentComplete(object sender, EventArgs e)
        {
            // todo:通过PaymentOperate完成与微信支付的回执
            if (this.IsProcessPaymentInfoSuccess)
            {

            }
            else
            {

            }
        }        

        /// <summary>
        /// 支付完成后，通知OTA接口购票成功并且获取二维码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WXPayment_AfterPaymentComplete(object sender, EventArgs e)
        {
            if (this.OrderObj != null)
            {
                var myOrder = OrderEntity.GetOrderByOrderId(OrderObj.OrderCode);
                var order = new OTAOrder(myOrder);
                var orderOperate = new OTAOrderOperate(order);

                orderOperate.OrderFinish();
            }
            
        }
    }
}
