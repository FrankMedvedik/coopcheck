using System.Collections.ObjectModel;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Domain.Contracts.Models.Reports;

namespace CoopCheck.WPF.Content.AccountManagement.Open
{
    public interface IOpenPaymentReportViewModel
    {
        string HeadingText { get; set; }
        ObservableCollection<Payment> Payments { get; set; }
        int PaymentsCnt { get; }
        decimal? PaymentsTotalDollars { get; }
        Payment SelectedPayment { get; set; }
        bool ShowGridData { get; set; }
        StatusInfo Status { get; set; }

        void GetPayments(PaymentReportCriteria p);
        void PrintReport(PaymentReportCriteria c);
        void ResetState();
    }
}