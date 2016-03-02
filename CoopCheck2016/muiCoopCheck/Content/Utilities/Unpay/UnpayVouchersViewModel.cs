using System;
using System.Collections.ObjectModel;
using CoopCheck.Library;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Utilities.Unpay
{
    public class UnpayVouchersViewModel : ViewModelBase
    {
    
        private vwUnclearedBatch _selectedBatch = new vwUnclearedBatch();

        public void RefreshBatchList()
        {
            Messenger.Default.Send(new NotificationMessage(Notifications.RefreshOpenBatchList));
        }
        public vwUnclearedBatch SelectedBatch
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
                //if (SelectedBatchEdit == null)
                //    BatchInfo = "";
                //else
                //    BatchInfo = string.Format("{0} vouchers for job {1}", SelectedBatchEdit.Vouchers.Count,
                //        SelectedBatchEdit.JobNum);
                //if (SelectedAccount.account_type == "CHECKING")
                //    SetCheckNumbers();
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
                SetAccountInfo();
            }

        }

        private void SetAccountInfo()
        {
            if (SelectedAccount.account_type == "CHECKING")
            {
                SetCheckNumbers();
                CanUnprintChecks = true;
                CanUnswiftPay = false;

            }
            else if (SelectedAccount.account_type == "PROMOCODE")
            {
                CanUnprintChecks = false;
                CanUnswiftPay = true;
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
                CanUnprintChecks = false;
                CanUnswiftPay = false;
            }
        }

        public UnpayVouchersViewModel()
        {

            Messenger.Default.Register<NotificationMessage<vwUnclearedBatch>>(this, message =>
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

        private StatusInfo _status;

        private int _startingCheckNum;
        private int _endingCheckNum;
        private ObservableCollection<bank_account> _accounts;
        private string _batchInfo;
        private BatchEdit _selectedBatchEdit;
        private bool _canUnswiftPay;
        private bool _canUnprintChecks;

        public async void ResetState()
        {
            Accounts = new ObservableCollection<bank_account>(await BankAccountSvc.GetAccounts());

        }

        public Boolean CanUnprintChecks
        {
            get { return _canUnprintChecks; }
            set { _canUnprintChecks = value; NotifyPropertyChanged(); }
        }

        public Boolean CanUnswiftPay
        {
            get { return _canUnswiftPay; }
            set { _canUnswiftPay = value; NotifyPropertyChanged(); }
        }

        public async void UnprintChecks()
        {
            Status = new StatusInfo()
            {
                ErrorMessage = "",
                IsBusy = true,
                StatusMessage = "printing checks..."
            };
            try
            {
                Status =
                    await
                        PaymentSvc.UnprintChecksAsync(SelectedAccount.account_id, SelectedBatch.batch_num,
                            StartingCheckNum);
            }
            catch (Exception e)
            {
                Status = new StatusInfo()
                {
                    ErrorMessage = e.Message,
                    IsBusy = true,
                    StatusMessage = "checks failed to print"
                };
            }
        }


        public async void CancelSwiftPay()
        {
            Status = new StatusInfo()
            {
                ErrorMessage = "",
                IsBusy = true,
                StatusMessage = "executing swiftpay..."
            };
            try
            {
                Status = await PaymentSvc.CancelSwiftFulfillAsync(SelectedAccount.account_id, SelectedBatch.batch_num);
                RefreshBatchList();
            }
            catch (Exception e)
            {
                Status = new StatusInfo()
                {
                    ErrorMessage = e.Message,
                    IsBusy = true,
                    StatusMessage = "swift payment failed"
                };
            }

        }
    }
}