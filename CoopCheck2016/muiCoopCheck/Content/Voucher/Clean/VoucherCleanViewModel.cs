using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.WPF.Converters;
using CoopCheck.WPF.Messages;
using GalaSoft.MvvmLight.Messaging;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using CoopCheck.WPF.Wrappers;
using DataClean.Models;
using DataClean.Services;

namespace CoopCheck.WPF.Content.Voucher.Clean
{
    public class VoucherCleanViewModel : ViewModelBase
    {
        private StatusInfo _status;
        private DataCleanCriteria  _dataCleanCriteria = null;
        private List<VoucherImport>  _vouchers = null;

        public VoucherCleanViewModel()
        {
            CanDataClean = false;
            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if (message.Notification == Notifications.DataCleanCriteriaUpdated )
                {
                    CanDataClean = true;
                }
            });

            Messenger.Default.Register<NotificationMessage<ExcelVouchersMessage>>(this, message =>
            {
                if (message.Notification == Notifications.ImportWorksheetReady && message.Content.VoucherImports.Any())
                {
                    _excelFileInfo = message.Content.ExcelFileInfo;
                    _dataCleanCriteria = _dataCleanCriteria ?? new DataCleanCriteria()
                    {
                        AutoFixAddressLine1 = true,
                        AutoFixCity = true,
                        AutoFixPostalCode = true,
                        AutoFixState = true,
                        ForceValidation = false
                    };
                    _vouchers = message.Content.VoucherImports;
                    CleanTheVouchers();
                }
            });
        }

        private ExcelFileInfoMessage _excelFileInfo;
        public bool CanDataClean
        {
            get { return _canDataClean; }
            set { _canDataClean = value;
                NotifyPropertyChanged(); }
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

        public async void CleanTheVouchers()
        {
            CanDataClean = false;

            await Task.Factory.StartNew(async () =>
            {
                try
                {
                    foreach (var v in _vouchers)
                    {
                        v.ID = HashHelperSvc.GetHashCode(v.Region, v.Municipality, v.PostalCode, v.AddressLine1,
                            v.AddressLine2, v.EmailAddress, v.PhoneNumber, v.Last, v.First);
                    }
                    var inputAddresses =
                        _vouchers.Select(v => VoucherImportConverter.ToInputStreetAddress(v)).ToList();
                    var dataCleanEvents =
                        new List<DataCleanEvent>(
                            await DataCleanSvc.ValidateAddressesAsync(inputAddresses, _dataCleanCriteria));
                    Messenger.Default.Send(
                        new NotificationMessage<ObservableCollection<DataCleanEvent>>(
                            new ObservableCollection<DataCleanEvent>(dataCleanEvents),
                            Notifications.NewDataCleanEvents));
                    var ilist = new List<VoucherImportWrapper>();
                    foreach (var e in dataCleanEvents)
                    {
                        var i = DataCleanEventConverter.ToVoucherImportWrapper(e,
                            new VoucherImportWrapper(_vouchers.First(x => x.ID == e.ID)));
                        // we want to join the row to get the data we did not send to the cleaner
                        ilist.Add(i);
                    }
                    ;
                    CleanVouchers = new ObservableCollection<VoucherImportWrapper>(ilist);
                    Messenger.Default.Send(
                        new NotificationMessage<VoucherWrappersMessage>(new VoucherWrappersMessage()
                        {
                            ExcelFileInfo = _excelFileInfo,
                            VoucherImports = CleanVouchers
                        }, Notifications.VouchersDataCleaned));

                    Status = new StatusInfo()
                    {
                        StatusMessage =
                            String.Format("address validation complete. {0} addressed validated",
                                dataCleanEvents.Count)
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
            });
        }

        private ObservableCollection<VoucherImportWrapper> _cleanVouchers  = new ObservableCollection<VoucherImportWrapper>();
        private bool _canDataClean;

        public ObservableCollection<VoucherImportWrapper> CleanVouchers
        {
            get { return _cleanVouchers; }
            set
            {
                _cleanVouchers = value;
                NotifyPropertyChanged();
            }
        }

    }
}
