using System;
using System.Deployment.Application;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.Library;
using CoopCheck.WPF.Models;
using FirstFloor.ModernUI.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

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
            _vm.PrintChecks();
            var cw = new ConfirmLastCheckPrintedDialog() { DataContext = _vm };
            var result = cw.ShowDialog();
            CommitCheckCommand.Execute(_vm.SelectedBatch.batch_num, _vm.EndingCheckNum);
            if (_vm.EndingCheckNum != 0)
            {
                _vm.RefreshBatchList();
            }
            _vm.IsBusy = false;
        }
    }
}
 
