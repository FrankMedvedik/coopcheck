using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using CoopCheck.DAL.Library;

namespace CoopCheck.DalADO.Library
{
    /// <summary>
    /// DAL SQL Server implementation of <see cref="IAccountListDal"/>
    /// </summary>
    public partial class AccountListDal : IAccountListDal
    {
        /// <summary>
        /// Loads a AccountList list from the database.
        /// </summary>
        /// <returns>A list of <see cref="AccountListItemDto"/>.</returns>
        public List<AccountListItemDto> Fetch()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("MyDatabase"))
            {
                using (var cmd = new SqlCommand("dbo.GetAccountList", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var dr = cmd.ExecuteReader();
                    return LoadCollection(dr);
                }
            }
        }

        private List<AccountListItemDto> LoadCollection(IDataReader data)
        {
            var accountList = new List<AccountListItemDto>();
            using (var dr = new SafeDataReader(data))
            {
                while (dr.Read())
                {
                    accountList.Add(Fetch(dr));
                }
            }
            return accountList;
        }

        private AccountListItemDto Fetch(SafeDataReader dr)
        {
            var accountListItem = new AccountListItemDto();
            accountListItem.Id = dr.GetInt32("account_id");
            accountListItem.Name = dr.GetString("account_name");

            return accountListItem;
        }
    }
}
