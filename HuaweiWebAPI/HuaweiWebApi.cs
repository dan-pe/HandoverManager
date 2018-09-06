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

        public static MonitoringStatus MonitoringStatus()
        {
            var monitoringXml = WebClient.XmlGet("api/monitoring/status");
            MonitoringStatus basicInformation = XmlSerialization.Deserialize<MonitoringStatus>(monitoringXml);

            return basicInformation;
        }

        public static string GatewatAddres()
        {
            var webRespose = WebClient.XmlGet("html/deviceinformation.html");
            var isIppresent = webRespose.Contains("100.94.248.69");
            var anotherResp = webRespose.Contains("<td>WAN IP Address:</td>");
            return string.Empty;
        }

       

        //CurrentNetworkType, CurrentNetworkTypeEx:
        //0 - brak usługi
        //1 - GSM
        //2 - GPRS
        //3 - EDGE
        //4 - WCDMA
        //5 - HSDPA
        //6 - HSUPA
        //7 - HSPA
        //8 - TDSCDMA
        //9 - HSPA+
        //10 - EVDO rev. 0
        //11 - EVDO rev. A
        //12 - EVDO rev. B
        //13 - 1xRTT
        //14 - UMB
        //15 - 1xEVDV
        //16 - 3xRTT
        //17 - HSPA+64QAM
        //18 - HSPA+MIMO
        //19 - LTE
        //21 - IS95A
        //22 - IS95B
        //23 - CDMA1x
        //24 - EVDO rev. 0
        //25 - EVDO rev. A
        //26 - EVDO rev. B
        //27 - Hybrydowa CDMA1x
        //28 - Hybrydowa EVDO rev. 0
        //29 - Hybrydowa EVDO rev.A
        //30 - Hybrydowa EVDO rev.B
        //31 - EHRPD rev. 0
        //32 - EHRPD rev. A
        //33 - EHRPD rev. B
        //34 - Hybrydowa EHRPD rev. 0
        //35 - Hybrydowa EHRPD rev.A
        //36 - Hybrydowa EHRPD rev.B
        //41 - WCDMA
        //42 - HSDPA
        //43 - HSUPA
        //44 - HSPA
        //45 - HSPA+
        //46 - DC HSPA+
        //61 - TD SCDMA
        //62 - TD HSDPA
        //63 - TD HSUPA
        //64 - TD HSPA
        //65 - TD HSPA+
        //81 - 802.16E
        //101 - LTE
    }
}
