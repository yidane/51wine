using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.DomainModules.Order.Core.Interface
{
    using Travel.Infrastructure.DomainDataAccess.Order;
    using Travel.Infrastructure.WeiXin.Advanced.Pay.Model;

    public interface IPaymentOperate
    {
        //Order MainOrder { get; set; }

        UnifiedOrderResult ConnectedPaymentPlatform(OrderEntity order);

        void GetPaymentCompleteInfomation(object paymentComplete);
    }
}
