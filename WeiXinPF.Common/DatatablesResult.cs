namespace WeiXinPF.Common
{
    /// <summary>
    /// Jquery.Datatables Ajax 请求返回结果集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DatatablesResult<T>
    {
        /// <summary>
        ///安全参数，原样返回即可
        /// </summary>
        public int Draw { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int RecordsTotal { get; set; }

        /// <summary>
        /// 过滤后的总数
        /// </summary>
        public int RecordsFiltered { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 友好的错误提示信息
        /// </summary>
        public string Error { get; set; }
    }
}
