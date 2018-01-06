using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using FileReaders;
using Logger;
using Microsoft.Win32;
using RadioNetworks;

namespace handovermgr.Controls
{
    #region Usings

    using System.Data;
    using System.Windows;
    using Profiler;

    #endregion

    /// <summary>
    /// Interaction logic for UserProfileView.xaml
    /// </summary>
    public partial class UserProfileView : Window
    {

        public List<UserProfile> StoredUserProfiles { get; set; }

        public UserProfileView()
        {
            var openFileDialog = new OpenFileDialog();

            openFileDialog.ShowDialog();

            try
            {
                Profiler.Instance.LoadFromFile(openFileDialog.FileName);
                Logger.Logger.AddMessage(
                    $"Succesfully loaded {Path.GetFileName(openFileDialog.FileName)}.",
                    MessageThreshold.SUCCESS);
            }
            catch (Exception exception)
            {
                Logger.Logger.AddMessage(
                    $"Error occurred while reading from {Path.GetFileName(openFileDialog.FileName)}: {exception.Message}",
                    MessageThreshold.WARNING);
            }

            this.InitializeComponent();
            //this.UserProfileDataGrid.ItemsSource = dataTable.DefaultView;

        }

        private static List<string> GetTypesWithHelpAttribute(IEnumerable<Assembly> assemblies)
        {
            List<string> types = new List<string>();

            foreach (var assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.GetCustomAttributes(typeof(NetworkMetricAttribute), true).Length > 0)
                    {
                        types.Add(type.ToString());
                    }
                }
            }

            return types;

        }
    }
}