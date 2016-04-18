using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Services;
using Reckner.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Utilities.Unpay
{
    public class UnclearedBatchListViewModel : ViewModelBase
    {
        private ObservableCollection<vwUnclearedBatch> _batchList;

        public ObservableCollection<vwUnclearedBatch> BatchList
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
        public UnclearedBatchListViewModel()
        {
            ResetState();

            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if(message.Notification == Notifications.RefreshOpenBatchList)
                    ResetState();
            });

        }

        public async void ResetState()
        {
            IsBusy = true;
            vwUnclearedBatch v = null; 
            if (SelectedBatch != null) v = SelectedBatch;
            BatchList = new ObservableCollection<vwUnclearedBatch>(await UnclearedBatchSvc.GetUnclearedBatches());
            if((v != null) && (BatchList.Contains(v)))
                SelectedBatch = BatchList.First(x => x.batch_num == v.batch_num);
            SelectedBatch = null;
            IsBusy = false;
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

        private vwUnclearedBatch _selectedBatch = new vwUnclearedBatch();
        private bool _isBusy;

        public vwUnclearedBatch SelectedBatch
        {
            get { return _selectedBatch; }
            set
            {
                _selectedBatch = value;
                NotifyPropertyChanged();
                Messenger.Default.Send(new NotificationMessage<vwUnclearedBatch>(SelectedBatch, Notifications.OpenBatchChanged));
            }
        }

    }
}