using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Domain.Contracts.Messages;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Reports.Contracts;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Reports.Contracts.Models;
using CoopCheck.WPF.Content.Interfaces;
using CoopCheck.WPF.Content.PaymentReports.Criteria;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.PaymentReports.Summary
{
    public class JobViewModel : ViewModelBase, IJobViewModel
    {
        private readonly IRptSvc _rptSvc;
        private string _headingText;
        private ObservableCollection<JobRpt> _jobs = new ObservableCollection<JobRpt>();


        private
            JobRpt _selectedJob = new JobRpt();

        private bool _showGridData;
        private StatusInfo _status;

        public JobViewModel(IRptSvc rptSvc)
        {
            _rptSvc = rptSvc;
            ShowGridData = false;
            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if (message.Notification == Notifications.RefreshPaymentSummaryJobs)
                {
                    RefreshAll();
                }
            });
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
                Messenger.Default.Send(new NotificationMessage<JobRpt>(SelectedJob,
                    Notifications.JobFinderSelectedJobChanged));
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
                HeadingText = string.Format("{0} Jobs", Jobs.Count);
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

        public ClientReportCriteria ClientReportCriteria { get; set; }

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
                Status = new StatusInfo
                {
                    ErrorMessage = "",
                    IsBusy = true,
                    StatusMessage = "refreshing job list..."
                };
                var jobs = new List<JobRpt>();
                var js = new List<JobRpt>();
                if (ClientReportCriteria.SearchType == ReportConstants.OneJob)
                    js = await _rptSvc.GetJob(int.Parse(ClientReportCriteria.SelectedJobNum));
                else
                {
                    if (ClientReportCriteria.SearchType == ReportConstants.AllClientJobs)
                        js = await _rptSvc.GetAllClientJobs(ClientReportCriteria.SelectedClientID);
                }
                if (ClientReportCriteria.SearchType == ReportConstants.AllClientJobsForOneYear)
                {
                    js = await _rptSvc.GetAllClientJobs(ClientReportCriteria.SelectedClientID,
                        ClientReportCriteria.SelectedJobYear);
                }
                Jobs = new ObservableCollection<JobRpt>(js);
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