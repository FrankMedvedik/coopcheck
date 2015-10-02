using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Content.Voucher.Import;
using CoopCheck.WPF.Models;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;

namespace CoopCheck.WPF.Content.BankAccount.Reconcile
{

    public partial class ReconcileBankView : UserControl
    {
        private ReconcileBankViewModel _vm;

        public ReconcileBankView()
        {
            InitializeComponent();
            _vm = new ReconcileBankViewModel();
            DataContext = _vm;
            Messenger.Default.Register<NotificationMessage<BankFileViewModel>>(this, message =>
            {
                _vm.BankFileViewModel = message.Content;
                prcv.PaymentReportCriteria.StartDate = _vm.BankFileViewModel.FirstTransactionDate.GetValueOrDefault(DateTime.Now);
                prcv.PaymentReportCriteria.EndDate = _vm.BankFileViewModel.LastTransactionDate.GetValueOrDefault(DateTime.Now);

            });

        }
        

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {

            _vm.PaymentReportCriteria = prcv.PaymentReportCriteria;

        }
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            //_vm.CreateVoucherBatch();
            //bev.VoucherImports = _vm.VoucherImports;
            //bev.Visibility = Visibility.Visible;
            //btnClose.Visibility = Visibility.Visible;
        }
        //private void Close_Click(object sender, RoutedEventArgs e)
        //{
        //    _vm.ResetState();
        //    //bev.Visibility = Visibility.Collapsed;
        //    btnClose.Visibility = Visibility.Collapsed;
        //}

    }

}
