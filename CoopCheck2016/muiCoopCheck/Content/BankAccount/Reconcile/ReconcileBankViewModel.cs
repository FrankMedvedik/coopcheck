using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.DAL;
using CoopCheck.Library;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.BankAccount.Reconcile
{
    public class ReconcileBankViewModel : ViewModelBase
    {
        private BankFileViewModel _bankFile = new BankFileViewModel();
        private AccountPaymentsViewModel _accountPayments = new AccountPaymentsViewModel();
        public BankFileViewModel BankFile
        {
            get { return _bankFile; }
            set
            {
                _bankFile = value;
                NotifyPropertyChanged();
            }
        }
        public RelayCommand ClearMatchedChecksCommand { get; private set; }

        public async void ClearMatchedChecks()
        {
            Status = new StatusInfo()
            {
                StatusMessage = "clearing matched vouchers",
                IsBusy = true
            };

            await Task.Factory.StartNew(() =>
            {
                try
                {
                   ClearCheckSvc.ClearChecks(_accountPayments.ChecksToClear);
                    Status = new StatusInfo()
                    {
                        StatusMessage = String.Format("{0} payments have been cleared", _accountPayments.MatchedPayments.Count)
                    };
                    CanClearChecks = true;
                    Messenger.Default.Send(new NotificationMessage(Notifications.BankAccountReconcileWizardCanFinish));
                }
                catch (Exception ex)
                {
                    Status = new StatusInfo()
                    {
                        StatusMessage = "failed to clear payments",
                        ErrorMessage = ex.Message
                        //,ShowMessageBox = true
                    };
                }
            });
        }

        public bool CanClearChecks
        {
            get { return _canClearChecks; }
            set
            {
                _canClearChecks = value;
                 NotifyPropertyChanged();
                ClearMatchedChecksCommand.RaiseCanExecuteChanged();

            }
        }

        public bool CanClearChecksFunc()
        {
            return CanClearChecks;
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
            ClearMatchedChecksCommand = new RelayCommand(ClearMatchedChecks, CanClearChecksFunc);
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

            await AccountPayments.GetPayments();

            Status = new StatusInfo()
            {
                ErrorMessage = "",
                IsBusy = true,
                StatusMessage = String.Format("loaded {0:n0} payments found totalling {1:c}",
                 AccountPayments.AllPayments.Count,
                 AccountPayments.AllPayments.Sum(x => x.tran_amount))
            };

            await Task.Factory.StartNew(() =>
            {
                try
                {
                    var query = from s in AccountPayments.AllPayments join t in BankFile.BankClearTransactions on new
                    {
                        a = s.check_num,
                        b = s.tran_amount.GetValueOrDefault(0)
                    } 
                    equals
                    new
                    {
                        a = t.SerialNumber,
                        b = t.Amount
                    }  
                    select new CheckInfoDto
                    {
                        ClearedAmount = t.Amount,
                        ClearedDate = t.PostDate,
                        CheckNum = s.check_num,
                        Id = s.tran_id 
                    };

                    AccountPayments.ChecksToClear = query.ToList();

                    AccountPayments.MatchedPayments =
                        (AccountPayments.AllPayments.Where(
                            s => BankFile.BankClearTransactions.Any(t => (t.SerialNumber == s.check_num)))).ToList();

                    //ClosedPayments = new List<vwPayment>(v);

                    //AccountPayments.OpenPayments = (AccountPayments.AllPayments.Where(
                    //    s => !BankFile.BankClearTransactions.Any(t => (t.SerialNumber == s.check_num)))).ToList();

                    BankFile.UnmatchedBankClearTransactions = new ObservableCollection<BankClearTransaction>(
                        (BankFile.BankClearTransactions.Where(s => !AccountPayments.ChecksToClear.Any(
                            t => (s.SerialNumber == t.CheckNum)))).ToList());

                    //Status = new StatusInfo()
                    //{
                    //    ErrorMessage = "",
                    //    IsBusy = true,
                    //    StatusMessage = "calculating totals..."
                    //};
                    AccountPayments.Stats.Clear();
                    AccountPayments.Stats.Add(new KeyValuePair<string, string>("Bank File First Transaction Date", String.Format("{0:d}", BankFile.FirstTransactionDate)));
                    AccountPayments.Stats.Add(new KeyValuePair<string, string>("Bank File Last Transaction Date", String.Format("{0:d}",BankFile.LastTransactionDate)));
                    AccountPayments.Stats.Add(new KeyValuePair<string, string>("Bank File Credits", String.Format("{0:c}", BankFile.CreditTransactionTotalDollars)));
                    AccountPayments.Stats.Add(new KeyValuePair<string, string>("Bank File Debits", String.Format("{0:c}", BankFile.DebitTransactionTotalDollars)));
                    AccountPayments.Stats.Add(new KeyValuePair<string, string>("Bank File Credit Transaction Cnt", String.Format("{0:n0}", BankFile.CreditTransactionCnt)));
                    AccountPayments.Stats.Add(new KeyValuePair<string, string>("Bank File Debit Transaction Cnt", String.Format("{0:n0}", BankFile.DebitTransactionCnt)));
                    AccountPayments.LoadStats();
                    AccountPayments.Stats.Add(new KeyValuePair<string, string>("Bank File Unmatched Credit Transaction Cnt", String.Format("{0:n0}", BankFile.UnmatchedCreditTransactionCnt)));
                    AccountPayments.Stats.Add(new KeyValuePair<string, string>("Bank File Unmatched Credits", String.Format("{0:c}", BankFile.UnmatchedCreditTransactionTotalDollars)));
                    AccountPayments.Stats.Add(new KeyValuePair<string, string>("Bank File Unmatched Debit Transaction Cnt", String.Format("{0:n0}", BankFile.UnmatchedDebitTransactionCnt)));
                    AccountPayments.Stats.Add(new KeyValuePair<string, string>("Bank File Unmatched Debits", String.Format("{0:c}", BankFile.UnmatchedDebitTransactionTotalDollars)));


                    Messenger.Default.Send(new NotificationMessage<AccountPaymentsViewModel>(AccountPayments, Notifications.ReconcileAccountPaymentsLoaded));
                    Messenger.Default.Send(new NotificationMessage<BankFileViewModel>(BankFile, Notifications.ReconcileBankFileLoaded));
                    CanClearChecks = true;
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

            Status = new StatusInfo()
            {
                ErrorMessage = "",
                StatusMessage = String.Format("Matching complete: Matched {0:n0} payments found totalling {1:c}",
                    AccountPayments.MatchedPayments.Count, AccountPayments.MatchedPayments.Sum(x => x.tran_amount))
            };
        }

        #region DisplayState


        public void ResetState()
        {
            CanClearChecks = false;
        }
    



        private StatusInfo _status;
        private bool _canClearChecks;

        #endregion
        
        
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