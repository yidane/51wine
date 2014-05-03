using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wine.Infrastructure.Model.Commodity;
using Wine.Infrastructure.Model.Orders;
using Wine.WebFacade.Commodity;
using Wine.WebFacade.Orders;

namespace Wine.WebServices
{
    public class MarketService
    {
        public List<Goods> QueryWineList(int wineType, int id, out int sum, out GoodsType goodsType)
        {
            goodsType = new GoodsTypeFacade().QueryAllGoodsType().Find(type => type.TypeID == wineType);
            var goodsList = new GoodsFacade().QueryGoods(wineType, id, out sum);
            return goodsList ?? new List<Goods>();
        }

        public Goods QueryGoodsDetail(int id)
        {
            return new GoodsFacade().QueryGood(id);
        }

        public bool CommitOrder(Order order)
        {
            return new OrderFacade().CommitOrder(order);
        }
    }
}