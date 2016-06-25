using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Domain.Contracts.Messages;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Library;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Reports.Contracts.Models;
using CoopCheck.WPF.Content.Interfaces;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.PaymentReports.Summary
{
    public class BatchViewModel : ViewModelBase, IBatchViewModel
    {
        private ObservableCollection<BatchRpt> _batches = new ObservableCollection<BatchRpt>();


        private decimal _batchTotalDollars;

        private bool _canRefresh = true;
        private bool _haveGoodNewJobNumber;


        private string _headingText;
        private int _newJobNum;

        private string _newJobNumError;
        private JobRpt _parentJob;

        private BatchRpt _selectedBatch = new BatchRpt();
        private bool _showGridData;
        private StatusInfo _status;
        private IRptSvc _rptSvc;
        public BatchViewModel(IRptSvc rptSvc)
        {
            _rptSvc = rptSvc;
            ShowGridData = false;
            Messenger.Default.Register<NotificationMessage<JobRpt>>(this, message =>
            {
                if (message.Notification == Notifications.JobFinderSelectedJobChanged)
                {
                    ParentJob = message.Content;
                }
            });
        }

        public int NewJobNum
        {
            get { return _newJobNum; }
            set
            {
                _newJobNum = value;
                NotifyPropertyChanged();
                //ValidateJobNumber();
            }
        }

        public string NewJobNumError
        {
            get { return _newJobNumError; }
            set
            {
                _newJobNumError = value;
                NotifyPropertyChanged();
            }
        }

        public bool HaveGoodNewJobNumber
        {
            get { return _haveGoodNewJobNumber; }
            set
            {
                _haveGoodNewJobNumber = value;
                NotifyPropertyChanged();
            }
        }

        public bool CanUpdateBatchJob
        {
            get { return SelectedBatch != null; }
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

        public BatchRpt SelectedBatch
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
                    NewJobNum = _selectedBatch.job_num.GetValueOrDefault();
                    Messenger.Default.Send(new NotificationMessage<BatchRpt>(SelectedBatch,
                        Notifications.JobFinderSelectedBatchChanged));
                    Status = new StatusInfo
                    {
                        ErrorMessage = "",
                        StatusMessage =
                            string.Format("batch {0} contains {1} payments total {2:C}", SelectedBatch.batch_num,
                                SelectedBatch.total_cnt, SelectedBatch.total_amount)
                    };
                }
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

        public int PaymentsCnt
        {
            get { return Batches.ToList().Sum(x => x.total_cnt.GetValueOrDefault(0)); }
        }

        public decimal? PaymentsTotalDollars
        {
            get { return Batches.Sum(x => x.total_amount); }
        }

        public ObservableCollection<BatchRpt> Batches
        {
            get { return _batches; }
            set
            {
                _batches = value;
                NotifyPropertyChanged();
                ShowGridData = true;

                //HeadingText = String.Format("{0} Batchs paid between {1:ddd, MMM d, yyyy} and {2:ddd, MMM d, yyyy}  Total = {3:c}",
                //                        Batches.Count, PaymentReportCriteria.StartDate, PaymentReportCriteria.EndDate, BatchTotalDollars);

                HeadingText = string.Format("{0} batches paid between", Batches.Count);
                NotifyPropertyChanged("PaymentsTotalDollars");
                NotifyPropertyChanged("PaymentsCnt");

                Status = new StatusInfo
                {
                    ErrorMessage = "",
                    StatusMessage = "select a batch to show the related payments"
                };
            }
        }

        public JobRpt ParentJob
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

        public void ValidateJobNumber(string JobNum)
        {
            var retVal = false;
            NewJobNumError = "";
            if (JobNum.Length != 8)
                NewJobNumError = "Supplied Job Number Invalid";
            else
            {
                retVal = true;
            }
            HaveGoodNewJobNumber = retVal;
        }

        public async void UpdateBatchJob(int batchNum, int jobNum)
        {
            Status = new StatusInfo
            {
                StatusMessage =
                    string.Format("updating job associated with batch {0} to job number {1}", batchNum, jobNum),
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
                    Status = new StatusInfo
                    {
                        StatusMessage = string.Format("failed to update job number for batch {0}", batchNum),
                        ErrorMessage = ex.Message
                    };
                }
            });
            Status = new StatusInfo
            {
                StatusMessage = string.Format("Batch {0} updated to JobNum {1} ", batchNum, jobNum)
            };
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
                var bs = await _rptSvc.GetJobBatchRpt(ParentJob.job_num);
                BatchTotalDollars = bs.Sum(x => x.total_amount).GetValueOrDefault(0);
                var batches = new List<BatchRpt>();
                foreach (var b in bs)
                {
                    batches.Add((BatchRpt) b);
                }
                Batches = new ObservableCollection<BatchRpt>(batches);
                SelectedBatch = null;
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