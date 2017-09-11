using System;

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
            Random random = new Random();

            foreach (var network in MainWindow.NetworksList)
            {


                network.Parameters.ThroughputInMbps = random.NextDouble();
                network.Parameters.BitErrorRate = random.NextDouble();
                network.Parameters.BurstErrorRate = random.NextDouble();
                network.Parameters.CostInUnitsPerByte = random.NextDouble();
                network.Parameters.DelayInMsec = random.NextDouble();
                network.Parameters.JitterInMsec = random.NextDouble();
                network.Parameters.PacketLossPercentage = random.NextDouble();
                network.Parameters.ResponseTimeInMsec = random.NextDouble();
                network.Parameters.SecurityLevel = random.NextDouble();
            }  
            

            List<RadioNetworkModel> networkList = new List<RadioNetworkModel>(MainWindow.NetworksList);

           


            NovelHandoverAlgorithm novelAlgorithm = new NovelHandoverAlgorithm(networkList);

            var resultNetwork = novelAlgorithm.SelectResultNetwork();
        }

        #endregion
    }
}
