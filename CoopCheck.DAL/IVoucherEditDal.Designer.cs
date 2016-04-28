using System;
using System.Collections.Generic;
using Csla;

namespace CoopCheck.DAL
{
    /// <summary>
    /// DAL Interface for VoucherEdit type
    /// </summary>
    public partial interface IVoucherEditDal
    {
        /// <summary>
        /// Inserts a new VoucherEdit object in the database.
        /// </summary>
        /// <param name="voucherEdit">The Voucher Edit DTO.</param>
        /// <returns>The new <see cref="VoucherEditDto"/>.</returns>
        VoucherEditDto Insert(VoucherEditDto voucherEdit);

        /// <summary>
        /// Updates in the database all changes made to the VoucherEdit object.
        /// </summary>
        /// <param name="voucherEdit">The Voucher Edit DTO.</param>
        /// <returns>The updated <see cref="VoucherEditDto"/>.</returns>
        VoucherEditDto Update(VoucherEditDto voucherEdit);

        /// <summary>
        /// Deletes the VoucherEdit object from database.
        /// </summary>
        /// <param name="id">The Id.</param>
        void Delete(int id);

        VoucherEditDto Fetch(int id);
    }
}
