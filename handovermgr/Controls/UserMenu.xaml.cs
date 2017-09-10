namespace handovermgr.Controls
{
    #region Usings

    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    using HandoverAlgorithmBase.PlainAlgorithms.NovelAlgorithm;
    using RadioNetworks;
    #endregion

    /// <summary>
    /// Interaction logic for UserMenu.xaml
    /// </summary>
    public partial class UserMenu : UserControl
    {
        #region Private Fields

        private readonly MainWindow _mainWindow;

        #endregion

        #region Public Methods

        public UserMenu(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AddNetwork();
        }

        private void Handover_Click(object sender, RoutedEventArgs e)
        {
            List<RadioNetworkModel> networkList = new List<RadioNetworkModel>(MainWindow.NetworksList);

            NovelHandoverAlgorithm novelAlgorithm = new NovelHandoverAlgorithm(networkList);

            var resultNetwork = novelAlgorithm.SelectResultNetwork();
        }

        #endregion
    }
}
