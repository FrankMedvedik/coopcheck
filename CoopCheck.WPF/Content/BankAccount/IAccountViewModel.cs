using System.Collections.ObjectModel;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.BankAccount
{
    public interface IAccountViewModel
    {
        ObservableCollection<Models.BankAccount> Accounts { get; set; }
        Models.BankAccount SelectedAccount { get; set; }
    }
}