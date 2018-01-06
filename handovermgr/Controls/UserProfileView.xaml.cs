namespace handovermgr.Controls
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.Win32;

    using Profiler;
    using Logger;


    #endregion

    /// <summary>
    /// Interaction logic for UserProfileView.xaml
    /// </summary>
    public partial class UserProfileView
    {

        public List<UserProfile> StoredUserProfiles { get; set; }

        public UserProfileView()
        {
            var openFileDialog = new OpenFileDialog();

            openFileDialog.ShowDialog();

            try
            {
                Profiler.Instance.LoadFromFile(openFileDialog.FileName);
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

            this.InitializeComponent();
            //this.UserProfileDataGrid.ItemsSource = dataTable.DefaultView;

        }
    }
}