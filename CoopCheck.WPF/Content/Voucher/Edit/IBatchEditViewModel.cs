using System.Collections.ObjectModel;
using System.Windows.Media;
using CoopCheck.Library;
using CoopCheck.WPF.Models;
using GalaSoft.MvvmLight.Command;

namespace CoopCheck.WPF.Content.Voucher.Edit
{
    public interface IBatchEditViewModel
    {
        string this[string columnName] { get; }

        int BatchNum { get; }
        bool CanDeleteVoucher { get; }
        bool CanPayBatch { get; set; }
        string Error { get; set; }
        bool HaveSelectedVoucher { get; }
        string HeaderText { get; set; }
        bool IsBusy { get; }
        bool IsDirty { get; }
        bool IsNewVoucher { get; set; }
        string JobName { get; set; }
        Brush JobNameBrush { get; }
        int? JobNum { get; set; }
        RelayCommand RefreshBatchListCommand { get; }
        BatchEdit SelectedBatch { get; set; }
        VoucherEdit SelectedVoucher { get; set; }
        bool ShowSelectedBatch { get; set; }
        bool ShowSelectedVoucher { get; set; }
        StatusInfo Status { get; set; }
        bool UserCanRead { get; set; }
        bool UserCanWrite { get; set; }
        ObservableCollection<VoucherEdit> VouchersWithErrors { get; set; }
        VoucherImport WorkVoucherImport { get; set; }

        void AutoSaveSelectedBatch();
        void CancelNewVoucher();
        void CreateNewVoucher();
        void DeleteSelectedBatch();
        void DeleteSelectedVoucher();
        void ResetState();
        void SaveNewVoucher();
        void SaveSelectedBatch();
        void SetJobName();
    }
}