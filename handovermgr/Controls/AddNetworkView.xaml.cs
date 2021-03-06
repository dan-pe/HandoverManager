﻿using Logger;
using RadioNetworks;

namespace handovermgr.Controls
{
    using System;
    using System.Linq;
    using System.Windows;

    /// <summary>
    /// Interaction logic for AddNetworkView.xaml
    /// </summary>
    public partial class AddNetworkView : Window
    {
        /// <summary>
        /// Initializes a new instance of AddNetworkView class.
        /// </summary>
        public AddNetworkView()
        {
            InitializeComponent();
            AddTypeComboBox.ItemsSource = Enum.GetValues(typeof(RadioNetworks.NetworkType)).Cast<NetworkType>();
        }

        /// <summary>
        /// Add network button interaction.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNetworkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var network = new RadioNetworkModel()
                {
                    NetworkName = AddNameBox.Text,
                    NetworkType = AddTypeComboBox.Text,
                    Parameters = new NetworkParameters()
                    {
                        ThroughputInMbps = double.Parse(AddThroughoutputBox.Text),
                        PacketLossPercentage = double.Parse(AddPacketLossBox.Text),
                        ResponseTimeInMsec = double.Parse(AddResponseBox.Text),
                        SecurityLevel = double.Parse(AddSecurityBox.Text)
                        // TODO: Add security level based on network type
                    }
                };
                MainWindow.NetworksList.Add(network);
                Logger.Logger.AddMessage($"Successfully added network {AddNameBox.Text}",
                                         MessageThreshold.SUCCESS);
            }
            catch (Exception exception)
            {
                Logger.Logger.AddMessage("Error occurred while trying to add network: " +
                                         $"{exception.Message}",
                                         MessageThreshold.FAIL);
            }
        }
    }
}