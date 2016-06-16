using System.Collections.ObjectModel;
using System.Windows.Media;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.Payment.Criteria
{
    public interface IPaymentReportCriteriaViewModel
    {
        SolidColorBrush AccentBrush { get; }
        ObservableCollection<Models.BankAccount> Accounts { get; set; }
        bool EnableCheckNum { get; set; }
        bool EnableMiscFields { get; set; }
        PaymentReportCriteria PaymentReportCriteria { get; set; }
        bool ShowAllAccountsOption { get; set; }
        bool ShowGridData { get; set; }

        void ResetState();
    }
}