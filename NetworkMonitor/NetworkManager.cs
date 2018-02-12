namespace NetworkMonitors
{
    #region Usings

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

        #endregion

        #region Public Methods

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

        public void ConnectToNetwork(string profileName)
        {
            this.ActiveInterface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, profileName);
        }

        public Wlan.WlanProfileInfo[] GetNetworkProfiles()
        {
            return this.ActiveInterface.GetProfiles();
        }

        #endregion
    }
}