using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Pages.Batch;

namespace CoopCheck.WPF.Pages.Voucher
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
