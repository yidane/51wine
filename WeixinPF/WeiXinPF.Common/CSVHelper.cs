using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
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
                var header = string.Empty;
                var propertyList = new List<string>();
                foreach (PropertyInfo info in propInfoArr)
                {
                    if (System.String.CompareOrdinal(info.Name.ToUpper(), "ID") != 0) //不考虑自增长的id或者自动生成的guid等
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
                    string strModel = string.Empty;
                    foreach (string strProp in propertyList)
                    {
                        foreach (PropertyInfo propInfo in propInfoArr)
                        {
                            if (System.String.CompareOrdinal(propInfo.Name.ToUpper(), strProp.ToUpper()) == 0)
                            {
                                PropertyInfo modelProperty = model.GetType().GetProperty(propInfo.Name);
                                if (modelProperty != null)
                                {
                                    var objResult = modelProperty.GetValue(model, null);
                                    var result = (objResult ?? string.Empty).ToString().Trim();
                                    if (result.IndexOf(',') != -1)
                                    {
                                        result = "\"" + result.Replace("\"", "\"\"") + "\""; //特殊字符处理 ？
                                        //result = result.Replace("\"", "“").Replace(',', '，') + "\"";
                                    }
                                    if (!string.IsNullOrEmpty(result))
                                    {
                                        Type valueType = modelProperty.PropertyType;
                                        if (valueType == typeof(decimal?))
                                        {
                                            result = decimal.Parse(result).ToString("#.#");
                                        }
                                        else if (valueType == typeof(decimal))
                                        {
                                            result = decimal.Parse(result).ToString("#.#");
                                        }
                                        else if (valueType == typeof(double?))
                                        {
                                            result = double.Parse(result).ToString("#.#");
                                        }
                                        else if (valueType == typeof(double))
                                        {
                                            result = double.Parse(result).ToString("#.#");
                                        }
                                        else if (valueType == typeof(float?))
                                        {
                                            result = float.Parse(result).ToString("#.#");
                                        }
                                        else if (valueType == typeof(float))
                                        {
                                            result = float.Parse(result).ToString("#.#");
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
        /// 保存CSV文件
        /// </summary>
        /// <param name="headerList"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static string SaveAsCSV(List<string> headerList, DataTable dataTable)
        {
            if (dataTable == null || dataTable.Rows.Count == 0 || dataTable.Columns.Count == 0)
                return string.Empty;

            var columnIndexList = new List<int>();
            if (headerList != null && headerList.Count > 0)
            {
                for (var index = 0; index < headerList.Count; index++)
                {
                    columnIndexList.Add(index);
                }
            }
            else
            {
                for (var index = 0; index < dataTable.Columns.Count; index++)
                {
                    columnIndexList.Add(index);
                }
            }

            return SaveAsCSV(headerList, columnIndexList, dataTable);
        }

        /// <summary>
        /// 保存SCV文件
        /// </summary>
        /// <param name="headerList">表头</param>
        /// <param name="columnIndexList">数据源筛选</param>
        /// <param name="dataTable">数据源</param>
        /// <returns></returns>
        public static string SaveAsCSV(List<string> headerList, List<int> columnIndexList, DataTable dataTable)
        {
            if (dataTable == null || dataTable.Rows.Count == 0 || dataTable.Columns.Count == 0)
                return string.Empty;

            var outputBuilder = new StringBuilder();
            const string scvSplitChar = ",";
            const string specialSplitChar = "'";

            var columnCount = 0;
            //组装Header
            if (headerList != null && headerList.Count > 0)
            {
                columnCount = headerList.Count;
                for (var index = 0; index < headerList.Count; index++)
                {
                    if (index == columnCount - 1)
                    {
                        outputBuilder.AppendFormat("{0}{1}", headerList[index], Environment.NewLine);
                    }
                    else
                    {
                        outputBuilder.AppendFormat("{0}{1}", headerList[index], scvSplitChar);
                    }
                }
            }
            else
            {
                columnCount = dataTable.Columns.Count;
                for (var index = 0; index < dataTable.Columns.Count; index++)
                {
                    if (index == columnCount - 1)
                    {
                        outputBuilder.AppendFormat("{0}{1}", dataTable.Columns[index].ColumnName, Environment.NewLine);
                    }
                    else
                    {
                        outputBuilder.AppendFormat("{0}{1}", dataTable.Columns[index].ColumnName, scvSplitChar);
                    }
                }
            }

            //组装数据
            foreach (DataRow row in dataTable.Rows)
            {
                for (var index = 0; index < columnCount; index++)
                {
                    //包含的索引才会输出
                    if (!columnIndexList.Contains(index)) continue;
                    var dataField = row[index];
                    if (dataField != null && dataField != DBNull.Value)
                    {
                        var datafieldString = dataField.ToString();
                        if (IsNumberic(datafieldString) && datafieldString.Length > 8)
                        {
                            outputBuilder.AppendFormat("{0}{1}", datafieldString, specialSplitChar);
                        }
                        else
                        {
                            outputBuilder.AppendFormat("{0}", datafieldString);
                        }
                    }
                    else
                    {
                        outputBuilder.AppendFormat("{0}", string.Empty);
                    }

                    //判断是否行结尾
                    if (index == columnIndexList[columnIndexList.Count - 1])
                    {
                        outputBuilder.Append(Environment.NewLine);
                    }
                    else
                    {
                        outputBuilder.AppendFormat(scvSplitChar);
                    }
                }
            }

            return outputBuilder.ToString();
        }

        /// <summary>
        /// 生成SCV文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="headerList"></param>
        /// <param name="columnIndexList"></param>
        /// <param name="dataTable"></param>
        public static void DownloadAsSCV(string fileName, List<string> headerList, List<int> columnIndexList, DataTable dataTable)
        {
            string csvDataString = columnIndexList == null ? SaveAsCSV(headerList, dataTable) : SaveAsCSV(headerList, columnIndexList, dataTable);

            DownloadCSV(fileName, csvDataString);
        }

        #region Private Functions
        /// <summary>
        /// 判断是否数字字符串
        /// </summary>
        /// <param name="numbericString"></param>
        /// <returns></returns>
        private static bool IsNumberic(string numbericString)
        {
            var rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            return rex.IsMatch(numbericString);
        }

        /// <summary>
        /// 下载CSV文件
        /// </summary>
        /// <param>fileName</param>
        /// <param>fileName</param>
        /// <param name="csvDataString"></param>
        private static void DownloadCSV(string fileName, string csvDataString)
        {
            //using (var stream = new MemoryStream())
            //{
            //    using (var sw = new StreamWriter(stream, System.Text.Encoding.UTF8))
            //    {
            //写入SCV文件流
            //sw.Write(csvDataString);
            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.Buffer = true;
            //HttpContext.Current.Response.Charset = "utf-8";

            HttpContext.Current.Response.AppendHeader("content-disposition", string.Format("attachment;filename={0}.csv", fileName));
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;

            //response.contenttype指定文件类型 能为application/ms-excel || application/ms-word || application/ms-txt || application/ms-html || 或其他浏览器可直接支持文件 
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            //HttpContext.Current.Response.BinaryWrite(stream.ToArray());
            HttpContext.Current.Response.Write(csvDataString);
            //HttpContext.Current.Response.Flush();
            //HttpContext.Current.Response.End();
            //    }
            //}
        }
        #endregion

    }
}