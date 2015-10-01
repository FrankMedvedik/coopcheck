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

        public ObservableCollection<KeyValuePair<String, Decimal>> Stats = new ObservableCollection<KeyValuePair<string, decimal>>();

        public void AddStat(String key, Decimal val)
        {
            Stats.Add(new KeyValuePair<string, decimal>(key, val));
        }

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
                    var task = RptSvc.GetPaymentReconcileReport(FilePaymentReportCriteria);
                    AllPayments = new List<vwPayment>(task.Result);

                    ClosedPayments =
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
        private List<vwPayment> _closedPayments = new List<vwPayment>();
        private List<vwPayment> _openPayments = new List< vwPayment >();
        private PaymentReportCriteria _filePaymentReportCriteria;

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

        public List<vwPayment> ClosedPayments
        {
            get { return _closedPayments; }
            set
            {
                _closedPayments = value;
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

        public PaymentReportCriteria FilePaymentReportCriteria
        {
            get { return _filePaymentReportCriteria; }
            set { _filePaymentReportCriteria = value;
                RefreshAll();
                NotifyPropertyChanged(); }
        }
    }
}