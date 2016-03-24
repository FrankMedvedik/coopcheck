using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Voucher.Edit
{
    public class BatchListViewModel : ViewModelBase
    {
        private ObservableCollection<vwOpenBatch> _batchList;

        public ObservableCollection<vwOpenBatch> BatchList
        {
            get { return _batchList; }
            set
            {
                _batchList = value;
                NotifyPropertyChanged();
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

        public async void ResetState()
        {
            IsBusy = true;
            vwOpenBatch v = null; 
            if (SelectedBatch != null) v = SelectedBatch;
            BatchList = new ObservableCollection<vwOpenBatch>(await OpenBatchSvc.GetOpenBatches());
            //if((v != null) && (BatchList.Contains(v)))
            if (v != null)
                try
                {
                    SelectedBatch = BatchList.First(x => x.batch_num == v.batch_num);
                }
                catch (Exception)
                {
                    SelectedBatch = null;
                }
          else
                SelectedBatch = null;
         IsBusy = false;
        }
        
        private vwOpenBatch _selectedBatch = new vwOpenBatch();
        private bool _isBusy;

        public vwOpenBatch SelectedBatch
        {
            get { return _selectedBatch; }
            set
            {
                _selectedBatch = value;
                NotifyPropertyChanged();
                Messenger.Default.Send(new NotificationMessage<vwOpenBatch>(SelectedBatch, Notifications.OpenBatchChanged));
            }
        }

    }
}