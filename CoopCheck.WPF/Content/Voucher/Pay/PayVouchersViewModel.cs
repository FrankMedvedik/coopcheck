using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using CoopCheck.Library;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Reports.Services;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Voucher.Pay
{
    public class PayVouchersViewModel : ViewModelBase, IPayVouchersViewModel
    {
        private ObservableCollection<Models.BankAccount> _accounts;
        private bool _canPrintChecks;
        private bool _canSwiftPay;
        private int _currentPrintCount;
        private int _endingCheckNum;

        private bool _isBusy;
        private decimal _percentComplete;

        private Models.BankAccount _selectedAccount = new Models.BankAccount();

        private OpenBatch _selectedBatch = new OpenBatch();
        private BatchEdit _selectedBatchEdit;
        private int _startingCheckNum;


        private StatusInfo _status;
        private readonly IBankAccountSvc _bankAccountSvc;

        public PayVouchersViewModel(IBankAccountSvc bankAccountSvc )
        {
            _bankAccountSvc = bankAccountSvc;
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

        public ObservableCollection<Models.BankAccount> Accounts
        {
            get { return _accounts; }
            set
            {
                _accounts = value;
                NotifyPropertyChanged();
            }
        }

        public Models.BankAccount SelectedAccount
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
            var accnts = await _bankAccountSvc.GetAccounts();
            var accounts = new List<Models.BankAccount>();
            foreach (var a in accnts)
            {
                accounts.Add((Models.BankAccount)a);
            }

            Accounts = new ObservableCollection<Models.BankAccount>(accounts);
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
                    var v = PaymentSvc.PrintChecksAsync(SelectedAccount.account_id, SelectedBatch.batch_num,
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
                    var v = PaymentSvc.SwiftFulfillAsync(SelectedBatch.batch_num);
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