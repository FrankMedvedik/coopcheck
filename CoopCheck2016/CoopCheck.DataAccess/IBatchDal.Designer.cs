using System;
using System.Collections.Generic;
using Csla;

namespace CoopCheck.DataAccess
{
    /// <summary>
    /// DAL Interface for Batch type
    /// </summary>
    public partial interface IBatchDal
    {
        /// <summary>
        /// Gets the Voucher List.
        /// </summary>
        /// <value>A <see cref="VoucherDto"/> object.</value>
        VoucherDto Voucher { get; }

        /// <summary>
        /// Gets the Voucher List.
        /// </summary>
        /// <value>A list of <see cref="VoucherDto"/>.</value>
        List<VoucherDto> VoucherList { get; }

        /// <summary>
        /// Loads a Batch object from the database.
        /// </summary>
        /// <param name="num">The fetch criteria.</param>
        /// <returns>A <see cref="BatchDto"/> object.</returns>
        BatchDto Fetch(int num);

        /// <summary>
        /// Inserts a new Batch object in the database.
        /// </summary>
        /// <param name="batch">The Batch DTO.</param>
        /// <returns>The new <see cref="BatchDto"/>.</returns>
        BatchDto Insert(BatchDto batch);

        /// <summary>
        /// Updates in the database all changes made to the Batch object.
        /// </summary>
        /// <param name="batch">The Batch DTO.</param>
        /// <returns>The updated <see cref="BatchDto"/>.</returns>
        BatchDto Update(BatchDto batch);

        /// <summary>
        /// Deletes the Batch object from database.
        /// </summary>
        /// <param name="num">The delete criteria.</param>
        void Delete(int num);
    }
}
