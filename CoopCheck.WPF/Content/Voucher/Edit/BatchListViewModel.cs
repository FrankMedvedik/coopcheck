using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Services;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Voucher.Edit
{
    public class BatchListViewModel : ViewModelBase
    {
        private ObservableCollection<vwOpenBatch> _batchList;
        private string _headingText;
        private bool _isBusy;

        private vwOpenBatch _selectedBatch = new vwOpenBatch();

        public BatchListViewModel()
        {
            ResetState();

            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if (message.Notification == Notifications.RefreshOpenBatchList)
                    ResetState();
            });
        }

        public string HeadingText
        {
            get { return _headingText; }
            set
            {
                _headingText = value;
                NotifyPropertyChanged();
            }
        }

        public int? SwiftPaymentsCnt
        {
            get { return BatchList?.Where(x => x.IsSwiftBatch).Sum(x => x.voucher_cnt); }
        }

        public decimal? SwiftPaymentsTotalDollars
        {
            get { return BatchList?.Where(x => x.IsSwiftBatch).Sum(x => x.total_amount); }
        }

        public int? CheckPaymentsCnt
        {
            get { return BatchList?.Where(x => !x.IsSwiftBatch).Sum(x => x.voucher_cnt); }
        }

        public decimal? CheckPaymentsTotalDollars
        {
            get { return BatchList?.Where(x => !x.IsSwiftBatch).Sum(x => x.total_amount); }
        }

        public ObservableCollection<vwOpenBatch> BatchList
        {
            get { return _batchList; }
            set
            {
                _batchList = value;
                NotifyPropertyChanged();
                HeadingText = string.Format("{0} batches", BatchList.Count);
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

        public vwOpenBatch SelectedBatch
        {
            get { return _selectedBatch; }
            set
            {
                _selectedBatch = value;
                NotifyPropertyChanged();
                Messenger.Default.Send(new NotificationMessage<vwOpenBatch>(SelectedBatch,
                    Notifications.OpenBatchChanged));
            }
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
    }
}