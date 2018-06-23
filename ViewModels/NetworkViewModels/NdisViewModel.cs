using NetworkManager;

namespace ViewModels.NetworkViewModels
{
    public class NdisViewModel : NetworkBaseViewModel
    {
        public NdisViewModel(INetworkInterface iNetworkInterface) : base(iNetworkInterface)
        {
        }
    }
}
