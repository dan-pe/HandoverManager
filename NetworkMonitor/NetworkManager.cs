
namespace NetworkMonitors
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NativeWifi;
    using Parsers;

    #endregion

    public class NetworkManager
    {
        #region Properties

        public WlanClient WlanClient { get;}

        public WlanClient.WlanInterface ActiveInterface
        {
            get { return this.WlanClient.Interfaces.FirstOrDefault(); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of NetworkManager.
        /// </summary>
        public NetworkManager()
        {
            this.WlanClient = new WlanClient();
        }

        public List<string> GetAvailableNetworkSSID()
        {
            var networkSSIDs = new List<string>();
            this.ActiveInterface.Scan();

            foreach (var availableNetwork in this.ActiveInterface
                .GetNetworkBssList())
            {
                networkSSIDs.Add(SsidParser.ParseFromBytes(availableNetwork.dot11Ssid.SSID));
            }

            return networkSSIDs;
        }

        public bool ConnectToNetwork(string profileName)
        {
            try
            {
                this.ActiveInterface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, profileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Wlan.WlanProfileInfo[] GetNetworkProfiles()
        {
            return this.ActiveInterface.GetProfiles();
        }

        #endregion
    }
}