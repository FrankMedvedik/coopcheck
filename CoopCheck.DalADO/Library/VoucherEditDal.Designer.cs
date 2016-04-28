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
    /// DAL SQL Server implementation of <see cref="IVoucherEditDal"/>
    /// </summary>
    public partial class VoucherEditDal : IVoucherEditDal
    {
        /// <summary>
        /// Inserts a new VoucherEdit object in the database.
        /// </summary>
        /// <param name="voucherEdit">The Voucher Edit DTO.</param>
        /// <returns>The new <see cref="VoucherEditDto"/>.</returns>
        public VoucherEditDto Insert(VoucherEditDto voucherEdit)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("MyDatabase"))
            {
                using (var cmd = new SqlCommand("dbo.AddVoucherEdit", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@batch_num", voucherEdit.Parent_Num).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@tran_id", voucherEdit.Id).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@tran_amount", voucherEdit.Amount == null ? (object)DBNull.Value : voucherEdit.Amount.Value).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@person_id", voucherEdit.PersonId == null ? (object)DBNull.Value : voucherEdit.PersonId).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@name_prefix", voucherEdit.NamePrefix == null ? (object)DBNull.Value : voucherEdit.NamePrefix).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@first_name", voucherEdit.First == null ? (object)DBNull.Value : voucherEdit.First).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@middle_name", voucherEdit.Middle == null ? (object)DBNull.Value : voucherEdit.Middle).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@last_name", voucherEdit.Last == null ? (object)DBNull.Value : voucherEdit.Last).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@name_suffix", voucherEdit.Suffix == null ? (object)DBNull.Value : voucherEdit.Suffix).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@title", voucherEdit.Title == null ? (object)DBNull.Value : voucherEdit.Title).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@company", voucherEdit.Company == null ? (object)DBNull.Value : voucherEdit.Company).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@address_1", voucherEdit.AddressLine1 == null ? (object)DBNull.Value : voucherEdit.AddressLine1).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@address_2", voucherEdit.AddressLine2 == null ? (object)DBNull.Value : voucherEdit.AddressLine2).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@municipality", voucherEdit.Municipality == null ? (object)DBNull.Value : voucherEdit.Municipality).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@region", voucherEdit.Region == null ? (object)DBNull.Value : voucherEdit.Region).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@postal_code", voucherEdit.PostalCode == null ? (object)DBNull.Value : voucherEdit.PostalCode).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@country", voucherEdit.Country == null ? (object)DBNull.Value : voucherEdit.Country).DbType = DbType.StringFixedLength;
                    cmd.Parameters.AddWithValue("@phone_number", voucherEdit.PhoneNumber == null ? (object)DBNull.Value : voucherEdit.PhoneNumber).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@email", voucherEdit.EmailAddress == null ? (object)DBNull.Value : voucherEdit.EmailAddress).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@updated", voucherEdit.Updated.DBValue).DbType = DbType.DateTime;
                    cmd.ExecuteNonQuery();
                    voucherEdit.Id = (int)cmd.Parameters["@tran_id"].Value;
                }
            }
            return voucherEdit;
        }

        /// <summary>
        /// Updates in the database all changes made to the VoucherEdit object.
        /// </summary>
        /// <param name="voucherEdit">The Voucher Edit DTO.</param>
        /// <returns>The updated <see cref="VoucherEditDto"/>.</returns>
        public VoucherEditDto Update(VoucherEditDto voucherEdit)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("MyDatabase"))
            {
                using (var cmd = new SqlCommand("dbo.UpdateVoucherEdit", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tran_id", voucherEdit.Id).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@tran_amount", voucherEdit.Amount == null ? (object)DBNull.Value : voucherEdit.Amount.Value).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@person_id", voucherEdit.PersonId == null ? (object)DBNull.Value : voucherEdit.PersonId).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@name_prefix", voucherEdit.NamePrefix == null ? (object)DBNull.Value : voucherEdit.NamePrefix).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@first_name", voucherEdit.First == null ? (object)DBNull.Value : voucherEdit.First).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@middle_name", voucherEdit.Middle == null ? (object)DBNull.Value : voucherEdit.Middle).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@last_name", voucherEdit.Last == null ? (object)DBNull.Value : voucherEdit.Last).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@name_suffix", voucherEdit.Suffix == null ? (object)DBNull.Value : voucherEdit.Suffix).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@title", voucherEdit.Title == null ? (object)DBNull.Value : voucherEdit.Title).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@company", voucherEdit.Company == null ? (object)DBNull.Value : voucherEdit.Company).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@address_1", voucherEdit.AddressLine1 == null ? (object)DBNull.Value : voucherEdit.AddressLine1).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@address_2", voucherEdit.AddressLine2 == null ? (object)DBNull.Value : voucherEdit.AddressLine2).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@municipality", voucherEdit.Municipality == null ? (object)DBNull.Value : voucherEdit.Municipality).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@region", voucherEdit.Region == null ? (object)DBNull.Value : voucherEdit.Region).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@postal_code", voucherEdit.PostalCode == null ? (object)DBNull.Value : voucherEdit.PostalCode).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@country", voucherEdit.Country == null ? (object)DBNull.Value : voucherEdit.Country).DbType = DbType.StringFixedLength;
                    cmd.Parameters.AddWithValue("@phone_number", voucherEdit.PhoneNumber == null ? (object)DBNull.Value : voucherEdit.PhoneNumber).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@email", voucherEdit.EmailAddress == null ? (object)DBNull.Value : voucherEdit.EmailAddress).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@updated", voucherEdit.Updated.DBValue).DbType = DbType.DateTime;
                    var rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new DataNotFoundException("VoucherEdit");
                }
            }
            return voucherEdit;
        }

        /// <summary>
        /// Deletes the VoucherEdit object from database.
        /// </summary>
        /// <param name="id">The Id.</param>
        public void Delete(int id)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("MyDatabase"))
            {
                using (var cmd = new SqlCommand("dbo.DeleteVoucherEdit", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tran_id", id).DbType = DbType.Int32;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
