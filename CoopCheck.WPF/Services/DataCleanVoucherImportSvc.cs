using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.WPF.Converters;
using CoopCheck.WPF.Wrappers;
using DataClean.Models;
using DataClean.Services;

namespace CoopCheck.WPF.Services
{
    public static class DataCleanVoucherImportSvc
    {
        public async static Task<ObservableCollection<VoucherImportWrapper>> CleanVouchers(List<VoucherImportWrapper> vouchers)
        {
            var l = await Task<ObservableCollection<VoucherImportWrapper>>.Factory.StartNew(() =>
            {
                var cleanVouchers = new ObservableCollection<VoucherImportWrapper>();
                foreach (var v in vouchers)
                {
                    v.ID = HashHelperSvc.GetHashCode(v.Region, v.Municipality, v.PostalCode, v.AddressLine1,
                        v.AddressLine2, v.EmailAddress, v.PhoneNumber, v.Last, v.First);
                }
                var inputAddresses = vouchers.Select(v => VoucherImportWrapperConverter.ToInputStreetAddress(v)).ToList();
                var dataCleanEvents = DataCleanSvc.ValidateAddresses(inputAddresses);
                var ilist = new List<VoucherImportWrapper>();
                foreach (var e in dataCleanEvents)
                {
                    var i = DataCleanEventConverter.ToVoucherImportWrapper(e,
                        vouchers.First(x => x.RecordID == e.RecordID));
                    // we want to join the row to get the data we did not send to the cleaner
                    ilist.Add(i);
                }
                return new ObservableCollection<VoucherImportWrapper>(ilist);
            });
            return l;
        }
    }
}