using System.Collections.ObjectModel;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Reports.Contracts.Models;

namespace CoopCheck.WPF.Content.Interfaces
{
    public interface IPaymentHistoryViewModel
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        ObservableCollection<Payment> Payments { get; set; }
        int PaymentsCnt { get; }
        decimal? PaymentsTotalDollars { get; }
        Payment SelectedPayment { get; set; }
        bool ShowGridData { get; set; }
        StatusInfo Status { get; set; }

        void GetPayments();
        void ResetState();
    }
}