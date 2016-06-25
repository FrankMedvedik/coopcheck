using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Domain.Contracts.Messages;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Reports.Contracts.Models;
using CoopCheck.WPF.Content.Interfaces;
using CoopCheck.WPF.Content.PaymentReports.Criteria;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.PaymentReports.Job
{
    public class JobSummaryPaymentReportViewModel : ViewModelBase, IJobSummaryPaymentReportViewModel
    {
        private readonly IRptSvc _rptSvc;
        private ObservableCollection<Payment> _allPayments = new ObservableCollection<Payment>();
        private ObservableCollection<Payment> _closedPayments = new ObservableCollection<Payment>();
        private string _headingText;
        private ObservableCollection<Payment> _openPayments = new ObservableCollection<Payment>();

        private PaymentReportCriteria _paymentReportCriteria = new PaymentReportCriteria();
        private bool _showGridData;
        private StatusInfo _status;
        private List<Payment> _workPayments;

        public JobSummaryPaymentReportViewModel(IRptSvc rptSvc)
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

        public PaymentReportCriteria PaymentReportCriteria
        {
            get { return _paymentReportCriteria; }
            set
            {
                _paymentReportCriteria = value;
                NotifyPropertyChanged();
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

        public List<Payment> WorkPayments
        {
            get { return _workPayments; }
            set
            {
                _workPayments = value;
                AllPayments = new ObservableCollection<Payment>(WorkPayments);
                ClosedPayments = new ObservableCollection<Payment>((
                    from l in _workPayments
                    where l.cleared_flag
                    orderby l.check_date
                    select l).ToList());
                OpenPayments = new ObservableCollection<Payment>((
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

        public ObservableCollection<Payment> AllPayments
        {
            get { return _allPayments; }
            set
            {
                _allPayments = value;
                NotifyPropertyChanged();
                ShowGridData = true;
            }
        }

        public ObservableCollection<Payment> ClosedPayments
        {
            get { return _closedPayments; }
            set
            {
                _closedPayments = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Payment> OpenPayments
        {
            get { return _openPayments; }
            set
            {
                _openPayments = value;
                NotifyPropertyChanged();
            }
        }

        public async void RefreshAll()
        {
            await GetPayments();
        }

        public async Task GetPayments()
        {
            var wps = await _rptSvc.GetJobPayments(PaymentReportCriteria);
            var workPayments = new List<Payment>();
            foreach (var p in wps)
            {
                workPayments.Add((Payment) p);
            }
            WorkPayments = workPayments;
        }
    }
}