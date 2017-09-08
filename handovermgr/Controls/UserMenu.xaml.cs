using HandoverAlgorithmBase.PlainAlgorithms.NovelAlgorithm;
using RadioNetworks;

namespace handovermgr.Controls
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

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

        public UserMenu()
        {
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
        }

        #endregion
    }
}
