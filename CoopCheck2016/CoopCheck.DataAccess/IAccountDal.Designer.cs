using System;
using System.Collections.Generic;
using Csla;

namespace CoopCheck.DataAccess
{
    /// <summary>
    /// DAL Interface for Account type
    /// </summary>
    public partial interface IAccountDal
    {
        /// <summary>
        /// Loads a Account object from the database.
        /// </summary>
        /// <param name="id">The fetch criteria.</param>
        /// <returns>A <see cref="AccountDto"/> object.</returns>
        AccountDto Fetch(int id);

        /// <summary>
        /// Inserts a new Account object in the database.
        /// </summary>
        /// <param name="account">The Account DTO.</param>
        /// <returns>The new <see cref="AccountDto"/>.</returns>
        AccountDto Insert(AccountDto account);

        /// <summary>
        /// Updates in the database all changes made to the Account object.
        /// </summary>
        /// <param name="account">The Account DTO.</param>
        /// <returns>The updated <see cref="AccountDto"/>.</returns>
        AccountDto Update(AccountDto account);

        /// <summary>
        /// Deletes the Account object from database.
        /// </summary>
        /// <param name="id">The delete criteria.</param>
        void Delete(int id);
    }
}
