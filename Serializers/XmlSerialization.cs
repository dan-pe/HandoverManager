namespace Serializers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    public static class XmlSerialization
    {
        /// <summary>
        /// Deserialize XML format to object of type T.
        /// </summary>
        /// <typeparam name="T">
        /// Type of object to deserialize to.
        /// </typeparam>
        /// <param name="input">
        /// The input data to deserialize.
        /// </param>
        /// <returns></returns>
        public static T Deserialize<T>(string input) where T : class
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        /// <summary>
        /// Deserializes XML to dictionary of strings.
        /// </summary>
        /// <param name="data">
        /// The input data.
        /// </param>
        /// <returns></returns>
        public static Dictionary<string, string> XmlToDictionary(string data)
        {
            var doc = XDocument.Parse(data);
            var dataDictionary = new Dictionary<string, string>();

            foreach (XElement element in doc.Descendants().Where(p => p.HasElements == false))
            {
                int keyInt = 0;
                string keyName = element.Name.LocalName;

                while (dataDictionary.ContainsKey(keyName))
                {
                    keyName = element.Name.LocalName + "_" + keyInt++;
                }

                dataDictionary.Add(keyName, element.Value);
            }

            return dataDictionary;
        }
    }
}