using NetworkManager;
using ViewModels.NetworkViewModels;

namespace handovermgr.Controls.NetworkViews
{
    using Logger;
    using NetworkMonitors;
    using RadioNetworks;
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for WifiView.xaml
    /// </summary>
    public partial class WifiView
    {
        private readonly WifiNetworkInterfaceManager _wifiNetworkInterfaceManager;
        private readonly WifiViewModel _wifiViewModel;

        public WifiView()
        {
            this._wifiNetworkInterfaceManager = new WifiNetworkInterfaceManager();
            this._wifiViewModel = new WifiViewModel(_wifiNetworkInterfaceManager);
            this.DataContext = _wifiViewModel;
            InitializeComponent();
        }

        private void NetworksListItem_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var choosenNetworkName = NetworksViewList.SelectedItem.ToString();

            Logger.AddMessage($"Trying to connect to: {choosenNetworkName}");

            try
            {
                _wifiViewModel.WifiNetworkInterfaceManager.ConnectToNetwork(choosenNetworkName);
                Logger.AddMessage($"Connected successfully to: {choosenNetworkName}",
                    MessageThreshold.SUCCESS);
            }
            catch (Exception exception)
            {
                Logger.AddMessage($"Error occurred during connection attempt: {exception.Message}",
                    MessageThreshold.FAIL);
            }

            MainWindow.NetworksList.Add(new RadioNetworkModel()
            {
                NetworkName = choosenNetworkName,
                NetworkType = _wifiViewModel.WifiNetworkInterfaceManager.GetInterfaceType().ToString(),
                Parameters = new NetworkMonitor(this._wifiNetworkInterfaceManager)
                .EvaluateNetwork()
            });
        }
    }
}