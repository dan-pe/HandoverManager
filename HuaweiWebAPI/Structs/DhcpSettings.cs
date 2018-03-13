using System.Xml.Serialization;

namespace HuaweiWebAPI.Structs
{
    [XmlRoot("response")]
    public class DhcpSettings
    {
        public string DhcpIPAddress;
        public string DhcpLanNetmask;
        public int DhcpStatus;
        public string DhcpStartIPAddress;
        public string DhcpEndIPAddress;
        public long DhcpLeaseTime;
        public int DnsStatus;
        public string PrimaryDns;
        public string SecondaryDns;


    }
}