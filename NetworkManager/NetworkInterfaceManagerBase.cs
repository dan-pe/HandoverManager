using System.Net;
using System.Net.NetworkInformation;

namespace NetworkManager
{
    public abstract class NetworkInterfaceManagerBase
    {
        public IPAddress GatewayIpAddress;
        public IPAddress IpAddress;
        public string Name;
        public NetworkInterface NetworkInterface;
        public string Type;
    }
}