﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.Repository.Contracts.Interfaces;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Payment.Job
{
    /// <summary>
    /// </summary>
    public partial class JobSummaryPaymentReportView : UserControl
    {
        public static readonly DependencyProperty AllPaymentsProperty =
            DependencyProperty.Register("AllPayments", typeof (ObservableCollection<Paymnt>),
                typeof (JobSummaryPaymentReportView), new PropertyMetadata(new ObservableCollection<Paymnt>()));

        public static readonly DependencyProperty OpenPaymentsProperty =
            DependencyProperty.Register("OpenPayments", typeof (ObservableCollection<Paymnt>),
                typeof (JobSummaryPaymentReportView), new PropertyMetadata(new ObservableCollection<Paymnt>()));

        public static readonly DependencyProperty ClosedPaymentsProperty =
            DependencyProperty.Register("ClosedPayments", typeof (ObservableCollection<Paymnt>),
                typeof (JobSummaryPaymentReportView), new PropertyMetadata(new ObservableCollection<Paymnt>()));

        private readonly JobSummaryPaymentReportViewModel _vm;

        public JobSummaryPaymentReportView()
        {
            InitializeComponent();
            _vm = new JobSummaryPaymentReportViewModel();
            DataContext = _vm;
            prcv.PaymentReportCriteria.StartDate = DateTime.Today.AddDays(-1);
            prcv.PaymentReportCriteria.EndDate = DateTime.Today;
            prcv.ShowAllAccountsOption = true;
            Messenger.Default.Register<NotificationMessage<IvwJobRpt>>(this, message =>
            {
                if (message.Notification == Notifications.SelectedJobChanged)
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

        private async Task RefreshChildren(IvwJobRpt JobRpt)
        {
            if (JobRpt != null)
            {
                await Task.Factory.StartNew(() =>
                {
                    _vm.PaymentReportCriteria.JobNumber = JobRpt.job_num.ToString();
                    _vm.RefreshAll();
                });
            }
            else
            {
                _vm.ShowGridData = false;
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            brv.PaymentReportCriteria = prcv.PaymentReportCriteria;
            _vm.PaymentReportCriteria = prcv.PaymentReportCriteria;
            _vm.ShowGridData = false;
        }

        private void brv_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}