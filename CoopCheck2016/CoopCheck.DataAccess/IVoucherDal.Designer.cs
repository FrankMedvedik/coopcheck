using System;
using System.Collections.Generic;
using Csla;

namespace CoopCheck.DataAccess
{
    /// <summary>
    /// DAL Interface for Voucher type
    /// </summary>
    public partial interface IVoucherDal
    {
        /// <summary>
        /// Inserts a new Voucher object in the database.
        /// </summary>
        /// <param name="voucher">The Voucher DTO.</param>
        /// <returns>The new <see cref="VoucherDto"/>.</returns>
        VoucherDto Insert(VoucherDto voucher);

        /// <summary>
        /// Updates in the database all changes made to the Voucher object.
        /// </summary>
        /// <param name="voucher">The Voucher DTO.</param>
        /// <returns>The updated <see cref="VoucherDto"/>.</returns>
        VoucherDto Update(VoucherDto voucher);

        /// <summary>
        /// Deletes the Voucher object from database.
        /// </summary>
        /// <param name="id">The Id.</param>
        void Delete(int id);
    }
}
