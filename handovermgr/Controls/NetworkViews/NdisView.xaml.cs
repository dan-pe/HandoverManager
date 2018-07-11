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

        public NdisView()
        {
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
                Parameters = new NetworkMonitor(NetworkInterface.GetAllNetworkInterfaces()
                        .FirstOrDefault(ni => ni.Description.Contains("NDIS")))
                    .EvaluateNetwork()
            });

        }
    }
}
