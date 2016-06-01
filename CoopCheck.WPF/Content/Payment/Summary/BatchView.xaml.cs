using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Payment.Summary
{
    /// <summary>
    /// </summary>
    public partial class BatchView : UserControl
    {
        private readonly BatchViewModel _vm;

        public BatchView()
        {
            InitializeComponent();
            _vm = new BatchViewModel();
            DataContext = _vm;
        }

        private async void ChangeBatchJob_Click(object sender, RoutedEventArgs e)
        {
            var cw = new ChangeBatchJobDialog {DataContext = _vm};
            var result = cw.ShowDialog();
            if ((_vm.ValidateNewJobNum() == null) && (result == true) &&
                (int.Parse(_vm.NewJobNum) != _vm.SelectedBatch.job_num))
            {
                try
                {
                    await Task.Run(() => { _vm.UpdateBatchJob(_vm.SelectedBatch.batch_num, int.Parse(_vm.NewJobNum)); });
                    Messenger.Default.Send(new NotificationMessage(Notifications.RefreshPaymentSummaryJobs));
                }
                catch (Exception ex)
                {
                    _vm.Status = new StatusInfo {ErrorMessage = ex.Message, StatusMessage = "change job failed"};
                }
            }
            if ((_vm.ValidateNewJobNum() != null) && (result == true))
            {
                _vm.Status = new StatusInfo
                {
                    ErrorMessage = "Invalid Job Number " + _vm.NewJobNum,
                    StatusMessage = "No Change Made"
                };
            }
        }
    }
}