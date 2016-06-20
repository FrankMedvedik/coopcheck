using System.Collections.ObjectModel;

namespace CoopCheck.WPF.Content.AccountManagement.Reconcile
{
    public interface IBankTransactionViewModel
    {
        ObservableCollection<BankClearTransaction> BankClearTransactions { get; set; }
        BankClearTransaction SelectedBankClearTransaction { get; set; }
        bool ShowGridData { get; set; }
        bool ShowSelectedBankClearTransaction { get; }
        StatusInfo Status { get; set; }
    }
}