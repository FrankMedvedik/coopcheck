using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CoopCheck.WPF.Converters;
using GalaSoft.MvvmLight.Messaging;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using CoopCheck.WPF.Wrappers;
using DataClean.Models;
using DataClean.Services;
using GalaSoft.MvvmLight.Command;

namespace CoopCheck.WPF.Content.Voucher.Clean
{
    public class VoucherCleanViewModel : ViewModelBase
    {
        private StatusInfo _status;
        public VoucherCleanViewModel()
        {
            CanDataClean = false;
            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if (message.Notification == Notifications.DataCleanCriteriaUpdated)
                {
                    CanDataClean = true;
                }
            });
        }

        public bool CanDataClean
        {
            get { return _canDataClean; }
            set { _canDataClean = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<DataCleanEvent> _validationResults;
        public ObservableCollection<DataCleanEvent> ValidationResults
        {
            get { return _validationResults; }
            set
            {
                _validationResults = value;
                var ilist = new List<VoucherImportWrapper>();
                foreach (var e in ValidationResults)
                {
                    var i = DataCleanEventConverter.ToVoucherImportWrapper(e, VoucherImports.First(x => x.ID == e.ID)); // we want to join the row to get the data we did not send to the cleaner
                    ilist.Add(i);
                };
                VoucherImports = new ObservableCollection<VoucherImportWrapper>(ilist);
                NotifyPropertyChanged();
            }
        }

        public List<VoucherImport> Vi
        {
            get { return VoucherImports.Select(r => r.Model).ToList(); }
            set
            {
                var a = new ObservableCollection<VoucherImportWrapper>();
                foreach (var v in value)
                {
                    a.Add(new VoucherImportWrapper(v));
                }

                VoucherImports = a;
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

        public async void DataCleanAddresses(DataCleanCriteria criteria)
        {
            try
            {
                foreach (var v in Vi)
                {
                    v.ID = HashHelperSvc.GetHashCode(v.Region, v.Municipality, v.PostalCode, v.AddressLine1,
                        v.AddressLine2, v.EmailAddress, v.PhoneNumber, v.Last, v.First);
                }

                var a = Vi.Select(v => VoucherImportConverter.ToInputStreetAddress(v)).ToList();
                var dataCleanEvents = new List<DataCleanEvent>(await DataCleanSvc.ValidateAddressesAsync(a, criteria));
                ValidationResults = new ObservableCollection<DataCleanEvent>(dataCleanEvents);
                Messenger.Default.Send(new NotificationMessage<List<DataCleanEvent>>(dataCleanEvents, Notifications.NewDataCleanEvents));
                //  ValidationResults = results;
                Status = new StatusInfo()
                {
                    StatusMessage =
                        String.Format("address validation complete. {0} addressed validated", dataCleanEvents.Count)
                };
                
            }
            catch (Exception e)
            {
                Status = new StatusInfo()
                {
                    StatusMessage = String.Format("Error during validation"),
                    ErrorMessage = e.Message
                };
            }
        }

        private ObservableCollection<VoucherImportWrapper> _voucherImports = new ObservableCollection<VoucherImportWrapper>();
        private bool _canDataClean;

        public ObservableCollection<VoucherImportWrapper> VoucherImports
        {
            get { return _voucherImports; }
            set
            {
                _voucherImports = value;
                NotifyPropertyChanged();
            }
        }

    }
}
