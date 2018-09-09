using System.Net;
using System.Net.NetworkInformation;

namespace NetworkManager
{
    public abstract class NetworkInterfaceManagerBase
    {
        public string Type;

        public string Name;

        public IPAddress GatewayIpAddress;

        public IPAddress IpAddress;

        public NetworkInterface NetworkInterface;
    }
}
