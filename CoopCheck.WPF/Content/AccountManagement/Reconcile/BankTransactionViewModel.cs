using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.AccountManagement.Reconcile
{
    public class BankTransactionViewModel : ViewModelBase, IBankTransactionViewModel
    {
        private ObservableCollection<BankClearTransaction> _bankClearTransaction =
            new ObservableCollection<BankClearTransaction>();

        private BankClearTransaction _selectedBankClearTransaction;

        private bool _showGridData;

        private StatusInfo _status;

        public BankTransactionViewModel()
        {
            ResetState();
            Messenger.Default.Register<NotificationMessage<BankFileViewModel>>(this,
                message => { BankClearTransactions = message.Content.UnmatchedBankClearTransactions; });
        }

        public ObservableCollection<BankClearTransaction> BankClearTransactions
        {
            get { return _bankClearTransaction; }
            set
            {
                _bankClearTransaction = value;
                NotifyPropertyChanged();
                ShowGridData = true;
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

        public bool ShowGridData
        {
            get { return _showGridData; }
            set
            {
                _showGridData = value;
                NotifyPropertyChanged();
            }
        }

        public bool ShowSelectedBankClearTransaction
        {
            get { return SelectedBankClearTransaction != null; }
        }

        public BankClearTransaction SelectedBankClearTransaction
        {
            get { return _selectedBankClearTransaction; }
            set
            {
                _selectedBankClearTransaction = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("ShowSelectedBankClearTransaction");
            }
        }


        private void ResetState()
        {
            ShowGridData = false;
            Status = new StatusInfo
            {
                StatusMessage = "",
                ErrorMessage = ""
            };
        }
    }
}