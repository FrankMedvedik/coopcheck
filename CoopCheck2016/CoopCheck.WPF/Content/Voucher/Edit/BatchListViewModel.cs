using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Services;
using Reckner.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Voucher.Edit
{
    public class BatchListViewModel : ViewModelBase
    {
        private ObservableCollection<vwOpenBatch> _batchList;
        private string _headingText;

        public string HeadingText
        {
            get { return _headingText; }
            set
            {
                _headingText = value;
                NotifyPropertyChanged();
            }
        }

    
        public int? PaymentsCnt
        {
            get { return BatchList?.Sum(x => x.voucher_cnt.GetValueOrDefault(0)); }
        }
        public decimal? PaymentsTotalDollars
        {
            get { return BatchList?.Sum(x => x.total_amount); }
        }
        public ObservableCollection<vwOpenBatch> BatchList
        {
            get { return _batchList; }
            set
            {
                _batchList = value;
                NotifyPropertyChanged();
                HeadingText = String.Format("{0} batches",BatchList.Count);
                NotifyPropertyChanged("PaymentsTotalDollars");
                NotifyPropertyChanged("PaymentsCnt");
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