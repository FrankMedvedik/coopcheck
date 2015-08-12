using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using CoopCheck.DataAccess;

namespace CoopCheck.DataAccess.Sql
{
    /// <summary>
    /// DAL SQL Server implementation of <see cref="IAccountDal"/>
    /// </summary>
    public partial class AccountDal : IAccountDal
    {
        /// <summary>
        /// Loads a Account object from the database.
        /// </summary>
        /// <param name="id">The fetch criteria.</param>
        /// <returns>A AccountDto object.</returns>
        public AccountDto Fetch(int id)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("MyDatabase"))
            {
                using (var cmd = new SqlCommand("dbo.GetAccount", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@account_id", id).DbType = DbType.Int32;
                    var dr = cmd.ExecuteReader();
                    return Fetch(dr);
                }
            }
        }

        private AccountDto Fetch(IDataReader data)
        {
            var account = new AccountDto();
            using (var dr = new SafeDataReader(data))
            {
                if (dr.Read())
                {
                    account.Id = dr.GetInt32("account_id");
                    account.Name = dr.GetString("account_name");
                    account.Description = !dr.IsDBNull("account_dscr") ? dr.GetString("account_dscr") : null;
                    account.Number = !dr.IsDBNull("account_number") ? dr.GetString("account_number") : null;
                    account.Balance = (Decimal?)dr.GetValue("balance");
                    account.LastReconciliationDate = !dr.IsDBNull("last_rec_date") ? dr.GetSmartDate("last_rec_date", true) : null;
                    account.LastReconciliationBalance = (Decimal?)dr.GetValue("last_rec_balance");
                }
            }
            return account;
        }

        /// <summary>
        /// Inserts a new Account object in the database.
        /// </summary>
        /// <param name="account">The Account DTO.</param>
        /// <returns>The new <see cref="AccountDto"/>.</returns>
        public AccountDto Insert(AccountDto account)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("MyDatabase"))
            {
                using (var cmd = new SqlCommand("dbo.AddAccount", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@account_id", account.Id).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@account_name", account.Name).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@account_dscr", account.Description == null ? (object)DBNull.Value : account.Description).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@account_number", account.Number == null ? (object)DBNull.Value : account.Number).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@balance", account.Balance == null ? (object)DBNull.Value : account.Balance.Value).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@last_rec_date", account.LastReconciliationDate.DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@last_rec_balance", account.LastReconciliationBalance == null ? (object)DBNull.Value : account.LastReconciliationBalance.Value).DbType = DbType.Decimal;
                    cmd.ExecuteNonQuery();
                    account.Id = (int)cmd.Parameters["@account_id"].Value;
                }
            }
            return account;
        }

        /// <summary>
        /// Updates in the database all changes made to the Account object.
        /// </summary>
        /// <param name="account">The Account DTO.</param>
        /// <returns>The updated <see cref="AccountDto"/>.</returns>
        public AccountDto Update(AccountDto account)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("MyDatabase"))
            {
                using (var cmd = new SqlCommand("dbo.UpdateAccount", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@account_id", account.Id).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@account_name", account.Name).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@account_dscr", account.Description == null ? (object)DBNull.Value : account.Description).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@account_number", account.Number == null ? (object)DBNull.Value : account.Number).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@balance", account.Balance == null ? (object)DBNull.Value : account.Balance.Value).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@last_rec_date", account.LastReconciliationDate.DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@last_rec_balance", account.LastReconciliationBalance == null ? (object)DBNull.Value : account.LastReconciliationBalance.Value).DbType = DbType.Decimal;
                    var rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new DataNotFoundException("Account");
                }
            }
            return account;
        }

        /// <summary>
        /// Deletes the Account object from database.
        /// </summary>
        /// <param name="id">The delete criteria.</param>
        public void Delete(int id)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("MyDatabase"))
            {
                using (var cmd = new SqlCommand("dbo.DeleteAccount", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@account_id", id).DbType = DbType.Int32;
                    var rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new DataNotFoundException("Account");
                }
            }
        }
    }
}
