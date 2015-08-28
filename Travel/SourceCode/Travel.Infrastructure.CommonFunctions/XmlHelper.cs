using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Travel.Infrastructure.CommonFunctions
{
    public class XmlHelper
    {
        /// <summary>
        /// 将一个对象序列化为XML字符串
        /// </summary>
        /// <param name="o">要序列化的对象</param>
        /// <returns>序列化产生的XML字符串</returns>
        public static string XmlSerialize<T>(T o)
        {
            using (var stream = new MemoryStream())
            {
                XmlSerializeInternal(stream, o, Encoding.UTF8);

                stream.Position = 0;
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// 从XML字符串中反序列化对象
        /// </summary>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="s">包含对象的XML字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>反序列化得到的对象</returns>
        public static T XmlDeserialize<T>(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                throw new ArgumentNullException("s");
            }

            var mySerializer = new XmlSerializer(typeof(T));
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(s)))
            {
                using (var sr = new StreamReader(ms, Encoding.UTF8))
                {
                    return (T)mySerializer.Deserialize(sr);
                }
            }
        }

        private static void XmlSerializeInternal<T>(Stream stream, T o, Encoding encoding)
        {
            if (o == null)
                throw new ArgumentNullException("o");
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            var serializer = new XmlSerializer(typeof(T));

            var settings = new XmlWriterSettings
            {
                Indent = true,
                NewLineChars = "",
                Encoding = encoding,
                IndentChars = "",
                OmitXmlDeclaration = true
            };

            // 强制指定命名空间，覆盖默认的命名空间。
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, o);
                writer.Close();
            }
        }
    }
}
