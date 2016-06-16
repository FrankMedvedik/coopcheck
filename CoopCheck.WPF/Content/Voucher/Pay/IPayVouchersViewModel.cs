using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using CoopCheck.Library;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.Voucher.Pay
{
    public interface IPayVouchersViewModel
    {
        ObservableCollection<Models.BankAccount> Accounts { get; set; }
        bool CanPrintChecks { get; set; }
        bool CanSwiftPay { get; set; }
        int CurrentPrintCount { get; set; }
        int EndingCheckNum { get; set; }
        bool IsBusy { get; set; }
        decimal PercentComplete { get; set; }
        Models.BankAccount SelectedAccount { get; set; }
        OpenBatch SelectedBatch { get; set; }
        BatchEdit SelectedBatchEdit { get; set; }
        int StartingCheckNum { get; set; }
        StatusInfo Status { get; set; }

        Task<PrintCheckProgress> PrintChecks(CancellationToken ctx);
        void RefreshBatchList();
        void ResetState();
        void SwiftPay();
    }
}