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

        private const string FileName = ".../inputNetworks.txt";

        private readonly string _filePath = Path.Combine(
                Environment.CurrentDirectory,
                "Debug",
                FileName);

        public static ObservableCollection<RadioNetworkModel> NetworksList;

        #endregion

        #region Constructors and Destructor

        public MainWindow()
        {
            InitializeComponent();
            Logger.Logger.InitializeLogger(LogBox);
            BindNetworks();
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
                        ThroughputInMbps = random.NextDouble(), BitErrorRate = random.NextDouble(), BurstErrorRate = random.NextDouble(), CostInUnitsPerByte = random.NextDouble(), DelayInMsec = random.NextDouble(),
                        JitterInMsec = random.NextDouble(), PacketLossPercentage = random.NextDouble(), ResponseTimeInMsec = random.NextDouble(), SecurityLevel = random.NextDouble()
                    }
                });
            NetworksList.Add(
                new RadioNetworkModel
                {
                    NetworkName = "network2",
                    NetworkType = NetworkType.LTE_Advanced.ToString(),
                    Parameters = new NetworkParameters()
                    {
                        ThroughputInMbps = random.NextDouble(), BitErrorRate = random.NextDouble(), BurstErrorRate = random.NextDouble(), CostInUnitsPerByte = random.NextDouble(), DelayInMsec = random.NextDouble(),
                        JitterInMsec = random.NextDouble(), PacketLossPercentage = random.NextDouble(), ResponseTimeInMsec = random.NextDouble(), SecurityLevel = random.NextDouble()
                    }
                });
            NetworksList.Add(
                new RadioNetworkModel
                {
                    NetworkName = "network3",
                    NetworkType = NetworkType.UMTS.ToString(),
                    Parameters = new NetworkParameters()
                    {
                        ThroughputInMbps = random.NextDouble(),
                        BitErrorRate = random.NextDouble(),
                        BurstErrorRate = random.NextDouble(),
                        CostInUnitsPerByte = random.NextDouble(),
                        DelayInMsec = random.NextDouble(),
                        JitterInMsec = random.NextDouble(),
                        PacketLossPercentage = random.NextDouble(),
                        ResponseTimeInMsec = random.NextDouble(),
                        SecurityLevel = random.NextDouble()
                    }
                });
            NetworksList.Add(
                new RadioNetworkModel
                {
                    NetworkName = "network4",
                    NetworkType = NetworkType.WiFi.ToString(),
                    Parameters = new NetworkParameters()
                    {
                        ThroughputInMbps = random.NextDouble(),
                        BitErrorRate = random.NextDouble(),
                        BurstErrorRate = random.NextDouble(),
                        CostInUnitsPerByte = random.NextDouble(),
                        DelayInMsec = random.NextDouble(),
                        JitterInMsec = random.NextDouble(),
                        PacketLossPercentage = random.NextDouble(),
                        ResponseTimeInMsec = random.NextDouble(),
                        SecurityLevel = random.NextDouble()
                    }
                });
            NetworkListView.ItemsSource = NetworksList;
        }

        /// <summary>
        /// Prepares radio network objects
        /// </summary>
        private void PrepareNetworkObjects()
        {
           var networkList = NetworksList.ToList();
           var novelHandoverAlgorithm = new NovelHandoverAlgorithm(networkList);
            var resultnoNetwork = novelHandoverAlgorithm.ResultNetwork;
            //ResultNetwork.Text = novelHandoverAlgorithm.SelectResultNetwork().NetworkName;
        }

        /// <summary>
        /// Performs decision action.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DecisionButton_OnClick(object sender, RoutedEventArgs e)
        {
            PrepareNetworkObjects();
        }
       

        #endregion

        private void NetworkListView_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NetworkPropertiesView networkPropertiesView = new NetworkPropertiesView(this);
            networkPropertiesView.Show();
        }
    }
}
