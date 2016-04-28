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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CoopCheck.WPF.Services;

namespace CoopCheck.WPF.Content.Settings
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : UserControl
    {
        private AboutViewModel _vm;
        public About()
        {
            InitializeComponent();
            _vm = new AboutViewModel();
            DataContext = _vm;

        }
        
    }
}
