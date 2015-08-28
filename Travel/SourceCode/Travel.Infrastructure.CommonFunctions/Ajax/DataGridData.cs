using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.CommonFunctions.Ajax
{
    /// <summary>
    /// DataGrid数据实体
    /// </summary>
    public class DataGridData<T> where T : class
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 返回行数据
        /// </summary>
        public List<T> rows { get; set; }
    }
}
