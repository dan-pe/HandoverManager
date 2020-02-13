using System.Collections.ObjectModel;
using System.Windows;
using System.Collections.Generic;

using RadioNetworks;

namespace handovermgr
{
    public partial class MainWindow : Window
    {
        public static ObservableCollection<RadioNetworkModel> NetworksList { get; set; }
        

        public MainWindow()
        {
            this.InitializeComponent();
            Logger.Logger.InitializeLogger(LogBox);
            this.BindNetworks();
        }

        public void SetNetworkList(List<RadioNetworkModel> radioNetworksList)
        {
            NetworksList = new ObservableCollection<RadioNetworkModel>(radioNetworksList);
        }

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
    }
}
