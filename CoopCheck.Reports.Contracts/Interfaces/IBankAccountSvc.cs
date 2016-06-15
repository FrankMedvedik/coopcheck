using System.Collections.Generic;
using System.Threading.Tasks;
using CoopCheck.Repository.Contracts.Interfaces;

namespace CoopCheck.Reports.Contracts.Interfaces
{
    public interface IBankAccountSvc
    {
        Ibank_account AllAccountsOption { get; }

        Task<List<Ibank_account>> GetAccounts();
    }
}