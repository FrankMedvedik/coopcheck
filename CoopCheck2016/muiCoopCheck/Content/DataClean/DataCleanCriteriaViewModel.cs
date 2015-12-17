using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Wrappers;
using DataClean.Models;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.DataClean
{

    public class DataCleanCriteriaViewModel : ModelWrappper<DataCleanCriteria>
    {
   public DataCleanCriteria Criteria
    {
        get { return Model; }
    }

        public  void BroadcastUpdate()
        {
            Messenger.Default.Send(new NotificationMessage<DataCleanCriteria>(Model, Notifications.DataCleanCriteriaUpdated));
        }

        public DataCleanCriteriaViewModel(DataCleanCriteria model) : base(model)
        {

        }

        public bool AutoFixPostalCode { get { return Model.AutoFixPostalCode; }
            set { Model.AutoFixPostalCode = value;
                NotifyPropertyChanged();
            } }

        public bool AutoFixState
        {
            get { return Model.AutoFixState; }
            set
            {
                Model.AutoFixState = value;
                NotifyPropertyChanged();
            }
        }
        public bool AutoFixCity
        {
            get { return Model.AutoFixCity; }
            set
            {
                Model.AutoFixCity = value;
                NotifyPropertyChanged();
            }
        }
        public bool AutoFixAddressLine1
        {
            get { return Model.AutoFixAddressLine1; }
            set
            {
                Model.AutoFixAddressLine1 = value;
                NotifyPropertyChanged();
            }
        }
        public bool ForceValidation
        {
            get { return Model.ForceValidation; }
            set
            {
                Model.ForceValidation = value;
                NotifyPropertyChanged();
            }
        }

        
    }
}
