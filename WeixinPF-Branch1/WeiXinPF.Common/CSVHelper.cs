using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace WeiXinPF.Common
{
    public class CSVHelper
    {
        /// <summary>
        /// 保存csv文件
        /// </summary>
        /// <param name="headerList"></param>
        /// <param name="fileName"></param>
        /// <param name="listModel"></param>
        public static bool SaveAsCSV<T>(string fileName, IList<string> headerList, IList<T> listModel) where T : class, new()
        {
            bool flag = false;
            try
            {
                var sb = new StringBuilder();
                //通过反射 显示要显示的列
                const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static; //反射标识
                var objType = typeof(T);
                var propInfoArr = objType.GetProperties(bindingFlags);
                var header = String.Empty;
                var propertyList = new List<string>();
                foreach (PropertyInfo info in propInfoArr)
                {
                    if (String.CompareOrdinal(info.Name.ToUpper(), "ID") != 0) //不考虑自增长的id或者自动生成的guid等
                    {
                        if (!propertyList.Contains(info.Name))
                        {
                            propertyList.Add(info.Name);
                        }
                        header += info.Name + ",";
                    }
                }
                sb.AppendLine(header.Trim(',')); //csv头

                foreach (T model in listModel)
                {
                    string strModel = String.Empty;
                    foreach (string strProp in propertyList)
                    {
                        foreach (PropertyInfo propInfo in propInfoArr)
                        {
                            if (String.CompareOrdinal(propInfo.Name.ToUpper(), strProp.ToUpper()) == 0)
                            {
                                PropertyInfo modelProperty = model.GetType().GetProperty(propInfo.Name);
                                if (modelProperty != null)
                                {
                                    var objResult = modelProperty.GetValue(model, null);
                                    var result = (objResult ?? String.Empty).ToString().Trim();
                                    if (result.IndexOf(',') != -1)
                                    {
                                        result = "\"" + result.Replace("\"", "\"\"") + "\""; //特殊字符处理 ？
                                        //result = result.Replace("\"", "“").Replace(',', '，') + "\"";
                                    }
                                    if (!String.IsNullOrEmpty(result))
                                    {
                                        Type valueType = modelProperty.PropertyType;
                                        if (valueType == typeof(decimal?))
                                        {
                                            result = Decimal.Parse(result).ToString("#.#");
                                        }
                                        else if (valueType == typeof(decimal))
                                        {
                                            result = Decimal.Parse(result).ToString("#.#");
                                        }
                                        else if (valueType == typeof(double?))
                                        {
                                            result = Double.Parse(result).ToString("#.#");
                                        }
                                        else if (valueType == typeof(double))
                                        {
                                            result = Double.Parse(result).ToString("#.#");
                                        }
                                        else if (valueType == typeof(float?))
                                        {
                                            result = Single.Parse(result).ToString("#.#");
                                        }
                                        else if (valueType == typeof(float))
                                        {
                                            result = Single.Parse(result).ToString("#.#");
                                        }
                                    }
                                    strModel += result + ",";
                                }
                                else
                                {
                                    strModel += ",";
                                }
                                break;
                            }
                        }
                    }
                    strModel = strModel.Substring(0, strModel.Length - 1);
                    sb.AppendLine(strModel);
                }
                string content = sb.ToString();
                string dir = Directory.GetCurrentDirectory();
                string fullName = Path.Combine(dir, fileName);
                if (File.Exists(fullName)) File.Delete(fullName);
                using (var fs = new FileStream(fullName, FileMode.CreateNew, FileAccess.Write))
                {
                    var sw = new StreamWriter(fs, Encoding.Default);
                    sw.Flush();
                    sw.Write(content);
                    sw.Flush();
                    sw.Close();
                }
                flag = true;
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 保存SCV文件
        /// </summary>
        /// <param name="dataTable">数据源</param>
        /// <param name="headerList">表头</param>
        /// <param name="columnIndexList">数据源筛选</param>
        /// <returns></returns>
        public static string SaveAsCSV(DataTable dataTable, List<int> columnIndexList = null, List<string> headerList = null)
        {
            if (dataTable == null || dataTable.Columns.Count == 0)
                return String.Empty;

            var outputBuilder = new StringBuilder();
            const string scvSplitChar = ",";
            const string specialSplitChar = "'";

            #region 组装ColumnIndex
            //如果ColumnIndexList为空
            if (columnIndexList == null || columnIndexList.Count == 0)
            {
                columnIndexList = new List<int>();
                for (var columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++)
                {
                    columnIndexList.Add(columnIndex);
                }
            }
            #endregion

            #region 组装Header
            //自定义了Header，则采用自定义表头
            if (headerList != null && headerList.Count > 0)
            {
                for (var index = 0; index < headerList.Count; index++)
                {
                    outputBuilder.AppendFormat("{0}{1}", headerList[index], index == headerList.Count - 1 ? Environment.NewLine : scvSplitChar);
                }
            }
            else
            {
                for (var index = 0; index < columnIndexList.Count; index++)
                {
                    outputBuilder.AppendFormat("{0}{1}", dataTable.Columns[columnIndexList[index]].ColumnName, index == columnIndexList.Count - 1 ? Environment.NewLine : scvSplitChar);
                }
            }
            #endregion

            #region 组装数据
            //组装数据
            var dataSourceColumnCount = dataTable.Columns.Count;
            foreach (DataRow row in dataTable.Rows)
            {
                for (var index = 0; index < columnIndexList.Count; index++)
                {
                    var dataField = columnIndexList[index] > 0 && columnIndexList[index] < dataSourceColumnCount
                                    ? row[columnIndexList[index]]
                                    : String.Empty;
                    var dataFieldString = String.Empty;

                    if (dataField != null && dataField != DBNull.Value)
                    {
                        dataFieldString = dataField.ToString();
                        if (dataFieldString.Length > 8)
                        {
                            dataFieldString = String.Format("{0}{1}", dataFieldString, specialSplitChar);
                        }
                        else
                        {
                            dataFieldString = dataField.ToString();
                        }
                    }

                    outputBuilder.AppendFormat("\"{0}\"{1}", dataFieldString, index < columnIndexList.Count - 1 ? scvSplitChar : Environment.NewLine);
                }
            }
            #endregion

            return outputBuilder.ToString();
        }

        /// <summary>
        /// 生成SCV文件
        /// </summary>
        /// <param name="response"></param>
        /// <param name="fileName"></param>
        /// <param name="headerList"></param>
        /// <param name="columnIndexList"></param>
        /// <param name="dataTable"></param>
        public static void DownloadAsSCV(HttpResponse response, string fileName, DataTable dataTable, List<int> columnIndexList = null, List<string> headerList = null)
        {
            string csvDataString = SaveAsCSV(dataTable, columnIndexList, headerList);

            DownloadCSV(response, fileName, csvDataString);
        }


        /// <summary>
        /// 生成SCV文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <param name="fileName"></param>
        /// <param name="list"></param>
        /// <param name="columnIndexList"></param>
        /// <param name="headerList"></param>
        public static void DownloadAsSCV<T>(HttpResponse response, string fileName, IList<T> list, List<int> columnIndexList = null, List<string> headerList = null) where T : class, new()
        {
            var dataTable = ToDataTable(list);
            string csvDataString = SaveAsCSV(dataTable, columnIndexList, headerList);

            DownloadCSV(response, fileName, csvDataString);
        }

        #region Private Functions
        /// <summary>
        /// 判断是否数字字符串
        /// </summary>
        /// <param name="numbericString"></param>
        /// <returns></returns>
        private static bool IsNumberic(string numbericString)
        {
            var rex = new Regex(@"^\d+$");
            return rex.IsMatch(numbericString);
        }

        /// <summary>
        /// 下载CSV文件
        /// </summary>
        /// <param name="response"></param>
        /// <param name="fileName"></param>
        /// <param name="csvDataString"></param>
        private static void DownloadCSV(HttpResponse response, string fileName, string csvDataString)
        {
            using (var stream = new MemoryStream())
            {
                using (var sw = new StreamWriter(stream, Encoding.UTF8))
                {
                    //写入SCV文件流
                    sw.Write(csvDataString);
                    sw.Flush();

                    //清理掉原始数据流
                    response.Clear();
                    response.ClearContent();
                    response.ClearHeaders();

                    response.Buffer = true;
                    response.Charset = "utf-8";

                    response.ContentType = "application/ms-excel";
                    response.AppendHeader("content-disposition", String.Format("attachment;filename={0}.csv", HttpUtility.UrlEncode(fileName)));
                    response.ContentType = "application/octet-stream";
                    response.AddHeader("Content-Length", stream.Length.ToString());
                    response.ContentEncoding = Encoding.Default;
                    response.BinaryWrite(stream.ToArray());
                    response.Flush();
                    response.End();
                }
            }
        }

        /// <summary>
        /// list转datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        private static DataTable ToDataTable<T>(IEnumerable<T> list)
        {

            //创建属性的集合    
            List<PropertyInfo> pList = new List<PropertyInfo>();
            //获得反射的入口    

            Type type = typeof(T);
            DataTable dt = new DataTable();
            //把所有的public属性加入到集合 并添加DataTable的列    
            Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType); });
            foreach (var item in list)
            {
                //创建一个DataRow实例    
                DataRow row = dt.NewRow();
                //给row 赋值    
                pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
                //加入到DataTable    
                dt.Rows.Add(row);
            }
            return dt;
        }

        #endregion
    }
}