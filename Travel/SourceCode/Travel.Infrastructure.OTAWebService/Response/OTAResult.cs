using Travel.Infrastructure.CommonFunctions;

namespace Travel.Infrastructure.OTAWebService.Response
{
    public class OTAResult<T> where T : class
    {

        /// <summary>
        /// 是否调用成功
        /// </summary>
        public bool IsTrue { get; set; }

        /// <summary>
        /// 结果代码
        /// </summary>
        public string ResultCode { get; set; }

        /// <summary>
        /// 结果代码详情
        /// </summary>
        public string ResultMsg { get; set; }

        /// <summary>
        /// 返回结果对象Json
        /// </summary>
        public string ResultJson { get; set; }

        /// <summary>
        /// 返回结果对象
        /// </summary>
        public T ResultData
        {
            get
            {
                if (string.IsNullOrEmpty(ResultJson))
                    return default(T);

                return JSONHelper.Deserialize<T>(ResultJson);
            }
        }

        public static OTAResult<T> CreateInstance(string json)
        {
            return JSONHelper.Deserialize<OTAResult<T>>(json);
        }
    }
}