﻿using RadioNetworks;
using System.Windows;

namespace handovermgr
{
    /// <summary>
    /// Interaction logic for NetworkPropertiesView.xaml
    /// </summary>
    public partial class NetworkPropertiesView : Window
    {
        private readonly MainWindow _mainWindow;

        public NetworkPropertiesView(MainWindow mainWindow)
        {
            InitializeComponent();
            this._mainWindow = mainWindow;
            PrepareBinding();
        }

        private void PrepareBinding()
        {
            var selectedNetwork = _mainWindow.NetworkListView.SelectedItem;

            if (selectedNetwork is RadioNetworkModel)
            {
                NetworkItemView.DataContext = selectedNetwork;
            }
        }
    }
}