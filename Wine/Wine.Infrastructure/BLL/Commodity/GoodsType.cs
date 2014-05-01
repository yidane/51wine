using System.Collections.Generic;
using Wine.Infrastructure.DAO.Commodity;
using Wine.Infrastructure.Model.Commodity;

namespace Wine.Infrastructure.BLL.Commodity
{
    public class GoodsTypeBLL
    {
        public List<GoodsType> QueryAllGoodsType()
        {
            return new GoodsTypeDAO().QueryAllGoodsType();
        }
    }
}
