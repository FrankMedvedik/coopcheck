using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CoopCheck.WPF.Content.AccountManagement.Reconcile
{
    public interface IAccountPaymentsViewModel
    {
        List<Paymnt> AllPayments { get; set; }
        List<CheckInfoDto> ChecksToClear { get; set; }
        List<Paymnt> MatchedPayments { get; set; }
        List<Paymnt> OpenPayments { get; set; }
        PaymentReportCriteria PaymentReportCriteria { get; set; }
        ObservableCollection<KeyValuePair<string, string>> Stats { get; set; }

        Task GetPayments();
        void LoadStats();
    }
}