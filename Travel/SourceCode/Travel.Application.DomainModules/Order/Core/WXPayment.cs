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
        public WXPayment(PaymentNotify paymentResponse)
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
            if (this.OrderObj != null)
            {
                if (this.OrderObj.OrderStatus.Equals(OrderStatus.OrderStatus_PayComplete))
                {
                    this.IsPaymentInfoProcessed = true;
                }
                else if (this.OrderObj.OrderStatus.Equals(OrderStatus.OrderStatus_WaitPay))
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

        /// <summary>
        /// 验证微信支付通知的参数是否正确
        /// </summary>
        /// <returns></returns>



        /// <summary>
        /// 异步接收微信支付通知并完成订单处理后，给微信回执
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WXPayment_PaymentComplete(object sender, EventArgs e)
        {
            var turnbackTemplate = "{{'return_code':'{0}', 'return_msg':'{1}'}}";

            if (this.IsPaymentInfoProcessed || this.IsProcessPaymentInfoSuccess)
            {
                this.WXTurnbackResult = string.Format(turnbackTemplate, "SUCCESS", "OK");
            }
            else
            {
                this.WXTurnbackResult = string.Format(turnbackTemplate, "FALL", "参数格式校验错误或签名失败");
            }
        }

        /// <summary>
        /// 支付完成后，通知OTA接口购票成功并且获取二维码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WXPayment_AfterPaymentComplete(object sender, EventArgs e)
        {
            //todo: 此事件单独抛出异常并进行处理，不应影响支付相关操作的结果
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
