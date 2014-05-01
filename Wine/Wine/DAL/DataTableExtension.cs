using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Wine
{
    /// <summary>
    /// 将DataTable转换为对应的实体集合
    /// </summary>
    public static class DataTableExtension
    {
        public static List<T> ToList<T>(this DataTable data) where T : new()
        {
            if (data == null && data.Rows.Count == 0)
            {
                return null;
            }

            List<T> list = new List<T>();
            PropertyInfo[] propertys = typeof(T).GetProperties();
            List<DataColumn> columnList = data.Columns.Cast<DataColumn>().ToList();
            foreach (DataRow row in data.Rows)
            {
                T model = new T();
                foreach (PropertyInfo property in propertys)
                {
                    object[] arrAtrr = property.GetCustomAttributes(typeof(ColumnMappingAttribute), true);

                    string colName = property.Name;
                    if (arrAtrr.Length > 0)
                    {
                        colName = (arrAtrr.First() as ColumnMappingAttribute).ColumnName;
                    }

                    try
                    {
                        //Author： yidane
                        //Date:2014年3月26日15:05:44
                        //Descript:添加判断，判断实体属性是否和DataTable的ColName有关联
                        //Resaon:避免可知的异常（row[colName]），不然此方法效率太低。
                        if (columnList.Any(col => string.Equals(col.ColumnName, colName, StringComparison.OrdinalIgnoreCase)))
                        {
                            object colValue = row[colName];

                            if (colValue != DBNull.Value)
                            {
                                property.SetValue(model, colValue, null);
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                list.Add(model);
            }
            return list;
        }
    }
}
