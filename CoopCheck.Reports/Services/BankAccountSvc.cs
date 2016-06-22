using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Reports.Contracts.Models;
using CoopCheck.Repository;


namespace CoopCheck.Reports.Services
{
    public class BankAccountSvc : IBankAccountSvc
    {
        private ICoopCheckEntities _coopCheckEntities ;
        
        
        public ICoopCheckEntities CoopCheckEntities
        {
            get { return _coopCheckEntities ?? (_coopCheckEntities = new CoopCheckEntities()); }
            set { _coopCheckEntities = value; }
        }


        public BankAccount AllAccountsOption
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

        public async Task<List<BankAccount>> GetAccounts()
        {
            var accounts = 
                await
                    _coopCheckEntities.bank_accounts.Where(a => (bool) a.IsActive)
                        .OrderBy(a => a.account_name)
                        .ToListAsync();


            var y = new List<BankAccount>();
            foreach (var a in accounts)
            {
                y.Add(Mapper.Map<bank_account, BankAccount>(a)); 
            }
            return y;
        }
    }
}