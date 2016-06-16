﻿using System.Collections.ObjectModel;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.Payment.Batch
{
    public interface IBatchReportViewModel
    {
        ObservableCollection<BatchRpt> Batches { get; set; }
        decimal BatchTotalDollars { get; set; }
        bool CanRefresh { get; set; }
        string HeadingText { get; set; }
        PaymentReportCriteria PaymentReportCriteria { get; set; }
        int? PaymentsCnt { get; }
        decimal? PaymentsTotalDollars { get; }
        BatchRpt SelectedBatch { get; set; }
        bool ShowGridData { get; set; }
        StatusInfo Status { get; set; }
        int? VoidsCnt { get; }
        decimal? VoidsTotalDollars { get; }

        void GetBatches();
        void RefreshAll();
    }
}