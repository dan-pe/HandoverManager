using System.Xml.Serialization;

namespace HuaweiWebAPI.Structs
{
    [XmlRoot("response")]
    public class DhcpSettings
    {
        public string DhcpEndIPAddress;
        public string DhcpIPAddress;
        public string DhcpLanNetmask;
        public long DhcpLeaseTime;
        public string DhcpStartIPAddress;
        public int DhcpStatus;
        public int DnsStatus;
        public string PrimaryDns;
        public string SecondaryDns;
    }
}