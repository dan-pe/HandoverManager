#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using NativeWifi;
using NetworkMonitors.Parsers;

#endregion

namespace NetworkManager
{
    public class WifiNetworkInterfaceManager : NetworkInterfaceManagerBase, INetworkInterface
    {
        #region Public Properties

        public WlanClient WlanClient { get; }

        public WlanClient.WlanInterface ActiveInterface => this.WlanClient.Interfaces.FirstOrDefault();

        #endregion

        #region Implementation of INetworkInterface

        public string GetInterfaceName()
        {
            var description = ActiveInterface.InterfaceDescription;
            return description;
        }

        public string GetInterfaceType()
        {
            return ActiveInterface.NetworkInterface.NetworkInterfaceType.ToString();
        }

        public string GetInterfaceSpeed()
        {
            return ActiveInterface.NetworkInterface.Speed.ToString();
        }

        #endregion

        #region Constructor

        public WifiNetworkInterfaceManager()
        {

            this.WlanClient = new WlanClient();
            this.NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()
                .FirstOrDefault(ni => ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 && ni.OperationalStatus == OperationalStatus.Up);
            this.IpAddress = ActiveInterface.NetworkInterface.GetIPProperties().UnicastAddresses
                .FirstOrDefault(ua => ua.PrefixOrigin == PrefixOrigin.Dhcp)?.Address;
            this.GatewayIpAddress = NetworkInterface?.GetIPProperties().GatewayAddresses.FirstOrDefault()?.Address;
        }

        #endregion

        #region Public Methods

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

        #endregion
    }
}
