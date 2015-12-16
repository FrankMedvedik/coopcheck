using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Payment
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
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            brv.PaymentReportCriteria = prcv.PaymentReportCriteria;
            _vm.PaymentReportCriteria = prcv.PaymentReportCriteria;

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
    }
}