using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using CoopCheck.WPF.Converters;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Wrappers;
using DataClean.Models;
using DataClean.Services;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Services
{
    public static class DataCleanVoucherImportSvc
    {
        public static async Task<ObservableCollection<VoucherImportWrapper>> CleanVouchers(List<VoucherImport> vouchers, DataCleanCriteria dataCleanCriteria, ExcelFileInfoMessage excelFileInfo)
        {

            var cleanVouchers = new ObservableCollection<VoucherImportWrapper>();
            foreach (var v in vouchers)
            {
                v.ID = HashHelperSvc.GetHashCode(v.Region, v.Municipality, v.PostalCode, v.AddressLine1,
                    v.AddressLine2, v.EmailAddress, v.PhoneNumber, v.Last, v.First);
            }
            var inputAddresses = vouchers.Select(v => VoucherImportConverter.ToInputStreetAddress(v)).ToList();
            var dataCleanEvents = new List<DataCleanEvent>(await DataCleanSvc.ValidateAddressesAsync(inputAddresses, dataCleanCriteria));

            Messenger.Default.Send(
                new NotificationMessage<ObservableCollection<DataCleanEvent>>(
                    new ObservableCollection<DataCleanEvent>(dataCleanEvents),
                    Notifications.NewDataCleanEvents));

            var ilist = new List<VoucherImportWrapper>();

            foreach (var e in dataCleanEvents)
            {
                var i = DataCleanEventConverter.ToVoucherImportWrapper(e,
                    new VoucherImportWrapper(vouchers.First(x => x.ID == e.ID)));
                // we want to join the row to get the data we did not send to the cleaner
                ilist.Add(i);
            }

            cleanVouchers = new ObservableCollection<VoucherImportWrapper>(ilist);

            Messenger.Default.Send(
                new NotificationMessage<VoucherWrappersMessage>(new VoucherWrappersMessage()
                {
                    ExcelFileInfo = excelFileInfo,
                    VoucherImports = cleanVouchers
                }, Notifications.VouchersDataCleaned));
            return cleanVouchers;
        }

    }
}
