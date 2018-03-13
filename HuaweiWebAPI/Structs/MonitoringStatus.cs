using System.Xml.Serialization;

namespace HuaweiWebAPI.Structs
{
    [XmlRoot("response")]
    public class MonitoringStatus
    {
        public int ConnectionStatus;
        public string WifiConnectionStatus;
        public string SignalStrength;
        public int SignalIcon;
        public int CurrentNetworkType;
        public int CurrentServiceDomain;
        public int RoamingStatus;
        public string BatteryStatus;
        public string BatteryLevel;
        public string BatteryPercent;
        public int simlockStatus;
        public string WanIPAddress;
        public string WanIPv6Address;
        public string PrimaryDns;
        public string SecondaryDns;
        public string PrimaryIPv6Dns;
        public string SecondaryIPv6Dns;
        public string CurrentWifiUser;
        public string TotalWifiUser;
        public int currenttotalwifiuser;
        public int ServiceStatus;
        public int SimStatus;
        public string WifiStatus;
        public int CurrentNetworkTypeEx;
        public int maxsignal;
        public int wifiindooronly;
        public int wififrequence;
        public string msisdn;
        public string classify;
        public int flymode;
    }
}
