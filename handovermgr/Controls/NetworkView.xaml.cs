using System.Net;
using System.Windows;
using NetworkMonitors;

namespace handovermgr.Controls
{
    /// <summary>
    /// Interaction logic for NetworkView.xaml
    /// </summary>
    public partial class NetworkView : Window
    {
        public NetworkView()
        {
            const string inputAddress = "192.168.1.1";
            IPAddress address = IPAddress.Parse(inputAddress);

            this.InitializeComponent();

            NetworkMonitorBase interfaceSpecification = new NetworkMonitorBase(address);

            var selected = interfaceSpecification.GetSelectedInterface();

            this.InterfaceNameTextBox.Text = selected.Description;
            this.InterfaceTypeTextBox.Text = selected.NetworkInterfaceType.ToString();
            this.InterfaceSpeedTextBox.Text = selected.Speed.ToString();
        }
    }
}
