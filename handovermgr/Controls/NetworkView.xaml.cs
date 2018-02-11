using System.Windows;
using ViewModels;

namespace handovermgr.Controls
{
    /// <summary>
    /// Interaction logic for NetworkView.xaml
    /// </summary>
    public partial class NetworkView : Window
    {
        private NetworkManagerViewModel networkManagerViewModel;

        public NetworkView()
        {
            this.networkManagerViewModel = new NetworkManagerViewModel();
            this.DataContext = networkManagerViewModel;
            this.InitializeComponent();

            //foreach (var activeInterface in activeInterfaces)
            //{
            //    activeInterface.Scan();
            //    string connectionName = "DanNet5";

            //    var activeProfile = activeInterface.GetProfiles( ).FirstOrDefault(p => p.profileName == connectionName);

            //    activeInterface.Connect(Wlan.WlanConnectionMode.Profile,Wlan.Dot11BssType.Any, activeProfile.profileName);

            //    foreach (var network in activeInterface.GetAvailableNetworkList(Wlan.WlanGetAvailableNetworkFlags.IncludeAllAdhocProfiles))
            //    {
            //        if (network.networkConnectable)
            //        {
            //            Logger.Logger.AddMessage($"Found connectable: {network.profileName.ToString()} network", MessageThreshold.SUCCESS);
            //        }

            //    }

            //}
            //const string inputAddress = "192.168.1.1";
            //IPAddress address = IPAddress.Parse(inputAddress);

            //this.InitializeComponent();

            //NetworkMonitorBase interfaceSpecification = new NetworkMonitorBase(address);

            //var selected = interfaceSpecification.GetSelectedInterface();

            //this.InterfaceNameTextBox.Text = selected.Description;
            //this.InterfaceTypeTextBox.Text = selected.NetworkInterfaceType.ToString();
            //this.InterfaceSpeedTextBox.Text = selected.Speed.ToString();
        }



    }
}
