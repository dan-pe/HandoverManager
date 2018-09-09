namespace handovermgr
{
    #region Usings

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
