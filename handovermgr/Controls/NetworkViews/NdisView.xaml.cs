using System.Linq;
using System.Net.NetworkInformation;
using NetworkManager;
using NetworkMonitors;
using RadioNetworks;
using ViewModels.NetworkViewModels;

namespace handovermgr.Controls.NetworkViews
{
    /// <summary>
    /// Interaction logic for NdisView.xaml
    /// </summary>
    public partial class NdisView
    {
        private readonly NdisViewModel _ndisViewModel;

        private readonly NdisNetworkInterfaceManager _ndisNetworkInterfaceManager;

        public NdisView()
        {
            this._ndisNetworkInterfaceManager = new NdisNetworkInterfaceManager();
            this._ndisViewModel = new NdisViewModel(new NdisNetworkInterfaceManager());
            this.DataContext = _ndisViewModel;
            this.InitializeComponent();
        }

        private void EvaluteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.NetworksList.Add(new RadioNetworkModel()
            {
                NetworkName = RDIInterfaceNameTextBox.Text,
                NetworkType = RDIInterfaceTypeTextBox.Text,
                Parameters = new NetworkMonitor(_ndisNetworkInterfaceManager)
                    .EvaluateNetwork()
            });

        }
    }
}
