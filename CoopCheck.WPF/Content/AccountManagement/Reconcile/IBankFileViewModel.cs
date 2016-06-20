using System;
using System.Collections.ObjectModel;

namespace CoopCheck.WPF.Content.AccountManagement.Reconcile
{
    public interface IBankFileViewModel
    {
        string this[string columnName] { get; }

        ObservableCollection<BankClearTransaction> BankClearTransactions { get; set; }
        bool CanImport { get; set; }
        bool CanReconcile { get; set; }
        int CreditTransactionCnt { get; set; }
        decimal CreditTransactionTotalDollars { get; set; }
        int DebitTransactionCnt { get; set; }
        decimal DebitTransactionTotalDollars { get; set; }
        string Error { get; set; }
        string FilePath { get; set; }
        DateTime? FirstTransactionDate { get; set; }
        DateTime? LastTransactionDate { get; set; }
        BankClearTransaction SelectedUnmatchedBankClearTransaction { get; set; }
        StatusInfo Status { get; set; }
        ObservableCollection<BankClearTransaction> UnmatchedBankClearTransactions { get; set; }
        int UnmatchedCreditTransactionCnt { get; set; }
        decimal UnmatchedCreditTransactionTotalDollars { get; set; }
        int UnmatchedDebitTransactionCnt { get; set; }
        decimal UnmatchedDebitTransactionTotalDollars { get; set; }

        void LoadCsv();
        void ResetState();
    }
}