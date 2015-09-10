using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.UnitTest
{
    using System.Data.Entity.Core;
    using System.Data.Objects;
    using NUnit.Framework;
    using System.Data.Entity.Infrastructure;

    using Travel.Infrastructure.DomainDataAccess;
    using Travel.Infrastructure.DomainDataAccess.Order;

    [TestFixture]
    public class OrderUnitTest
    {
        [Test]
        public void SaveOrder_BuyTicket_ReturnVoid()
        {
            try
            {
                var firctx = new TravelDBContext();

                var t = firctx.DateTicket.First();
                t.CurrentStatus = "t1";

                var secctx = new TravelDBContext();
                var t2 = secctx.DateTicket.First();
                t2.CurrentStatus = "t2";
                secctx.SaveChanges();

                firctx.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                
                throw;
            }
            catch (Exception e)
            {
                throw;
            }

        }

        [Test]
        public void fdf()
        {
            var tickets = new List<TicketCategoryEntity>();

            tickets.Add(new TicketCategoryEntity()
                            {
                                TicketName = "gb"
                            });
            tickets.Add(null);

             var df=tickets.Count;
        }
    }
}
