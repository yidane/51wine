using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wine.Infrastructure.Model.Commodity
{
    public class SpecialPriceType : GoodsType
    {
        /// <summary>
        /// 特价映射
        /// </summary>
        public List<int> GoodsList = new List<int>();
    }
}
