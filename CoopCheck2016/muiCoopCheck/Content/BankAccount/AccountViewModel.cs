using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CoopCheck.Library;
using CoopCheck.Repository;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using FirstFloor.ModernUI.Presentation;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.BankAccount
{
    public class AccountViewModel : WPF.ViewModel.ViewModelBase {
    

        private ObservableCollection<bank_account>  _accounts;
        public ObservableCollection<bank_account> Accounts
        {
            get { return _accounts; }
            set
            {
                _accounts = value;
                 
                NotifyPropertyChanged("");
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

        public  AccountViewModel()
        {
            ResetState();
            HeaderText = String.Format("Account");
        }


        private StatusInfo _status;
        private async void ResetState()
        {
            var s = new StatusInfo()
            {
                StatusMessage = "",
                ErrorMessage = ""
            };
            Status = s;
            CanProceed = false;
            Accounts = new ObservableCollection<bank_account>(await BankAccountSvc.GetAccounts());
        }

        private bool _canProceed;
        public bool CanProceed
        {
            get { return _canProceed; }
            set{
                _canProceed = value;
                NotifyPropertyChanged();
            }

        }

        private bank_account _selectedBankAccount;
        public bank_account SelectedBankAccount
        {
            get { return _selectedBankAccount; }
            set
            {
                _selectedBankAccount = value;
                NotifyPropertyChanged();
                SelectedAccount = Account.GetAccount(SelectedBankAccount.account_id);
                HeaderText = String.Format("{0} Details", SelectedBankAccount.account_name);
            }

        }

        private Account _selectedAccount;
        public Account  SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                _selectedAccount = value;
                NotifyPropertyChanged();
            }
        }

        private string _headerText;
        public string HeaderText
        {
            get { return _headerText; }
            set
            {
                _headerText = value;
                NotifyPropertyChanged();
            }
        }


    }

}