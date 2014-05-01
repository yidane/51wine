using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wine.Infrastructure.DAO.Commodity;
using Wine.Infrastructure.Model.Commodity;

namespace Wine.Infrastructure.BLL.Commodity
{
    public class GoodsBLL
    {
        public void ResetGoodsData(List<Goods> goodsList)
        {
            new GoodsDAO().TruncateAndBulkGoods(goodsList);
        }

        public List<Goods> QueryGoods(int goodsType, int id, out int count)
        {
            return new GoodsDAO().QueryGoodsByGoodsType(goodsType, id, out count);
        }

        public Goods QueryGoodsDetail(int id)
        {
            return new GoodsDAO().QueryGoods(id);
        }
    }
}