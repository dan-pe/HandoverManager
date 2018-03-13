using System.Collections.Generic;
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

        public static IDictionary<string, string> GetBasicInformationDictionary()
        {
            
            var basicInformation = WebClient.HttpGet("api/device/basic_information");
            return basicInformation;
        }

        public static DhcpSettings DhcpSettings()
        {
            var dhcpXml = WebClient.XmlGet("api/dhcp/settings");
            DhcpSettings dhcpSettings = XmlSerialization.Deserialize<DhcpSettings>(dhcpXml);
            return dhcpSettings;
        }

        public static BasicInformation BasicInformation()
        {
            var basicInfoXml = WebClient.XmlGet("api/device/basic_information");
            BasicInformation basicInformation = XmlSerialization.Deserialize<BasicInformation>(basicInfoXml);

            return basicInformation;
        }
    }
}
