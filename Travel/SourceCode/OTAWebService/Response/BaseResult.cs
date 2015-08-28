using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTAWebService.Response
{
    public class BaseResult<T> where T : class
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
        /// 返回结果对象
        /// </summary>
        public T ResultJson { get; set; }
    }
}