using System.Collections.Generic;
using Wine.Infrastructure.BLL.Commodity;
using Wine.Infrastructure.Model.Commodity;

namespace Wine.WebFacade.Commodity
{
    public class GoodsFacade
    {
        public bool ResetGoodsData(List<Goods> goodsList)
        {
            new GoodsBLL().ResetGoodsData(goodsList);
            return true;
        }

        public List<Goods> QueryGoods(int goodsType, int id, out int count)
        {
            return new GoodsBLL().QueryGoods(goodsType, id, out count);
        }

        public Goods QueryGood(int id)
        {
            return new GoodsBLL().QueryGoodsDetail(id);
        }
    }
}
