using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Travel.Infrastructure.CommonFunctions
{
    public class JSONHelper
    {
        /// <summary>
        /// 将对象转换成为JSON字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Serialize(object data)
        {
            //设置时间类型的序列化
            var settings = new JsonSerializerSettings()
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };

            return JsonConvert.SerializeObject(data, settings);
        }

        /// <summary>
        /// 将JSON字符串转换为指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string jsonString) where T : class
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        /// <summary>
        /// 将对象转换成为JSON字符串
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string ObjectToJson<TObject>(TObject item) where TObject : class
        {
            var serializer = JsonConvert.SerializeObject(item);// JavaScriptConvert.SerializeObject();// DataContractJsonSerializer(item.GetType());  

            return serializer;
        }
    }
}
