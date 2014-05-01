using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wine
{
    /// <summary>
    /// Json转换时与数据库字段映射
    /// </summary>
    public class ColumnMappingAttribute : Attribute
    {
        public string ColumnName { get; set; }

        public ColumnMappingAttribute(string name)
        {
            ColumnName = name;
        }
    }
}