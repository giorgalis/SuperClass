using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SuperClass
{
    public class SuperClass
    {
        /// <summary>
        /// Converts an XmlNode to an object.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="xmlNode">The XmlNode</param>
        /// <returns></returns>
        public static T ConvertFromXmlNode<T>(XmlNode xmlNode) where T : class
        {
            if (xmlNode == null) throw new ArgumentNullException(nameof(xmlNode));

            using (MemoryStream stm = new MemoryStream())
            {
                using (StreamWriter stw = new StreamWriter(stm))
                {
                    stw.Write(xmlNode.OuterXml);
                    stw.Flush();
                    stm.Position = 0;

                    XmlSerializer ser = new XmlSerializer(typeof(T));
                    T result = (ser.Deserialize(stm) as T);

                    return result;
                }
            }
        }

        /// <summary>
        /// Converts an XML string to an object.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="xmlString">The xml string</param>
        /// <returns></returns>
        protected T ConvertFromXmlString<T>(string xmlString) where T : class
        {
            if (string.IsNullOrEmpty(xmlString)) throw new ArgumentNullException(nameof(xmlString));

            T retClass = default(T);
            using (TextReader reader = new StringReader(xmlString))
            {
                try
                {
                    retClass = (T)new XmlSerializer(typeof(T)).Deserialize(reader);
                }
                catch (InvalidOperationException)
                {
                    throw new InvalidOperationException(nameof(xmlString));
                }
            }
            return retClass;
        }
    }
}
