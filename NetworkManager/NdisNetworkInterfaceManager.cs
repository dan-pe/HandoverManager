#region Usings

using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using HuaweiWebAPI;
using HuaweiWebAPI.Structs;
using NetTool;

#endregion

namespace NetworkManager
{
    public class NdisNetworkInterfaceManager : NetworkInterfaceManagerBase, INetworkInterface
    {
        private readonly BasicInformation _basicInformation;

        public NdisNetworkInterfaceManager()
        {
            this._basicInformation = HuaweiWebApi.BasicInformation();
            this.NetworkInterface = NetworkInterface
                .GetAllNetworkInterfaces()
                .FirstOrDefault(ni => ni.Description.Contains("NDIS"));
            this.IpAddress = NetworkInterface?.GetIPProperties().UnicastAddresses
                .FirstOrDefault(ua => ua.PrefixOrigin == PrefixOrigin.Dhcp)?.Address;

            IPAddress remoteIp = IPAddress.Parse("1.1.1.1");

            this.GatewayIpAddress = TraceRoute.GetTraceRoute(this.IpAddress, remoteIp).First().MapToIPv4();
        }

        public string GetInterfaceName()
        {
            return _basicInformation.DeviceName;
        }

        public string GetInterfaceType()
        {
            return ((NetworkType)HuaweiWebApi.MonitoringStatus().CurrentNetworkType).ToString();
        }

        public string GetInterfaceSpeed()
        {
            return "NotImplemented";
        }
    }

}
