using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Domain.Contracts.Messages;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Reports.Contracts.Models;
using CoopCheck.WPF.Content.Interfaces;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Voucher.Edit
{
    public class BatchListViewModel : ViewModelBase, IBatchListViewModel
    {
        private readonly IOpenBatchSvc _openBatchSvc;
        private ObservableCollection<OpenBatch> _batchList;
        private string _headingText;
        private bool _isBusy;
        private OpenBatch _selectedBatch = new OpenBatch();


        public BatchListViewModel(IOpenBatchSvc openBatchSvc)
        {
            _openBatchSvc = openBatchSvc;
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
            get { return BatchList?.Where(x => x.IsSwiftBatch).Sum(x => x.voucher_cnt).GetValueOrDefault(0); }
        }

        public decimal? SwiftPaymentsTotalDollars
        {
            get { return BatchList?.Where(x => x.IsSwiftBatch).Sum(x => x.total_amount).GetValueOrDefault(0); }
        }

        public int? CheckPaymentsCnt
        {
            get { return BatchList?.Where(x => !x.IsSwiftBatch).Sum(x => x.voucher_cnt).GetValueOrDefault(0); }
        }

        public decimal? CheckPaymentsTotalDollars
        {
            get { return BatchList?.Where(x => !x.IsSwiftBatch).Sum(x => x.total_amount).GetValueOrDefault(0); }
        }

        public ObservableCollection<OpenBatch> BatchList
        {
            get { return _batchList; }
            set
            {
                _batchList = value;
                NotifyPropertyChanged();
                HeadingText = string.Format("{0} batches", BatchList.Count);
                //HeadingText = string.Format(
                //    "{0} batches {1} checks totalling {2} and {3} swift payments totalling {4}", BatchList.Count,
                //    SwiftPaymentsCnt, SwiftPaymentsTotalDollars, CheckPaymentsCnt, CheckPaymentsTotalDollars);
                NotifyPropertyChanged("SwiftPaymentsCnt");
                NotifyPropertyChanged("SwiftPaymentsTotalDollars");
                NotifyPropertyChanged("CheckPaymentsCnt");
                NotifyPropertyChanged("CheckPaymentsTotalDollars");
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

        public OpenBatch SelectedBatch
        {
            get { return _selectedBatch; }
            set
            {
                _selectedBatch = value;
                NotifyPropertyChanged();
                Messenger.Default.Send(new NotificationMessage<OpenBatch>(SelectedBatch,
                    Notifications.OpenBatchChanged));
            }
        }

        public async void ResetState()
        {
            IsBusy = true;
            OpenBatch v = null;
            if (SelectedBatch != null) v = SelectedBatch;
            BatchList = new ObservableCollection<OpenBatch>(await _openBatchSvc.GetOpenBatches());
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