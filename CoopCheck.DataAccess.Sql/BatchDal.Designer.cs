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
    /// DAL SQL Server implementation of <see cref="IBatchDal"/>
    /// </summary>
    public partial class BatchDal : IBatchDal
    {
        private VoucherDto _voucher = new VoucherDto();
        private List<VoucherDto> _voucherList = new List<VoucherDto>();

        /// <summary>
        /// Gets the Voucher List.
        /// </summary>
        /// <value>A <see cref="VoucherDto"/> object.</value>
        public VoucherDto Voucher
        {
            get { return _voucher; }
        }

        /// <summary>
        /// Gets the Voucher List.
        /// </summary>
        /// <value>A list of <see cref="VoucherDto"/>.</value>
        public List<VoucherDto> VoucherList
        {
            get { return _voucherList; }
        }

        /// <summary>
        /// Loads a Batch object from the database.
        /// </summary>
        /// <param name="num">The fetch criteria.</param>
        /// <returns>A BatchDto object.</returns>
        public BatchDto Fetch(int num)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("MyDatabase"))
            {
                using (var cmd = new SqlCommand("dbo.GetBatch", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@batch_num", num).DbType = DbType.Int32;
                    var dr = cmd.ExecuteReader();
                    return Fetch(dr);
                }
            }
        }

        private BatchDto Fetch(IDataReader data)
        {
            var batch = new BatchDto();
            using (var dr = new SafeDataReader(data))
            {
                if (dr.Read())
                {
                    batch.Num = dr.GetInt32("batch_num");
                    batch.BatchDate = !dr.IsDBNull("batch_date") ? dr.GetSmartDate("batch_date", true) : null;
                    batch.PayDate = !dr.IsDBNull("pay_date") ? dr.GetSmartDate("pay_date", true) : null;
                    batch.Amount = (Decimal?)dr.GetValue("batch_amount");
                    batch.JobNum = (int?)dr.GetValue("job_num");
                    batch.Description = !dr.IsDBNull("batch_dscr") ? dr.GetString("batch_dscr") : null;
                    batch.Updated = !dr.IsDBNull("updated") ? dr.GetSmartDate("updated", true) : null;
                    batch.ThankYouMessage1 = !dr.IsDBNull("thank_you_1") ? dr.GetString("thank_you_1") : null;
                    batch.StudyTopic = !dr.IsDBNull("study_topic") ? dr.GetString("study_topic") : null;
                    batch.ThankYouMessage2 = !dr.IsDBNull("thank_you_2") ? dr.GetString("thank_you_2") : null;
                    batch.MarketingResearchMessage = !dr.IsDBNull("marketing_research_message") ? dr.GetString("marketing_research_message") : null;
                }
                FetchChildren(dr);
            }
            return batch;
        }

        private void FetchChildren(SafeDataReader dr)
        {
            dr.NextResult();
            while (dr.Read())
            {
                _voucher.Add(FetchVoucher(dr));
            }
            dr.NextResult();
            while (dr.Read())
            {
                _voucherList.Add(FetchVoucher(dr));
            }
        }

        private VoucherDto FetchVoucher(SafeDataReader dr)
        {
            var voucher = new VoucherDto();
            // Value properties
            voucher.Id = dr.GetInt32("tran_id");
            voucher.Amount = (Decimal?)dr.GetValue("tran_amount");
            voucher.PersonId = !dr.IsDBNull("person_id") ? dr.GetString("person_id") : null;
            voucher.NamePrefix = !dr.IsDBNull("name_prefix") ? dr.GetString("name_prefix") : null;
            voucher.FirstName = !dr.IsDBNull("first_name") ? dr.GetString("first_name") : null;
            voucher.MiddleName = !dr.IsDBNull("middle_name") ? dr.GetString("middle_name") : null;
            voucher.LastName = !dr.IsDBNull("last_name") ? dr.GetString("last_name") : null;
            voucher.NameSuffix = !dr.IsDBNull("name_suffix") ? dr.GetString("name_suffix") : null;
            voucher.Title = !dr.IsDBNull("title") ? dr.GetString("title") : null;
            voucher.Company = !dr.IsDBNull("company") ? dr.GetString("company") : null;
            voucher.AddressLine1 = !dr.IsDBNull("address_1") ? dr.GetString("address_1") : null;
            voucher.AddressLine2 = !dr.IsDBNull("address_2") ? dr.GetString("address_2") : null;
            voucher.Municipality = !dr.IsDBNull("municipality") ? dr.GetString("municipality") : null;
            voucher.Region = !dr.IsDBNull("region") ? dr.GetString("region") : null;
            voucher.PostalCode = !dr.IsDBNull("postal_code") ? dr.GetString("postal_code") : null;
            voucher.Country = !dr.IsDBNull("country") ? dr.GetString("country") : null;
            voucher.PhoneNumber = !dr.IsDBNull("phone_number") ? dr.GetString("phone_number") : null;
            voucher.Email = !dr.IsDBNull("email") ? dr.GetString("email") : null;
            voucher.Updated = !dr.IsDBNull("updated") ? dr.GetSmartDate("updated", true) : null;

            return voucher;
        }

        private VoucherDto FetchVoucher(SafeDataReader dr)
        {
            var voucher = new VoucherDto();
            // Value properties
            voucher.Id = dr.GetInt32("tran_id");
            voucher.Amount = (Decimal?)dr.GetValue("tran_amount");
            voucher.PersonId = !dr.IsDBNull("person_id") ? dr.GetString("person_id") : null;
            voucher.NamePrefix = !dr.IsDBNull("name_prefix") ? dr.GetString("name_prefix") : null;
            voucher.FirstName = !dr.IsDBNull("first_name") ? dr.GetString("first_name") : null;
            voucher.MiddleName = !dr.IsDBNull("middle_name") ? dr.GetString("middle_name") : null;
            voucher.LastName = !dr.IsDBNull("last_name") ? dr.GetString("last_name") : null;
            voucher.NameSuffix = !dr.IsDBNull("name_suffix") ? dr.GetString("name_suffix") : null;
            voucher.Title = !dr.IsDBNull("title") ? dr.GetString("title") : null;
            voucher.Company = !dr.IsDBNull("company") ? dr.GetString("company") : null;
            voucher.AddressLine1 = !dr.IsDBNull("address_1") ? dr.GetString("address_1") : null;
            voucher.AddressLine2 = !dr.IsDBNull("address_2") ? dr.GetString("address_2") : null;
            voucher.Municipality = !dr.IsDBNull("municipality") ? dr.GetString("municipality") : null;
            voucher.Region = !dr.IsDBNull("region") ? dr.GetString("region") : null;
            voucher.PostalCode = !dr.IsDBNull("postal_code") ? dr.GetString("postal_code") : null;
            voucher.Country = !dr.IsDBNull("country") ? dr.GetString("country") : null;
            voucher.PhoneNumber = !dr.IsDBNull("phone_number") ? dr.GetString("phone_number") : null;
            voucher.Email = !dr.IsDBNull("email") ? dr.GetString("email") : null;
            voucher.Updated = !dr.IsDBNull("updated") ? dr.GetSmartDate("updated", true) : null;

            return voucher;
        }

        /// <summary>
        /// Inserts a new Batch object in the database.
        /// </summary>
        /// <param name="batch">The Batch DTO.</param>
        /// <returns>The new <see cref="BatchDto"/>.</returns>
        public BatchDto Insert(BatchDto batch)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("MyDatabase"))
            {
                using (var cmd = new SqlCommand("dbo.AddBatch", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@batch_num", batch.Num).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@batch_date", batch.BatchDate.DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@pay_date", batch.PayDate.DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@batch_amount", batch.Amount == null ? (object)DBNull.Value : batch.Amount.Value).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@job_num", batch.JobNum == null ? (object)DBNull.Value : batch.JobNum.Value).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@batch_dscr", batch.Description == null ? (object)DBNull.Value : batch.Description).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@updated", batch.Updated.DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@thank_you_1", batch.ThankYouMessage1 == null ? (object)DBNull.Value : batch.ThankYouMessage1).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@study_topic", batch.StudyTopic == null ? (object)DBNull.Value : batch.StudyTopic).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@thank_you_2", batch.ThankYouMessage2 == null ? (object)DBNull.Value : batch.ThankYouMessage2).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@marketing_research_message", batch.MarketingResearchMessage == null ? (object)DBNull.Value : batch.MarketingResearchMessage).DbType = DbType.String;
                    cmd.ExecuteNonQuery();
                    batch.Num = (int)cmd.Parameters["@batch_num"].Value;
                }
            }
            return batch;
        }

        /// <summary>
        /// Updates in the database all changes made to the Batch object.
        /// </summary>
        /// <param name="batch">The Batch DTO.</param>
        /// <returns>The updated <see cref="BatchDto"/>.</returns>
        public BatchDto Update(BatchDto batch)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("MyDatabase"))
            {
                using (var cmd = new SqlCommand("dbo.UpdateBatch", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@batch_num", batch.Num).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@batch_date", batch.BatchDate.DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@pay_date", batch.PayDate.DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@batch_amount", batch.Amount == null ? (object)DBNull.Value : batch.Amount.Value).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@job_num", batch.JobNum == null ? (object)DBNull.Value : batch.JobNum.Value).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@batch_dscr", batch.Description == null ? (object)DBNull.Value : batch.Description).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@updated", batch.Updated.DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@thank_you_1", batch.ThankYouMessage1 == null ? (object)DBNull.Value : batch.ThankYouMessage1).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@study_topic", batch.StudyTopic == null ? (object)DBNull.Value : batch.StudyTopic).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@thank_you_2", batch.ThankYouMessage2 == null ? (object)DBNull.Value : batch.ThankYouMessage2).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@marketing_research_message", batch.MarketingResearchMessage == null ? (object)DBNull.Value : batch.MarketingResearchMessage).DbType = DbType.String;
                    var rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new DataNotFoundException("Batch");
                }
            }
            return batch;
        }

        /// <summary>
        /// Deletes the Batch object from database.
        /// </summary>
        /// <param name="num">The delete criteria.</param>
        public void Delete(int num)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("MyDatabase"))
            {
                using (var cmd = new SqlCommand("dbo.DeleteBatch", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@batch_num", num).DbType = DbType.Int32;
                    var rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new DataNotFoundException("Batch");
                }
            }
        }
    }
}
