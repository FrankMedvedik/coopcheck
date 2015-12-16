using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.ViewModel;
using CsvHelper;
using CsvHelper.Configuration;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.BankAccount.Reconcile
{
    public class BankFileViewModel : ViewModelBase, IDataErrorInfo
    {
        private ObservableCollection<BankClearTransaction> _bankClearTransactions = new ObservableCollection<BankClearTransaction>();
        private ObservableCollection<BankClearTransaction> _unmatchedBankClearTransactions = new ObservableCollection<BankClearTransaction>();

        public ObservableCollection<BankClearTransaction> BankClearTransactions
        {
            get { return _bankClearTransactions; }
            set
            {
                _bankClearTransactions = value;
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

        private string DefaultFilePath = "Select a bank file";
        public BankFileViewModel()
        {
            ResetState();
            FilePath = DefaultFilePath;
            Status = new StatusInfo()
            {
                StatusMessage = "Please select the bank file",
            };
        }

        #region DisplayState

        private bool _canReconcile = false;

        public void ResetState()
        {
            CanReconcile = false;
            CanImport = true;
            BankClearTransactions = new ObservableCollection<BankClearTransaction>();
            DebitTransactionCnt = 0;
            CreditTransactionCnt = 0;
            CreditTransactionTotalDollars = 0;
            DebitTransactionTotalDollars = 0;
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
                    var p = new PaymentReportCriteria()
                    {
                        StartDate = FirstTransactionDate.GetValueOrDefault(DateTime.Now.AddDays(-30)),
                        EndDate = LastTransactionDate.GetValueOrDefault(DateTime.Now)
                    };
                    Messenger.Default.Send(new NotificationMessage<BankFileViewModel>(this,
                        Notifications.ReconcileBankFileLoaded));
                }
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
                CreditTransactionCnt = BankClearTransactions.Where(x=>x.CRDR == "CR").Count();
                CreditTransactionTotalDollars = BankClearTransactions.Where(x => x.CRDR == "CR").Select(x => x.Amount).Sum();
                DebitTransactionCnt = BankClearTransactions.Where(x => x.CRDR == "DR").Count();
                DebitTransactionTotalDollars = BankClearTransactions.Where(x => x.CRDR == "DR").Select(x => x.Amount).Sum();
                FirstTransactionDate = BankClearTransactions.Min(x => x.PostDate);
                LastTransactionDate = BankClearTransactions.Max(x => x.PostDate);
                CanReconcile = true;
            }
        }

        public DateTime? LastTransactionDate
        {
            get { return _lastTransactionDate; }
            set { _lastTransactionDate = value; NotifyPropertyChanged(); }
        }

        public DateTime? FirstTransactionDate
        {   
            get { return _firstTransactionDate; }
            set { _firstTransactionDate = value; NotifyPropertyChanged(); }
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
        private bool _isBusy;
        private string _headerText;
        private DateTime? _firstTransactionDate;
        private DateTime? _lastTransactionDate;


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
                StatusInfo s = new StatusInfo()
                {
                    ErrorMessage = value,
                    StatusMessage = "Error"
                };
                Status = s;
            }
        }

 
        public string HeaderText
        {
            get { return _headerText; }
            set { _headerText = value; }
        }

        
    }
}