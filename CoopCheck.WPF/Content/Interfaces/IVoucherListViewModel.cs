using System.Collections.Generic;
using System.Collections.ObjectModel;
using CoopCheck.Domain.Contracts.Interfaces;
using CoopCheck.Domain.Contracts.Messages;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Domain.Contracts.Wrappers;
using GalaSoft.MvvmLight.Command;

namespace CoopCheck.WPF.Content.Interfaces
{
    public interface IVoucherListViewModel
    {
        bool CanPost { get; set; }
        RelayCommand CleanAndPostVouchersCommand { get; }
        ExcelFileInfoMessage ExcelFileInfo { get; set; }
        ObservableCollection<VoucherImportWrapper> FilteredVoucherImports { get; set; }
        bool FilterRows { get; set; }
        bool HasErrors { get; }
        bool IsCleaning { get; set; }
        VoucherImportWrapper SelectedVoucher { get; set; }
        bool ShowSelectedVoucher { get; }
        StatusInfo Status { get; set; }
        List<IVoucherImport> Vi { get; set; }
        int VoucherErrorCnt { get; }
        ObservableCollection<VoucherImportWrapper> VoucherImports { get; set; }
        VoucherImportWrapper WorkVoucherImport { get; set; }

        void AddNewVoucher();
        void CancelNewVoucher();
        bool CanRunBackgroundCleaner();
        void CleanVouchers(List<VoucherImportWrapper> vouchers);
        void CreateNewVoucher();
        void DeleteSelectedVoucher();
    }
}