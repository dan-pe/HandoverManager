using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaweiWebAPI
{
    // WebAPi available here http://forum.jdtech.pl/Watek-hilink-api-dla-urzadzen-huawei


    public static class HuaweiWebApi
    {
        private static readonly HuaweiWebClient WebClient = new HuaweiWebClient();

        public static IDictionary<string, string> GetNetworkInfo()
        {
            var networkInfo = WebClient.HttpGet("http://192.168.8.1/api/global/module-switch");
            return networkInfo;
        }

        public static IDictionary<string, string> GetBasicInformation()
        {
            
            var basicInformation = WebClient.HttpGet("http://192.168.8.1/api/device/basic_information");
            return basicInformation;
        }
    }
}
