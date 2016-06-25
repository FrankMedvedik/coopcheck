using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.Domain.Contracts.Messages;
using CoopCheck.Reports.Contracts.Models;
using CoopCheck.WPF.Content.Interfaces;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.PaymentReports.Job
{
    /// <summary>
    /// </summary>
    public partial class JobSummaryPaymentReportView : UserControl
    {
        public static readonly DependencyProperty AllPaymentsProperty =
            DependencyProperty.Register("AllPayments", typeof(ObservableCollection<Payment>),
                typeof(JobSummaryPaymentReportView), new PropertyMetadata(new ObservableCollection<Payment>()));

        public static readonly DependencyProperty OpenPaymentsProperty =
            DependencyProperty.Register("OpenPayments", typeof(ObservableCollection<Payment>),
                typeof(JobSummaryPaymentReportView), new PropertyMetadata(new ObservableCollection<Payment>()));

        public static readonly DependencyProperty ClosedPaymentsProperty =
            DependencyProperty.Register("ClosedPayments", typeof(ObservableCollection<Payment>),
                typeof(JobSummaryPaymentReportView), new PropertyMetadata(new ObservableCollection<Payment>()));

        private readonly IJobSummaryPaymentReportViewModel _vm;

        public JobSummaryPaymentReportView(IJobSummaryPaymentReportViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            DataContext = _vm;
            prcv.PaymentReportCriteria.StartDate = DateTime.Today.AddDays(-1);
            prcv.PaymentReportCriteria.EndDate = DateTime.Today;
            prcv.ShowAllAccountsOption = true;
            Messenger.Default.Register<NotificationMessage<JobRpt>>(this, message =>
            {
                if (message.Notification == Notifications.SelectedJobChanged)
                {
                    RefreshChildren(message.Content);
                }
            });
        }


        public ObservableCollection<Payment> AllPayments
        {
            get { return _vm.AllPayments; }
        }

        public ObservableCollection<Payment> OpenPayments
        {
            get { return _vm.OpenPayments; }
        }

        public ObservableCollection<Payment> ClosedPayments
        {
            get { return _vm.ClosedPayments; }
        }

        private async Task RefreshChildren(JobRpt jobRpt)
        {
            if (jobRpt != null)
            {
                await Task.Factory.StartNew(() =>
                {
                    _vm.PaymentReportCriteria.JobNumber = jobRpt.job_num.ToString();
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