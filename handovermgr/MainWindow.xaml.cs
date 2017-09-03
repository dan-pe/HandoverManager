using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using HandoverAlgorithmBase.PlainAlgorithms.NovelAlgorithm;
using Logger;
using RadioNetworks;

namespace handovermgr
{
    #region Usings

    using System.Collections.Generic;
    using System.Windows;

    using FileReaders;

    using HandoverAlgorithmBase;

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

        private Logger.Logger Logger
        {
            get { return global::Logger.Logger.GetLoggerInstance(LogList); }
        }

        private static ObservableCollection<RadioNetworkModel> _networksList;

        public static void AddNetwork()
        {
            _networksList.Add(new RadioNetworkModel
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

        private void BindNetworks()
        {
            _networksList = new ObservableCollection<RadioNetworkModel>();

            networkListView.ItemsSource = _networksList;

            _networksList.Add(new RadioNetworkModel { NetworkName = "network1", NetworkType = NetworkType.GPRS.ToString() });
            _networksList.Add(
                new RadioNetworkModel
                {
                    NetworkName = "network2",
                    NetworkType = NetworkType.LTE_Advanced.ToString()
                });
        }

        #endregion

        #region Public Methods

        public ListBox ServeLogBox()
        {
            return LogList;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Prepares radio network objects
        /// </summary>
        private void PrepareNetworkObjects()
        {
            //var networkList = new List<RadioNetworkModel>();
            //var novelHandoverAlgorithm = new NovelHandoverAlgorithm(networkList);

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

        /// <summary>
        /// Open/Close user weights windows.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputWeightsAccept_Click(object sender, RoutedEventArgs e)
        {
            var isOpen = UserPopup.IsOpen;
            UserPopup.IsOpen = !isOpen;

            global::Logger.Logger.GetLoggerInstance(LogList).AddMessage("costam");

            CsvReader.ReadCsvFile(_filePath);

        }

        private void PrepareFuzzyRegulesSet()
        {
            //FuzzyReguleSet Mamdani = new FuzzyRegulesSet(
            //{
            //    FuzzyValue reg = new FuzzyValue();
            //})
        }

        #endregion
    }
}
