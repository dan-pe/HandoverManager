using System.Collections.Generic;

namespace handovermgr
{
    #region Usings

    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Linq;

    using HandoverAlgorithmBase.PlainAlgorithms.NovelAlgorithm;

    using RadioNetworks;

    #endregion

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        
        public static ObservableCollection<RadioNetworkModel> NetworksList { get; set; }

        #endregion

        #region Constructors and Destructor

        public MainWindow()
        {
            InitializeComponent();
            Logger.Logger.InitializeLogger(LogBox);
            BindNetworks();
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
            Random random = new Random(100);
            NetworksList.Add(
                new RadioNetworkModel
                {
                    NetworkName = "network1",
                    NetworkType = NetworkType.GPRS.ToString(),
                    Parameters = new NetworkParameters()
                    {
                        ThroughputInMbps = random.NextDouble() * 10,
                        BitErrorRate = random.NextDouble() * 0.01d,
                        BurstErrorRate = random.NextDouble() * 0.01d,
                        CostInUnitsPerByte = random.NextDouble(),
                        DelayInMsec = random.NextDouble() * 0.1d,
                        JitterInMsec = random.NextDouble() * 0.1d,
                        PacketLossPercentage = random.NextDouble(),
                        ResponseTimeInMsec = random.NextDouble() * 0.1d,
                        SecurityLevel = 1
                    }
                });
            NetworksList.Add(
                new RadioNetworkModel
                {
                    NetworkName = "network2",
                    NetworkType = NetworkType.LTE_Advanced.ToString(),
                    Parameters = new NetworkParameters()
                    {
                        ThroughputInMbps = random.NextDouble() * 100,
                        BitErrorRate = random.NextDouble() * 0.01d,
                        BurstErrorRate = random.NextDouble() * 0.01d,
                        CostInUnitsPerByte = random.NextDouble(),
                        DelayInMsec = random.NextDouble() * 0.1d,
                        JitterInMsec = random.NextDouble() * 0.1d,
                        PacketLossPercentage = random.NextDouble(),
                        ResponseTimeInMsec = random.NextDouble() * 0.1d,
                        SecurityLevel = 4

                    }
                });
            NetworksList.Add(
                new RadioNetworkModel
                {
                    NetworkName = "network3",
                    NetworkType = NetworkType.UMTS.ToString(),
                    Parameters = new NetworkParameters()
                    {
                        ThroughputInMbps = random.NextDouble() * 10,
                        BitErrorRate = random.NextDouble() * 0.01d,
                        BurstErrorRate = random.NextDouble() * 0.01d,
                        CostInUnitsPerByte = random.NextDouble(),
                        DelayInMsec = random.NextDouble() * 0.1d,
                        JitterInMsec = random.NextDouble() * 0.1d,
                        PacketLossPercentage = random.NextDouble(),
                        ResponseTimeInMsec = random.NextDouble() * 0.1d,
                        SecurityLevel = 3

                    }
                });
            NetworksList.Add(
                new RadioNetworkModel
                {
                    NetworkName = "network4",
                    NetworkType = NetworkType.WiFi.ToString(),
                    Parameters = new NetworkParameters()
                    {
                        ThroughputInMbps = random.NextDouble() * 10,
                        BitErrorRate = random.NextDouble() * 0.01d,
                        BurstErrorRate = random.NextDouble() * 0.01d,
                        CostInUnitsPerByte = random.NextDouble(),
                        DelayInMsec = random.NextDouble() * 0.1d,
                        JitterInMsec = random.NextDouble() * 0.1d,
                        PacketLossPercentage = random.NextDouble(),
                        ResponseTimeInMsec = random.NextDouble() * 0.1d,
                        SecurityLevel = 2
                    }
                });
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
