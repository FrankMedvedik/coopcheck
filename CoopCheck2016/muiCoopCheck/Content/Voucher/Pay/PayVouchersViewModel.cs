using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CoopCheck.Library;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Voucher.Pay
{
    public class PayVouchersViewModel : ViewModelBase
    {
    
        private vwOpenBatch _selectedBatch = new vwOpenBatch();

        public void RefreshBatchList()
        {
            Messenger.Default.Send(new NotificationMessage(Notifications.RefreshOpenBatchList));
        }
        public vwOpenBatch SelectedBatch
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
                SetAccountInfo();
            }

        }

        private void SetAccountInfo()
        {
            if (SelectedAccount.account_type == "CHECKING")
            {
                SetCheckNumbers();
                CanPrintChecks = true;
                CanSwiftPay = false;

            }
            else if (SelectedAccount.account_type == "PROMOCODE")
            {
                CanPrintChecks = false;
                CanSwiftPay = true;
            }
        }
        private async void SetCheckNumbers()
        {
            StartingCheckNum = await BatchSvc.NextCheckNum(SelectedAccount.account_id);
        }

        private async void SetSelectedBatchEdit()
        {
            if (SelectedBatch != null)
            {
                SelectedBatchEdit = await BatchSvc.GetBatchEditAsync(SelectedBatch.batch_num);
                SetAccountInfo();
            }
            else
            {
                CanPrintChecks = false;
                CanSwiftPay = false;
            }
        }

        public PayVouchersViewModel()
        {

            Messenger.Default.Register<NotificationMessage<vwOpenBatch>>(this, message =>
            {
                //if (message.Content != null)
                    SelectedBatch = message.Content;
            });
            ResetState();
            
        }

        public int StartingCheckNum
        {
            get { return _startingCheckNum; }
            set
            {
                _startingCheckNum = value;
                NotifyPropertyChanged();
                EndingCheckNum = StartingCheckNum + SelectedBatchEdit.Vouchers.Count-1;
            }
        }
        public int EndingCheckNum
        {
            get { return _endingCheckNum; }
            set
            {
                _endingCheckNum = value;
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

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                NotifyPropertyChanged();
                if (value)
                {
                    //_dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                    //_dispatcherTimer.Tick += (dispatcherTimer_Tick);
                    //_dispatcherTimer.Interval = new TimeSpan(0, 5, 0);
                    //_dispatcherTimer.Start();
                    CanPrintChecks = false;
                    CanSwiftPay = false;
                }
                //else
                //{
                //    //_dispatcherTimer.Stop();
                //    //_dispatcherTimer.Tick -= (dispatcherTimer_Tick);
                //    SetAccountInfo();
                //}
                
            }
        }


        //private int _remainingPaymentCnt;
        //public int RemainingPaymentCnt
        //{
        //    get { return _remainingPaymentCnt; }
        //    set
        //    {
        //        _remainingPaymentCnt = value;
        //        NotifyPropertyChanged();
        //    }
        //}


        private StatusInfo _status;

        private int _startingCheckNum;
        private int _endingCheckNum;
        private ObservableCollection<bank_account> _accounts;
        private BatchEdit _selectedBatchEdit;
        private bool _canSwiftPay;
        private bool _canPrintChecks;


        public async void ResetState()
        {
            Accounts = new ObservableCollection<bank_account>(await BankAccountSvc.GetAccounts());

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

        public async Task<StatusInfo> PrintChecks()
        {
            Status = new StatusInfo()
            {
                ErrorMessage = "",
                IsBusy = true,
                StatusMessage = "printing checks..."
            };
            IsBusy = true;

            Status = await Task.Factory.StartNew( () =>
            {
                try
                {
                    var v = PaymentSvc.PrintChecksAsync(SelectedAccount.account_id, SelectedBatch.batch_num,StartingCheckNum);
                     return v.Result;
                }
                catch (Exception e)
                {
                    return new StatusInfo()
                    {
                        ErrorMessage = e.Message,
                        IsBusy = true,
                        StatusMessage = "checks failed to print"
                    };
                }
            });
            IsBusy = false;
            return Status;
        }


        public async void SwiftPay()
        {
            Status = new StatusInfo()
            {
                ErrorMessage = "",
                IsBusy = true,
                StatusMessage = "executing swiftpay..."
            };
            IsBusy = true;
            Status = await Task<StatusInfo>.Factory.StartNew(() =>
            {
                try
                {
                    var v = PaymentSvc.SwiftFulfillAsync(SelectedAccount.account_id, SelectedBatch.batch_num);
                    return v.Result;
                }
                catch (Exception e)
                {
                    Status = new StatusInfo()
                    {
                        ErrorMessage = e.Message,
                        IsBusy = true,
                        StatusMessage = "swift payment failed"
                    };
                    return Status;
                }
            });
            IsBusy = false;

        }
    }
    }
