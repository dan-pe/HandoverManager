using System.Collections.Generic;
using System.Linq;
using HandoverAlgorithmBase.PlainAlgorithms.NovelAlgorithm;

namespace handovermgr
{
    #region Usings

    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;

    using FileReaders;

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

        public static void AddNetwork()
        {
            NetworksList.Add(new RadioNetworkModel
            {
                NetworkName = "mock",
                NetworkType = "anothermock"
            });
        }

        #endregion

        #region Constructors and Destructor

        public MainWindow()
        {
            
            InitializeComponent();
            BindNetworks();
            
        }

        #endregion

        #region Public Methods

        public ListBox ServeLogBox()
        {
            return LogList;
        }

        #endregion

        #region Private Methods

        private void BindNetworks()
        {
            NetworksList = new ObservableCollection<RadioNetworkModel>();

            NetworksList.Add(
                new RadioNetworkModel
                {
                    NetworkName = "network1",
                    NetworkType = NetworkType.GPRS.ToString(),
                    Parameters = new NetworkParameters() {ThroughputInMbps = 2}
                });
            NetworksList.Add(new RadioNetworkModel{NetworkName = "network2",NetworkType = NetworkType.LTE_Advanced.ToString()});

            NetworkListView.ItemsSource = NetworksList;
        }

        /// <summary>
        /// Prepares radio network objects
        /// </summary>
        private void PrepareNetworkObjects()
        {
           var networkList = NetworksList.ToList();
           var novelHandoverAlgorithm = new NovelHandoverAlgorithm(networkList);

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
        
        private Logger.Logger Logger
        {
            get { return global::Logger.Logger.GetLoggerInstance(LogList); }
        }

        #endregion

        private void NetworkListView_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NetworkPropertiesView networkPropertiesView = new NetworkPropertiesView(this);
            networkPropertiesView.Show();
        }
    }
}
