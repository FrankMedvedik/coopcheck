using System.Collections.Generic;
using System.Collections.ObjectModel;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Reports.Contracts.Models;
using CoopCheck.WPF.Content.PaymentReports.Criteria;

namespace CoopCheck.WPF.Content.Interfaces
{
    public interface IClientPaymentViewModel
    {
        ObservableCollection<Payment> AllPayments { get; set; }
        ClientReportCriteria ClientReportCriteria { get; set; }
        ObservableCollection<Payment> ClosedPayments { get; set; }
        string HeadingText { get; }
        int Job { get; set; }
        ObservableCollection<Payment> OpenPayments { get; set; }
        BatchRpt ParentBatch { get; set; }
        bool ShowGridData { get; set; }
        StatusInfo Status { get; set; }
        List<Payment> WorkPayments { get; set; }

        void RefreshAll();
    }
}