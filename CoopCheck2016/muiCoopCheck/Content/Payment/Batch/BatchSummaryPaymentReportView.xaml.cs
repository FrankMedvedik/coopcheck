using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.Library;
using CoopCheck.Repository;
using CoopCheck.WPF.Content.Voucher;
using CoopCheck.WPF.Messages;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Payment.Batch
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
            prcv.PaymentReportCriteria.StartDate = DateTime.Today.AddDays(-1);
            prcv.PaymentReportCriteria.EndDate = DateTime.Today;
            prcv.ShowAllAccountsOption = true;

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

        private void Unprint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _vm.StartingCheckNum = int.Parse(SelectedvwBatchRpt.first_check_num);
                var cw = new ConfirmLastCheckPrintedDialog() { DataContext = _vm };

                var result = cw.ShowDialog();
                if (result == true)
                {
                    CommitCheckCommand.Execute(SelectedvwBatchRpt.batch_num, _vm.EndingCheckNum);
                    _vm.Status = new Models.StatusInfo
                    {
                        StatusMessage = string.Format("batch - {0} last check number printed - {1}",
                            SelectedvwBatchRpt.batch_num, _vm.EndingCheckNum)
                    };
                }
            }
            catch (Exception ex)
            {
                _vm.Status = new Models.StatusInfo
                {
                    StatusMessage = string.Format("batch - {0} cannot be unprinted",
                      SelectedvwBatchRpt.batch_num),
                    ErrorMessage = ex.Message
                };
            }

        }
    }
}