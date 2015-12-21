using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityServerPF.Messages.RequestResponse;
using NServiceBus;

namespace CityServerPF.Server.Restautant
{
    public class RestaurantCategoryHandler:IHandleMessages<RestaurantCategoryRequest>
    {
        private IBus bus;

        public RestaurantCategoryHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(RestaurantCategoryRequest message)
        {
            Console.WriteLine("Receive RestaurantCategoryRequest");



            bus.Reply(new RestaurantCategoryResponse()
            {
                Id = 1,
                CategoryName = "小食",
                CreateDate = DateTime.Now,
                IsStart = true,
                miaoshu = "Very Good",
                ShopId = message.ShopId,
                SortId = 1
            });
        }
    }
}
