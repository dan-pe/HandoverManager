using System.Collections.Generic;
using System.Windows;
using NetworkManager;
using NetworkMonitors;

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


            var urls = new List<string>()
            {
                "http://hdqwalls.com/wallpapers/bthumb/small-memory-lp.jpg",
                "http://images.pexels.com/photos/853199/pexels-photo-853199.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940",
                "http://s1.picswalls.com/wallpapers/2016/06/10/4k-background_065217183_309.jpg",
                "http://s1.picswalls.com/wallpapers/2016/06/10/4k-backgrounds_065217465_309.jpg"
            };


            foreach (var url in urls)
            {
                if (!ServerList.Contains(url))
                {
                    ServerListHandler.GetInstance().ServerList.Add(url);
                }
            }

            ServerListViewBox.ItemsSource = ServerList;
        }

        private void AddServerButton_OnClick(object sender, RoutedEventArgs e)
        {
            ServerListHandler.GetInstance().ServerList.Add(ServerNameTextBox.Text);
            ServerListViewBox.Items.Refresh();
        }
    }
}
