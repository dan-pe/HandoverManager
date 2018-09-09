using NetworkManager;
using ViewModels.NetworkViewModels;

namespace handovermgr.Controls.NetworkViews
{
    #region Usings

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

        private readonly WifiNetworkInterfaceManager _wifiNetworkInterfaceManager;

        #endregion

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
