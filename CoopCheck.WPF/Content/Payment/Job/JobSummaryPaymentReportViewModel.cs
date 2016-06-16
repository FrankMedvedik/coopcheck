using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Payment.Job
{
    public class JobSummaryPaymentReportViewModel : ViewModelBase, IJobSummaryPaymentReportViewModel
    {
        private readonly IRptSvc _rptSvc;
        private ObservableCollection<Paymnt> _allPayments = new ObservableCollection<Paymnt>();
        private ObservableCollection<Paymnt> _closedPayments = new ObservableCollection<Paymnt>();
        private string _headingText;
        private ObservableCollection<Paymnt> _openPayments = new ObservableCollection<Paymnt>();

        private PaymentReportCriteria _paymentReportCriteria = new PaymentReportCriteria();
        private bool _showGridData;
        private StatusInfo _status;
        private List<Paymnt> _workPayments;

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

        public List<Paymnt> WorkPayments
        {
            get { return _workPayments; }
            set
            {
                _workPayments = value;
                AllPayments = new ObservableCollection<Paymnt>(WorkPayments);
                ClosedPayments = new ObservableCollection<Paymnt>((
                    from l in _workPayments
                    where l.cleared_flag
                    orderby l.check_date
                    select l).ToList());
                OpenPayments = new ObservableCollection<Paymnt>((
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

        public ObservableCollection<Paymnt> AllPayments
        {
            get { return _allPayments; }
            set
            {
                _allPayments = value;
                NotifyPropertyChanged();
                ShowGridData = true;
            }
        }

        public ObservableCollection<Paymnt> ClosedPayments
        {
            get { return _closedPayments; }
            set
            {
                _closedPayments = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Paymnt> OpenPayments
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
            var workPayments = new List<Paymnt>();
            foreach (var p in wps)
            {
                workPayments.Add((Paymnt) p);
            }
            WorkPayments = workPayments;
        }
    }
}