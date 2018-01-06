namespace handovermgr.Controls
{
    #region Usings

    using System;
    using System.IO;
    using System.Windows;
    using FileReaders;
    using Logger;
    using Microsoft.Win32;

    #endregion

    /// <summary>
    ///     Interaction logic for UserMenu.xaml
    /// </summary>
    public partial class UserMenu
    {
        #region Properties

        

        #endregion

        #region Public Methods

        /// <summary>
        ///     Initialize user menu.
        /// </summary>
        public UserMenu()
        {
            this.InitializeComponent();
            this.NovelProfileComboBox.ItemsSource =
                Profiler.Profiler.Instance.LoadFromFile("C:\\Repositories\\handovermanager\\userProfiles.txt");

            //this.NovelProfileComboBox.ItemsSource =
            //    Enum.GetValues(typeof(NovelNetworkProfile)).Cast<NovelNetworkProfile>();
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Interaction for add network button.
        /// </summary>
        private void AddNetwork_Click(object sender, RoutedEventArgs e)
        {
            var addNetworkView = new AddNetworkView();
            addNetworkView.Show();
        }

        /// <summary>
        ///     Interaction for handover button click.
        /// </summary>
        private void Handover_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var handoverView = new HandoverView(MainWindow.NetworksList, this.NovelProfileComboBox);
                handoverView.Show();
            }
            catch (Exception exception)
            {
                Logger.AddMessage(
                    $"Error occurred during handover {exception.Message}.",
                    MessageThreshold.FAIL);
            }
        }

        private void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();

            try
            {
                var csvNetworksCollection = CsvReader.ReadCsvFile(openFileDialog.FileName);

                MainWindow.NetworksList.Clear();
                foreach (var csvNetwork in csvNetworksCollection)
                    MainWindow.NetworksList.Add(csvNetwork);

                Logger.AddMessage(
                    $"Succesfully loaded {Path.GetFileName(openFileDialog.FileName)}.",
                    MessageThreshold.SUCCESS);
            }
            catch (Exception exception)
            {
                Logger.AddMessage(
                    $"Error occurred while reading from {Path.GetFileName(openFileDialog.FileName)}: {exception.Message}",
                    MessageThreshold.WARNING);
            }
        }

        private void ManageUserProfile_OnCLick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();


            try
            {
                
            }
            catch (Exception exe)
            {
                Logger.AddMessage($"Error occurred during user profile loading from: {openFileDialog.FileName}.");
            }
        }

        #endregion
    }
}