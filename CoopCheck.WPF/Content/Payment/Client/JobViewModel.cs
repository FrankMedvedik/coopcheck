using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using Reckner.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Payment.Client
{
    public class JobViewModel : ViewModelBase
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

        public JobViewModel()
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
                Messenger.Default.Send(new NotificationMessage<vwJobRpt>(SelectedJob, Notifications.JobFinderSelectedJobChanged));
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
        private ClientReportCriteria _clientReportCriteria;

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

        public ClientReportCriteria ClientReportCriteria
        {
            get { return _clientReportCriteria; }
            set
            {
                _clientReportCriteria = value;
            }
        }

        public async void GetJobs()
        {
              ShowGridData = false;
              Jobs = new ObservableCollection<vwJobRpt>();
            try
            {
                Status = new StatusInfo()
                {
                    ErrorMessage = "",
                    IsBusy = true,
                    StatusMessage = "refreshing job list..."
                };
                if(ClientReportCriteria.SearchType == ClientReportCriteria.ONEJOB)
                    Jobs = new ObservableCollection<vwJobRpt>(await RptSvc.GetJob(int.Parse(ClientReportCriteria.SelectedJobNum)));
                else
                {
                    if (ClientReportCriteria.SearchType == ClientReportCriteria.ALLCLIENTJOBS)
                        Jobs =
                            new ObservableCollection<vwJobRpt>(
                                await RptSvc.GetAllClientJobs(ClientReportCriteria.SelectedClientID));
                }
                if (ClientReportCriteria.SearchType == ClientReportCriteria.ALLCLIENTJOBSFORYEAR)
                {
                    Jobs =
                            new ObservableCollection<vwJobRpt>(
                                await RptSvc.GetAllClientJobs(ClientReportCriteria.SelectedClientID, ClientReportCriteria.SelectedJobYear));
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
