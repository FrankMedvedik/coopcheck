using System.IO;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.Library;
using CoopCheck.WPF.Content.Voucher;
using FirstFloor.ModernUI.Windows.Controls;

namespace CoopCheck.WPF.Content.Utilities.Unpay
{
    /// <summary>
    /// Interaction logic for PrintChecksPage.xaml
    /// </summary>
    public partial class UnpayVoucherView : UserControl
    {
        private UnpayVouchersViewModel _vm;
        public UnpayVoucherView()
        {
            InitializeComponent();
            _vm = new UnpayVouchersViewModel();
            DataContext = _vm;
            //Messenger.Default.Register<NotificationMessage<StatusInfo>>(this, message =>
            //{
            //    Status = message.Content;
            //});
        }

        private void CancelSwiftPay(object sender, RoutedEventArgs e)
        {
            _vm.CancelSwiftPay();

        }

        private void CancelChecks(object sender, RoutedEventArgs e)
        {
            _vm.UnprintChecks();
        }
    }
}
 
