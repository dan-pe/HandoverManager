
using NetworkManager;

namespace handovermgr.Controls
{
    #region Usings

    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Net.NetworkInformation;
    using System.Linq;

    using Logger;
    using RadioNetworks;
    using ViewModels;
    using NetworkMonitors;

    #endregion

    /// <summary>
    /// Interaction logic for NetworkView.xaml
    /// </summary>
    public partial class NetworkView : Window
    {
        

        #region Constructors

        public NetworkView()
        {

            this.InitializeComponent();


            var interfaces = NetworkInterface.GetAllNetworkInterfaces();

            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces()
                .Where(i => i.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                i.Description.Contains("Remote NDIS based Internet Sharing Device")
                && i.Speed != -1).ToList();

            var costam = new NdisNetworkInterfaceManager();

        }

        #endregion

        #region Private Methods

        

        

        #endregion
    }
}