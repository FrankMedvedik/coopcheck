using System.Collections.Generic;
using System.Collections.ObjectModel;
using CoopCheck.WPF.Contracts.Interfaces;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Wrappers;
using GalaSoft.MvvmLight.Command;

namespace CoopCheck.WPF.Content.Voucher.Clean
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