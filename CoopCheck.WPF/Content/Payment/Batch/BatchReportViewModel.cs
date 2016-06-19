using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Payment.Batch
{
    public class BatchReportViewModel : ViewModelBase
    {
        private ObservableCollection<vwBatchRpt> _batches = new ObservableCollection<vwBatchRpt>();


        private decimal _batchTotalDollars;

        private bool _canRefresh = true;
        private string _headingText;
        private PaymentReportCriteria _paymentReportCriteria;

        private vwBatchRpt _selectedBatch = new vwBatchRpt();


        private bool _showGridData;
        private StatusInfo _status;

        public BatchReportViewModel()
        {
            ShowGridData = false;
        }


        public bool ShowGridData
        {
            get { return _showGridData; }
            set
            {
                _showGridData = value;
                NotifyPropertyChanged();
            }
        }

        public vwBatchRpt SelectedBatch
        {
            get { return _selectedBatch; }
            set
            {
                _selectedBatch = value;
                NotifyPropertyChanged();
                Messenger.Default.Send(new NotificationMessage<vwBatchRpt>(SelectedBatch,
                    Notifications.SelectedBatchChanged));
                if(SelectedBatch != null)
                Status = new StatusInfo
                {
                    ErrorMessage = "",
                    StatusMessage =
                        string.Format("batch {0} contains {1} payments total {2:C}", SelectedBatch.batch_num,
                            SelectedBatch.total_cnt, SelectedBatch.total_amount)
                };
            }
        }

        public bool CanRefresh
        {
            get { return _canRefresh; }
            set
            {
                _canRefresh = value;
                NotifyPropertyChanged();
            }
        }

        public decimal BatchTotalDollars
        {
            get { return _batchTotalDollars; }
            set
            {
                _batchTotalDollars = value;
                NotifyPropertyChanged();
            }
        }

        public int? PaymentsCnt
        {
            get { return Batches?.Where(x => x.total_amount > 0).Sum(x => x.total_cnt.GetValueOrDefault(0)); }
        }

        public decimal? VoidsTotalDollars
        {
            get { return Batches?.Where(x => x.total_amount < 0).Sum(x => x.total_amount); }
        }

        public int? VoidsCnt
        {
            get { return Batches?.Where(x => x.total_amount < 0).Sum(x => x.total_cnt.GetValueOrDefault(0)); }
        }

        public decimal? PaymentsTotalDollars
        {
            get { return Batches?.Where(x => x.total_amount > 0).Sum(x => x.total_amount); }
        }

        public ObservableCollection<vwBatchRpt> Batches
        {
            get { return _batches; }
            set
            {
                _batches = value;
                NotifyPropertyChanged();
                ShowGridData = true;

                //HeadingText = String.Format("{0} Batchs paid between {1:ddd, MMM d, yyyy} and {2:ddd, MMM d, yyyy}  Total = {3:c}",
                //                        Batches.Count, PaymentReportCriteria.StartDate, PaymentReportCriteria.EndDate, BatchTotalDollars);

                HeadingText = string.Format("{0} batches paid between {1:ddd, MMM d, yyyy} and {2:ddd, MMM d, yyyy}",
                    Batches.Count, PaymentReportCriteria.StartDate, PaymentReportCriteria.EndDate);
                NotifyPropertyChanged("PaymentsTotalDollars");
                NotifyPropertyChanged("PaymentsCnt");
                NotifyPropertyChanged("VoidsTotalDollars");
                NotifyPropertyChanged("VoidsCnt");
                Status = new StatusInfo
                {
                    ErrorMessage = "",
                    StatusMessage = "select a batch to show the related payments"
                };
            }
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

        public StatusInfo Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyPropertyChanged();
                Messenger.Default.Send(new NotificationMessage<StatusInfo>(_status, Notifications.StatusInfoChanged));
            }
        }

        public PaymentReportCriteria PaymentReportCriteria
        {
            get { return _paymentReportCriteria; }
            set
            {
                _paymentReportCriteria = value;
                GetBatches();
            }
        }

        public void RefreshAll()
        {
            GetBatches();
        }

        public async void GetBatches()
        {
            ShowGridData = false;
            try
            {
                Status = new StatusInfo
                {
                    ErrorMessage = "",
                    IsBusy = true,
                    StatusMessage = "refreshing Batch list..."
                };
                var v = await RptSvc.GetBatchRpt(PaymentReportCriteria);
                BatchTotalDollars = v.Sum(x => x.total_amount).GetValueOrDefault(0);
                Batches = new ObservableCollection<vwBatchRpt>(v);
            }
            catch (Exception e)
            {
                Status = new StatusInfo
                {
                    StatusMessage = "Error loading Batch list",
                    ErrorMessage = e.Message
                };
            }
        }
    }
}