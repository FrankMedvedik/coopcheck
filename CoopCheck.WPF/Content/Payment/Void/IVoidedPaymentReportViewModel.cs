using System.Collections.ObjectModel;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.Payment.Void
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