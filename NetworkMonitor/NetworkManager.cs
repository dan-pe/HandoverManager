using System.Collections.Generic;
using System.Linq;
using NativeWifi;
using NetworkMonitors.Parsers;

namespace NetworkMonitors
{
    public class NetworkManager
    {
        public WlanClient WlanClient { get;}

        public WlanClient.WlanInterface ActiveInterface
        {
            get { return this.WlanClient.Interfaces.FirstOrDefault(); }
        }

        public NetworkManager()
        {
            this.WlanClient = new WlanClient();
        }

        public List<string> AvailableNetworkSSID
        {
            get
            {
                var networkSSIDs = new List<string>();
                this.ActiveInterface.Scan();
                var debugNetworks = this.ActiveInterface.GetNetworkBssList();
                var jeden = this.ActiveInterface.GetAvailableNetworkList(Wlan.WlanGetAvailableNetworkFlags.IncludeAllAdhocProfiles);
                var dwa = this.ActiveInterface.GetAvailableNetworkList(Wlan.WlanGetAvailableNetworkFlags.IncludeAllManualHiddenProfiles
                    );

                foreach (var availableNetwork in this.ActiveInterface
                    .GetNetworkBssList())
                {
                    var costam = availableNetwork.dot11Ssid.SSID.ToString();
                    networkSSIDs.Add(SsidParser.ParseFromBytes(availableNetwork.dot11Ssid.SSID));
                }



                foreach (var availableNetwork in this.ActiveInterface
                    .GetAvailableNetworkList(Wlan.WlanGetAvailableNetworkFlags.IncludeAllManualHiddenProfiles))
                {
                    networkSSIDs.Add(SsidParser.ParseFromBytes(availableNetwork.dot11Ssid.SSID));
                }
                return networkSSIDs;
            }
        }

        public void ConnectToNewtwork()
        {
            //this.WlanClient.Interfaces.FirstOrDefault().CurrentConnection
        }
    }
}