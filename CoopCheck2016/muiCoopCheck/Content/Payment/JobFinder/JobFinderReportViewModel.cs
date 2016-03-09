﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Payment.JobFinder
{
    public class JobFinderReportViewModel : ViewModelBase
    {
        public bool ShowGridData
        {
            get { return _showGridData; }
            set
            {
                _showGridData = value;
                NotifyPropertyChanged();
            }
        }

        public JobFinderReportViewModel()
        {
            ShowGridData = false;
        }

        private vwJobRpt _selectedJob = new vwJobRpt();
        public vwJobRpt SelectedJob
        {
            get { return _selectedJob; }
            set
            {
                _selectedJob = value;
                NotifyPropertyChanged();
                Messenger.Default.Send(new NotificationMessage<vwJobRpt>(SelectedJob, Notifications.SelectedJobChanged));
                Status = new StatusInfo()
                {
                    ErrorMessage = "",
                    StatusMessage = string.Format("Job {0} contains {1} payments total {2:C}", SelectedJob.job_num, SelectedJob.total_cnt, SelectedJob.total_amount)
                };

            }
        }

        private bool _canRefresh = true;
        public Boolean CanRefresh       
        {
            get { return _canRefresh; }
            set { _canRefresh = value; }
        }

        public int PaymentsCnt
        {
            get { return Jobs.ToList().Sum(x => x.total_cnt.GetValueOrDefault(0)); }
        }

        public decimal? PaymentsTotalDollars
        {
            get { return Jobs.Sum(x => x.total_amount); }
        }
        private ObservableCollection<vwJobRpt> _jobs = new ObservableCollection<vwJobRpt>();
        public ObservableCollection<vwJobRpt> Jobs
        {
            get { return _jobs; }
            set
            {
                _jobs = value;
                NotifyPropertyChanged();
                ShowGridData = true;
                HeadingText = String.Format("{0} Jobs",Jobs.Count);
                NotifyPropertyChanged("PaymentsTotalDollars");
                NotifyPropertyChanged("PaymentsCnt");
                Status = new StatusInfo()
                {
                    ErrorMessage = "",
                    StatusMessage = "select a job"
                };
            }
        }

        public  void RefreshAll()
        {
                GetJobs();
        }
        

        private string _headingText;
        private bool _showGridData;
        private StatusInfo _status;
        private JobReportCriteria _JobReportCriteria;

        public string HeadingText
        {
            get {return _headingText;}
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

        public JobReportCriteria JobReportCriteria
        {
            get { return _JobReportCriteria; }
            set
            {
                _JobReportCriteria = value;
            }
        }

        public async void GetJobs()
        {
              ShowGridData = false;
              Jobs = new ObservableCollection<vwJobRpt>();
            try
            {
                Jobs = new ObservableCollection<vwJobRpt>();

                Status = new StatusInfo()
                {
                    ErrorMessage = "",
                    IsBusy = true,
                    StatusMessage = "refreshing job list..."
                };
                if(JobReportCriteria.SearchType == JobReportCriteria.ONEJOB)
                    Jobs = new ObservableCollection<vwJobRpt>(await RptSvc.GetJob(int.Parse(JobReportCriteria.SelectedJobNum)));
                else
                {
                    if (JobReportCriteria.SearchType == JobReportCriteria.ALLCLIENTJOBS)
                }
                if (JobReportCriteria.SearchType == JobReportCriteria.ALLCLIENTJOBSFORYEAR)
                {
                    Jobs = new ObservableCollection<vwJobRpt>(await RptSvc.GetJob(
                }
            }
            catch (Exception e)
            {
                Status = new StatusInfo()
                {
                    StatusMessage = "Error loading job list",
                    ErrorMessage = e.Message
                };

            }
        }

        }
    }
