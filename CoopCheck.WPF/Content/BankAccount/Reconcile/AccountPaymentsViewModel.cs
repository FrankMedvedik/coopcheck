using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.DAL;
using CoopCheck.Repository;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.BankAccount.Reconcile
{
    public class AccountPaymentsViewModel : ViewModelBase
    {
        private List<vwPayment> _allPayments = new List<vwPayment>();

        private List<CheckInfoDto> _checksToClear = new List<CheckInfoDto>();
        private List<vwPayment> _matchedPayments = new List<vwPayment>();
        private List<vwPayment> _openPayments = new List<vwPayment>();
        private PaymentReportCriteria _paymentReportCriteria;

        private ObservableCollection<KeyValuePair<string, string>> _stats =
            new ObservableCollection<KeyValuePair<string, string>>();

        public List<vwPayment> MatchedPayments
        {
            get { return _matchedPayments; }
            set
            {
                _matchedPayments = value;
                NotifyPropertyChanged();
            }
        }

        public List<vwPayment> OpenPayments
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

        public List<vwPayment> AllPayments
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
            AllPayments = await RptSvc.GetPaymentReconcileReport(PaymentReportCriteria);
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