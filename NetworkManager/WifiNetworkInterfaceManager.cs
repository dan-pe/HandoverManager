using NativeWifi;
using NetworkMonitors.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;

namespace NetworkManager
{
    public class WifiNetworkInterfaceManager : NetworkInterfaceManagerBase, INetworkInterface
    {
        public WifiNetworkInterfaceManager()
        {
            this.WlanClient = new WlanClient();
            this.NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()
                .FirstOrDefault(ni => ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 && ni.OperationalStatus == OperationalStatus.Up);
            this.IpAddress = ActiveInterface.NetworkInterface.GetIPProperties().UnicastAddresses
                .FirstOrDefault(ua => ua.PrefixOrigin == PrefixOrigin.Dhcp)?.Address;
            this.GatewayIpAddress = NetworkInterface?.GetIPProperties().GatewayAddresses.FirstOrDefault()?.Address;
        }

        public WlanClient.WlanInterface ActiveInterface => this.WlanClient.Interfaces.FirstOrDefault();
        public WlanClient WlanClient { get; }

        public bool ConnectToNetwork(string profileName)
        {
            try
            {
                this.ActiveInterface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, profileName);
                Thread.Sleep(TimeSpan.FromSeconds(3));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<string> GetAvailableNetworkSsids()
        {
            var networkSsiDs = new List<string>();
            this.ActiveInterface.Scan();

            foreach (var availableNetwork in this.ActiveInterface
                .GetNetworkBssList()
                .Where(n => n.dot11Ssid.SSID.Length > 0))
            {
                networkSsiDs.Add(SsidParser.ParseFromBytes(availableNetwork.dot11Ssid.SSID));
            }

            return networkSsiDs;
        }

        public string GetInterfaceName()
        {
            var description = ActiveInterface.InterfaceDescription;
            return description;
        }

        public string GetInterfaceSpeed()
        {
            return ActiveInterface.NetworkInterface.Speed.ToString();
        }

        public string GetInterfaceType()
        {
            return ActiveInterface.NetworkInterface.NetworkInterfaceType.ToString();
        }
    }
}