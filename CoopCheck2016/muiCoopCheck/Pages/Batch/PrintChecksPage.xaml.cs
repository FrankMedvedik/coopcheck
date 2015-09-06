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

namespace muiCoopCheck.Pages.Batch
{
    /// <summary>
    /// Interaction logic for PrintChecksPage.xaml
    /// </summary>
    public partial class PrintChecksPage : UserControl
    {
        private PrintChecksViewModel _vm;
        public PrintChecksPage()
        {
            InitializeComponent();
            _vm = new PrintChecksViewModel();
            DataContext = _vm;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            _vm.PrintChecks();
        }
    }
}
