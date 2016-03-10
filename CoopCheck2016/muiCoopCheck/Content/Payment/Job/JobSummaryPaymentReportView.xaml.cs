using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Payment.Job
{
    /// <summary>
    /// </summary>
    public partial class JobSummaryPaymentReportView : UserControl
    {
        private JobSummaryPaymentReportViewModel _vm = null;
       
        public JobSummaryPaymentReportView()
        {
            InitializeComponent();
            _vm = new JobSummaryPaymentReportViewModel();
            DataContext = _vm;
            prcv.PaymentReportCriteria.StartDate = DateTime.Today.AddDays(-1);
            prcv.PaymentReportCriteria.EndDate = DateTime.Today;
            Messenger.Default.Register<NotificationMessage<vwJobRpt>>(this, message =>
            {
                if (message.Notification == Notifications.SelectedJobChanged)
                {
                    RefreshChildren(message.Content);
                }
            });
        }

        private async Task RefreshChildren(vwJobRpt vwJobRpt)
        {
            if (vwJobRpt != null)
            {
                await Task.Factory.StartNew(() =>
                {
                    _vm.PaymentReportCriteria.JobNumber = vwJobRpt.job_num.ToString();
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


        public ObservableCollection<vwPayment> AllPayments
        {
            get { return _vm.AllPayments; }
        }

        public static readonly DependencyProperty AllPaymentsProperty =
            DependencyProperty.Register("AllPayments", typeof(ObservableCollection<vwPayment>), typeof(JobSummaryPaymentReportView), new PropertyMetadata(new ObservableCollection<vwPayment>()));

        public ObservableCollection<vwPayment> OpenPayments
        {
            get { return _vm.OpenPayments; }
            
        }

        public static readonly DependencyProperty OpenPaymentsProperty =
            DependencyProperty.Register("OpenPayments", typeof(ObservableCollection<vwPayment>), typeof(JobSummaryPaymentReportView), new PropertyMetadata(new ObservableCollection<vwPayment>()));

        public ObservableCollection<vwPayment> ClosedPayments
        {
            get { return _vm.ClosedPayments; }
        }

        public static readonly DependencyProperty ClosedPaymentsProperty =
            DependencyProperty.Register("ClosedPayments", typeof(ObservableCollection<vwPayment>), typeof(JobSummaryPaymentReportView), new PropertyMetadata(new ObservableCollection<vwPayment>()));

        private void brv_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}