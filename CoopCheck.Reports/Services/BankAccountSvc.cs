using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Reports.Models;
using CoopCheck.Repository.Contracts.Interfaces;

namespace CoopCheck.Reports.Services
{
    public class BankAccountSvc : IBankAccountSvc
    {
        private readonly ICoopCheckEntities _coopCheckEntities = null;

        public BankAccountSvc(ICoopCheckEntities coopCheckEntities)
        {
            _coopCheckEntities = coopCheckEntities;
        }

        public Ibank_account AllAccountsOption
        {
            get
            {
                return new BankAccount
                {
                    account_id = -999,
                    account_name = "All Accounts"
                };
            }
        }

        public async Task<List<Ibank_account>> GetAccounts()
        {

            var x =
                await
                    _coopCheckEntities.bank_accounts.Where(a => (bool) a.IsActive)
                        .OrderBy(a => a.account_name)
                        .ToListAsync();
            return x;
        }

    }
}