using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.BankAccount.Reconcile
{
    public class ReconcileWizardViewModel : ViewModelBase
    {
        public ReconcileWizardViewModel()
        {
            ResetAll();
            Messenger.Default.Register<NotificationMessage<BankFileViewModel>>(this, message =>
            {
                HaveValidBankFile = true;
            });

            Messenger.Default.Register<NotificationMessage<BankFileViewModel>>(this, message =>
            {
                HaveValidBankFile = true;
            });

            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if(message.Notification == Notifications.BankAccountReconcileWizardCanFinish)
                CanFinish = true;
            });
            
        }

        public bool CanFinish
        {
            get { return _canFinish; }
            set { _canFinish = value; NotifyPropertyChanged(); }
        }

        private void ResetAll()
        {
            HaveValidBankFile = false;
            CanFinish = false;
        }
        
        private StatusInfo _status;
        
        private bool _haveValidBankFile;
        private bool _haveValidAccount;
        private bool _haveAutoReconciledAccount;
        private bool _canFinish;

        public bool HaveValidBankFile
        {
            get { return _haveValidBankFile; }
            set { _haveValidBankFile = value;
                NotifyPropertyChanged(); }
        }

        public bool HaveValidAccount
        {
            get { return _haveValidAccount; }
            set
            {
                _haveValidAccount = value;
                NotifyPropertyChanged();
            }
        }

        public bool HaveAutoReconciledAccount
        {
            get { return _haveAutoReconciledAccount; }
            set
            {
                _haveAutoReconciledAccount = value;
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


    }

}
