using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.AccountManagement.Reconcile
{
    public class ReconcileWizardViewModel : ViewModelBase, IReconcileWizardViewModel
    {
        private bool _canFinish;
        private bool _haveAutoReconciledAccount;
        private bool _haveValidAccount;

        private bool _haveValidBankFile;

        private StatusInfo _status;

        public ReconcileWizardViewModel()
        {
            ResetAll();
            Messenger.Default.Register<NotificationMessage<BankFileViewModel>>(this,
                message => { HaveValidBankFile = true; });

            Messenger.Default.Register<NotificationMessage<BankFileViewModel>>(this,
                message => { HaveValidBankFile = true; });

            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if (message.Notification == Notifications.BankAccountReconcileWizardCanFinish)
                    CanFinish = true;
            });
        }

        public bool CanFinish
        {
            get { return _canFinish; }
            set
            {
                _canFinish = value;
                NotifyPropertyChanged();
            }
        }

        public bool HaveValidBankFile
        {
            get { return _haveValidBankFile; }
            set
            {
                _haveValidBankFile = value;
                NotifyPropertyChanged();
            }
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

        private void ResetAll()
        {
            HaveValidBankFile = false;
            CanFinish = false;
        }
    }
}