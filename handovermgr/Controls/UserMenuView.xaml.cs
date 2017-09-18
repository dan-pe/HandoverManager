
using System.IO;
using Logger;

namespace handovermgr.Controls
{
    #region Usings

    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    using FileReaders;

    using HandoverAlgorithmBase.PlainAlgorithms.NovelAlgorithm;

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
        private void AddNetwork_Click(object sender, RoutedEventArgs e)
        {
            AddNetworkView addNetworkView = new AddNetworkView();
            addNetworkView.Show();
        }

        /// <summary>
        /// Interaction for handover button click.
        /// </summary>
        private void Handover_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HandoverView handoverView = new HandoverView(MainWindow.NetworksList, NovelProfileComboBox);
                handoverView.Show();
            }
            catch (Exception exception)
            {
                Logger.Logger.AddMessage(
                    $"Error occurred during handover {exception.Message}.",
                    MessageThreshold.FAIL);
            }
         

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

        private void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            //dlg.DefaultExt = ".txt";
            //dlg.Filter = "TXT Files (*.txt)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            try
            {
                var csvNetworksCollection = CsvReader.ReadCsvFile(dlg.FileName);

                MainWindow.NetworksList.Clear();
                foreach (var csvNetwork in csvNetworksCollection)
                {
                    MainWindow.NetworksList.Add(csvNetwork);
                }

                Logger.Logger.AddMessage(
                    string.Format("Succesfully loaded {0}.",
                    Path.GetFileName(dlg.FileName)),
                    MessageThreshold.SUCCESS);
            }
            catch (Exception exception)
            {
               Logger.Logger.AddMessage(
                   string.Format("Error occurred while reading from {0}",
                   Path.GetFileName(dlg.FileName)),
                   MessageThreshold.WARNING);
            }
        }
    }
}
