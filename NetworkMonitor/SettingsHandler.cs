using System.Collections.Generic;
using System.Net;

namespace NetworkMonitors
{
    public class SettingsHandler
    {
        #region Private Fields

        private static SettingsHandler _settingsHandler;

        private readonly List<string> _serverList;

        private int _pingCount;

        #endregion

        #region Constructors

        private SettingsHandler()
        {
            _serverList = new List<string>();
            _pingCount = 100;
            BufferSizeInBytes = 32;
            PingTimeoutInMsec = 120;

        }

        #endregion

        public List<string> ServerList => _serverList ?? new List<string>();

        public int PingTimeoutInMsec { get; set; }

        public int BufferSizeInBytes { get; set; }

        public int PingCount
        {
            get => _pingCount;

            set => value = _pingCount;
        }

        public static SettingsHandler GetInstance()
        {
            return _settingsHandler ?? (_settingsHandler = new SettingsHandler());
        }

        public List<IPAddress> DnsAddresses
        {
            get
            {
                var dnsAddresses = new List<IPAddress>()
                {
                    IPAddress.Parse("8.8.8.8"),
                    IPAddress.Parse("8.8.4.4"),
                    IPAddress.Parse("1.1.1.1"),
                    IPAddress.Parse("1.0.0.1")
                };
                return dnsAddresses;
            }
        }
    }
}
