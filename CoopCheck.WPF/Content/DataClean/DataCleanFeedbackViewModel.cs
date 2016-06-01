using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using DataClean.Models;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.DataClean
{
    public class DataCleanFeedbackViewModel : ViewModelBase
    {
        private ObservableCollection<KeyValuePair<string, string>> _stats =
            new ObservableCollection<KeyValuePair<string, string>>();

        private StatusInfo _status;
        private ObservableCollection<DataCleanEvent> _validationResults = new ObservableCollection<DataCleanEvent>();

        public DataCleanFeedbackViewModel()
        {
            Messenger.Default.Register<NotificationMessage<ObservableCollection<DataCleanEvent>>>(this, message =>
            {
                if (message.Notification == Notifications.NewDataCleanEvents)
                    ValidationResults = message.Content;
            });
        }

        public int AutoFixAddressCnt
        {
            get { return ValidationResults.Count(x => x.Output.HasNewStreetAddressLine1); }
        }


        public int AutoFixPostalCodeCnt
        {
            get { return ValidationResults.Count(x => x.Output.HasNewPostalCode); }
        }

        public int AutoFixStateCnt
        {
            get { return ValidationResults.Count(x => x.Output.HasNewStateCode); }
        }

        public int AutoFixCityCnt
        {
            get
            {
                return
                    ValidationResults.Count(x => x.Output.HasNewCity);
            }
        }

        public int EventCnt
        {
            get { return ValidationResults.Count; }
        }

        public int EventErrorCnt
        {
            get { return ValidationResults.Count(x => !x.Output.OkComplete); }
        }

        public int EventEmailErrorCnt
        {
            get { return ValidationResults.Count(x => !x.Output.OkEmailAddress); }
        }

        public int EventPhoneErrorCnt
        {
            get { return ValidationResults.Count(x => !x.Output.OkPhone); }
        }

        public int EventMailingAddressErrorCnt
        {
            get { return ValidationResults.Count(x => !x.Output.OkMailingAddress); }
        }

        public int FirstNameErrorCnt
        {
            get { return ValidationResults.Count(x => x.Input.FirstName.Trim().Length == 0); }
        }

        public int LastNameErrorCnt
        {
            get { return ValidationResults.Count(x => x.Input.LastName.Trim().Length == 0); }
        }

        public int NameErrorCnt
        {
            get { return LastNameErrorCnt + FirstNameErrorCnt; }
        }

        public ObservableCollection<DataCleanEvent> ValidationResults
        {
            get { return _validationResults; }
            set
            {
                _validationResults = value;
                NotifyPropertyChanged();
                LoadStats();
            }
        }

        public ObservableCollection<KeyValuePair<string, string>> Stats
        {
            get { return _stats; }
            set
            {
                _stats = value;
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

        public void LoadStats()
        {
            var s =
                new ObservableCollection<KeyValuePair<string, string>>();

            s.Add(new KeyValuePair<string, string>("Fixed Street Address", string.Format("{0:n0}", AutoFixAddressCnt)));
            s.Add(new KeyValuePair<string, string>("Fixed City", string.Format("{0:n0}", AutoFixCityCnt)));
            s.Add(new KeyValuePair<string, string>("Fixed State", string.Format("{0:n0}", AutoFixCityCnt)));
            s.Add(new KeyValuePair<string, string>("Fixed Postal Code", string.Format("{0:n0}", AutoFixPostalCodeCnt)));
            s.Add(new KeyValuePair<string, string>("Vouches", string.Format("{0:n0}", EventCnt)));
            s.Add(new KeyValuePair<string, string>("Vouches with errors", string.Format("{0:n0}", EventCnt)));
            s.Add(new KeyValuePair<string, string>("Bad email addresses", string.Format("{0:n0}", EventEmailErrorCnt)));
            s.Add(new KeyValuePair<string, string>("Bad street addresses",
                string.Format("{0:n0}", EventMailingAddressErrorCnt)));
            s.Add(new KeyValuePair<string, string>("Bad phones", string.Format("{0:n0}", EventPhoneErrorCnt)));
            s.Add(new KeyValuePair<string, string>("Bad first names", string.Format("{0:n0}", FirstNameErrorCnt)));
            s.Add(new KeyValuePair<string, string>("Bad last names", string.Format("{0:n0}", LastNameErrorCnt)));
            Stats = s;
        }
    }
}