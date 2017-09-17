using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace handovermgr.Controls
{
    /// <summary>
    /// Interaction logic for HandoverView.xaml
    /// </summary>
    public partial class HandoverView : Window
    {
        public HandoverView()
        {
            var int2DList = new int[4]
            {
                1,2,3,4
            };
            InitializeComponent();
            DataGrid2.DataContext = int2DList;
        }


    }
}
