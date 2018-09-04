using System.Collections.Generic;
using System.Windows;
using NetworkManager;

namespace handovermgr.Controls
{
    /// <summary>
    /// Interaction logic for ServerListView.xaml
    /// </summary>
    public partial class ServerListView : Window
    {
        private List<string> ServerList => ServerListHandler.GetInstance().ServerList;

        public ServerListView()
        {
            InitializeComponent();
            ServerListViewBox.ItemsSource = ServerList;
        }

        private void AddServerButton_OnClick(object sender, RoutedEventArgs e)
        {
            ServerListHandler.GetInstance().ServerList.Add(ServerNameTextBox.Text);
            ServerListViewBox.Items.Refresh();
        }
    }
}
