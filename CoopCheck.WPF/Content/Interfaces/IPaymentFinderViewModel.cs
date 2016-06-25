using System.Collections.ObjectModel;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Reports.Contracts.Models;
using CoopCheck.WPF.Content.PaymentReports.Criteria;
using GalaSoft.MvvmLight.Command;

namespace CoopCheck.WPF.Content.Interfaces
{
    public interface IPaymentFinderViewModel
    {
        bool CanClearSelectedCheck { get; }
        bool CanVoidSelectedCheck { get; }
        PaymentReportCriteria PaymentReportCriteria { get; set; }
        ObservableCollection<Payment> Payments { get; set; }
        int PaymentsCnt { get; }
        decimal? PaymentsTotalDollars { get; }
        Payment SelectedPayment { get; set; }
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