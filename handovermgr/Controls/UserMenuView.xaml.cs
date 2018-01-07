﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using Profiler;

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

        public ObservableCollection<UserProfile> UserProfiles
        {
            //TODO: Extract to view model and implement INotify members
            set { this.NovelProfileComboBox.ItemsSource = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Initialize user menu.
        /// </summary>
        public UserMenu()
        {
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
            catch (Exception exception)
            {
                Logger.AddMessage($"Error occurred during user profile loading from: {fileName}.");
            }
        }

        #endregion
    }
}