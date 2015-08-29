using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.DomainModules.Order.Core.Interface
{
    using Travel.Infrastructure.DomainDataAccess.Order;
    using Travel.Infrastructure.OTAWebService.Response;

    public interface IOrderOperate
    {
        Order MainOrder { get; set; }

        OTAResult<OrderOccupiesResponse> OrderOccupies();

        bool OrderRelease();

        void OrderFinish();

        bool ChangeOrderEdit();
    }
}
