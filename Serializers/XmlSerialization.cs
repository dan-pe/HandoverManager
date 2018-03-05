using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Serializers
{
    public static class XmlSerialization
    {
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
