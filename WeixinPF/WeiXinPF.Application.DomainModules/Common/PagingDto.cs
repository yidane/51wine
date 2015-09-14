using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Application.DomainModules.Common
{
    public partial class PagingDto<T>
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int pageIndex { get; set; }
        /// <summary>
        /// 一页多少条
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 一共多少页
        /// </summary>
        public int totalCount { get; set; }

        public List<T> list { get; set; } 
    }
}
