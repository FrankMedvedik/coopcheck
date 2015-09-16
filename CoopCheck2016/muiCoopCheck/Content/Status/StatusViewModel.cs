using CoopCheck.WPF.Models;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Status
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class StatusViewModel : ViewModel.ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the StatusViewModel class.
        /// </summary>
        /// 
        public StatusViewModel()
        {
            var s = new StatusInfo()
            {
                StatusMessage = "welcome to coopcheck",
                ErrorMessage = ""
            };

            Status = s;

            Messenger.Default.Register<NotificationMessage<StatusInfo>>(this, message =>
            {
                Status = message.Content;
            });

        }
        private StatusInfo _status;
        public StatusInfo Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyPropertyChanged();
            }
        }

    }
}