using System;
using System.Windows;
using System.Windows.Input;
using Logger;
using RadioNetworks;
using ViewModels;

namespace handovermgr.Controls
{
    /// <summary>
    /// Interaction logic for NetworkView.xaml
    /// </summary>
    public partial class NetworkView : Window
    {
        private readonly NetworkManagerViewModel _networkManagerViewModel;

        public NetworkView()
        {
            this._networkManagerViewModel = new NetworkManagerViewModel();
            this.DataContext = _networkManagerViewModel;
            this.InitializeComponent();
            this.BindInterfaceInfoToView();
        }

        private void BindInterfaceInfoToView()
        {
            var wlanInterface = this._networkManagerViewModel
                                        .ActiveWlanInterface
                                        .NetworkInterface;

            InterfaceNameTextBox.Text = wlanInterface.Description;
            InterfaceTypeTextBox.Text = wlanInterface.NetworkInterfaceType.ToString();
            InterfaceSpeedTextBox.Text = $"{wlanInterface.Speed.ToString()} bps";
        }

        private void NetworksViewList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var choosenNetworkName = NetworksViewList.SelectedItem.ToString();

            Logger.Logger.AddMessage($"Trying to connect to: {choosenNetworkName }");

            try
            {
                _networkManagerViewModel._networkManager.ConnectToNetwork(choosenNetworkName);
                Logger.Logger.AddMessage($"Connected successfully to: {choosenNetworkName}", 
                                                                    MessageThreshold.SUCCESS);
            }
            catch (Exception exception)
            {
                Logger.Logger.AddMessage($"Error occurred during connection attempt: {exception.Message}",
                    MessageThreshold.FAIL);
            }

            // Mock of adding evaluated netwok to Main View.

            var network = new RadioNetworkModel()
            {
                NetworkName = AddNameBox.Text,
                NetworkType = AddTypeComboBox.Text,
                Parameters = new NetworkParameters()
                {
                    ThroughputInMbps = double.Parse(AddThroughoutputBox.Text),
                    PacketLossPercentage = double.Parse(AddPacketLossBox.Text),
                    DelayInMsec = double.Parse(AddDelayBox.Text),
                    ResponseTimeInMsec = double.Parse(AddResponseBox.Text)
                    // TODO: Add security level based on network type
                }
            };
            MainWindow.NetworksList.Add(network);
        }
    }
}
