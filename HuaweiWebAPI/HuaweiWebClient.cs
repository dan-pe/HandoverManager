namespace HuaweiWebAPI
{
    #region Usings

    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using Serializers;

    #endregion

    public class HuaweiWebClient
    {
        #region Private Fields

        private static string SessionId { get; set; }

        #endregion

        #region Constructors

        public HuaweiWebClient()
        {
            this.SetSessionId();
        }

        #endregion

        #region Public Methods

        public IDictionary<string, string> HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Headers.Set("Cookie", SessionId);

            using (HttpWebResponse res = (HttpWebResponse)request.GetResponse())
            using (Stream stream = res.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return XmlSerialization.XmlToDictionary(reader.ReadToEnd());
            }
        }

        #endregion

        #region Private Methods

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

        private void SetSessionId()
        {
            var tokenReposne = this.SimpleGet("http://192.168.8.1/api/webserver/SesTokInfo");
            var tokenId = tokenReposne.FirstOrDefault(key => key.Key == "SesInfo").Value;
            SessionId = tokenId;
        }

        #endregion 
    }
}
