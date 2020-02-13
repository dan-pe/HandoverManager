using NetworkManager;
using System.Collections.ObjectModel;

namespace ViewModels.NetworkViewModels
{
    public class WifiViewModel : NetworkBaseViewModel
    {
        public WifiNetworkInterfaceManager WifiNetworkInterfaceManager;

        public WifiViewModel(INetworkInterface iNetworkInterface) : base(iNetworkInterface)
        {
            this.WifiNetworkInterfaceManager = new WifiNetworkInterfaceManager();
        }

        public ObservableCollection<string> ActiveNetworks
        {
            get
            {
                var networks = new ObservableCollection<string>(this.WifiNetworkInterfaceManager.GetAvailableNetworkSsids());
                return networks;
            }
        }
    }
}