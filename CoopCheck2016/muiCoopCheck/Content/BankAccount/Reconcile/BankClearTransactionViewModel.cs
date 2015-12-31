using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Library;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.BankAccount.Reconcile
{

    public class BankClearTransactionViewModel : ViewModelBase
    {
        private ObservableCollection<BankClearTransaction> _bankClearTransaction = new ObservableCollection<BankClearTransaction>();
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

        private StatusInfo _status;
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

        public BankClearTransactionViewModel()
        {
            ResetState();
            Messenger.Default.Register<NotificationMessage<BankFileViewModel>>(this, message =>
            {
                BankClearTransactions  = message.Content.UnmatchedBankClearTransactions;
            });
        }


        private void ResetState()
        {
            ShowGridData = false;
            Status = new StatusInfo()
            {
                StatusMessage = "",
                ErrorMessage = ""
            };

        }

        private Boolean _showGridData;

        public Boolean ShowGridData
        {
            get { return _showGridData; }
            set
            {
                _showGridData = value;
                NotifyPropertyChanged();
            }
        }

        public Boolean ShowSelectedBankClearTransaction
        {
            get { return SelectedBankClearTransaction != null; }
        }

        private BankClearTransaction _selectedBankClearTransaction;

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
    }
}