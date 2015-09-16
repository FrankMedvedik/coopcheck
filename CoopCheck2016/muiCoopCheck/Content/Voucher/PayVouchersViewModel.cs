using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CoopCheck.Library;
using CoopCheck.Repository;
using CoopCheck.WPF.Content.Voucher.Import;
using CoopCheck.WPF.Converters;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Voucher
{
    public class PayVouchersViewModel : ViewModelBase
    {
        private ObservableCollection<OpenBatch> _openBatches;

        public ObservableCollection<OpenBatch> OpenBatches
        {
            get { return _openBatches; }
            set
            {
                _openBatches = value;
                NotifyPropertyChanged();
            }
        }

        private OpenBatch _selectedBatch = new OpenBatch();

        public OpenBatch SelectedBatch
        {
            get { return _selectedBatch; }
            set
            {
                _selectedBatch = value;
                NotifyPropertyChanged();
                SetSelectedBatchEdit();
            }
        }

        public BatchEdit SelectedBatchEdit
        {
            get { return _selectedBatchEdit; }
            set
            {
                _selectedBatchEdit = value;
                NotifyPropertyChanged();
                BatchInfo = String.Format("{0} vouchers for job {1}", SelectedBatchEdit.Vouchers.Count,
                    SelectedBatchEdit.JobNum);
            }
        }

        public string BatchInfo
        {
            get { return _batchInfo; }
            set
            {
                _batchInfo = value;
                NotifyPropertyChanged();
            }
        }


        public ObservableCollection<bank_account> Accounts
        {
            get { return _accounts; }
            set
            {
                _accounts = value;
                NotifyPropertyChanged();
            }
        }

        private bank_account _selectedAccount = new bank_account();

        public bank_account SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                _selectedAccount = value;
                NotifyPropertyChanged();
                if (SelectedAccount.account_type == "CHECKING")
                {
                    SetStartingCheckNum();
                    CanPrintChecks = true;
                    CanSwiftPay = false;

                }
                else if (SelectedAccount.account_type == "PROMOCODE")
                {
                    CanPrintChecks = false;
                    CanSwiftPay = true;
                }
            }

        }


        //private ObservableCollection<PaymentMethod> _paymentMethods;
        //public ObservableCollection<PaymentMethod> PaymentMethods
        //{
        //    get { return _paymentMethods; }
        //    set
        //    {
        //        _paymentMethods = value;
        //        NotifyPropertyChanged();
        //    }
        //}

        //private PaymentMethod _selectedPaymentMethod = new PaymentMethod();
        //public PaymentMethod SelectedPaymentMethod
        //{
        //    get { return _selectedPaymentMethod; }
        //    set
        //    {
        //        _selectedPaymentMethod = value;
        //        ShowCheckInfo = (SelectedPaymentMethod.Key == _check); 
        //        NotifyPropertyChanged();

        //    }
        //}
        private async void SetStartingCheckNum()
        {
            StartingCheckNum = await BatchSvc.NextCheckNum(SelectedAccount.account_id);
        }

        private async void SetSelectedBatchEdit()
        {
            SelectedBatchEdit = await BatchSvc.GetBatchEditAsync(SelectedBatch.batch_num);
        }

        //public ICommand PayCommand
        //{
        //    get
        //    {
        //        if (_cmdPay == null)
        //            _cmdPay = new 
        //    return _cmdPay;
        //    }
        //}

        public PayVouchersViewModel()
        {
            var s = new StatusInfo()
            {
                StatusMessage = "click select file to choose a new excel file to import voucher from",
                ErrorMessage = "No Errors"
            };

            Status = s;
            ResetState();
        }

        public int StartingCheckNum
        {
            get { return _startingCheckNum; }
            set
            {
                _startingCheckNum = value;
                NotifyPropertyChanged();
            }
        }

        private void HandleNotification(NotificationMessage message)
        {
            if (message.Notification == Notifications.ImportCannotProceed)
            {
                CanProceed = false;
            }
            if (message.Notification == Notifications.ImportCanProceed)
            {
                CanProceed = true;
            }
        }

        public bool CanProceed
        {
            get { return _canProceed; }
            set
            {
                _canProceed = value;
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

        private StatusInfo _status;

        private bool _canProceed;
        private int _startingCheckNum;
        private ObservableCollection<bank_account> _accounts;
        private string _batchInfo;
        private BatchEdit _selectedBatchEdit;
        private bool _canSwiftPay;
        private bool _canPrintChecks;

        public async void ResetState()
        {
            Accounts = new ObservableCollection<bank_account>(await BankAccountSvc.GetAccounts());
            OpenBatches = new ObservableCollection<OpenBatch>(await OpenBatchSvc.GetOpenBatches());

        }

        public Boolean CanPrintChecks
        {
            get { return _canPrintChecks; }
            set { _canPrintChecks = value; NotifyPropertyChanged(); }
        }

        public Boolean CanSwiftPay
        {
            get { return _canSwiftPay; }
            set { _canSwiftPay = value; NotifyPropertyChanged(); }
        }

        public async void PrintChecks()
        {
            Status = new StatusInfo()
            {
                ErrorMessage = "",
                IsBusy = true,
                StatusMessage = "printing checks"
            };

            Status = await PaymentSvc.PrintChecksAsync(SelectedAccount.account_id, SelectedBatch.batch_num, StartingCheckNum);
        }


        public async void SwiftPay()
        {
            Status = new StatusInfo()
            {
                ErrorMessage = "",
                IsBusy = true,
                StatusMessage = "executing swiftpay"
            };

            Status = await PaymentSvc.SwiftFulfillAsync(SelectedAccount.account_id, SelectedBatch.batch_num);
        }
    }
}