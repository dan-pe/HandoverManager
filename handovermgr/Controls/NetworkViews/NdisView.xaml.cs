using NetworkManager;
using ViewModels.NetworkViewModels;

namespace handovermgr.Controls.NetworkViews
{
    /// <summary>
    /// Interaction logic for NdisView.xaml
    /// </summary>
    public partial class NdisView
    {
        public NdisView()
        {
            this.DataContext = new NdisViewModel(new NdisNetworkInterfaceManager());
            InitializeComponent();
        }
    }
}
