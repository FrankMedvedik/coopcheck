using System.Collections.ObjectModel;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Reports.Contracts.Models;

namespace CoopCheck.WPF.Content.Interfaces
{
    public interface IBatchViewModel
    {
        ObservableCollection<BatchRpt> Batches { get; set; }
        decimal BatchTotalDollars { get; set; }
        bool CanRefresh { get; set; }
        bool CanUpdateBatchJob { get; }
        bool HaveGoodNewJobNumber { get; set; }
        string HeadingText { get; set; }
        int NewJobNum { get; set; }
        string NewJobNumError { get; set; }
        JobRpt ParentJob { get; set; }
        int PaymentsCnt { get; }
        decimal? PaymentsTotalDollars { get; }
        BatchRpt SelectedBatch { get; set; }
        bool ShowGridData { get; set; }
        StatusInfo Status { get; set; }

        void GetBatches();
        void RefreshAll();
        void UpdateBatchJob(int batchNum, int jobNum);
        void ValidateJobNumber(string JobNum);
    }
}