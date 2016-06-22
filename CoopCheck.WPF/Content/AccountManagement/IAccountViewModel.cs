using System.Collections.ObjectModel;
using CoopCheck.Reports.Contracts.Models;

namespace CoopCheck.WPF.Content.AccountManagement
{
    public interface IAccountViewModel
    {
        ObservableCollection<BankAccount> Accounts { get; set; }
        BankAccount SelectedAccount { get; set; }
    }
}