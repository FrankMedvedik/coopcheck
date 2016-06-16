using System.Collections.ObjectModel;
using CoopCheck.WPF.Models;
using GalaSoft.MvvmLight.Command;

namespace CoopCheck.WPF.Content.Payment.PaymentFinder
{
    public interface IPaymentFinderViewModel
    {
        bool CanClearSelectedCheck { get; }
        bool CanVoidSelectedCheck { get; }
        PaymentReportCriteria PaymentReportCriteria { get; set; }
        ObservableCollection<Paymnt> Payments { get; set; }
        int PaymentsCnt { get; }
        decimal? PaymentsTotalDollars { get; }
        Paymnt SelectedPayment { get; set; }
        bool ShowGridData { get; set; }
        StatusInfo Status { get; set; }
        RelayCommand TheClearCheckCommand { get; }
        RelayCommand TheVoidCheckCommand { get; }

        bool CanClearCheck();
        bool CanVoidCheck();
        void ClearCheck();
        void GetPayments();
        void ResetState();
        void VoidCheck();
    }
}