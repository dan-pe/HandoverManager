
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
        }

        #endregion

        private void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            //dlg.DefaultExt = ".txt";
            //dlg.Filter = "TXT Files (*.txt)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();

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
