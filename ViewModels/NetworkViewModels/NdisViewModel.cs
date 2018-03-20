using NetworkManager;

namespace ViewModels.NetworkViewModels
{
    public class NdisViewModel : NetworkBaseViewModel
    {
        private readonly NdisNetworkInterfaceManager _ndisNetworkInterfaceManager;

        public NdisViewModel()
        {
            this._ndisNetworkInterfaceManager = new NdisNetworkInterfaceManager();
            this.Name = _ndisNetworkInterfaceManager.Name;
            this.Type = _ndisNetworkInterfaceManager.Type;
        }
    }
}
