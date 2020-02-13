namespace HuaweiWebAPI
{
    using Serializers;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;

    public class HuaweiWebClient
    {
        private static readonly string apiRoute = "http://192.168.8.1/";

        public HuaweiWebClient()
        {
            this.SetSessionId();
        }

        private static string SessionId { get; set; }

        public IDictionary<string, string> HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest
                .Create(string.Concat(apiRoute, url));
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Headers.Set("Cookie", SessionId);

            using (HttpWebResponse res = (HttpWebResponse)request.GetResponse())
            using (Stream stream = res.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return XmlSerialization.XmlToDictionary(reader.ReadToEnd());
            }
        }

        public string XmlGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest
                .Create(string.Concat(apiRoute, url));
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Headers.Set("Cookie", SessionId);

            using (HttpWebResponse res = (HttpWebResponse)request.GetResponse())
            using (Stream stream = res.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private void SetSessionId()
        {
            var tokenReposne = this.SimpleGet("http://192.168.8.1/api/webserver/SesTokInfo");
            var tokenId = tokenReposne.FirstOrDefault(key => key.Key == "SesInfo").Value;
            SessionId = tokenId;
        }

        private IDictionary<string, string> SimpleGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
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