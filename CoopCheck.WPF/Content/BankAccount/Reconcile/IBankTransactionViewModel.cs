using System.Collections.ObjectModel;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.BankAccount.Reconcile
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