using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Library;
using CoopCheck.Repository;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using CsvHelper;
using CsvHelper.Configuration;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.BankAccount.Reconcile
{
    internal class ReconcileBankViewModel : ViewModelBase  
    {
        private BankFileViewModel _bankFileViewModel = new BankFileViewModel();
        private bool _showGridData;

        private ObservableCollection<KeyValuePair<String, String>> _stats = new ObservableCollection<KeyValuePair<string, string>>();

        public ObservableCollection<KeyValuePair<String, String>> Stats
        {
            get { return _stats; }
            set { _stats = value;
                NotifyPropertyChanged();
            }
        }

   

        private int _allTransactionCnt;
        public int AllTransactionCnt
        {
            get { return _allTransactionCnt; }
            set
            {
                _allTransactionCnt = value;
                NotifyPropertyChanged();
            }
        }

        private decimal _allTransactionAmt;
        public Decimal AllTransactionAmt
        {
            get { return _allTransactionAmt; }
            set
            {
                _allTransactionAmt = value;
                NotifyPropertyChanged();
            }
        }

        private int _openTransactionCnt;
        public int OpenTransactionCnt
        {
            get { return _openTransactionCnt; }
            set
            {
                _openTransactionCnt = value;
                NotifyPropertyChanged();
            }
        }

        private decimal _openTransactionAmt;
        public Decimal OpenTransactionAmt
        {
            get { return _openTransactionAmt; }
            set
            {
                _openTransactionAmt = value;
                NotifyPropertyChanged();
            }
        }

        private int _matchedTransactionCnt;
        public int MatchedTransactionCnt
        {
            get { return _matchedTransactionCnt; }
            set
            {
                _matchedTransactionCnt = value;
                NotifyPropertyChanged();
            }
        }

        private decimal _matchedTransactionAmt;
        public Decimal MatchedTransactionAmt
        {
            get { return _matchedTransactionAmt; }
            set
            {
                _matchedTransactionAmt = value;
                NotifyPropertyChanged();
            }
        }

        private string _filePath;


        public BankFileViewModel BankFileViewModel
        {
            get { return _bankFileViewModel; }
            set
            {
                _bankFileViewModel = value;
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
            try
            {
                WorkPayments = await Task<List<vwPayment>>.Factory.StartNew(() =>
                {
                    var task = RptSvc.GetPaymentReconcileReport(PaymentReportCriteria);
                    AllPayments = new List<vwPayment>(task.Result);

                    MatchedPayments =
                        (from s in AllPayments
                            where
                                BankFileViewModel.BankClearTransactions.Any(
                                    t => (t.SerialNumber == s.check_num) )
                            select s).ToList();

                    //ClosedPayments = new List<vwPayment>(v);

                    OpenPayments = (from s in AllPayments
                        where
                            !BankFileViewModel.BankClearTransactions.Any(
                                t => (t.SerialNumber == s.check_num.ToString()))
                        select s).ToList();

                    BankFileViewModel.UnmatchedBankClearTransactions = new ObservableCollection<BankClearTransaction>(
                         (from s in BankFileViewModel.BankClearTransactions
                          where
                                 !AllPayments.Any(
                                     t => (s.SerialNumber == t.check_num))
                          select s).ToList());

                    return new List<vwPayment>(task.Result);
                });
                LoadStats();

            }
            catch (Exception e)
            {
                Status = new StatusInfo()
                {
                    StatusMessage = "Error loading payments",
                    ErrorMessage = e.Message
                };

            }
        }

        private void LoadStats()
        {
            MatchedTransactionAmt = MatchedPayments.Sum(p => p.tran_amount).GetValueOrDefault(0);
            MatchedTransactionCnt = MatchedPayments.Count;

            OpenTransactionAmt = OpenPayments.Sum(p => p.tran_amount).GetValueOrDefault(0);
            OpenTransactionCnt = OpenPayments.Count;

            AllTransactionAmt = AllPayments.Sum(p => p.tran_amount).GetValueOrDefault(0);
            AllTransactionCnt = AllPayments.Count;

            LoadStatsIntoCollection();
        }

        private void LoadStatsIntoCollection()
        {
            ObservableCollection<KeyValuePair<String, String>> s =
                new ObservableCollection<KeyValuePair<string, string>>();
            // bank file stats
            s.Add(new KeyValuePair<string, string>("Bank File First Transaction Date", BankFileViewModel.FirstTransactionDate.ToString()));
            s.Add(new KeyValuePair<string, string>("Bank File Last Transaction Date", BankFileViewModel.LastTransactionDate.ToString()));
            s.Add(new KeyValuePair<string, string>("Bank File Credit $", BankFileViewModel.CreditTransactionTotalDollars.ToString()));
            s.Add(new KeyValuePair<string, string>("Bank File Debit $", BankFileViewModel.DebitTransactionTotalDollars.ToString()));
            s.Add(new KeyValuePair<string, string>("Bank File Credit Transaction Cnt", BankFileViewModel.CreditTransactionCnt.ToString()));
            s.Add(new KeyValuePair<string, string>("Bank File Debit Transaction Cnt", BankFileViewModel.DebitTransactionCnt.ToString()));

            s.Add(new KeyValuePair<string, string>("Total Payments Cnt", AllTransactionCnt.ToString()));
            s.Add(new KeyValuePair<string, string>("Total Payments $", AllTransactionAmt.ToString()));

            s.Add(new KeyValuePair<string, string>("Matched Payments Cnt", MatchedTransactionCnt.ToString()));
            s.Add(new KeyValuePair<string, string>("Matched Payments $", MatchedTransactionAmt.ToString()));

            s.Add(new KeyValuePair<string, string>("Open Payments Cnt", OpenTransactionCnt.ToString()));
            s.Add(new KeyValuePair<string, string>("Open Payments $", OpenTransactionAmt.ToString()));
            Stats = s;
        }


        public async void RefreshAll()
        {
              GetPayments();
        }
        #region DisplayState

        private bool _canReconcile = false;

        public void ResetState()
        {
            CanReconcile = false;
            //BankClearTransactions = new ObservableCollection<BankClearTransaction>();
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

        private StatusInfo _status;
        private bool _canImport;
        private bool _isBusy;

        #endregion

        public string this[string columnName]
        {
            get
            {
                //if (columnName == "FilePath")
                //{
                //    if (string.IsNullOrEmpty(FilePath)) return "Select a bank file";
                //    if (!File.Exists(FilePath)) return "Invalid file name";
                //}
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

        private List<vwPayment> _allPayments = new List< vwPayment >();
       private List<vwPayment> _workPayments;
        private List<vwPayment> _matchedPayments = new List<vwPayment>();
        private List<vwPayment> _openPayments = new List< vwPayment >();
        private PaymentReportCriteria _paymentReportCriteria;

        public List<vwPayment> WorkPayments
        {
            get { return _workPayments; }
            set
            {
                _workPayments = value;
                //AllPayments = new ObservableCollection<vwPayment>(WorkPayments);
            }
        }

        public List<vwPayment> AllPayments
        {
            get { return _allPayments; }
            set
            {
                _allPayments = value;
                NotifyPropertyChanged();
                ShowGridData = true;
                Status = new StatusInfo()
                {
                    StatusMessage = "payments loaded"
                };
            }
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

        public PaymentReportCriteria PaymentReportCriteria
        {
            get { return _paymentReportCriteria; }
            set { _paymentReportCriteria = value;
                RefreshAll();
                NotifyPropertyChanged(); }
        }
    }
}