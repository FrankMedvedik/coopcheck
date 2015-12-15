using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Repository;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.BankAccount.Reconcile
{
    public class ReconcileBankViewModel : ViewModelBase
    {
        private BankFileViewModel _bankFile = new BankFileViewModel();
        private AccountPaymentsViewModel _accountPayments = new AccountPaymentsViewModel();
        private bool _showGridData;

        public BankFileViewModel BankFile
        {
            get { return _bankFile; }
            set
            {
                _bankFile = value;
                NotifyPropertyChanged();
            }
        }

        public AccountPaymentsViewModel AccountPayments
        {
            get { return _accountPayments; }
            set
            {
                _accountPayments = value;
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

        public ReconcileBankViewModel()
        {
            ResetState();

        }

        public async void GetPayments()
        {
            Status = new StatusInfo()
            {
                ErrorMessage = "",
                IsBusy = true,
                StatusMessage = "getting payments..."
            };

            await Task.Factory.StartNew(async () =>
            {
                try
                {
                    await AccountPayments.GetPayments();
                    AccountPayments.MatchedPayments =
                        (from s in AccountPayments.AllPayments
                         where BankFile.BankClearTransactions.Any(t => (t.SerialNumber == s.check_num))
                         select s).ToList();

                    //ClosedPayments = new List<vwPayment>(v);

                    AccountPayments.OpenPayments = (from s in AccountPayments.AllPayments
                                                    where
                                                        !BankFile.BankClearTransactions.Any(
                                                            t => (t.SerialNumber == s.check_num.ToString()))
                                                    select s).ToList();

                    BankFile.UnmatchedBankClearTransactions = new ObservableCollection<BankClearTransaction>(
                        (from s in BankFile.BankClearTransactions
                         where
                             !AccountPayments.AllPayments.Any(
                                 t => (s.SerialNumber == t.check_num))
                         select s).ToList());

                    AccountPayments.Stats.Add(new KeyValuePair<string, string>("Bank File First Transaction Date", String.Format("{0:d}", BankFile.FirstTransactionDate)));
                    AccountPayments.Stats.Add(new KeyValuePair<string, string>("Bank File Last Transaction Date", String.Format("{0:d}",BankFile.LastTransactionDate)));
                    AccountPayments.Stats.Add(new KeyValuePair<string, string>("Bank File Credits", String.Format("{0:c}", BankFile.CreditTransactionTotalDollars)));
                    AccountPayments.Stats.Add(new KeyValuePair<string, string>("Bank File Debits", String.Format("{0:c}", BankFile.DebitTransactionTotalDollars)));
                    AccountPayments.Stats.Add(new KeyValuePair<string, string>("Bank File Credit Transaction Cnt", String.Format("{0:n0}", BankFile.CreditTransactionCnt)));
                    AccountPayments.Stats.Add(new KeyValuePair<string, string>("Bank File Debit Transaction Cnt", String.Format("{0:n0}", BankFile.DebitTransactionCnt)));
                    AccountPayments.LoadStats();

                    Status = new StatusInfo()
                    {
                        ErrorMessage = "",
                        StatusMessage = String.Format("{0:n0} payments found totalling {1:c}",
                            AccountPayments.AllPayments.Count(), 
                            AccountPayments.AllPayments.Sum(x=>x.tran_amount))
                    };

                }
                catch (Exception e)
                {
                    Status = new StatusInfo()
                    {
                        StatusMessage = "Error loading payments",
                        ErrorMessage = e.Message
                    };
                }
            });
        }

        #region DisplayState


        public void ResetState()
        {
            CanReconcile = false;
        }
        private bool _canReconcile = false;
        public bool CanReconcile
        {
            get { return _canReconcile; }
            set
            {
                _canReconcile = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                NotifyPropertyChanged();
            }
        }

        private StatusInfo _status;
        private bool _isBusy;

        #endregion
        
        private string _headerText;

        public string HeaderText
        {
            get { return _headerText; }
            set { _headerText = value; }
        }

        public PaymentReportCriteria PaymentReportCriteria
        {
            get { return _accountPayments.PaymentReportCriteria; }
            set {
                 _accountPayments.PaymentReportCriteria = value;
                  NotifyPropertyChanged();
            }
        }
    }
}