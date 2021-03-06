﻿using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Services;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.BankAccount
{
    public class AccountViewModel : ViewModelBase
    {
        private ObservableCollection<bank_account> _accounts;

        private bank_account _selectedAccount;

        public AccountViewModel()
        {
            ResetState();
        }

        public ObservableCollection<bank_account> Accounts
        {
            get { return _accounts; }
            set
            {
                _accounts = value;

                NotifyPropertyChanged("");
            }
        }

        public bank_account SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                _selectedAccount = value;
                NotifyPropertyChanged();
                if (SelectedAccount != null)
                {
                    Messenger.Default.Send(new NotificationMessage<bank_account>(SelectedAccount,
                        Notifications.SelectedAccountChanged));
                    Properties.Settings.Default.SelectedAccountID = value.account_id.ToString();
                    Properties.Settings.Default.Save();
                }
            }
        }

        private async void ResetState()
        {
            Accounts = new ObservableCollection<bank_account>(await BankAccountSvc.GetAccounts());
            //SelectedAccount = (from l in Accounts where l.IsDefault.GetValueOrDefault(false) == true select l).First();
            SelectedAccount =
                (from l in Accounts
                    where l.account_id == int.Parse(Properties.Settings.Default.SelectedAccountID)
                    select l).First();
        }
    }
}