using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Domain.Contracts.Models.Reports;
using CoopCheck.Library;

namespace CoopCheck.WPF.Content.Voucher.Pay
{
    public interface IPayVouchersViewModel
    {
        ObservableCollection<BankAccount> Accounts { get; set; }
        bool CanPrintChecks { get; set; }
        bool CanSwiftPay { get; set; }
        int CurrentPrintCount { get; set; }
        int EndingCheckNum { get; set; }
        bool IsBusy { get; set; }
        decimal PercentComplete { get; set; }
        BankAccount SelectedAccount { get; set; }
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