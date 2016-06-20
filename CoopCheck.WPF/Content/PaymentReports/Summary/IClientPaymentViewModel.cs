using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CoopCheck.WPF.Content.PaymentReports.Summary
{
    public interface IClientPaymentViewModel
    {
        ObservableCollection<Paymnt> AllPayments { get; set; }
        ClientReportCriteria ClientReportCriteria { get; set; }
        ObservableCollection<Paymnt> ClosedPayments { get; set; }
        string HeadingText { get; }
        int Job { get; set; }
        ObservableCollection<Paymnt> OpenPayments { get; set; }
        BatchRpt ParentBatch { get; set; }
        bool ShowGridData { get; set; }
        StatusInfo Status { get; set; }
        List<Paymnt> WorkPayments { get; set; }

        void RefreshAll();
    }
}