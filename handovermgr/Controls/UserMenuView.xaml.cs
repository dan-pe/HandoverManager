using handovermgr.Controls.NetworkViews;

namespace handovermgr.Controls
{
    using FileReaders;
    using Logger;
    using Microsoft.Win32;
    using Profiler;
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using ViewModels;

    /// <summary>
    ///     Interaction logic for UserMenu.xaml
    /// </summary>
    public partial class UserMenu
    {
        private UserMenuViewModel _userMenuViewModel;

        /// <summary>
        ///     Initialize user menu.
        /// </summary>
        public UserMenu()
        {
            this._userMenuViewModel = new UserMenuViewModel();
            this.InitializeComponent();
            this.DataContext = this;
        }

        public ObservableCollection<UserProfile> UserProfiles
        {
            //TODO: Extract to view model and implement INotify members
            set
            {
                this.NovelProfileComboBox.ItemsSource = value;
                this.NovelProfileComboBox.DisplayMemberPath = "Name";
            }
        }

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
            if (openFileDialog.FileName != string.Empty)
            {
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

        /// <summary>
        ///     On selected profile change event.
        /// </summary>
        private void OnSelectedProfileChange(object sender, SelectionChangedEventArgs e)
        {
            ProfileManager.Instance
                .SetProfile((sender as ComboBox)?.SelectedItem as string);
        }

        private void RadioNetworks_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var networkView = new RadioNetworksView();
                networkView.Show();
            }
            catch (Exception exception)
            {
                Logger.AddMessage($"Failed to initalize Cellular Networks: {exception.Message}",
                    MessageThreshold.FAIL);
            }
        }

        private void ServerList_OnCLick(object sender, RoutedEventArgs e)
        {
            var serverListView = new ServerListView();
            serverListView.Show();
        }

        private void WifiNetworks_OnClick(object sender, RoutedEventArgs e)
        {
            var wifiNetworksView = new WifiNetworksView();
            wifiNetworksView.Show();
        }
    }
}