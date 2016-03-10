using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Payment.Job
{
    public class JobSummaryPaymentReportViewModel : ViewModelBase
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

        public JobSummaryPaymentReportViewModel()
        {
            ShowGridData = false;
      
        }

        private PaymentReportCriteria _paymentReportCriteria = new PaymentReportCriteria();
        public PaymentReportCriteria PaymentReportCriteria
        {
            get { return _paymentReportCriteria; }
            set
            {
                _paymentReportCriteria = value;
                NotifyPropertyChanged();
            }
        }
        private string _headingText;
        public string HeadingText
        {
            get { return _headingText; }
            set
            {
                _headingText = value;
                NotifyPropertyChanged();
            }
        }
   
        public  async void RefreshAll()
        {
            WorkPayments = await RptSvc.GetJobPayments(PaymentReportCriteria);

        }
        private bool _showGridData;
        private StatusInfo _status;
        private ObservableCollection<vwPayment> _allPayments = new ObservableCollection<vwPayment>();
        private List<vwPayment> _workPayments;
        private ObservableCollection<vwPayment> _closedPayments = new ObservableCollection<vwPayment>();
        private ObservableCollection<vwPayment> _openPayments = new ObservableCollection<vwPayment>();

        public List<vwPayment> WorkPayments
        {
            get { return _workPayments; }
            set
            {
                _workPayments = value;
                AllPayments = new ObservableCollection<vwPayment>(WorkPayments);
                ClosedPayments = new ObservableCollection<vwPayment>((
                                    from l in _workPayments
                                    where l.cleared_flag
                                    orderby l.check_date
                                    select l).ToList());
                OpenPayments = new ObservableCollection<vwPayment>((
                    from l in _workPayments
                    where !l.cleared_flag
                    orderby l.check_date
                    select l).ToList());
                ShowGridData = true;
                //HeadingText = String.Format("{0} Jobs for Account {1} Between {2:ddd, MMM d, yyyy} to {3:ddd, MMM d, yyyy}", a,
                //         PaymentReportCriteria.Account.account_name, PaymentReportCriteria.StartDate,PaymentReportCriteria.EndDate);

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

        public ObservableCollection<vwPayment> AllPayments
        {
            get { return _allPayments; }
            set
            {
                _allPayments = value;
                NotifyPropertyChanged();
                ShowGridData = true;
            }
            
        }

        public ObservableCollection<vwPayment> ClosedPayments
        {
            get { return _closedPayments; }
            set
            {
                _closedPayments = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<vwPayment> OpenPayments
        {
            get { return _openPayments; }
            set
            {
                _openPayments = value;
                NotifyPropertyChanged();
            }
        }
    }
}
