using System.Windows;
using CoopCheck.WPF.Models;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Status
{

    public class StatusViewModel : ViewModelBase
    {
        private StatusInfo _status;

        /// <summary>
        ///     Initializes a new instance of the StatusViewModel class.
        /// </summary>
        public StatusViewModel()
        {
            var s = new StatusInfo
            {
                StatusMessage = "Welcome to Coopcheck",
                ErrorMessage = "",
                IsBusy = false
            };

            Status = s;

            Messenger.Default.Register<NotificationMessage<StatusInfo>>(this, message =>
            {
                Status = ObjectCopier.Clone(message.Content);
                //if (Status.ShowMessageBox)
                //{
                //    Messenger.Default.Send(new NotificationMessage(Notifications.ShowPopupStatus));
                //    Status.ShowMessageBox = false;
                //}
            });
        }

        public StatusInfo Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("ShowError");
            }
        }

        public Visibility ShowError
        {
            get
            {
                return Status?.ErrorMessage?.Trim().Length > 0
                    ? Visibility.Visible
                    : Visibility.Hidden;
            }
        }
    }
}