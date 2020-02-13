using NetworkManager;

namespace ViewModels.NetworkViewModels
{
    public abstract class NetworkBaseViewModel
    {
        protected NetworkBaseViewModel(INetworkInterface iNetworkInterface)
        {
            this.NetworkInterface = iNetworkInterface;
            this.Name = NetworkInterface.GetInterfaceName();
            this.Type = NetworkInterface.GetInterfaceType();
        }

        public string Name { get; set; }

        public int Speed { get; set; }
        public string Type { get; set; }
        protected INetworkInterface NetworkInterface { get; }
    }
}