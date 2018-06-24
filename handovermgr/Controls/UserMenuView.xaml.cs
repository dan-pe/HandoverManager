using handovermgr.Controls.NetworkViews;
using RadioNetworks;
using ViewModels.NetworkViewModels;

namespace handovermgr.Controls
{
    #region Usings

    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Collections.ObjectModel;

    using Microsoft.Win32;
    using ViewModels;
    using FileReaders;
    using Logger;
    using Profiler;

    #endregion

    /// <summary>
    ///     Interaction logic for UserMenu.xaml
    /// </summary>
    public partial class UserMenu
    {
        #region Fields

        private UserMenuViewModel _userMenuViewModel;

        #endregion

        #region Properties

        public ObservableCollection<UserProfile> UserProfiles
        {
            //TODO: Extract to view model and implement INotify members
            set
            {
                this.NovelProfileComboBox.ItemsSource = value;
                this.NovelProfileComboBox.DisplayMemberPath = "Name";
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Initialize user menu.
        /// </summary>
        public UserMenu()
        {
            this._userMenuViewModel = new UserMenuViewModel();
            this.InitializeComponent();
            this.DataContext = this;
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

        /// <summary>
        ///     Interaction for load network parameters button click.
        /// </summary>
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
                    $"Successfully loaded {Path.GetFileName(openFileDialog.FileName)}.",
                    MessageThreshold.SUCCESS);
            }
            catch (Exception exception)
            {
                Logger.AddMessage(
                    $"Error occurred while reading from {Path.GetFileName(openFileDialog.FileName)}: {exception.Message}",
                    MessageThreshold.WARNING);
            }
        }


        /// <summary>
        ///     Interaction for manage user profiles button click.
        /// </summary>
        private void ManageUserProfile_OnCLick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();

            var fileName = openFileDialog.FileName;

            try
            {

                this.UserProfiles = new ObservableCollection<UserProfile>(
                    ProfileManager.Instance.LoadFromFile(openFileDialog.FileName));  
                Logger.AddMessage($"Successfully loaded user profiles from: {fileName}");
            }
            catch (Exception)
            {
                Logger.AddMessage($"Error occurred during user profile loading from: {fileName}.");
            }
        }

        #endregion

        /// <summary>
        ///     On selected profile change event.
        /// </summary>
        private void OnSelectedProfileChange(object sender, SelectionChangedEventArgs e)
        {
            ProfileManager.Instance
                .SetProfile((sender as ComboBox)?.SelectedItem as string);
        }

        private void WifiNetworks_OnClick(object sender, RoutedEventArgs e)
        {
            var wifiNetworksView = new WifiNetworksView();
            wifiNetworksView.Show();
        }

        private void RadioNetworks_OnClick(object sender, RoutedEventArgs e)
        {
            var networkView = new RadioNetworksView();
            networkView.Show();
        }
    }
}