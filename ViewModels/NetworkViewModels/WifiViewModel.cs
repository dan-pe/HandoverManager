using System.Collections.ObjectModel;
using NetworkManager;

namespace ViewModels.NetworkViewModels
{
    public class WifiViewModel : NetworkBaseViewModel
    {
        public WifiNetworkInterfaceManager WifiNetworkInterfaceManager;

        public ObservableCollection<string> ActiveNetworks
        {
            get
            {
                var networks = new ObservableCollection<string>(this.WifiNetworkInterfaceManager.GetAvailableNetworkSsids());
                return networks;
            }
        }

        public WifiViewModel(INetworkInterface iNetworkInterface) : base(iNetworkInterface)
        {
            this.WifiNetworkInterfaceManager = new WifiNetworkInterfaceManager();
        }
    }
}