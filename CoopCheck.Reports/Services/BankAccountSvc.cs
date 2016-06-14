using System.Collections.Generic;
using System.Threading.Tasks;
using CoopCheck.Reports.Models;

namespace CoopCheck.Reports.Services
{
    public static class BankAccountSvc
    {
        public static BankAccount AllAccountsOption
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

        public static async Task<List<BankAccount>> GetAccounts()
        {
            using (var ctx = new CoopCheckEntities())
            {
                var x = await (
                    ctx.bank_accounts.Where(a => (bool) a.IsActive).OrderBy(a => a.account_name)).ToListAsync();
                return x;
            }
        }

        public static Task<int> NextCheckNum(int accountId)
        {
            return Task<int>.Factory.StartNew(() => NextCheckNumCommand.Execute(accountId));
        }
    }
}