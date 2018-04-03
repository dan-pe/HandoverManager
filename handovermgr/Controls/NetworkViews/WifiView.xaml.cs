using NetworkManager;
using ViewModels.NetworkViewModels;

namespace handovermgr.Controls.NetworkViews
{
    #region Usings

    using ViewModels;
    using System;
    using System.Windows.Input;
    using Logger;
    using NetworkMonitors;
    using RadioNetworks;

    #endregion

    /// <summary>
    /// Interaction logic for WifiView.xaml
    /// </summary>
    public partial class WifiView
    {
        #region Private Fields

        private readonly WifiViewModel _wifiViewModel;

        #endregion

        public WifiView()
        {
            this._wifiViewModel = new WifiViewModel(new WifiNetworkInterfaceManager());
            this.DataContext = _wifiViewModel;
            InitializeComponent();

            // Debugging network monitor
            var monitor = new BandwidthMonitor();
            monitor.NewBandwidthMonitor();


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

            //// Mock of adding evaluated network to Main View.
            //MainWindow.NetworksList.Add(new RadioNetworkModel()
            //{
            //    NetworkName = choosenNetworkName,
            //    NetworkType = _wifiViewModel.NetworkInterface.NetworkInterfaceType.ToString(),
            //    Parameters = new NetworkMonitorBase().EvaluateNetwork()
            //});
        }
    }
}
