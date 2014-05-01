using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wine.Infrastructure.Model.Commodity;
using Wine.Infrastructure.BLL.Commodity;

namespace Wine.WebFacade.Commodity
{
    public class SpecialPriceTypeFacade
    {
        public List<SpecialPriceType> GetAllSpecialPriceType()
        {
            return new SpecialPriceTypeBLL().GetAllSpecialPriceType();
        }

        public bool ResetSpecialPriceTypeMapping(List<SpecialPriceType> specialPriceTypeList)
        {
            new SpecialPriceTypeBLL().ResetSpecialPriceTypeMapping(specialPriceTypeList);
            return true;
        }
    }
}
