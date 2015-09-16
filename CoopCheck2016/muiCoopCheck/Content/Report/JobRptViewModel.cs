using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Repository;
using GalaSoft.MvvmLight.Messaging;
using CoopCheck.WPF.Content.Report;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Report
{
    public class JobRptViewModel : ViewModelBase
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

        public JobRptViewModel()
        {
            ShowGridData = false;
            Messenger.Default.Register<NotificationMessage<GlobalReportCriteria>>(this, message =>
            {
                if (message.Notification == Notifications.GlobalReportCriteriaChanged)
                {
                        ReportDateRange = message.Content.ReportDateRange;
                        RefreshAll();
                }
            });
        }

        private vwJobRpt _selectedJob = new vwJobRpt();
        public vwJobRpt SelectedJob
        {
            get { return _selectedJob; }
            set
            {
                _selectedJob = value;

                NotifyPropertyChanged();
            }
        }

        private bool _canRefresh = true;
        public Boolean CanRefresh       
        {
            get { return _canRefresh; }
            set { _canRefresh = value; }
        }

        #region reporting variables
        public  ReportDateRange ReportDateRange = new ReportDateRange();
        #endregion

        private ObservableCollection<vwJobRpt> _jobs = new ObservableCollection<vwJobRpt>();
        public ObservableCollection<vwJobRpt> Jobs
        {
            get { return _jobs; }
            set
            {
                _jobs = value;
                NotifyPropertyChanged();
                ShowGridData = true;
                HeadingText = String.Format("{0} Jobs paid between {1:ddd, MMM d, yyyy}  and {2:ddd, MMM d, yyyy}  ",
                                        Jobs.Count, ReportDateRange.StartRptDate, ReportDateRange.EndRptDate);
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
        public async void GetJobs()
        {
              ShowGridData = false;
            try
            {
                Status = new StatusInfo()
                {
                    ErrorMessage = "",
                    IsBusy = true,
                    StatusMessage = "refreshing job list..."
                };
                Jobs = new ObservableCollection<vwJobRpt>(await RptSvc.GetJobRpt(ReportDateRange));
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
