using NetworkMonitors;

namespace handovermgr
{
    #region Usings

    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Collections.Generic;

    using Logger;

    using RadioNetworks;

    #endregion

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Properties
        
        public static ObservableCollection<RadioNetworkModel> NetworksList { get; set; }

        #endregion

        #region Constructors and Destructor

        public MainWindow()
        {
            this.InitializeComponent();
            Logger.InitializeLogger(LogBox);
            this.BindNetworks();

            // Add servers to list

            var urls = new List<string>()
            {
                ////"ftp://speedtest.tele2.net/500MB.zip",
                ////"https://cdimage.debian.org/debian-cd/current/arm64/iso-cd/debian-9.5.0-arm64-xfce-CD-1.iso",
                //"ftp://speedtest.tele2.net/200MB.zip",
                ////"https://download.fedoraproject.org/pub/fedora/linux/releases/28/Workstation/x86_64/iso/Fedora-Workstation-netinst-x86_64-28-1.1.iso",
                ////"http://cdimage.kali.org/kali-2018.3/kali-linux-light-2018.3-armhf.img.xz"
            };

            var ServerList = SettingsHandler.GetInstance().ServerList;

            foreach (var url in urls)
            {
                if (!ServerList.Contains(url))
                {
                    SettingsHandler.GetInstance().ServerList.Add(url);
                }
            }
        }

        #endregion

        #region Public Methods

        public void SetNetworkList(List<RadioNetworkModel> radioNetworksList)
        {
            NetworksList = new ObservableCollection<RadioNetworkModel>(radioNetworksList);
        }

        #endregion

        #region Private Methods

        private void BindNetworks()
        {
            NetworksList = new ObservableCollection<RadioNetworkModel>();
            NetworkListView.ItemsSource = NetworksList;
        }

        private void NetworkListView_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NetworkPropertiesView networkPropertiesView = new NetworkPropertiesView(this);
            networkPropertiesView.Show();
        }

        #endregion
    }
}
