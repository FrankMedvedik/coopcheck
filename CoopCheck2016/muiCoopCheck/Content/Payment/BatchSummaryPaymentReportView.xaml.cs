using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Payment
{
    /// <summary>
    /// </summary>
    public partial class BatchSummaryPaymentReportView : UserControl
    {
        private BatchSummaryPaymentReportViewModel _vm = null;
       
        public BatchSummaryPaymentReportView()
        {
            InitializeComponent();
            _vm = new BatchSummaryPaymentReportViewModel();
            DataContext = _vm;
            Messenger.Default.Register<NotificationMessage<vwBatchRpt>>(this, message =>
            {
                if (message.Notification == Notifications.SelectedBatchChanged)
                {
                    RefreshChildren(message.Content);
                }
            });
        }

        private vwBatchRpt SelectedvwBatchRpt = null;

        private async Task RefreshChildren(vwBatchRpt vwBatchRpt)
        {
            if (vwBatchRpt == null) return;
               if((SelectedvwBatchRpt == null) 
                || (vwBatchRpt != SelectedvwBatchRpt))
                 await Task.Factory.StartNew(() =>
                {
                    _vm.PaymentReportCriteria.BatchNumber = vwBatchRpt.batch_num;
                    _vm.RefreshAll();
                });
            SelectedvwBatchRpt = vwBatchRpt;
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
            DependencyProperty.Register("AllPayments", typeof(ObservableCollection<vwPayment>), typeof(BatchSummaryPaymentReportView), new PropertyMetadata(new ObservableCollection<vwPayment>()));

        public ObservableCollection<vwPayment> OpenPayments
        {
            get { return _vm.OpenPayments; }
            
        }

        public static readonly DependencyProperty OpenPaymentsProperty =
            DependencyProperty.Register("OpenPayments", typeof(ObservableCollection<vwPayment>), typeof(BatchSummaryPaymentReportView), new PropertyMetadata(new ObservableCollection<vwPayment>()));

        public ObservableCollection<vwPayment> ClosedPayments
        {
            get { return _vm.ClosedPayments; }
        }

        public static readonly DependencyProperty ClosedPaymentsProperty =
            DependencyProperty.Register("ClosedPayments", typeof(ObservableCollection<vwPayment>), typeof(BatchSummaryPaymentReportView), new PropertyMetadata(new ObservableCollection<vwPayment>()));
    }
}