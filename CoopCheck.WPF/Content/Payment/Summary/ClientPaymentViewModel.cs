using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Payment.Summary
{
    public class ClientPaymentViewModel : ViewModelBase, IClientPaymentViewModel
    {
        private readonly IRptSvc _rptSvc;
        private ObservableCollection<Paymnt> _allPayments = new ObservableCollection<Paymnt>();
        private ClientReportCriteria _clientReportCriteria;
        private ObservableCollection<Paymnt> _closedPayments = new ObservableCollection<Paymnt>();

        private int _jobNum;
        private ObservableCollection<Paymnt> _openPayments = new ObservableCollection<Paymnt>();

        private BatchRpt _parentBatch;
        private bool _showGridData;
        private StatusInfo _status;
        private List<Paymnt> _workPayments;

        public ClientPaymentViewModel(IRptSvc rptSvc)
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

        public int Job
        {
            get { return _jobNum; }
            set
            {
                _jobNum = value;
                NotifyPropertyChanged();
            }
        }

        public BatchRpt ParentBatch
        {
            get { return _parentBatch; }
            set
            {
                if (value != null)
                {
                    _parentBatch = value;
                    NotifyPropertyChanged();
                    RefreshAll();
                }
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
            }
        }


        //public string HeadingText => JobReportCriteria.ToFormattedString(' ');
        public string HeadingText => "This is a placeholder";

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

        public ClientReportCriteria ClientReportCriteria
        {
            get { return _clientReportCriteria; }
            set
            {
                _clientReportCriteria = value;
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

        private async Task GetPayments()
        {
            var wps = await _rptSvc.GetAllBatchPayments(ParentBatch.batch_num);
            var workPayments = new List<Paymnt>();
            foreach (var p in wps)
            {
                workPayments.Add((Paymnt) p);
            }
            WorkPayments = workPayments;
        }
    }
}