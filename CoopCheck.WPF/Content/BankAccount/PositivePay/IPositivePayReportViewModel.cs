using System.Collections.Generic;
using System.Collections.ObjectModel;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.BankAccount.PositivePay
{
    public interface IPositivePayReportViewModel
    {
        string HeadingText { get; set; }
        PaymentReportCriteria PaymentReportCriteria { get; set; }
        int PaymentsCnt { get; }
        decimal? PaymentsTotalDollars { get; }
        List<string> PositivePayData { get; }
        ObservableCollection<PositivePayItem> PositivePays { get; set; }
        PositivePayItem SelectedPositivePay { get; set; }
        bool ShowGridData { get; set; }
        StatusInfo Status { get; set; }

        void GetPositivePays();
        void RefreshAll();
    }
}