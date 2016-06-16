using System.Collections.ObjectModel;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.BankAccount.Open
{
    public interface IOpenPaymentReportViewModel
    {
        string HeadingText { get; set; }
        ObservableCollection<Paymnt> Payments { get; set; }
        int PaymentsCnt { get; }
        decimal? PaymentsTotalDollars { get; }
        Paymnt SelectedPayment { get; set; }
        bool ShowGridData { get; set; }
        StatusInfo Status { get; set; }

        void GetPayments(PaymentReportCriteria p);
        void PrintReport(PaymentReportCriteria c);
        void ResetState();
    }
}