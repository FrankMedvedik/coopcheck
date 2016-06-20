using System.Collections.ObjectModel;

namespace CoopCheck.WPF.Content.PaymentReports.Void
{
    public interface IVoidedPaymentReportViewModel
    {
        ObservableCollection<VoidedPaymnt> Payments { get; set; }
        VoidedPaymnt SelectedPayment { get; set; }
        bool ShowGridData { get; set; }
        StatusInfo Status { get; set; }

        void GetPayments(PaymentReportCriteria p);
        void ResetState();
    }
}