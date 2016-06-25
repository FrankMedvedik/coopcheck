using System.Collections.ObjectModel;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Reports.Contracts.Models;
using CoopCheck.WPF.Content.PaymentReports.Criteria;

namespace CoopCheck.WPF.Content.Interfaces
{
    public interface IVoidedPaymentReportViewModel
    {
        ObservableCollection<VoidedPayment> Payments { get; set; }
        VoidedPayment SelectedPayment { get; set; }
        bool ShowGridData { get; set; }
        StatusInfo Status { get; set; }

        void GetPayments(PaymentReportCriteria p);
        void ResetState();
    }
}