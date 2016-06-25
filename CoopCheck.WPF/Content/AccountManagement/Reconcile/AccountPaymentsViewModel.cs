using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Reports.Contracts.Models;
using CoopCheck.WPF.Content.PaymentReports.Criteria;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.AccountManagement.Reconcile
{
    public class AccountPaymentsViewModel : ViewModelBase, IAccountPaymentsViewModel
    {
        private readonly IRptSvc _rptSvc;
        private List<Payment> _allPayments = new List<Payment>();

        private List<CheckInfoDto> _checksToClear = new List<CheckInfoDto>();
        private List<Payment> _matchedPayments = new List<Payment>();
        private List<Payment> _openPayments = new List<Payment>();
        private PaymentReportCriteria _paymentReportCriteria;

        private ObservableCollection<KeyValuePair<string, string>> _stats =
            new ObservableCollection<KeyValuePair<string, string>>();

        public AccountPaymentsViewModel(IRptSvc rptSvc)
        {
            _rptSvc = rptSvc;
        }

        public List<Payment> MatchedPayments
        {
            get { return _matchedPayments; }
            set
            {
                _matchedPayments = value;
                NotifyPropertyChanged();
            }
        }

        public List<Payment> OpenPayments
        {
            get { return _openPayments; }
            set
            {
                _openPayments = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<KeyValuePair<string, string>> Stats
        {
            get { return _stats; }
            set
            {
                _stats = value;
                NotifyPropertyChanged();
            }
        }

        public List<CheckInfoDto> ChecksToClear
        {
            get { return _checksToClear; }
            set
            {
                _checksToClear = value;
                NotifyPropertyChanged();
            }
        }

        public List<Payment> AllPayments
        {
            get { return _allPayments; }
            set
            {
                _allPayments = value;
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

        public async Task GetPayments()
        {
            var payments = await _rptSvc.GetPaymentReconcileReport(PaymentReportCriteria);
            var allPayments = new List<Payment>();
            foreach (var p in allPayments)
            {
                allPayments.Add(p);
            }
            AllPayments = allPayments;
        }


        public async void LoadStats()
        {
            await Task.Factory.StartNew(() =>
            {
                var s =
                    new List<KeyValuePair<string, string>>();
                var matchedTransactionAmt = MatchedPayments.Sum(p => p.tran_amount).GetValueOrDefault(0);
                var matchedTransactionCnt = MatchedPayments.Count;
                s.Add(new KeyValuePair<string, string>("Matched Payments Cnt",
                    string.Format("{0:n0}", matchedTransactionCnt)));
                s.Add(new KeyValuePair<string, string>("Matched Payments", string.Format("{0:c}", matchedTransactionAmt)));
                foreach (var si in s)
                    Stats.Add(si);
            });
        }
    }
}