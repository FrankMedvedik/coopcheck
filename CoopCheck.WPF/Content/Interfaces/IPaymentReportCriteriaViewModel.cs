using System.Collections.ObjectModel;
using System.Windows.Media;
using CoopCheck.Reports.Contracts.Models;
using CoopCheck.WPF.Content.PaymentReports.Criteria;

namespace CoopCheck.WPF.Content.Interfaces
{
    public interface IPaymentReportCriteriaViewModel
    {
        SolidColorBrush AccentBrush { get; }
        ObservableCollection<BankAccount> Accounts { get; set; }
        bool EnableCheckNum { get; set; }
        bool EnableMiscFields { get; set; }
        PaymentReportCriteria PaymentReportCriteria { get; set; }
        bool ShowAllAccountsOption { get; set; }
        bool ShowGridData { get; set; }

        void ResetState();
    }
}