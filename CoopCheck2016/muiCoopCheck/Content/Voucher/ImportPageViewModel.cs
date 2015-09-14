using CoopCheck.WPF.Content.Voucher.Import;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Voucher
{
    public class ImportPageViewModel : ViewModelBase
    {
        public ImportPageViewModel()
        {
            var s = new StatusInfo()
            {
                StatusMessage = "click select file to choose a new excel file to import voucher from",
                ErrorMessage = "No Errors"
            };

            Status = s;
            Messenger.Default.Register<NotificationMessage>(this, HandleNotification);
            Messenger.Default.Register<NotificationMessage<StatusInfo>>(this, message =>
            {
                Status = message.Content;
            });

            StartOver();
        }

         

        private void HandleNotification(NotificationMessage message)
        {
            if (message.Notification == Notifications.ImportCannotProceed)
            {
                CanProceed = false;
            }
            if (message.Notification == Notifications.ImportCanProceed)
            {
                CanProceed = true;
            }
        }
        public bool CanProceed
        {
            get { return _canProceed; }
            set
            {
                _canProceed = value;
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
            }
        }

        private StatusInfo _status;
        private ImportProcessStep _currentProcessingStep;
        private bool _canProceed;


        public ImportProcessStep CurrentProcessingStep
        {
            get { return _currentProcessingStep; }
            set
            {
                _currentProcessingStep = value;
                NotifyPropertyChanged();
            }
        }

        public void GoToNextStep()
        {
            CurrentProcessingStep++;
        }

        public void GoBackAStep()
        {
            CurrentProcessingStep--;
        }

        public void StartOver()
        {
            CurrentProcessingStep = ImportProcessStep.Import;
            CanProceed = false;
        }
    }
}