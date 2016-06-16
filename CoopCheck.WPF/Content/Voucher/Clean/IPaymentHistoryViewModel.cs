using System.Collections.ObjectModel;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.Voucher.Clean
{
    public interface IPaymentHistoryViewModel
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        ObservableCollection<Paymnt> Payments { get; set; }
        int PaymentsCnt { get; }
        decimal? PaymentsTotalDollars { get; }
        Paymnt SelectedPayment { get; set; }
        bool ShowGridData { get; set; }
        StatusInfo Status { get; set; }

        void GetPayments();
        void ResetState();
    }
}