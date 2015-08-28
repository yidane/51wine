using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.CommonFunctions.Ajax
{
    /// <summary>
    /// 树绑定数据
    /// </summary>
    public class TreeData
    {
        public int id { get; set; }

        public string text { get; set; }

        public object attributes { get; set; }

        /// <summary>
        /// 图标样式
        /// </summary>
        public string iconCls { get; set; }

        public string state { get; set; }

        public List<TreeData> children { get; set; }

        public TreeData()
        {
            children = new List<TreeData>();
        }
    }
}
