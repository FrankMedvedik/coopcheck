using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Library;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Payment.Summary
{
    public class BatchViewModel : ViewModelBase
    {
        private int _newJobNum = 0; 
        public int NewJobNum
        {
            get { return _newJobNum; }
            set { _newJobNum = value; NotifyPropertyChanged(); }
        }

        public async void UpdateBatchJob(int batchNum, int jobNum)
        {
            Status = new StatusInfo()
            {
                StatusMessage = String.Format("updating job associated with batch {0} to job number {1}", batchNum, jobNum),
                IsBusy = true
            };
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    var sb = BatchEdit.GetBatchEdit(batchNum);
                    sb.JobNum = jobNum;
                    sb = sb.Save();
                }
                catch (Exception ex)
                {
                    Status = new StatusInfo()
                    {
                        StatusMessage = String.Format("failed to update job number for batch {0}", batchNum),
                        ErrorMessage = ex.Message
                    };
                }
            });
            Status = new StatusInfo()
            {
                StatusMessage = String.Format("Batch {0} updated to JobNum {1} ", batchNum, jobNum)
            };
        }

        public bool CanUpdateBatchJob
        {
            get
            {
                return SelectedBatch != null;

            }
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

        public BatchViewModel()
        {
            ShowGridData = false;
            Messenger.Default.Register<NotificationMessage<vwJobRpt>>(this, message =>
            {
                if (message.Notification == Notifications.JobFinderSelectedJobChanged)
                {
                    ParentJob = message.Content;
                }
            }); 
        }

        private vwBatchRpt _selectedBatch = new vwBatchRpt();
        public vwBatchRpt SelectedBatch
        {
            get { return _selectedBatch; }
            set
            {
                if (value == null)
                {
                    _selectedBatch = null;
                    NotifyPropertyChanged();
                }
                if (value != null)
                {
                    _selectedBatch = value;
                    Messenger.Default.Send(new NotificationMessage<vwBatchRpt>(SelectedBatch,
                        Notifications.JobFinderSelectedBatchChanged));
                    Status = new StatusInfo()
                    {
                        ErrorMessage = "",
                        StatusMessage =
                            string.Format("batch {0} contains {1} payments total {2:C}", SelectedBatch.batch_num,
                                SelectedBatch.total_cnt, SelectedBatch.total_amount)
                    };
                }


            }
        }

        private bool _canRefresh = true;
        public Boolean CanRefresh
        {
            get { return _canRefresh; }
            set { _canRefresh = value; NotifyPropertyChanged(); }
        }


        private decimal _batchTotalDollars = 0;
        public decimal BatchTotalDollars
        {
            get { return _batchTotalDollars; }
            set
            {
                _batchTotalDollars = value; NotifyPropertyChanged();
            }
        }
        public int PaymentsCnt
        {
            get { return Batches.ToList().Sum(x => x.total_cnt.GetValueOrDefault(0)); }
        }

        public decimal? PaymentsTotalDollars
        {
            get { return Batches.Sum(x => x.total_amount); }
        }
        private ObservableCollection<vwBatchRpt> _batches = new ObservableCollection<vwBatchRpt>();
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

                HeadingText = String.Format("{0} batches paid between",Batches.Count);
                NotifyPropertyChanged("PaymentsTotalDollars");
                NotifyPropertyChanged("PaymentsCnt");

                Status = new StatusInfo()
                {
                    ErrorMessage = "",
                    StatusMessage = "select a batch to show the related payments"
                };
            }
        }

        public void RefreshAll()
        {
            GetBatches();
        }


        private string _headingText;
        private bool _showGridData;
        private StatusInfo _status;
        private vwJobRpt _parentJob;
        public vwJobRpt ParentJob 
        {
            get { return _parentJob; }
            set
            {

                if (value != null)
                {
                    _parentJob = value;
                    NotifyPropertyChanged();
                    RefreshAll();
                }
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

        public async void GetBatches()
        {
            ShowGridData = false;
            try
            {
                Status = new StatusInfo()
                {
                    ErrorMessage = "",
                    IsBusy = true,
                    StatusMessage = "refreshing Batch list..."
                };
                var v = await RptSvc.GetJobBatchRpt(ParentJob.job_num);
                BatchTotalDollars = v.Sum(x => x.total_amount).GetValueOrDefault(0);
                Batches = new ObservableCollection<vwBatchRpt>(v);
                SelectedBatch = null;
            }
            catch (Exception e)
            {
                Status = new StatusInfo()
                {
                    StatusMessage = "Error loading Batch list",
                    ErrorMessage = e.Message
                };

            }
        }

    }
}
