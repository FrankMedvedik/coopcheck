using System.Collections.Generic;
using System.Collections.ObjectModel;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Reports.Contracts.Models;
using CoopCheck.WPF.Content.PaymentReports.Criteria;

namespace CoopCheck.WPF.Content.Interfaces
{
    public interface IBatchSummaryPaymentReportViewModel
    {
        ObservableCollection<Payment> AllPayments { get; set; }
        bool CanUnprint { get; set; }
        bool CanVoid { get; set; }
        ObservableCollection<Payment> ClosedPayments { get; set; }
        int EndingCheckNum { get; set; }
        string HeadingText { get; }
        ObservableCollection<Payment> OpenPayments { get; set; }
        PaymentReportCriteria PaymentReportCriteria { get; set; }
        bool ShowGridData { get; set; }
        int StartingCheckNum { get; set; }
        StatusInfo Status { get; set; }
        List<Payment> WorkPayments { get; set; }
        
        void RefreshAll();
    }
}