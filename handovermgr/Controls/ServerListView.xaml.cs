using Logger;
using NetworkMonitors;
using System;
using System.Collections.Generic;
using System.Windows;

namespace handovermgr.Controls
{
    /// <summary>
    /// Interaction logic for ServerListView.xaml
    /// </summary>
    public partial class ServerListView : Window
    {
        public ServerListView()
        {
            InitializeComponent();
            var settingsHandler = SettingsHandler.GetInstance();
            ServerListViewBox.ItemsSource = ServerList;
            PingCountTextBox.Text = settingsHandler.PingCount.ToString();
            PingTimeoutTextBox.Text = settingsHandler.PingTimeoutInMsec.ToString();
            BufferSizeTextBox.Text = settingsHandler.BufferSizeInBytes.ToString();
        }

        private List<string> ServerList => SettingsHandler.GetInstance().ServerList;

        private void AddServerButton_OnClick(object sender, RoutedEventArgs e)
        {
            SettingsHandler.GetInstance().ServerList.Add(ServerNameTextBox.Text);
            ServerListViewBox.Items.Refresh();
        }

        private void SavePingSettings_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(PingTimeoutTextBox.Text, out var timeout))
            {
                SettingsHandler.GetInstance().PingTimeoutInMsec = timeout;
            }
            else
            {
                Logger.Logger.AddMessage($"Error parsing ping timeout {PingTimeoutTextBox.Text}", MessageThreshold.FAIL);
            }

            if (Int32.TryParse(BufferSizeTextBox.Text, out var bufferSize))
            {
                SettingsHandler.GetInstance().BufferSizeInBytes = bufferSize;
            }
            else
            {
                Logger.Logger.AddMessage($"Error parsing buffer size {BufferSizeTextBox.Text}", MessageThreshold.FAIL);
            }

            if (Int32.TryParse(PingCountTextBox.Text, out var pingCount))
            {
                SettingsHandler.GetInstance().PingCount = pingCount;
            }
            else
            {
                Logger.Logger.AddMessage($"Error parsing ping count {PingCountTextBox.Text}", MessageThreshold.FAIL);
            }

            this.Close();
        }
    }
}