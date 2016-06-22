using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Reports.Contracts.Models;

namespace CoopCheck.WPF.Content.AccountManagement.Reconcile
{
    public interface IAccountPaymentsViewModel
    {
        List<Payment> AllPayments { get; set; }
        List<CheckInfoDto> ChecksToClear { get; set; }
        List<Payment> MatchedPayments { get; set; }
        List<Payment> OpenPayments { get; set; }
        PaymentReportCriteriaDto PaymentReportCriteria { get; set; }
        ObservableCollection<KeyValuePair<string, string>> Stats { get; set; }

        Task GetPayments();
        void LoadStats();
    }
}