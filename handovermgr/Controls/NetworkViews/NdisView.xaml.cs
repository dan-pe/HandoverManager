using System.Windows.Controls;
using ViewModels.NetworkViewModels;

namespace handovermgr.Controls.NetworkViews
{
    /// <summary>
    /// Interaction logic for NdisView.xaml
    /// </summary>
    public partial class NdisView : UserControl
    {
        public NdisView()
        {
            this.DataContext = new NdisViewModel();
            InitializeComponent();
        }
    }
}
