using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Serializers;

namespace HuaweiWebAPI
{
    public class GetRequest
    {
        private static readonly HttpClient Client = new HttpClient();

        public GetRequest()
        {
        }

        public IDictionary<string, string> Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse res = (HttpWebResponse)request.GetResponse())
            using (Stream stream = res.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return XmlSerialization.XmlToDictionary(reader.ReadToEnd());
            }
        }
    }
}
