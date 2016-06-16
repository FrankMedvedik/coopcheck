using System.Collections.ObjectModel;
using CoopCheck.WPF.Models;
using GalaSoft.MvvmLight.Command;

namespace CoopCheck.WPF.Content.Voucher.Import
{
    public interface IImportWorksheetViewModel
    {
        string this[string columnName] { get; }

        bool CanImport { get; set; }
        bool CanProceed { get; set; }
        ObservableCollection<ColumnPropertyMap> ColumnMap { get; set; }
        string Error { get; set; }
        string ExcelFilePath { get; set; }
        ObservableCollection<ColumnPropertyMap> FilteredColumnMap { get; }
        RelayCommand ImportWorksheetCommand { get; }
        bool IsCleaning { get; set; }
        bool IsSaving { get; set; }
        RelayCommand PostVouchersCommand { get; }
        string SelectedWorksheet { get; set; }
        bool ShowColumnErrorData { get; set; }
        ObservableCollection<string> SrcColumnNames { get; set; }
        bool StartEnabled { get; set; }
        StatusInfo Status { get; set; }
        int VoucherCnt { get; set; }
        ObservableCollection<VoucherImport> VoucherImports { get; set; }
        decimal VoucherTotalDollars { get; set; }
        ObservableCollection<string> WorkSheets { get; set; }

        bool CanImportWorkSheet();
        bool CanPostVouchers();
        void ColumnNameValidator();
        void ImportVouchers();
        void LoadWorkSheetData();
        void LoadWorksheetMetaData();
        void LoadWorksheetStats();
        void PostVouchers();
        void ResetState();
    }
}