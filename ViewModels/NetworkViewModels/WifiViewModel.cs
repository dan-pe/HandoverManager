using System.Collections.Generic;

namespace ViewModels.NetworkViewModels
{
    public class WifiViewModel : NetworkBaseViewModel
    {
        public List<string> NetworkSsidsList { get; set; }

        public WifiViewModel()
        {
            this.NetworkSsidsList = new WifiNetworkInterfaceManager();
        }
    }
}