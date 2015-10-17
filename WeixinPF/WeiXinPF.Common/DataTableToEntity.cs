using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WeiXinPF.Common
{
    public static class DataTableToEntity
    {
        /// <summary>
        /// 扩展DataTable类，将某个DataTable转换成指定类型的实体
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> ToObject<TSource>(this DataTable dt)
            where TSource : class
        {
            PropertyInfo[] propertys = null;

            if (dt == null || dt.Rows.Count <= 0)
                yield break;

            //获取传入类型所包含的属性
            propertys = typeof(TSource).GetProperties();

            foreach (DataRow row in dt.Rows)
            {
                //创建传入类型的实例          
                TSource objTmp = (TSource)Assembly.GetAssembly(typeof(TSource)).CreateInstance(typeof(TSource).FullName);

                for (int i = 0; i < propertys.Length; i++)
                {
                    //属性名称是否与DataTable中的列名相同
                    if (row.Table.Columns.Contains(propertys[i].Name))
                    {
                        propertys[i].SetValue(objTmp,
                        row[propertys[i].Name].GetType().Equals(propertys[i].PropertyType) ?
                            row[propertys[i].Name] :
                            Convert.ChangeType(row[propertys[i].Name], propertys[i].PropertyType),
                        null);
                    }
                    //是否有自定义属性与DataTable中的列名形同
                    else if (propertys[i].IsDefined(typeof(ColumnAttribute), true))
                    {
                        var obj = propertys[i].GetCustomAttributes(true).Where(
                            attr => row.Table.Columns.Contains(((ColumnAttribute)attr).ColumnName));

                        if (obj.ToList().Count > 0)
                        {
                            propertys[i].SetValue(objTmp,
                                                row[((ColumnAttribute)(obj.ToList()[0])).ColumnName].GetType().Equals(propertys[i].PropertyType) ?
                                                    row[((ColumnAttribute)(obj.ToList()[0])).ColumnName] :
                                                    Convert.ChangeType(row[((ColumnAttribute)(obj.ToList()[0])).ColumnName], propertys[i].PropertyType),
                                                null);
                        }

                    }
                }

                yield return objTmp;
            }
        }
    }
}
