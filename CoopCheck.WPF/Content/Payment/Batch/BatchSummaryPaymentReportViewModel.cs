using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Payment.Batch
{
    public class BatchSummaryPaymentReportViewModel : ViewModelBase
    {
        private ObservableCollection<vwPayment> _allPayments = new ObservableCollection<vwPayment>();
        private bool _canUnprint;
        private bool _canVoid;
        private ObservableCollection<vwPayment> _closedPayments = new ObservableCollection<vwPayment>();
        private int _endingCheckNum;
        private ObservableCollection<vwPayment> _openPayments = new ObservableCollection<vwPayment>();

        private PaymentReportCriteria _paymentReportCriteria = new PaymentReportCriteria();
        private bool _showGridData;
        private int _startingCheckNum;
        private StatusInfo _status;
        private List<vwPayment> _workPayments;

        public BatchSummaryPaymentReportViewModel()
        {
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
                SetCanUnPrint();
                SetCanVoid();
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
            WorkPayments = await RptSvc.GetBatchPayments(PaymentReportCriteria);
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