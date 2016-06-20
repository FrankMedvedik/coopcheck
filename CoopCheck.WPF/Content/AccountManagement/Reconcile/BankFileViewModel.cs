using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.AccountManagement.Reconcile
{
    public class BankFileViewModel : ViewModelBase, IDataErrorInfo, IBankFileViewModel
    {
        private readonly string DefaultFilePath = "Select a bank file";

        private ObservableCollection<BankClearTransaction> _bankClearTransactions =
            new ObservableCollection<BankClearTransaction>();

        private ObservableCollection<BankClearTransaction> _unmatchedBankClearTransactions =
            new ObservableCollection<BankClearTransaction>();

        public BankFileViewModel()
        {
            ResetState();
            FilePath = DefaultFilePath;
            Status = new StatusInfo
            {
                StatusMessage = "Please select the bank file"
            };
        }

        public ObservableCollection<BankClearTransaction> BankClearTransactions
        {
            get { return _bankClearTransactions; }
            set
            {
                _bankClearTransactions = value;
                NotifyPropertyChanged();
                if (BankClearTransactions.Any())
                {
                    CreditTransactionCnt = BankClearTransactions.Count(x => x.CRDR == "CR");
                    CreditTransactionTotalDollars =
                        BankClearTransactions.Where(x => x.CRDR == "CR").Select(x => x.Amount).Sum();
                    DebitTransactionCnt = BankClearTransactions.Count(x => x.CRDR == "DR");
                    DebitTransactionTotalDollars =
                        BankClearTransactions.Where(x => x.CRDR == "DR").Select(x => x.Amount).Sum();
                    FirstTransactionDate = BankClearTransactions.Min(x => x.PostDate);
                    LastTransactionDate = BankClearTransactions.Max(x => x.PostDate);
                    CanReconcile = true;
                }
            }
        }

        public BankClearTransaction SelectedUnmatchedBankClearTransaction
        {
            get { return _selectedUnmatchedBankClearTransaction; }
            set
            {
                _selectedUnmatchedBankClearTransaction = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<BankClearTransaction> UnmatchedBankClearTransactions
        {
            get { return _unmatchedBankClearTransactions; }
            set
            {
                _unmatchedBankClearTransactions = value;
                NotifyPropertyChanged();
                if (UnmatchedBankClearTransactions.Any())
                {
                    UnmatchedCreditTransactionCnt = UnmatchedBankClearTransactions.Count(x => x.CRDR == "CR");
                    UnmatchedCreditTransactionTotalDollars =
                        UnmatchedBankClearTransactions.Where(x => x.CRDR == "CR").Select(x => x.Amount).Sum();
                    UnmatchedDebitTransactionCnt = UnmatchedBankClearTransactions.Count(x => x.CRDR == "DR");
                    UnmatchedDebitTransactionTotalDollars =
                        UnmatchedBankClearTransactions.Where(x => x.CRDR == "DR").Select(x => x.Amount).Sum();
                }
            }
        }

        public decimal UnmatchedDebitTransactionTotalDollars
        {
            get { return _unmatchedDebitTransactionTotalDollars; }
            set
            {
                _unmatchedDebitTransactionTotalDollars = value;
                NotifyPropertyChanged();
            }
        }

        public int UnmatchedDebitTransactionCnt
        {
            get { return _unmatchedDebitTransactionCnt; }
            set
            {
                _unmatchedDebitTransactionCnt = value;
                NotifyPropertyChanged();
            }
        }

        public decimal UnmatchedCreditTransactionTotalDollars
        {
            get { return _unmatchedCreditTransactionTotalDollars; }
            set
            {
                _unmatchedCreditTransactionTotalDollars = value;
                NotifyPropertyChanged();
            }
        }

        public int UnmatchedCreditTransactionCnt
        {
            get { return _unmatchedCreditTransactionCnt; }
            set
            {
                _unmatchedCreditTransactionCnt = value;
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

        public string this[string columnName]
        {
            get
            {
                if (columnName == "FilePath")
                {
                    if (string.IsNullOrEmpty(FilePath)) return "Select a bank file";
                    if (!File.Exists(FilePath)) return "Invalid file name";
                }
                return null;
            }
        }

        public string Error
        {
            get { return Status.ErrorMessage; }
            set
            {
                var s = new StatusInfo
                {
                    ErrorMessage = value,
                    StatusMessage = "Error"
                };
                Status = s;
            }
        }

        #region DisplayState

        private bool _canReconcile;

        public void ResetState()
        {
            CanReconcile = false;
            CanImport = true;
            BankClearTransactions = new ObservableCollection<BankClearTransaction>();
            UnmatchedBankClearTransactions = new ObservableCollection<BankClearTransaction>();
            DebitTransactionCnt = 0;
            CreditTransactionCnt = 0;
            CreditTransactionTotalDollars = 0;
            DebitTransactionTotalDollars = 0;
            UnmatchedDebitTransactionCnt = 0;
            UnmatchedCreditTransactionCnt = 0;
            UnmatchedCreditTransactionTotalDollars = 0;
            UnmatchedDebitTransactionTotalDollars = 0;
            FirstTransactionDate = null;
            LastTransactionDate = null;
        }

        public bool CanReconcile
        {
            get { return _canReconcile; }
            set
            {
                _canReconcile = value;
                NotifyPropertyChanged();
                if (CanReconcile)
                {
                    Messenger.Default.Send(new NotificationMessage<BankFileViewModel>(this,
                        Notifications.ReconcileBankFileLoaded));
                }
            }
        }

        public bool CanImport
        {
            get { return _canImport; }
            set
            {
                _canImport = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Workbook

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                NotifyPropertyChanged();
                if (value != DefaultFilePath)
                {
                    LoadCsv();
                }
            }
        }

        public sealed class BankClearTransactionMap : CsvClassMap<BankClearTransaction>
        {
            public BankClearTransactionMap()
            {
                Map(m => m.AccountDesignator).Name("Account Designator");
                Map(m => m.RawAmount).Name("Amount");
                Map(m => m.CRDR).Name("CR/DR");
                Map(m => m.Description).Name("Description");
                Map(m => m.SerialNumber).Name("Serial Number");
                Map(m => m.PostDate).Name("Posted Date");
            }
        }

        public void LoadCsv()
        {
            using (TextReader readFile = new StreamReader(FilePath))
            {
                var csv = new CsvReader(readFile);
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.RegisterClassMap(new BankClearTransactionMap());
                BankClearTransactions =
                    new ObservableCollection<BankClearTransaction>(csv.GetRecords<BankClearTransaction>().ToList());
            }
        }

        public DateTime? LastTransactionDate
        {
            get { return _lastTransactionDate; }
            set
            {
                _lastTransactionDate = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime? FirstTransactionDate
        {
            get { return _firstTransactionDate; }
            set
            {
                _firstTransactionDate = value;
                NotifyPropertyChanged();
            }
        }

        private string _filePath;

        private int _debitTransactionCnt;

        public int DebitTransactionCnt
        {
            get { return _debitTransactionCnt; }
            set
            {
                _debitTransactionCnt = value;
                NotifyPropertyChanged();
            }
        }

        private int _creditTransactionCnt;

        public int CreditTransactionCnt
        {
            get { return _creditTransactionCnt; }
            set
            {
                _creditTransactionCnt = value;
                NotifyPropertyChanged();
            }
        }

        private decimal _debitTransactionTotalDollars;
        private decimal _creditTransactionTotalDollars;
        private StatusInfo _status;
        private bool _canImport;
        private DateTime? _firstTransactionDate;
        private DateTime? _lastTransactionDate;
        private int _unmatchedCreditTransactionCnt;
        private decimal _unmatchedCreditTransactionTotalDollars;
        private int _unmatchedDebitTransactionCnt;
        private decimal _unmatchedDebitTransactionTotalDollars;
        private BankClearTransaction _selectedUnmatchedBankClearTransaction;

        public decimal CreditTransactionTotalDollars
        {
            get { return _creditTransactionTotalDollars; }
            set
            {
                _creditTransactionTotalDollars = value;
                NotifyPropertyChanged();
            }
        }

        public decimal DebitTransactionTotalDollars
        {
            get { return _debitTransactionTotalDollars; }
            set
            {
                _debitTransactionTotalDollars = value;
                NotifyPropertyChanged();
            }
        }

        #endregion
    }
}