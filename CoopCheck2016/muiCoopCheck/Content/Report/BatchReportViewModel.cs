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
    public class BatchReportViewModel : ViewModelBase
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

        public BatchReportViewModel()
        {
            ShowGridData = false;
        }

        private vwBatchRpt _selectedBatch = new vwBatchRpt();
        public vwBatchRpt SelectedBatch
        {
            get { return _selectedBatch; }
            set
            {
                _selectedBatch = value;
                NotifyPropertyChanged();
                Messenger.Default.Send(new NotificationMessage<vwBatchRpt>(SelectedBatch, Notifications.SelectedBatchChanged));
            }
        }

        private bool _canRefresh = true;
        public Boolean CanRefresh       
        {
            get { return _canRefresh; }
            set { _canRefresh = value; }
        }


        private ObservableCollection<vwBatchRpt> _Batchs = new ObservableCollection<vwBatchRpt>();
        public ObservableCollection<vwBatchRpt> Batchs
        {
            get { return _Batchs; }
            set
            {
                _Batchs = value;
                NotifyPropertyChanged();
                ShowGridData = true;
                HeadingText = String.Format("{0} Batchs paid between {1:ddd, MMM d, yyyy} and {2:ddd, MMM d, yyyy}",
                                        Batchs.Count, PaymentReportCriteria.StartDate, PaymentReportCriteria.EndDate);
                Status = new StatusInfo()
                {
                    ErrorMessage = "",
                    StatusMessage = "select a Batch"
                };
            }
        }

        public  void RefreshAll()
        {
                GetBatchs();
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

        public async void GetBatchs()
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
                Batchs = new ObservableCollection<vwBatchRpt>(await RptSvc.GetBatchRpt(PaymentReportCriteria));
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
