using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace handovermgr
{
    #region Usings

    using System.Collections.Generic;
    using System.Windows;

    using FileReaders;

    using HandoverAlgorithmBase;

    using RadioNetwork;

    #endregion

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        private static string fileName = ".../inputNetworks.txt";

        private readonly string _filePath = Path.Combine(
                Environment.CurrentDirectory,
                "Debug",
                fileName);


        #endregion

        #region Constructors and Destructor

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Prepares radio network objects
        /// </summary>
        private void PrepareNetworkObjects()
        {
            var networkList = new List<RadioNetworkModel>();

            #region Newtwork Objects

            var network1 = new RadioNetworkModel(
                Network1.Name,
                NetworkType.LTE,
                new NetworkParameters()
                {
                    ThroughputInMbps = float.Parse(N1Throu.Text),
                    BitErrorRate = float.Parse(N1Ber.Text),
                    BurstErrorRate = float.Parse(N1Burst.Text),
                    PacketLossPercentage = float.Parse(N1PacketLoss.Text),
                    DelayInMsec = float.Parse(N1Delay.Text),
                    ResponseTimeInMsec = float.Parse(N1Response.Text),
                    JitterInMsec = float.Parse(N1Jitter.Text),
                    SecurityLevel = float.Parse(N1Security.Text),
                    CostInUnitsPerByte = float.Parse(N1SCost.Text)

                });
            var network2 = new RadioNetworkModel(
               Network2.Name,
               NetworkType.LTE,
               new NetworkParameters()
               {
                   ThroughputInMbps = float.Parse(N2Throu.Text),
                   BitErrorRate = float.Parse(N2Ber.Text),
                   BurstErrorRate = float.Parse(N2Burst.Text),
                   PacketLossPercentage = float.Parse(N2PacketLoss.Text),
                   DelayInMsec = float.Parse(N2Delay.Text),
                   ResponseTimeInMsec = float.Parse(N2Response.Text),
                   JitterInMsec = float.Parse(N2Jitter.Text),
                   SecurityLevel = float.Parse(N2Security.Text),
                   CostInUnitsPerByte = float.Parse(N2SCost.Text)

               });

            var network3 = new RadioNetworkModel(
               Network3.Name,
               NetworkType.LTE,
               new NetworkParameters()
               {
                   ThroughputInMbps = float.Parse(N3Throu.Text),
                   BitErrorRate = float.Parse(N3Ber.Text),
                   BurstErrorRate = float.Parse(N3Burst.Text),
                   PacketLossPercentage = float.Parse(N3PacketLoss.Text),
                   DelayInMsec = float.Parse(N3Delay.Text),
                   ResponseTimeInMsec = float.Parse(N3Response.Text),
                   JitterInMsec = float.Parse(N3Jitter.Text),
                   SecurityLevel = float.Parse(N3Security.Text),
                   CostInUnitsPerByte = float.Parse(N3SCost.Text)

               });
            var network4 = new RadioNetworkModel(
               Network4.Name,
               NetworkType.LTE,
               new NetworkParameters()
               {
                   ThroughputInMbps = float.Parse(N4Throu.Text),
                   BitErrorRate = float.Parse(N4Ber.Text),
                   BurstErrorRate = float.Parse(N4Burst.Text),
                   PacketLossPercentage = float.Parse(N4PacketLoss.Text),
                   DelayInMsec = float.Parse(N4Delay.Text),
                   ResponseTimeInMsec = float.Parse(N4Response.Text),
                   JitterInMsec = float.Parse(N4Jitter.Text),
                   SecurityLevel = float.Parse(N4Security.Text),
                   CostInUnitsPerByte = float.Parse(N4SCost.Text)

               });

            #endregion

            networkList.Add(network1);
            networkList.Add(network2);
            networkList.Add(network3);
            networkList.Add(network4);

            var novelAlgo = new NovelHanoverAlgorithm(networkList);
            ResultNetwork.Text = novelAlgo.SelectResultNetwork();
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
