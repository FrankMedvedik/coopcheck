using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.DAL;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.BankAccount.Reconcile
{
    public class ReconcileBankViewModel : ViewModelBase
    {
        private AccountPaymentsViewModel _accountPayments = new AccountPaymentsViewModel();
        private BankFileViewModel _bankFile = new BankFileViewModel();

        public ReconcileBankViewModel()
        {
            ClearMatchedChecksCommand = new RelayCommand(ClearMatchedChecks, CanClearChecksFunc);
            MatchCheckCommand = new RelayCommand(MatchCheck, CanMatchCheck);
            //SaveReconciliationToExcelCommand = new RelayCommand(SaveReconciliationRptToExcel, CanClearChecksFunc);
            //ResetState();
        }

        private bool CanMatchCheck()
        {
            return (BankFile.SelectedUnmatchedBankClearTransaction != null);
        }
       
        public BankFileViewModel BankFile
        {
            get { return _bankFile; }
            set
            {
                _bankFile = value;
                NotifyPropertyChanged();
            }
        }
  
        public RelayCommand MatchCheckCommand { get; }
        public RelayCommand ClearMatchedChecksCommand { get; }
        public RelayCommand SaveReconciliationToExcelCommand { get; private set; }

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


        public PaymentReportCriteria PaymentReportCriteria
        {
            get { return _accountPayments.PaymentReportCriteria; }
            set
            {
                _accountPayments.PaymentReportCriteria = value;
                NotifyPropertyChanged();
            }
        }

        public async void ClearMatchedChecks()
        {
            Status = new StatusInfo
            {
                StatusMessage = "clearing matched vouchers",
                IsBusy = true
            };

            await Task.Factory.StartNew( () =>
            {
                try
                {
                    ClearCheckSvc.ClearChecks(_accountPayments.ChecksToClear);
                    Status = new StatusInfo
                    {
                        StatusMessage =
                            string.Format("{0} payments have been cleared", _accountPayments.MatchedPayments.Count)
                    };
                    CanClearChecks = true;
                    Messenger.Default.Send(new NotificationMessage(Notifications.BankAccountReconcileWizardCanFinish));
                    _accountPayments.LoadStats();
                }
                catch (Exception ex)
                {
                    Status = new StatusInfo
                    {
                        StatusMessage = "failed to clear payments",
                        ErrorMessage = ex.Message
                        //,ShowMessageBox = true
                    };
                }
            });

        }
        public async void MatchCheck()
        {
            await Task.Factory.StartNew(() =>
            {
                    // see if the BankFile.SelectedUnmatchedBankClearTransaction exists in unmatched payments if it does then add the payment to matched payments
                    // remove the transaction from the unmatched payments and transactions
                    try
                    {
                        var match =
                            (AccountPayments.OpenPayments.First(
                                s => s.check_num == BankFile.SelectedUnmatchedBankClearTransaction.SerialNumber));

                        AccountPayments.MatchedPayments.Add(match);
                        AccountPayments.OpenPayments.Remove(match);
                        Status = new StatusInfo
                        {
                            StatusMessage =
                                string.Format("{0} payment matched",
                                    BankFile.SelectedUnmatchedBankClearTransaction.SerialNumber)
                        };
                        BankFile.UnmatchedBankClearTransactions.Remove(BankFile.SelectedUnmatchedBankClearTransaction);
                        CalculateMatchStats();
                    }
                    catch (Exception e)
                    {
                        Status = new StatusInfo
                        {
                            StatusMessage =
                                string.Format("a matching payment was not found"),
                            ErrorMessage = e.Message

                        };
                    }
                });
        }

        public bool CanClearChecksFunc()
        {
            return CanClearChecks;
        }

        public async void GetPayments()
        {
         
                Status = new StatusInfo
            {
                ErrorMessage = "",
                IsBusy = true,
                StatusMessage = "getting payments..."
            };

            await AccountPayments.GetPayments();

            Status = new StatusInfo
            {
                ErrorMessage = "",
                IsBusy = true,
                StatusMessage = string.Format("loaded {0:n0} payments found totalling {1:c}",
                    AccountPayments.AllPayments.Count,
                    AccountPayments.AllPayments.Sum(x => x.tran_amount))
            };
            await Task.Factory.StartNew( () =>
            {
                try
                {
                    var query = from s in AccountPayments.AllPayments
                        join t in BankFile.BankClearTransactions on new
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

                    AccountPayments.OpenPayments = (AccountPayments.AllPayments.Where(
                        s => !BankFile.BankClearTransactions.Any(t => (t.SerialNumber == s.check_num)))).ToList();

                    BankFile.UnmatchedBankClearTransactions = new ObservableCollection<BankClearTransaction>(
                        (BankFile.BankClearTransactions.Where(s => !AccountPayments.ChecksToClear.Any(
                            t => (s.SerialNumber == t.CheckNum)))).ToList());

                    CalculateMatchStats();
                   
                    Messenger.Default.Send(new NotificationMessage<AccountPaymentsViewModel>(AccountPayments,
                        Notifications.ReconcileAccountPaymentsLoaded));
                    Messenger.Default.Send(new NotificationMessage<BankFileViewModel>(BankFile,
                        Notifications.ReconcileBankFileLoaded));
                    CanClearChecks = true;
                    ShowDetails = true;
                }
                catch (Exception e)
                {
                    Status = new StatusInfo
                    {
                        StatusMessage = "Error loading payments",
                        ErrorMessage = e.Message
                    };
                }
            });

            Status = new StatusInfo
            {
                ErrorMessage = "",
                StatusMessage = string.Format("Matching complete: Matched {0:n0} payments found totalling {1:c}",
                    AccountPayments.MatchedPayments.Count, AccountPayments.MatchedPayments.Sum(x => x.tran_amount))
            };
        }

        public bool ShowDetails
        {
            get { return _showDetails; }
            set { _showDetails = value; NotifyPropertyChanged(); }
        }

        private void CalculateMatchStats()
        {
            AccountPayments.Stats.Clear();
            AccountPayments.Stats.Add(new KeyValuePair<string, string>("Bank File First Transaction Date",
                string.Format("{0:d}", BankFile.FirstTransactionDate)));
            AccountPayments.Stats.Add(new KeyValuePair<string, string>("Bank File Last Transaction Date",
                string.Format("{0:d}", BankFile.LastTransactionDate)));
            AccountPayments.Stats.Add(new KeyValuePair<string, string>("Bank File Credits",
                string.Format("{0:c}", BankFile.CreditTransactionTotalDollars)));
            AccountPayments.Stats.Add(new KeyValuePair<string, string>("Bank File Debits",
                string.Format("{0:c}", BankFile.DebitTransactionTotalDollars)));
            AccountPayments.Stats.Add(new KeyValuePair<string, string>("Bank File Credit Transaction Cnt",
                string.Format("{0:n0}", BankFile.CreditTransactionCnt)));
            AccountPayments.Stats.Add(new KeyValuePair<string, string>("Bank File Debit Transaction Cnt",
                string.Format("{0:n0}", BankFile.DebitTransactionCnt)));
            AccountPayments.LoadStats();
            AccountPayments.Stats.Add(
                new KeyValuePair<string, string>("Bank File Unmatched Credit Transaction Cnt",
                    string.Format("{0:n0}", BankFile.UnmatchedCreditTransactionCnt)));
            AccountPayments.Stats.Add(new KeyValuePair<string, string>("Bank File Unmatched Credits",
                string.Format("{0:c}", BankFile.UnmatchedCreditTransactionTotalDollars)));
            AccountPayments.Stats.Add(
                new KeyValuePair<string, string>("Bank File Unmatched Debit Transaction Cnt",
                    string.Format("{0:n0}", BankFile.UnmatchedDebitTransactionCnt)));
            AccountPayments.Stats.Add(new KeyValuePair<string, string>("Bank File Unmatched Debits",
                string.Format("{0:c}", BankFile.UnmatchedDebitTransactionTotalDollars)));


        }

        //    public async void SaveReconciliationRptToExcel()
        //    {
        //        await Task.Factory.StartNew(() =>
        //        {
        //            var dtFmt = string.Format("{0:g}", DateTime.Now).Replace('/', '.');
        //            dtFmt = dtFmt.Replace(':', '.');
        //            dtFmt = dtFmt.Replace(' ', '.');

        //            try
        //            {
        //                if (AccountPayments.Stats.Count > 0)
        //                {
        //                    var statList = new StatsList();
        //                    foreach (var stat in AccountPayments.Stats)
        //                        statList.Add(new StatsType
        //                        {
        //                            Label = stat.Key,
        //                            Val = stat.Value
        //                        });

        //                    var s = new ExportToExcel<StatsType, StatsList>
        //                    {
        //                        ExcelSourceWorkbook = BankFile.FilePath + ".Reconcile",
        //                        ExcelWorksheetName = "Summary",
        //                        dataToPrint = statList
        //                    };
        //                    s.GenerateReport();


        //                    if (BankFile.UnmatchedBankClearTransactions.Count > 0)
        //                    {
        //                        var unmatchedSheet =
        //                            new ExportToExcel<BankClearTransaction, BankClearTransactionList>
        //                            {
        //                                ExcelSourceWorkbook = BankFile.FilePath + ".Reconcile",
        //                                ExcelWorksheetName = string.Format("Processed.{0}", dtFmt),
        //                                dataToPrint = BankFile.UnmatchedBankClearTransactions.ToList()
        //                            };
        //                        unmatchedSheet.GenerateReport();
        //                    }
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                Status = new StatusInfo
        //                {
        //                    StatusMessage = "export to excel failed",
        //                    ErrorMessage = e.Message //,ShowMessageBox = true
        //                };
        //            }
        //        });
        //    }

        #region DisplayState

        public void ResetState()
        {
            CanClearChecks = false;
        }


        private StatusInfo _status;
        private bool _canClearChecks;
        private bool _showDetails;

        #endregion

    }

    public class BankClearTransactionList : List<BankClearTransaction>
    {
    }
}