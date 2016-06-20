using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CoopCheck.WPF.Content.PaymentReports.Job
{
    public interface IJobSummaryPaymentReportViewModel
    {
        ObservableCollection<Paymnt> AllPayments { get; set; }
        ObservableCollection<Paymnt> ClosedPayments { get; set; }
        string HeadingText { get; set; }
        ObservableCollection<Paymnt> OpenPayments { get; set; }
        PaymentReportCriteria PaymentReportCriteria { get; set; }
        bool ShowGridData { get; set; }
        StatusInfo Status { get; set; }
        List<Paymnt> WorkPayments { get; set; }

        Task GetPayments();
        void RefreshAll();
    }
}