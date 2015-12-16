using System;
using System.Collections.ObjectModel;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Payment
{
    public class JobReportViewModel : ViewModelBase
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

        public JobReportViewModel()
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
            }
        }

        private bool _canRefresh = true;
        public Boolean CanRefresh       
        {
            get { return _canRefresh; }
            set { _canRefresh = value; }
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
                HeadingText = String.Format("{0} Jobs paid between {1:ddd, MMM d, yyyy} and {2:ddd, MMM d, yyyy}",
                                        Jobs.Count, PaymentReportCriteria.StartDate, PaymentReportCriteria.EndDate);
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
        private PaymentReportCriteria _paymentReportCriteria;

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

        public PaymentReportCriteria PaymentReportCriteria
        {
            get { return _paymentReportCriteria; }
            set
            {
                _paymentReportCriteria = value;
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
                Jobs = new ObservableCollection<vwJobRpt>(await RptSvc.GetJobRpt(PaymentReportCriteria));
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
