using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Services;
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

        public  AccountViewModel()
        {
            ResetState();
        }
        private async void ResetState()
        {
            Accounts = new ObservableCollection<bank_account>(await BankAccountSvc.GetAccounts());
            SelectedAccount = (from l in Accounts where l.IsDefault.GetValueOrDefault(false) == true select l).First();
        }

        private bank_account _selectedAccount;
        public bank_account SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                _selectedAccount = value;
                NotifyPropertyChanged();
                if( SelectedAccount != null)
                    Messenger.Default.Send(new NotificationMessage<bank_account>(SelectedAccount, Notifications.SelectedAccountChanged));
            }
        }

    }

}