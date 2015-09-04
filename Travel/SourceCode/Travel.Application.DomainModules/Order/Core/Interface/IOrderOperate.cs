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

        OTAResult<OrderReleaseResponse> OrderRelease();

        void OrderFinish();

        OTAResult<List<ChangeOrderEditResponse>> ChangeOrderEdit(ICollection<TicketEntity> refundTickets);
    }
}
