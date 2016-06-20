using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.PaymentReports.Summary
{
    /// <summary>
    /// </summary>
    public partial class BatchView : UserControl
    {
        private readonly IBatchViewModel _vm;

        public BatchView(IBatchViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            DataContext = _vm;
        }

        private async void ChangeBatchJob_Click(object sender, RoutedEventArgs e)
        {
            var cw = new ChangeBatchJobDialog {DataContext = _vm};
            var result = cw.ShowDialog();
            if (result == true)
            {
                try
                {
                    await Task.Run(() => { _vm.UpdateBatchJob(_vm.SelectedBatch.batch_num, _vm.NewJobNum); });
                    Messenger.Default.Send(new NotificationMessage(Notifications.RefreshPaymentSummaryJobs));
                }
                catch (Exception ex)
                {
                    _vm.Status = new StatusInfo {ErrorMessage = ex.Message, StatusMessage = "change job failed"};
                }
            }
        }
    }
}