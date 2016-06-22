using System.Collections.Generic;
using System.Threading.Tasks;
using CoopCheck.Reports.Contracts.Models;

namespace CoopCheck.Reports.Contracts.Interfaces
{
    public interface IBankAccountSvc
    {
        BankAccount AllAccountsOption { get; }

        Task<List<BankAccount>> GetAccounts();
    }
}