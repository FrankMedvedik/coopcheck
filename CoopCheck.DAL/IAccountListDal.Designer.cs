using System;
using System.Collections.Generic;
using Csla;

namespace CoopCheck.DAL
{
    /// <summary>
    /// DAL Interface for AccountList type
    /// </summary>
    public partial interface IAccountListDal
    {
        /// <summary>
        /// Loads a AccountList list from the database.
        /// </summary>
        /// <returns>A list of <see cref="AccountListItemDto"/>.</returns>
        List<AccountListItemDto> Fetch();
    }
}
