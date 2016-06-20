using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.PaymentReports.Summary
{
    /// <summary>
    /// </summary>
    public partial class PaymentSummaryView : UserControl
    {
        public static readonly DependencyProperty AllPaymentsProperty =
            DependencyProperty.Register("AllPayments", typeof(ObservableCollection<Paymnt>),
                typeof(PaymentSummaryView), new PropertyMetadata(new ObservableCollection<Paymnt>()));

        public static readonly DependencyProperty OpenPaymentsProperty =
            DependencyProperty.Register("OpenPayments", typeof(ObservableCollection<Paymnt>),
                typeof(PaymentSummaryView), new PropertyMetadata(new ObservableCollection<Paymnt>()));

        public static readonly DependencyProperty ClosedPaymentsProperty =
            DependencyProperty.Register("ClosedPayments", typeof(ObservableCollection<Paymnt>),
                typeof(PaymentSummaryView), new PropertyMetadata(new ObservableCollection<Paymnt>()));

        private readonly IClientPaymentViewModel _vm;

        private BatchRpt SelectedvwBatchRpt;

        public PaymentSummaryView(IClientPaymentViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            DataContext = _vm;

            Messenger.Default.Register<NotificationMessage<BatchRpt>>(this, message =>
            {
                if (message.Notification == Notifications.JobFinderSelectedBatchChanged)
                {
                    _vm.ParentBatch = message.Content;
                    pgv.Visibility = Visibility.Visible;
                }
            });


            Messenger.Default.Register<NotificationMessage<JobRpt>>(this, message =>
            {
                if (message.Notification == Notifications.JobFinderSelectedJobChanged)
                {
                    brv.Visibility = Visibility.Visible;
                    pgv.Visibility = Visibility.Collapsed;
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
                    _vm.ClientReportCriteria.SelectedBatch.batch_num = BatchRpt.batch_num;
                    _vm.RefreshAll();
                });
            SelectedvwBatchRpt = BatchRpt;
            pgv.Visibility = Visibility;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            jfrv.ClientReportCriteria = jrcv.ClientReportCriteria;
            _vm.ClientReportCriteria = jrcv.ClientReportCriteria;
            brv.Visibility = Visibility.Collapsed;
            pgv.Visibility = Visibility.Collapsed;
        }
    }
}