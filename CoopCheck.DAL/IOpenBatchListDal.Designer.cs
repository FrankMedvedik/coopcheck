using System;
using System.Collections.Generic;
using Csla;

namespace CoopCheck.DAL
{
    /// <summary>
    /// DAL Interface for OpenBatchList type
    /// </summary>
    public partial interface IOpenBatchListDal
    {
        /// <summary>
        /// Loads a OpenBatchList list from the database.
        /// </summary>
        /// <returns>A list of <see cref="OpenBatchListItemDto"/>.</returns>
        List<OpenBatchListItemDto> Fetch();
    }
}
