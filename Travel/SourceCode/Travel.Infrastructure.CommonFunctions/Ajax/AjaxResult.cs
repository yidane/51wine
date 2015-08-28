using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.CommonFunctions.Ajax
{
    /// <summary>
    /// 通用Ajax请求返回数据
    /// </summary>
    public class AjaxResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 返回的消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public object Data { get; set; }

        # region Success
        public static AjaxResult Success(string message, object data)
        {
            return new AjaxResult()
            {
                IsSuccess = true,
                Message = message,
                Data = data
            };
        }
        public static AjaxResult Success(object data)
        {
            return Success(string.Empty, data);
        }
        #endregion


        # region Error
        public static AjaxResult Error(string message)
        {
            return new AjaxResult()
            {
                IsSuccess = false,
                Message = message
            };
        }
        #endregion

        #region Overrides
        /// <summary>
        /// 重构Tostring方法，使其序列化成JSON格式
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JSONHelper.Serialize(this);
        }
        #endregion
    }
}
