using System.Collections.ObjectModel;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.Voucher.Edit
{
    public interface IBatchListViewModel
    {
        ObservableCollection<OpenBatch> BatchList { get; set; }
        int? CheckPaymentsCnt { get; }
        decimal? CheckPaymentsTotalDollars { get; }
        string HeadingText { get; set; }
        bool IsBusy { get; set; }
        OpenBatch SelectedBatch { get; set; }
        int? SwiftPaymentsCnt { get; }
        decimal? SwiftPaymentsTotalDollars { get; }

        void ResetState();
    }
}