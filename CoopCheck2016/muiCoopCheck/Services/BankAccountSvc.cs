using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Library;
using CoopCheck.Repository;

namespace CoopCheck.WPF.Services
{
    public static class BankAccountSvc
    {
        public static async Task<List<bank_account>> GetAccounts()
        {
            using (var ctx = new CoopCheckEntities())
            {

                var x = await (
                    from a in ctx.bank_accounts
                    orderby a.account_name
                    select a).ToListAsync();
                return x;
            }
        }
        public static Task<int> NextCheckNum(int accountId)
        {
            return Task<int>.Factory.StartNew(() => NextCheckNumCommand.Execute(accountId));
        }

    }
}
