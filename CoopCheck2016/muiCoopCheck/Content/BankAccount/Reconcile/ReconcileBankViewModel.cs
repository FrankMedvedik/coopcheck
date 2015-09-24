using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Library;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using CsvHelper;
using CsvHelper.Configuration;
using FirstFloor.ModernUI.Presentation;
using GalaSoft.MvvmLight.Messaging;
using LinqToExcel;

namespace CoopCheck.WPF.Content.BankAccount.Reconcile
{
    class ReconcileBankViewModel : ViewModelBase, IDataErrorInfo
    {
        private ObservableCollection<BankClearTransaction> _bankClearTransactions = new ObservableCollection<BankClearTransaction>();

        public ObservableCollection<BankClearTransaction> BankClearTransactions
        {
            get { return _bankClearTransactions; }
            set
            {
                _bankClearTransactions = value;
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
        public ReconcileBankViewModel()
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
            TransactionCnt = 0;
            TransactionTotalDollars = 0;
        }

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

        private void LoadCsv()
        {
            using (TextReader readFile = new StreamReader(FilePath))
            {
                var csv = new CsvReader(readFile);
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.RegisterClassMap(new BankClearTransactionMap());
                                BankClearTransactions =
                    new ObservableCollection<BankClearTransaction>(csv.GetRecords<BankClearTransaction>().ToList());
                TransactionCnt = BankClearTransactions.Count();
                TransactionTotalDollars = BankClearTransactions.Select(x => x.Amount).Sum();

            }
        }

        private string _filePath;

        private int _transactionCnt;
        public int TransactionCnt
        {
            get { return _transactionCnt; }
            set
            {
                _transactionCnt = value;
                NotifyPropertyChanged();
            }
        }


        private decimal _transactionTotalDollars;
        private StatusInfo _status;
        private bool _canImport;
        private bool _isBusy;
        private BatchEdit _selectedBatch;

        public decimal TransactionTotalDollars
        {
            get { return _transactionTotalDollars; }
            set
            {
                _transactionTotalDollars = value;
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


        private string _headerText;

        public string HeaderText
        {
            get { return _headerText; }
            set { _headerText = value; }
        }

        //public BatchEdit SelectedBatch
        //{
        //    get { return _selectedBatch; }
        //    set
        //    {
        //        _selectedBatch = value;
        //        NotifyPropertyChanged();
        //        if (SelectedBatch != null)
        //        {

        //            HeaderText = string.Format("Batch Number {0} Job Number {1}  Transaction Cnt {2} Total Amount {3:C}",
        //                SelectedBatch.Num, SelectedBatch.JobNum, SelectedBatch.Transactions.Count,
        //                SelectedBatch.Amount.GetValueOrDefault(0));
        //            Status = new StatusInfo()
        //            {
        //                StatusMessage = HeaderText
        //            };
        //        }
        //        else
        //            ShowSelectedBatch = false;
        //    }
        //}

        //private Boolean _showSelectedBatch;

        //public Boolean ShowSelectedBatch
        //{
        //    get { return _showSelectedBatch; }
        //    set
        //    {
        //        _showSelectedBatch = value;
        //        NotifyPropertyChanged();
        //    }
        //}
    }
}