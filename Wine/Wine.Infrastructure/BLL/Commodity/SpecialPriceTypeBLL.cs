using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wine.Infrastructure.Model.Commodity;
using Wine.Infrastructure.DAO.Commodity;

namespace Wine.Infrastructure.BLL.Commodity
{
    public class SpecialPriceTypeBLL
    {
        public List<SpecialPriceType> GetAllSpecialPriceType()
        {
            return new SpecialPriceTypeDAO().QueryAll();
        }

        public void ResetSpecialPriceTypeMapping(List<SpecialPriceType> specialPriceTypeList)
        {
            new SpecialPriceTypeDAO().TruncateAndBulkSpecialPriceType(specialPriceTypeList);
        }
    }
}
