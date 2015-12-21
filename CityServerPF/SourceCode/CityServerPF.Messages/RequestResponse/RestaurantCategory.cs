using System;

namespace CityServerPF.Messages.RequestResponse
{
    public class RestaurantCategoryRequest
    {
        public int Wid { get; set; }
        public int ShopId { get; set; }
    }

    public class RestaurantCategoryResponse
    {
        public int Id { get; set; }
        public int ShopId { get; set; }

        public int SortId { get; set; }
        public string CategoryName { get; set; }
        public string miaoshu { get; set; }

        public bool IsStart { get; set; }
        public DateTime CreateDate { get; set; }
    }
}