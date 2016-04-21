﻿using System;
using System.IO;
using System.Threading;
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
        CancellationTokenSource cts;
        private PayVouchersViewModel _vm;
        public PayVoucherView()
        {
            InitializeComponent();
            _vm = new PayVouchersViewModel();
            DataContext = _vm;
        }

        private async void SwiftPay(object sender, RoutedEventArgs e)
        {
            _vm.IsBusy = true;
            _vm.SwiftPay();
            _vm.IsBusy = false;

        }

        private async void PrintChecks(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();

            try
            {
                var f = System.AppDomain.CurrentDomain.BaseDirectory + @"\" + @Properties.Settings.Default.CheckTemplate;
                if (!File.Exists(f))
                {
                    ModernDialog.ShowMessage(f + " not found, cannot print checks. ",
                        "check template missing", MessageBoxButton.OK);
                    return;
                }

                try
                {
                    _vm.IsBusy = true;
                    var c = await _vm.PrintChecks(cts.Token);
                    _vm.Status = c.Status;
                    _vm.EndingCheckNum = c.CurrentCheckNum;
                    _vm.PercentComplete = c.ProgressPercentage;
                }
                catch (OperationCanceledException)
                {
                    _vm.Status = new Models.StatusInfo
                    {
                        StatusMessage = "printing canceled",
                        ErrorMessage = "the check run was cancelled"
                    };
                }
                catch (Exception ex)
                {
                    _vm.Status = new Models.StatusInfo
                    {
                        StatusMessage = "printing failed",
                        ErrorMessage = ex.Message
                    };
                }

                cts = null;

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
            catch (Exception ex)
            {

                _vm.Status = new Models.StatusInfo
                {
                    StatusMessage = "error printing batch ",
                    ErrorMessage = ex.Message
                };
            }
            
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (cts != null)
            {
                cts.Cancel();
            }
        }

    }
}
 