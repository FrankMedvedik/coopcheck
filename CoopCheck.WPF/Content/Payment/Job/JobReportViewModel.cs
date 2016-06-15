using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Payment.Job
{
    public class JobReportViewModel : ViewModelBase
    {
        private string _headingText;
        private ObservableCollection<JobRpt> _jobs = new ObservableCollection<JobRpt>();

        private JobRpt _selectedJob = new JobRpt();
        private bool _showGridData;
        private StatusInfo _status;
        private IRptSvc _rptSvc;

        public JobReportViewModel(IRptSvc rptSvc)
        {
            _rptSvc = rptSvc;
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

        public JobRpt SelectedJob
        {
            get { return _selectedJob; }
            set
            {
                _selectedJob = value;
                NotifyPropertyChanged();
                Messenger.Default.Send(new NotificationMessage<JobRpt>(SelectedJob, Notifications.SelectedJobChanged));
                Status = new StatusInfo
                {
                    ErrorMessage = "",
                    StatusMessage =
                        string.Format("Job {0} contains {1} payments total {2:C}", SelectedJob.job_num,
                            SelectedJob.total_cnt, SelectedJob.total_amount)
                };
            }
        }

        public bool CanRefresh { get; set; } = true;

        public int PaymentsCnt
        {
            get { return Jobs.ToList().Sum(x => x.total_cnt.GetValueOrDefault(0)); }
        }

        public decimal? PaymentsTotalDollars
        {
            get { return Jobs.Sum(x => x.total_amount); }
        }

        public ObservableCollection<JobRpt> Jobs
        {
            get { return _jobs; }
            set
            {
                _jobs = value;
                NotifyPropertyChanged();
                ShowGridData = true;
                HeadingText = string.Format("{0} Jobs paid between {1:ddd, MMM d, yyyy} and {2:ddd, MMM d, yyyy}",
                    Jobs.Count, PaymentReportCriteria.StartDate, PaymentReportCriteria.EndDate);
                NotifyPropertyChanged("PaymentsTotalDollars");
                NotifyPropertyChanged("PaymentsCnt");
                Status = new StatusInfo
                {
                    ErrorMessage = "",
                    StatusMessage = "select a job"
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

        public PaymentReportCriteria PaymentReportCriteria { get; set; }

        public void RefreshAll()
        {
            GetJobs();
        }

        public async void GetJobs()
        {
            ShowGridData = false;
            Jobs = new ObservableCollection<JobRpt>();
            try
            {
                Jobs = new ObservableCollection<JobRpt>();

                Status = new StatusInfo
                {
                    ErrorMessage = "",
                    IsBusy = true,
                    StatusMessage = "refreshing job list..."
                };
                var js = await _rptSvc.GetJobRpt(PaymentReportCriteria); 

                Jobs = new ObservableCollection<JobRpt>();
            }
            catch (Exception e)
            {
                Status = new StatusInfo
                {
                    StatusMessage = "Error loading job list",
                    ErrorMessage = e.Message
                };
            }
        }
    }
}