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
        public UserProfileView()
        {

            //int[,] mockArray = {{1,1,1,1,1}, {1,1,1,1,1}};
            //int[] mockRow = {1, 1, 1, 1, 1};
            //var costam = new NetworkParameters();

            //var registeredAttributes = GetTypesWithHelpAttribute(AppDomain.CurrentDomain.GetAssemblies());


            //UserProfile userProfile = new UserProfile("Mock Profile", mockArray);
            //Profiler.Instance.SetProfile(userProfile);


            //DataTable dataTable = new DataTable();
            //dataTable.Columns.Add();
            //dataTable.Columns.Add();
            //dataTable.Columns.Add();
            //dataTable.Columns.Add();
            //dataTable.Columns.Add();

            //foreach (var arrayElement in mockArray)
            //{
            //    dataTable.Rows.Add(arrayElement);
            //}


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