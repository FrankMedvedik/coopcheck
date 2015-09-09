using System;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Pages.Report
{
    public class ReportDateRangeViewModel : ViewModelBase
    {
        public ReportDateRangeViewModel()
        {
            _startRptDate = DateTime.Today.AddDays(-60);
            _endRptDate = DateTime.Today.AddDays(1);
            Messenger.Default.Send(new NotificationMessage(this, _notificationMessage));
        }

        private string  _notificationMessage = Notifications.DateRangeChanged;
        public string  NotificationMessage
        {
            get { return _notificationMessage; }
            set
            {
                _notificationMessage = value; 
                
            }
        }

        private bool _canRefresh;
        public Boolean CanRefresh       
        {
            get { return _canRefresh; }
            set { _canRefresh = value; }
        }

        #region reporting variables

        private DateTime _startRptDate;
        public DateTime StartRptDate
        {
            get { return _startRptDate; }
            set
            {
                _startRptDate = value;
                NotifyPropertyChanged();
                Messenger.Default.Send(new NotificationMessage(this, _notificationMessage));
            }
        }

        private DateTime _endRptDate;
        public DateTime EndRptDate
        {
            get { return _endRptDate; }
            set
            {
                _endRptDate = value;
                NotifyPropertyChanged();
                Messenger.Default.Send(new NotificationMessage(this, _notificationMessage));
            }
        }

       }
        #endregion
 
}
