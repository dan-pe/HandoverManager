using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Serializers;

namespace HuaweiWebAPI
{
    public class HuaweiWebClient
    {
        private static readonly HttpClient Client = new HttpClient();

        public static CookieContainer Container { get; set; }

        public HuaweiWebClient()
        {

            this.GetSessionId();


            this.SetCookieContainter();

            var costam = this.Get("http://192.168.8.1/api/global/module-switch");
        }

        private void SetCookieContainter()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.8.1/api/webserver/SesTokInfo");
            Dictionary<string,string> result = new Dictionary<string, string>();
            request.Headers.Set("Cookie", this.GetSessionId());
            //Container = new CookieContainer();
            //request.CookieContainer = Container;

            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse res = (HttpWebResponse) request.GetResponse())
            {
                using (Stream stream = res.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = XmlSerialization.XmlToDictionary(reader.ReadToEnd());
                }
            }

        }

        public IDictionary<string, string> Get(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Headers.Set("Cookie", this.GetSessionId());

            using (HttpWebResponse res = (HttpWebResponse)request.GetResponse())
            using (Stream stream = res.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return XmlSerialization.XmlToDictionary(reader.ReadToEnd());
            }
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

        private string GetSessionId()
        {
            var tokenReposne = this.SimpleGet("http://192.168.8.1/api/webserver/SesTokInfo");
            var tokenId = tokenReposne.FirstOrDefault(key => key.Key == "SesInfo").Value;
            return tokenId;

        }

    }
}
