using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Common
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
        /// 返回的消息类型
        /// </summary>
        public string MessageType { get; set; }

        /// <summary>
        /// 返回的消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public object Data { get; set; }

        

        # region Success

        public static AjaxResult Success(string message, object data,string messageType= "success")
        {
            return new AjaxResult()
            {
                IsSuccess = true,
                MessageType = messageType,
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
        /// <summary>
        /// 返回错误消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageType">默认系统异常</param>
        /// <returns></returns>
        public static AjaxResult Error(string message, string messageType = "system")
        {
            return new AjaxResult()
            {
                IsSuccess = false,
                MessageType = messageType,
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
