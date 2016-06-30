using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using CoopCheck.Library;
using CoopCheck.WPF.Models;
using FirstFloor.ModernUI.Windows.Controls;
using UserControl = System.Windows.Controls.UserControl;

namespace CoopCheck.WPF.Content.Voucher.Pay
{
    /// <summary>
    ///     Interaction logic for PrintChecksPage.xaml
    /// </summary>
    public partial class PayVoucherView : UserControl
    {
        private readonly PayVouchersViewModel _vm;
        private CancellationTokenSource _cts;

        public PayVoucherView()
        {
            InitializeComponent();
            _vm = new PayVouchersViewModel();
            DataContext = _vm;
        }

        private async void SwiftPay(object sender, RoutedEventArgs e)
        {
            _vm.IsBusy = true;
            await Task.Run(() => _vm.SwiftPay());
            _vm.IsBusy = false;
        }

        private async void PrintChecks(object sender, RoutedEventArgs e)
        {
            _cts = new CancellationTokenSource();

            int printedBatchNum = _vm.SelectedBatch.batch_num;

            try
            {
                var f = AppDomain.CurrentDomain.BaseDirectory + @"\" + Properties.Settings.Default.CheckTemplate;
                if (!File.Exists(f))
                {
                    ModernDialog.ShowMessage(f + " not found, cannot print checks. ",
                        "check template missing", MessageBoxButton.OK);
                    return;
                }

                try
                {
                    _vm.IsBusy = true;
                    var c = await _vm.PrintChecks(_cts.Token);
                    _vm.Status = c.Status;
                    _vm.EndingCheckNum = c.CurrentCheckNum;
                    _vm.PercentComplete = c.ProgressPercentage;
                }
                catch (OperationCanceledException)
                {
                    _vm.Status = new StatusInfo
                    {
                        StatusMessage = "printing canceled",
                        ErrorMessage = "the check run was cancelled"
                    };
                }
                catch (Exception ex)
                {
                    _vm.Status = new StatusInfo
                    {
                        StatusMessage = "printing failed",
                        ErrorMessage = ex.Message
                    };
                }

                _cts = null;

                var cw = new ConfirmLastCheckPrintedDialog {DataContext = _vm};
                var result = cw.ShowDialog();
                CommitCheckCommand.Execute(printedBatchNum, _vm.EndingCheckNum);

                _vm.RefreshBatchList();
                _vm.IsBusy = false;
                _vm.Status = new StatusInfo
                {
                    StatusMessage = string.Format("batch - {0} last check number printed - {1}",
                       printedBatchNum, _vm.EndingCheckNum)
                };
            }
            catch (Exception ex)
            {
                _vm.Status = new StatusInfo
                {
                    StatusMessage = "error printing batch ",
                    ErrorMessage = ex.Message
                };
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (_cts != null)
            {
                _cts.Cancel();
            }
        }
    }
}