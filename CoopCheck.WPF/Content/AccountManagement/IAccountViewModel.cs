using System.Collections.ObjectModel;
using CoopCheck.Domain.Contracts.Models.Reports;

namespace CoopCheck.WPF.Content.AccountManagement
{
    public interface IAccountViewModel
    {
        ObservableCollection<BankAccount> Accounts { get; set; }
        BankAccount SelectedAccount { get; set; }
    }
}