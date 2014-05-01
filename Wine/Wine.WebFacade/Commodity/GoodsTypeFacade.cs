using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wine.Infrastructure.BLL.Commodity;
using Wine.Infrastructure.Model.Commodity;

namespace Wine.WebFacade.Commodity
{
    public class GoodsTypeFacade
    {
        public List<GoodsType> QueryAllGoodsType()
        {
            return new GoodsTypeBLL().QueryAllGoodsType();
        }
    }
}
