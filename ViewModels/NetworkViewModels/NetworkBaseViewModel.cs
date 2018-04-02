using NetworkManager;

namespace ViewModels.NetworkViewModels
{
    public abstract class NetworkBaseViewModel
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public int Speed { get; set; }

        protected INetworkInterface NetworkInterface { get;}

        protected NetworkBaseViewModel(INetworkInterface iNetworkInterface)
        {
            this.NetworkInterface = iNetworkInterface;
            this.Name = NetworkInterface.GetInterfaceName();
            this.Type = NetworkInterface.GetInterfaceType();
        }
    }
}