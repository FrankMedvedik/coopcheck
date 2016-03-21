using System.IO;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.Library;
using FirstFloor.ModernUI.Windows.Controls;

namespace CoopCheck.WPF.Content.Voucher.Pay
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
            //Messenger.Default.Register<NotificationMessage<StatusInfo>>(this, message =>
            //{
            //    Status = message.Content;
            //});
        }

        private async void SwiftPay(object sender, RoutedEventArgs e)
        {
            _vm.IsBusy = true;
            _vm.SwiftPay();
            _vm.IsBusy = false;

        }

        private async void PrintChecks(object sender, RoutedEventArgs e)
        {
            var f = System.AppDomain.CurrentDomain.BaseDirectory + @"\" + @Properties.Settings.Default.CheckTemplate;
            if (!File.Exists(f))
            {
                ModernDialog.ShowMessage(f + " not found, cannot print checks. ",
                    "check template missing", MessageBoxButton.OK);
                return;
            }
            _vm.IsBusy = true;
            _vm.Status = await _vm.PrintChecks();
            var cw = new ConfirmLastCheckPrintedDialog() { DataContext = _vm };
            var result = cw.ShowDialog();
            CommitCheckCommand.Execute(_vm.SelectedBatch.batch_num, _vm.EndingCheckNum);
            _vm.RefreshBatchList();
            _vm.IsBusy = false;
            _vm.Status = new Models.StatusInfo
            {
                StatusMessage = string.Format("batch - {0} last check number printed - {1}",
                    _vm.SelectedBatch.batch_num, _vm.EndingCheckNum)
            };
        }
    }
}
 
