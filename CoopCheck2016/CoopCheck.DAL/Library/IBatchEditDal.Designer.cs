using System;
using System.Collections.Generic;
using Csla;

namespace CoopCheck.DAL.Library
{
    /// <summary>
    /// DAL Interface for BatchEdit type
    /// </summary>
    public partial interface IBatchEditDal
    {
        /// <summary>
        /// Gets the Vouchers.
        /// </summary>
        /// <value>A list of <see cref="VoucherEditDto"/>.</value>
        List<VoucherEditDto> VoucherList { get; }

        /// <summary>
        /// Loads a BatchEdit object from the database.
        /// </summary>
        /// <param name="batch_num">The fetch criteria.</param>
        /// <returns>A <see cref="BatchEditDto"/> object.</returns>
        BatchEditDto Fetch(int batch_num);

        /// <summary>
        /// Inserts a new BatchEdit object in the database.
        /// </summary>
        /// <param name="batchEdit">The Batch Edit DTO.</param>
        /// <returns>The new <see cref="BatchEditDto"/>.</returns>
        BatchEditDto Insert(BatchEditDto batchEdit);

        /// <summary>
        /// Updates in the database all changes made to the BatchEdit object.
        /// </summary>
        /// <param name="batchEdit">The Batch Edit DTO.</param>
        /// <returns>The updated <see cref="BatchEditDto"/>.</returns>
        BatchEditDto Update(BatchEditDto batchEdit);

        /// <summary>
        /// Deletes the BatchEdit object from database.
        /// </summary>
        /// <param name="batch_num">The delete criteria.</param>
        void Delete(int batch_num);
    }
}
