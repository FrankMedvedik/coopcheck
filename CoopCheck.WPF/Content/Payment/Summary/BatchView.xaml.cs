using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CoopCheck.WPF.Messages;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;

namespace CoopCheck.WPF.Content.Payment.Summary
{
    /// <summary>
    /// </summary>
    public partial class BatchView : UserControl
    {
        private BatchViewModel _vm = null;

        public BatchView()
        {
            InitializeComponent();
            _vm = new BatchViewModel();
            DataContext = _vm;
        }
        private async void ChangeBatchJob_Click(object sender, RoutedEventArgs e)
        {
            var cw = new ChangeBatchJobDialog() { DataContext = _vm };
            var result = cw.ShowDialog();
            if (result == true)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        _vm.UpdateBatchJob(_vm.SelectedBatch.batch_num, _vm.NewJobNum);

                    });
                    Messenger.Default.Send(new NotificationMessage(Notifications.RefreshPaymentSummaryJobs));
                }
                catch (Exception ex)
                {
                    _vm.Status = new Models.StatusInfo { ErrorMessage = ex.Message, StatusMessage = "change job failed" };
                }
            }
        }



    }
}