using System.Linq;

namespace handovermgr.Controls
{
    #region Usings

    using System;
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

        /// <summary>
        /// Initialize user menu.
        /// </summary>
        public UserMenu()
        {
            InitializeComponent();
            NovelProfileComboBox.ItemsSource = Enum.GetValues(typeof(NovelNetworkProfile)).Cast<NovelNetworkProfile>();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Interaction for add network button.
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AddNetwork();
        }

        /// <summary>
        /// Interaction for handover button click.
        /// </summary>
        private void Handover_Click(object sender, RoutedEventArgs e)
        {

            HandoverView handoverView = new HandoverView(MainWindow.NetworksList);
            handoverView.Show();

            // TODO Place holder, each subsequent execution of handover
            // will result in parameters randomization
            //Random random = new Random();

            //foreach (var network in MainWindow.NetworksList)
            //{
            //    network.Parameters.ThroughputInMbps = random.NextDouble();
            //    network.Parameters.BitErrorRate = random.NextDouble();
            //    network.Parameters.BurstErrorRate = random.NextDouble();
            //    network.Parameters.CostInUnitsPerByte = random.NextDouble();
            //    network.Parameters.DelayInMsec = random.NextDouble();
            //    network.Parameters.JitterInMsec = random.NextDouble();
            //    network.Parameters.PacketLossPercentage = random.NextDouble();
            //    network.Parameters.ResponseTimeInMsec = random.NextDouble();
            //    network.Parameters.SecurityLevel = random.NextDouble();
            //}  

            //List<RadioNetworkModel> networkList = new List<RadioNetworkModel>(MainWindow.NetworksList);
            //NovelHandoverAlgorithm novelAlgorithm = new NovelHandoverAlgorithm(networkList);

            //var resultNetwork = novelAlgorithm.SelectResultNetwork();
            //ResultNetworkTextBox.Text = resultNetwork.NetworkName;
        }

        #endregion
    }
}
