using System;
using System.Collections.Generic;
using Csla;

namespace CoopCheck.DAL
{
    /// <summary>
    /// DAL Interface for CheckInfoList type
    /// </summary>
    public partial interface ICheckInfoListDal
    {
        /// <summary>
        /// Loads a CheckInfoList collection from the database.
        /// </summary>
        /// <param name="personId">The fetch criteria.</param>
        /// <returns>A list of <see cref="CheckInfoDto"/>.</returns>
        List<CheckInfoDto> Fetch(string personId);
    }
}
