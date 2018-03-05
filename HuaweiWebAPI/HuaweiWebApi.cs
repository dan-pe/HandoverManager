using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaweiWebAPI
{
    public static class HuaweiWebApi
    {
        private static readonly HuaweiWebClient WebClient = new HuaweiWebClient();

        public static IDictionary<string, string> GetNetworkInfo()
        {
            var networkInfo = WebClient.Get("http://192.168.8.1/api/global/module-switch");
            return networkInfo;
        }
    }
}
