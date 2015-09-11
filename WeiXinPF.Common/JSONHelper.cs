using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace WeiXinPF.Common
{
    public class JSONHelper
    {
        /// <summary>
        /// 将对象转换成为JSON字符串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="useCamel">是否使用驼峰命名法序列化</param>
        /// <returns></returns>
        public static string Serialize(object data, bool useCamel = false)
        {
            //设置时间类型的序列化
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new IsoDateTimeConverter()
            {
                DateTimeFormat = "yyyy-MM-dd"
            });
            if (useCamel)
            {
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }
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
