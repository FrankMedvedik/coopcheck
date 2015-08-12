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
    /// DAL SQL Server implementation of <see cref="IVoucherDal"/>
    /// </summary>
    public partial class VoucherDal : IVoucherDal
    {
        /// <summary>
        /// Inserts a new Voucher object in the database.
        /// </summary>
        /// <param name="voucher">The Voucher DTO.</param>
        /// <returns>The new <see cref="VoucherDto"/>.</returns>
        public VoucherDto Insert(VoucherDto voucher)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("MyDatabase"))
            {
                using (var cmd = new SqlCommand("dbo.AddVoucher", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@batch_num", voucher.Parent_Num).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@tran_id", voucher.Id).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@tran_amount", voucher.Amount == null ? (object)DBNull.Value : voucher.Amount.Value).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@person_id", voucher.PersonId == null ? (object)DBNull.Value : voucher.PersonId).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@name_prefix", voucher.NamePrefix == null ? (object)DBNull.Value : voucher.NamePrefix).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@first_name", voucher.FirstName == null ? (object)DBNull.Value : voucher.FirstName).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@middle_name", voucher.MiddleName == null ? (object)DBNull.Value : voucher.MiddleName).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@last_name", voucher.LastName == null ? (object)DBNull.Value : voucher.LastName).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@name_suffix", voucher.NameSuffix == null ? (object)DBNull.Value : voucher.NameSuffix).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@title", voucher.Title == null ? (object)DBNull.Value : voucher.Title).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@company", voucher.Company == null ? (object)DBNull.Value : voucher.Company).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@address_1", voucher.AddressLine1 == null ? (object)DBNull.Value : voucher.AddressLine1).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@address_2", voucher.AddressLine2 == null ? (object)DBNull.Value : voucher.AddressLine2).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@municipality", voucher.Municipality == null ? (object)DBNull.Value : voucher.Municipality).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@region", voucher.Region == null ? (object)DBNull.Value : voucher.Region).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@postal_code", voucher.PostalCode == null ? (object)DBNull.Value : voucher.PostalCode).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@country", voucher.Country == null ? (object)DBNull.Value : voucher.Country).DbType = DbType.StringFixedLength;
                    cmd.Parameters.AddWithValue("@phone_number", voucher.PhoneNumber == null ? (object)DBNull.Value : voucher.PhoneNumber).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@email", voucher.Email == null ? (object)DBNull.Value : voucher.Email).DbType = DbType.String;
                    cmd.ExecuteNonQuery();
                    voucher.Id = (int)cmd.Parameters["@tran_id"].Value;
                }
            }
            return voucher;
        }

        /// <summary>
        /// Updates in the database all changes made to the Voucher object.
        /// </summary>
        /// <param name="voucher">The Voucher DTO.</param>
        /// <returns>The updated <see cref="VoucherDto"/>.</returns>
        public VoucherDto Update(VoucherDto voucher)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("MyDatabase"))
            {
                using (var cmd = new SqlCommand("dbo.UpdateVoucher", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tran_id", voucher.Id).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@tran_amount", voucher.Amount == null ? (object)DBNull.Value : voucher.Amount.Value).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@person_id", voucher.PersonId == null ? (object)DBNull.Value : voucher.PersonId).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@name_prefix", voucher.NamePrefix == null ? (object)DBNull.Value : voucher.NamePrefix).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@first_name", voucher.FirstName == null ? (object)DBNull.Value : voucher.FirstName).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@middle_name", voucher.MiddleName == null ? (object)DBNull.Value : voucher.MiddleName).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@last_name", voucher.LastName == null ? (object)DBNull.Value : voucher.LastName).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@name_suffix", voucher.NameSuffix == null ? (object)DBNull.Value : voucher.NameSuffix).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@title", voucher.Title == null ? (object)DBNull.Value : voucher.Title).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@company", voucher.Company == null ? (object)DBNull.Value : voucher.Company).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@address_1", voucher.AddressLine1 == null ? (object)DBNull.Value : voucher.AddressLine1).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@address_2", voucher.AddressLine2 == null ? (object)DBNull.Value : voucher.AddressLine2).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@municipality", voucher.Municipality == null ? (object)DBNull.Value : voucher.Municipality).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@region", voucher.Region == null ? (object)DBNull.Value : voucher.Region).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@postal_code", voucher.PostalCode == null ? (object)DBNull.Value : voucher.PostalCode).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@country", voucher.Country == null ? (object)DBNull.Value : voucher.Country).DbType = DbType.StringFixedLength;
                    cmd.Parameters.AddWithValue("@phone_number", voucher.PhoneNumber == null ? (object)DBNull.Value : voucher.PhoneNumber).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@email", voucher.Email == null ? (object)DBNull.Value : voucher.Email).DbType = DbType.String;
                    var rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new DataNotFoundException("Voucher");
                }
            }
            return voucher;
        }

        /// <summary>
        /// Deletes the Voucher object from database.
        /// </summary>
        /// <param name="id">The Id.</param>
        public void Delete(int id)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("MyDatabase"))
            {
                using (var cmd = new SqlCommand("dbo.DeleteVoucher", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tran_id", id).DbType = DbType.Int32;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
