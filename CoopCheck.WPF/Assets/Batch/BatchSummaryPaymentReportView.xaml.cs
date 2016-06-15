﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.Library;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.WPF.Content.Voucher;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using FirstFloor.ModernUI.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Payment.Batch
{
    /// <summary>
    /// </summary>
    public partial class BatchSummaryPaymentReportView : UserControl
    {
        public static readonly DependencyProperty AllPaymentsProperty =
            DependencyProperty.Register("AllPayments", typeof (ObservableCollection<Paymnt>),
                typeof (BatchSummaryPaymentReportView), new PropertyMetadata(new ObservableCollection<Paymnt>()));

        public static readonly DependencyProperty OpenPaymentsProperty =
            DependencyProperty.Register("OpenPayments", typeof (ObservableCollection<Paymnt>),
                typeof (BatchSummaryPaymentReportView), new PropertyMetadata(new ObservableCollection<Paymnt>()));

        public static readonly DependencyProperty ClosedPaymentsProperty =
            DependencyProperty.Register("ClosedPayments", typeof (ObservableCollection<Paymnt>),
                typeof (BatchSummaryPaymentReportView), new PropertyMetadata(new ObservableCollection<Paymnt>()));

        private readonly BatchSummaryPaymentReportViewModel _vm;

        private BatchRpt SelectedvwBatchRpt;

        public BatchSummaryPaymentReportView()
        {
            InitializeComponent();
            _vm = new BatchSummaryPaymentReportViewModel();
            DataContext = _vm;
            prcv.PaymentReportCriteria.StartDate = DateTime.Today.AddDays(-1);
            prcv.PaymentReportCriteria.EndDate = DateTime.Today;
            prcv.ShowAllAccountsOption = true;

            Messenger.Default.Register<NotificationMessage<BatchRpt>>(this, message =>
            {
                if (message.Notification == Notifications.SelectedBatchChanged)
                {
                    RefreshChildren(message.Content);
                }
            });
        }


        public ObservableCollection<Paymnt> AllPayments
        {
            get { return _vm.AllPayments; }
        }

        public ObservableCollection<Paymnt> OpenPayments
        {
            get { return _vm.OpenPayments; }
        }

        public ObservableCollection<Paymnt> ClosedPayments
        {
            get { return _vm.ClosedPayments; }
        }

        private async Task RefreshChildren(BatchRpt BatchRpt)
        {
            if (BatchRpt == null) return;
            if ((SelectedvwBatchRpt == null)
                || (BatchRpt != SelectedvwBatchRpt))
                await Task.Factory.StartNew(() =>
                {
                    _vm.PaymentReportCriteria.BatchNumber = BatchRpt.batch_num;
                    _vm.RefreshAll();
                });
            SelectedvwBatchRpt = BatchRpt;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            brv.PaymentReportCriteria = prcv.PaymentReportCriteria;
            _vm.PaymentReportCriteria = prcv.PaymentReportCriteria;
        }

        private void Unprint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _vm.StartingCheckNum = int.Parse(SelectedvwBatchRpt.first_check_num);
                var cw = new ConfirmLastCheckPrintedDialog {DataContext = _vm};

                var result = cw.ShowDialog();
                if (result == true)
                {
                    CommitCheckCommand.Execute(SelectedvwBatchRpt.batch_num, _vm.EndingCheckNum);
                    _vm.Status = new StatusInfo
                    {
                        StatusMessage = string.Format("batch - {0} last check number printed - {1}",
                            SelectedvwBatchRpt.batch_num, _vm.EndingCheckNum)
                    };
                }
            }
            catch (Exception ex)
            {
                _vm.Status = new StatusInfo
                {
                    StatusMessage = string.Format("batch - {0} cannot be unprinted",
                        SelectedvwBatchRpt.batch_num),
                    ErrorMessage = ex.Message
                };
            }
        }


        private async void Void_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result =
                    ModernDialog.ShowMessage(
                        string.Format(" Void Swift Payments for batch {0}", _vm.PaymentReportCriteria.BatchNumber),
                        "Coop Check", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    await PaymentSvc.SwiftVoidAsync(SelectedvwBatchRpt.batch_num);
                    _vm.Status = new StatusInfo
                    {
                        StatusMessage =
                            string.Format(
                                "batch - {0} submitted for void. You will receive an email when it is completed. ",
                                SelectedvwBatchRpt.batch_num)
                    };
                }
            }
            catch (Exception ex)
            {
                _vm.Status = new StatusInfo
                {
                    StatusMessage = string.Format("batch - {0} cannot be voided", SelectedvwBatchRpt.batch_num),
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}