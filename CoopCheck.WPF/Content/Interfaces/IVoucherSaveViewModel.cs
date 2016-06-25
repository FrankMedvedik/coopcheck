using System.Collections.Generic;
using System.Collections.ObjectModel;
using CoopCheck.Domain.Contracts.Messages;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Domain.Contracts.Wrappers;
using GalaSoft.MvvmLight.Command;

namespace CoopCheck.WPF.Content.Interfaces
{
    public interface IVoucherSaveViewModel
    {
        List<VoucherExcelExport> BadVoucherExports { get; }
        List<VoucherImportWrapper> BadVouchers { get; }
        bool CanExport { get; set; }
        bool CanSave { get; set; }
        string ErrorBatchInfoMessage { get; set; }
        ExcelFileInfoMessage ExcelFileInfo { get; set; }
        RelayCommand ExportVouchersCommand { get; }
        List<VoucherExcelExport> GoodVoucherExports { get; }
        List<VoucherImportWrapper> GoodVouchers { get; }
        string SaveBatchInfoMessage { get; set; }
        RelayCommand SaveVouchersCommand { get; }
        bool StartEnabled { get; set; }
        StatusInfo Status { get; set; }
        ObservableCollection<VoucherImportWrapper> VoucherImportWrappers { get; set; }

        bool CanSaveVouchers();
    }
}