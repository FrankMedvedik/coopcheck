using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Payment.Summary
{
    public class JobViewModel : ViewModelBase
    {
        private string _headingText;
        private ObservableCollection<vwJobRpt> _jobs = new ObservableCollection<vwJobRpt>();


        private
            vwJobRpt _selectedJob = new vwJobRpt();

        private bool _showGridData;
        private StatusInfo _status;

        public JobViewModel()
        {
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

        public vwJobRpt SelectedJob
        {
            get { return _selectedJob; }
            set
            {
                _selectedJob = value;
                NotifyPropertyChanged();
                Messenger.Default.Send(new NotificationMessage<vwJobRpt>(SelectedJob,
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

        public ObservableCollection<vwJobRpt> Jobs
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
            Jobs = new ObservableCollection<vwJobRpt>();
            try
            {
                Status = new StatusInfo
                {
                    ErrorMessage = "",
                    IsBusy = true,
                    StatusMessage = "refreshing job list..."
                };
                if (ClientReportCriteria.SearchType == ClientReportCriteria.ONEJOB)
                    Jobs =
                        new ObservableCollection<vwJobRpt>(
                            await RptSvc.GetJob(int.Parse(ClientReportCriteria.SelectedJobNum)));
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
                            await
                                RptSvc.GetAllClientJobs(ClientReportCriteria.SelectedClientID,
                                    ClientReportCriteria.SelectedJobYear));
                }
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