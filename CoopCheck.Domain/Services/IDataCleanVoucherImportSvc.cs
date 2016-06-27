using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CoopCheck.Domain.Contracts.Wrappers;
using DataClean.Contracts.Interfaces;

namespace CoopCheck.Domain.Services
{
    public interface IDataCleanVoucherImportSvc
    {
        Task<ObservableCollection<VoucherImportWrapper>> CleanVouchers(List<VoucherImportWrapper> vouchers);
        Task<List<IDataCleanEvent>> ValidateAddresses(List<IInputStreetAddress> newVouchers);
    }
}