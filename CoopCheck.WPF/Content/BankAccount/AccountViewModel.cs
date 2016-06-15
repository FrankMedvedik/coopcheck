using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Repository.Contracts.Interfaces;
using CoopCheck.WPF.Messages;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.BankAccount
{
    public class AccountViewModel : ViewModelBase
    {
        private ObservableCollection<Models.BankAccount> _accounts;

        private Models.BankAccount _selectedAccount;
        private readonly IBankAccountSvc _bankAccountSvc;
        public AccountViewModel(IBankAccountSvc bankAccountSvc)
        {
            _bankAccountSvc = bankAccountSvc;
            ResetState();
        }

        public ObservableCollection<Models.BankAccount> Accounts
        {
            get { return _accounts; }
            set
            {
                _accounts = value;

                NotifyPropertyChanged("");
            }
        }

        public Models.BankAccount SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                _selectedAccount = value;
                NotifyPropertyChanged();
                if (SelectedAccount != null)
                {
                    Messenger.Default.Send(new NotificationMessage<Ibank_account>(SelectedAccount,
                        Notifications.SelectedAccountChanged));
                    Properties.Settings.Default.SelectedAccountID = value.account_id.ToString();
                    Properties.Settings.Default.Save();
                }
            }
        }

        private async void ResetState()
        {
            var acnts = await _bankAccountSvc.GetAccounts();

            var accounts = new List<Models.BankAccount>();
            foreach (var a in acnts)
                accounts.Add((Models.BankAccount) a);
            Accounts = new ObservableCollection<Models.BankAccount>(accounts);
            SelectedAccount =
                (from l in Accounts
                    where l.account_id == int.Parse(Properties.Settings.Default.SelectedAccountID)
                    select l).First();
        }
    }
}