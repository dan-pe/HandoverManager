using System.Xml.Serialization;

namespace HuaweiWebAPI.Structs
{
    [XmlRoot("response")]
    public class MonitoringStatus
    {
        public string BatteryLevel;
        public string BatteryPercent;
        public string BatteryStatus;
        public string classify;
        public int ConnectionStatus;
        public int CurrentNetworkType;
        public int CurrentNetworkTypeEx;
        public int CurrentServiceDomain;
        public int currenttotalwifiuser;
        public string CurrentWifiUser;
        public int flymode;
        public int maxsignal;
        public string msisdn;
        public string PrimaryDns;
        public string PrimaryIPv6Dns;
        public int RoamingStatus;
        public string SecondaryDns;
        public string SecondaryIPv6Dns;
        public int ServiceStatus;
        public int SignalIcon;
        public string SignalStrength;
        public int simlockStatus;
        public int SimStatus;
        public string TotalWifiUser;
        public string WanIPAddress;
        public string WanIPv6Address;
        public string WifiConnectionStatus;
        public int wififrequence;
        public int wifiindooronly;
        public string WifiStatus;
    }
}