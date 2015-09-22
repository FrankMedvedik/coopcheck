using System.Collections.ObjectModel;
using CoopCheck.Repository;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Voucher.Edit
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
                IsBusy = false;
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                NotifyPropertyChanged();
            }

        }
        public BatchListViewModel()
        {
            ResetState();

            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if(message.Notification == Notifications.RefreshOpenBatchList)
                    ResetState();
            });

        }

        private async void ResetState()
        {
            IsBusy = true;
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
        private bool _isBusy;

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