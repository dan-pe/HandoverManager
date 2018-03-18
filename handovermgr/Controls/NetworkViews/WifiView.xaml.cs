using System;
using System.Windows.Input;
using Logger;
using NetworkMonitors;
using RadioNetworks;

namespace handovermgr.Controls.NetworkViews
{
    #region Usings

    using System.Windows.Controls;
    using ViewModels;

    #endregion

    /// <summary>
    /// Interaction logic for WifiView.xaml
    /// </summary>
    public partial class WifiView
    {
        #region Private Fields

        private readonly NetworkManagerViewModel _networkManagerViewModel;

        #endregion

        public WifiView()
        {
            this._networkManagerViewModel = new NetworkManagerViewModel();
            this.DataContext = _networkManagerViewModel;
            InitializeComponent();
            this.BindInterfaceInfoToView();
        }

        private void BindInterfaceInfoToView()
        {
            var wlanInterface = this._networkManagerViewModel
                .ActiveWlanInterface
                .NetworkInterface;

            InterfaceNameTextBox.Text = wlanInterface.Description;
            InterfaceTypeTextBox.Text = wlanInterface.NetworkInterfaceType.ToString();
            InterfaceSpeedTextBox.Text = $"{wlanInterface.Speed} bps";
        }

        private void NetworksViewList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var choosenNetworkName = NetworksViewList.SelectedItem.ToString();

            Logger.Logger.AddMessage($"Trying to connect to: {choosenNetworkName}");

            try
            {
                _networkManagerViewModel.NetworkManagerObsolete.ConnectToNetwork(choosenNetworkName);
                Logger.Logger.AddMessage($"Connected successfully to: {choosenNetworkName}",
                    MessageThreshold.SUCCESS);
            }
            catch (Exception exception)
            {
                Logger.Logger.AddMessage($"Error occurred during connection attempt: {exception.Message}",
                    MessageThreshold.FAIL);
            }


            // Mock of adding evaluated network to Main View.
            MainWindow.NetworksList.Add(new RadioNetworkModel()
            {
                NetworkName = choosenNetworkName,
                NetworkType = _networkManagerViewModel.ActiveWlanInterface.NetworkInterface.NetworkInterfaceType.ToString(),
                Parameters = new NetworkMonitorBase().EvaluateNetwork()
            });
        }
    }
}
