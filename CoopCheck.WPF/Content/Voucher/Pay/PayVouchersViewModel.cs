using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using CoopCheck.Domain.Contracts.Messages;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Domain.Services;
using CoopCheck.Library;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Reports.Contracts.Models;
using CoopCheck.WPF.Content.Interfaces;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Voucher.Pay
{
    public class PayVouchersViewModel : ViewModelBase, IPayVouchersViewModel
    {
        private ObservableCollection<BankAccount> _accounts;
        private bool _canPrintChecks;
        private bool _canSwiftPay;
        private int _currentPrintCount;
        private int _endingCheckNum;

        private bool _isBusy;
        private decimal _percentComplete;

        private BankAccount _selectedAccount = new BankAccount();

        private OpenBatch _selectedBatch = new OpenBatch();
        private BatchEdit _selectedBatchEdit;
        private int _startingCheckNum;


        private StatusInfo _status;
        private readonly IBankAccountSvc _bankAccountSvc;
        private readonly PaymentSvc _paymentSvc;


        public PayVouchersViewModel(IBankAccountSvc bankAccountSvc, PaymentSvc paymentSvc )
        {
            _bankAccountSvc = bankAccountSvc;
            _paymentSvc = paymentSvc;

            Messenger.Default.Register<NotificationMessage<OpenBatch>>(this, message =>
            {
                //if (message.Content != null)
                SelectedBatch = message.Content;
            });
            ResetState();
        }
        

  

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
            }
        }

        public ObservableCollection<BankAccount> Accounts
        {
            get { return _accounts; }
            set
            {
                _accounts = value;
                NotifyPropertyChanged();
            }
        }

        public BankAccount SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                _selectedAccount = value;
                NotifyPropertyChanged();
                SetAccountInfo();
            }
        }

        public int StartingCheckNum
        {
            get { return _startingCheckNum; }
            set
            {
                _startingCheckNum = value;
                NotifyPropertyChanged();
                EndingCheckNum = StartingCheckNum + SelectedBatchEdit.Vouchers.Count - 1;
            }
        }

        public int EndingCheckNum
        {
            get { return _endingCheckNum; }
            set
            {
                _endingCheckNum = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("CurrentPrintCount");
            }
        }

        public int CurrentPrintCount
        {
            get { return _currentPrintCount; }

            set
            {
                _currentPrintCount = value;
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

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                NotifyPropertyChanged();
                if (value)
                {
                    CanPrintChecks = false;
                    CanSwiftPay = false;
                }
            }
        }

        public bool CanPrintChecks
        {
            get { return _canPrintChecks; }
            set
            {
                _canPrintChecks = value;
                NotifyPropertyChanged();
            }
        }

        public decimal PercentComplete
        {
            get { return _percentComplete; }
            set
            {
                _percentComplete = value;
                NotifyPropertyChanged();
            }
        }

        public bool CanSwiftPay
        {
            get { return _canSwiftPay; }
            set
            {
                _canSwiftPay = value;
                NotifyPropertyChanged();
            }
        }

        public void RefreshBatchList()
        {
            Messenger.Default.Send(new NotificationMessage(Notifications.RefreshOpenBatchList));
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
            if (SelectedAccount.account_type == "CHECKING")
                StartingCheckNum = await BatchSvc.NextCheckNum(SelectedAccount.account_id);
        }

        private async void SetSelectedBatchEdit()
        {
            if (SelectedBatch != null)
            {
                SelectedBatchEdit = await BatchSvc.GetBatchEditAsync(SelectedBatch.batch_num);
                //SelectedBatchEdit = BatchSvc.GetBatchEdit(SelectedBatch.batch_num);
                SetAccountInfo();
            }
            else
            {
                CanPrintChecks = false;
                CanSwiftPay = false;
            }
        }


        public async void ResetState()
        {
            await GetAccounts();
            PercentComplete = 0;
        }

        private async Task GetAccounts()
        {

            var accounts =  await _bankAccountSvc.GetAccounts();
            Accounts = new ObservableCollection<BankAccount>(accounts);
            CurrentPrintCount = 0;
        }

        public async Task<PrintCheckProgress> PrintChecks(CancellationToken ctx)
        {
            PercentComplete = 0;
            CurrentPrintCount = 0;
            Status = new StatusInfo
            {
                ErrorMessage = "",
                IsBusy = true,
                StatusMessage = "printing checks..."
            };
            IsBusy = true;

            var progress = new Progress<PrintCheckProgress>();

            progress.ProgressChanged += (s, e) =>
            {
                PercentComplete = (int) e.ProgressPercentage;
                Status = e.Status;
                EndingCheckNum = e.CurrentCheckNum;
                CurrentPrintCount = EndingCheckNum - StartingCheckNum;
            };

            var result = await Task<PrintCheckProgress>.Factory.StartNew(() =>
            {
                try
                {
                    var v = _paymentSvc.PrintChecksAsync(SelectedAccount.account_id, SelectedBatch.batch_num,
                        StartingCheckNum, ctx, progress);
                    return v.Result;
                }
                catch (Exception e)
                {
                    return new PrintCheckProgress
                    {
                        Status = new StatusInfo
                        {
                            ErrorMessage = e.Message,
                            IsBusy = true,
                            StatusMessage = "checks failed to print"
                        },
                        CurrentCheckNum = 0,
                        ProgressPercentage = 100
                    };
                }
            });
            result.Status.IsBusy = false;
            return result;
        }


        public async void SwiftPay()
        {
            Status = new StatusInfo
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
                    var v = _paymentSvc.SwiftFulfillAsync(SelectedBatch.batch_num);
                    return v.Result;
                }
                catch (Exception e)
                {
                    Status = new StatusInfo
                    {
                        ErrorMessage = e.Message,
                        IsBusy = true,
                        StatusMessage = "swift Payment failed"
                    };
                    return Status;
                }
            });
            IsBusy = false;
        }
    }
}