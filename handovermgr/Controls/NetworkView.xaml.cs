using System.Windows;
using ViewModels;

namespace handovermgr.Controls
{
    /// <summary>
    /// Interaction logic for NetworkView.xaml
    /// </summary>
    public partial class NetworkView : Window
    {
        private NetworkManagerViewModel networkManagerViewModel;

        public NetworkView()
        {
            this.networkManagerViewModel = new NetworkManagerViewModel();
            this.DataContext = networkManagerViewModel;
            this.InitializeComponent();
        }
    }
}
