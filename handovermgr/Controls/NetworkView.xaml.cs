using NetworkMonitors;

namespace handovermgr.Controls
{
    #region Usings

    using System;
    using System.Windows;
    using System.Windows.Input;
    using Logger;
    using NetworkManager;
    using RadioNetworks;
    using ViewModels;

    #endregion

    /// <summary>
    /// Interaction logic for NetworkView.xaml
    /// </summary>
    public partial class NetworkView : Window
    {
        #region Private Fields

        private readonly NetworkManagerViewModel _networkManagerViewModel;

        #endregion

        #region Constructors

        public NetworkView()
        {
            this._networkManagerViewModel = new NetworkManagerViewModel();
            this.DataContext = _networkManagerViewModel;
            this.InitializeComponent();
            this.BindInterfaceInfoToView();
        }

        #endregion

        #region Private Methods

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

            Logger.AddMessage($"Trying to connect to: {choosenNetworkName}");

            try
            {
                _networkManagerViewModel.NetworkManagerObsolete.ConnectToNetwork(choosenNetworkName);
                Logger.AddMessage($"Connected successfully to: {choosenNetworkName}",
                    MessageThreshold.SUCCESS);
            }
            catch (Exception exception)
            {
                Logger.AddMessage($"Error occurred during connection attempt: {exception.Message}",
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

        #endregion
    }
}