using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Domain.Contracts.Messages;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Reports.Contracts.Models;
using CoopCheck.WPF.Content.Interfaces;
using CoopCheck.WPF.Content.PaymentReports.Criteria;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.PaymentReports.Batch
{
    public class BatchSummaryPaymentReportViewModel : ViewModelBase, IBatchSummaryPaymentReportViewModel
    {
        private readonly IRptSvc _rptSvc;
        private ObservableCollection<Payment> _allPayments = new ObservableCollection<Payment>();
        private bool _canUnprint;
        private bool _canVoid;
        private ObservableCollection<Payment> _closedPayments = new ObservableCollection<Payment>();
        private int _endingCheckNum;
        private ObservableCollection<Payment> _openPayments = new ObservableCollection<Payment>();

        private PaymentReportCriteria _paymentReportCriteria = new PaymentReportCriteria();
        private bool _showGridData;
        private int _startingCheckNum;
        private StatusInfo _status;
        private List<Payment> _workPayments;

        public BatchSummaryPaymentReportViewModel(IRptSvc rptSvc)
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

        public bool CanVoid
        {
            get { return _canVoid; }
            set
            {
                _canVoid = value;
                NotifyPropertyChanged();
            }
        }

        public bool CanUnprint
        {
            get { return _canUnprint; }
            set
            {
                _canUnprint = value;
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
            }
        }


        public string HeadingText => PaymentReportCriteria.ToFormattedString(' ');

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
                SetCanUnPrint();
                SetCanVoid();
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

        public int EndingCheckNum
        {
            get { return _endingCheckNum; }
            set
            {
                _endingCheckNum = value;
                NotifyPropertyChanged();
            }
        }

        public int StartingCheckNum
        {
            get { return _startingCheckNum; }
            set
            {
                _startingCheckNum = value;
                NotifyPropertyChanged();
            }
        }

        public async void RefreshAll()
        {
            WorkPayments  = await _rptSvc.GetBatchPayments(PaymentReportCriteria);
        }

        private void SetCanVoid()
        {
            CanVoid = false;
            if (AllPayments.First().IsSwiftPayment)
                CanVoid = true;
        }

        private void SetCanUnPrint()
        {
            var pdate = AllPayments.Max(x => x.check_date).GetValueOrDefault();
            CanUnprint = (ClosedPayments.Count == 0) && (pdate.AddDays(7) > DateTime.Now);
        }
    }
}