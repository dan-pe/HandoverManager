using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using HuaweiWebAPI.Structs;
using Serializers;

namespace HuaweiWebAPI
{
    // WebAPi available here http://forum.jdtech.pl/Watek-hilink-api-dla-urzadzen-huawei


    public static class HuaweiWebApi
    {

        private static readonly HuaweiWebClient WebClient = new HuaweiWebClient();

        public static IDictionary<string, string> GetNetworkInfo()
        {
            var networkInfo = WebClient.HttpGet("api/global/module-switch");
            return networkInfo;
        }

        public static IDictionary<string, string> GetBasicInformation()
        {
            
            var basicInformation = WebClient.HttpGet("api/device/basic_information");
            return basicInformation;
        }

        public static BasicInformation BasicInformationFrom()
        {
            var basicInfoXml = WebClient.XmlGet("api/device/basic_information");
            BasicInformation basicInformation = XmlSerialization.Deserialize<BasicInformation>(basicInfoXml);

            return basicInformation;
        }
    }
}
