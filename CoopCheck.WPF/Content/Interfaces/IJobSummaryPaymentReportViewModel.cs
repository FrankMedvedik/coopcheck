using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Reports.Contracts.Models;
using CoopCheck.WPF.Content.PaymentReports.Criteria;

namespace CoopCheck.WPF.Content.Interfaces
{
    public interface IJobSummaryPaymentReportViewModel
    {
        ObservableCollection<Payment> AllPayments { get; set; }
        ObservableCollection<Payment> ClosedPayments { get; set; }
        string HeadingText { get; set; }
        ObservableCollection<Payment> OpenPayments { get; set; }
        PaymentReportCriteria PaymentReportCriteria { get; set; }
        bool ShowGridData { get; set; }
        StatusInfo Status { get; set; }
        List<Payment> WorkPayments { get; set; }

        Task GetPayments();
        void RefreshAll();
    }
}