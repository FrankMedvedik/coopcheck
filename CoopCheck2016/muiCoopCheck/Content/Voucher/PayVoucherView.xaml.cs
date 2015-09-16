using System.Windows;
using System.Windows.Controls;

namespace CoopCheck.WPF.Content.Voucher
{
    /// <summary>
    /// Interaction logic for PrintChecksPage.xaml
    /// </summary>
    public partial class PayVoucherView : UserControl
    {
        private PayVouchersViewModel _vm;
        public PayVoucherView()
        {
            InitializeComponent();
            _vm = new PayVouchersViewModel();
            DataContext = _vm;
        }

        private void SwiftPay(object sender, RoutedEventArgs e)
        {
            _vm.SwiftPay();

        }

        private void PrintChecks(object sender, RoutedEventArgs e)
        {
            _vm.PrintChecks();
        }
    }
 
}
