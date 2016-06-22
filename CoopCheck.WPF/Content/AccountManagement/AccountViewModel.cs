using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Domain.Contracts.Messages;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Reports.Contracts.Models;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.AccountManagement
{
    public class AccountViewModel : ViewModelBase, IAccountViewModel
    {
        private readonly IBankAccountSvc _bankAccountSvc;
        private ObservableCollection<BankAccount> _accounts;

        private BankAccount _selectedAccount;

        public AccountViewModel(IBankAccountSvc bankAccountSvc)
        {
            _bankAccountSvc = bankAccountSvc;
            ResetState();
        }

        public ObservableCollection<BankAccount> Accounts
        {
            get { return _accounts; }
            set
            {
                _accounts = value;

                NotifyPropertyChanged("");
            }
        }

        public BankAccount SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                _selectedAccount = value;
                NotifyPropertyChanged();
                if (SelectedAccount != null)
                {
                    Messenger.Default.Send(new NotificationMessage<BankAccount>(SelectedAccount,
                        Notifications.SelectedAccountChanged));
                    Properties.Settings.Default.SelectedAccountID = value.account_id.ToString();
                    Properties.Settings.Default.Save();
                }
            }
        }

        private async void ResetState()
        {
            var acnts = await _bankAccountSvc.GetAccounts();

            var accounts = new List<BankAccount>();
            foreach (var a in acnts)
                accounts.Add((BankAccount) a);
            Accounts = new ObservableCollection<BankAccount>(accounts);
            SelectedAccount =
                (from l in Accounts
                    where l.account_id == int.Parse(Properties.Settings.Default.SelectedAccountID)
                    select l).First();
        }
    }
}