using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace NetworkManager
{
    public class NetworkManager
    {
        private static readonly NetworkManager _instance;

        public NetworkManager()
        {
        }

        public static NetworkManager Instance()
        {
            return _instance ?? new NetworkManager();

        }

        public List<NetworkInterface> ActiveInterfaces;
    }
}
