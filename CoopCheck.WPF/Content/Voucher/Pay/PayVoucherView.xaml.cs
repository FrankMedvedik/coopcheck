using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.Library;
using FirstFloor.ModernUI.Windows.Controls;

namespace CoopCheck.WPF.Content.Voucher.Pay
{
    /// <summary>
    ///     Interaction logic for PrintChecksPage.xaml
    /// </summary>
    public partial class PayVoucherView : UserControl
    {
        private readonly IPayVouchersViewModel _vm;
        private CancellationTokenSource _cts;

        public PayVoucherView(IPayVouchersViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
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

            var printedBatchNum = _vm.SelectedBatch.batch_num;

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

                // fix exception iussue on save  
                //var cw = new ConfirmLastCheckPrintedDialog { DataContext = _vm };

                //var result = cw.ShowDialog();
                //while (result != null && result == false)
                //{
                //    try
                //    {
                //        //                        CommitCheckCommand.Execute(printedBatchNum, _vm.EndingCheckNum);
                //        throw new Exception("broke!");
                //    }
                //    catch (Exception ex)
                //    {

                //        result = cw.ShowDialog();
                //    }
                //}



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