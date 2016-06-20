using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CoopCheck.WPF.Content.PaymentReports.Batch
{
    public interface IBatchSummaryPaymentReportViewModel
    {
        ObservableCollection<Paymnt> AllPayments { get; set; }
        bool CanUnprint { get; set; }
        bool CanVoid { get; set; }
        ObservableCollection<Paymnt> ClosedPayments { get; set; }
        int EndingCheckNum { get; set; }
        string HeadingText { get; }
        ObservableCollection<Paymnt> OpenPayments { get; set; }
        PaymentReportCriteria PaymentReportCriteria { get; set; }
        bool ShowGridData { get; set; }
        int StartingCheckNum { get; set; }
        StatusInfo Status { get; set; }
        List<Paymnt> WorkPayments { get; set; }

        void RefreshAll();
    }
}