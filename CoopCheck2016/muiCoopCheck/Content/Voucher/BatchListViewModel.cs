using System;
using System.Collections.ObjectModel;
using CoopCheck.Library;
using CoopCheck.Repository;
using CoopCheck.WPF.Content.Voucher.Import;
using CoopCheck.WPF.Converters;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Voucher
{
    public class BatchListViewModel : ViewModelBase
    {
        private ObservableCollection<OpenBatch> _batchList;

        public ObservableCollection<OpenBatch> BatchList
        {
            get { return _batchList; }
            set
            {
                _batchList = value;
                NotifyPropertyChanged();
            }
        }


        public BatchListViewModel()
        {
            ResetState();
        }

        private async void ResetState()
        {
            BatchList = new ObservableCollection<OpenBatch>(await OpenBatchSvc.GetOpenBatches());
        }

        private int _selectedBatchNum = 0;

        public int SelectedBatchNum
        {
            get { return _selectedBatchNum; }
            set
            {
                _selectedBatchNum = value;
                NotifyPropertyChanged();
            }
        }

        private OpenBatch _selectedBatch = new OpenBatch();

        public OpenBatch SelectedBatch
        {
            get { return _selectedBatch; }
            set
            {
                _selectedBatch = value;
                NotifyPropertyChanged();
                Messenger.Default.Send(new NotificationMessage<OpenBatch>(SelectedBatch, Notifications.OpenBatchChanged));
            }
        }
    }
}