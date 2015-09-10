using System.Windows;
using System.Windows.Controls;

namespace CoopCheck.WPF.Content.Voucher
{
    /// <summary>
    /// Interaction logic for PrintChecksPage.xaml
    /// </summary>
    public partial class PayVoucherView : UserControl
    {
        private PrintChecksViewModel _vm;
        public PayVoucherView()
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
