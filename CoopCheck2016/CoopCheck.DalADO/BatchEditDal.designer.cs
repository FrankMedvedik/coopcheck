using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using CoopCheck.DAL;

namespace CoopCheck.DalADO
{
    /// <summary>
    /// DAL SQL Server implementation of <see cref="IBatchEditDal"/>
    /// </summary>
    public partial class BatchEditDal : IBatchEditDal
    {
        private List<VoucherEditDto> _voucherList = new List<VoucherEditDto>();

        /// <summary>
        /// Gets the Vouchers.
        /// </summary>
        /// <value>A list of <see cref="VoucherEditDto"/>.</value>
        public List<VoucherEditDto> VoucherList
        {
            get { return _voucherList; }
        }

        /// <summary>
        /// Loads a BatchEdit object from the database.
        /// </summary>
        /// <param name="batch_num">The fetch criteria.</param>
        /// <returns>A BatchEditDto object.</returns>
        public BatchEditDto Fetch(int batch_num)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
            {
                using (var cmd = new SqlCommand("dbo.dsa_GetBatchEdit", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@batch_num", batch_num).DbType = DbType.Int32;
                    var dr = cmd.ExecuteReader();
                    return Fetch(dr);
                }
            }
        }

        private BatchEditDto Fetch(IDataReader data)
        {
            var batchEdit = new BatchEditDto();
            using (var dr = new SafeDataReader(data))
            {
                if (dr.Read())
                {
                    batchEdit.Num = dr.GetInt32("batch_num");
                    batchEdit.Date = !dr.IsDBNull("batch_date") ? dr.GetSmartDate("batch_date", true) : null;
                    batchEdit.PayDate = !dr.IsDBNull("pay_date") ? dr.GetSmartDate("pay_date", true) : null;
                    batchEdit.Amount = (Decimal?)dr.GetValue("batch_amount");
                    batchEdit.JobNum = (int?)dr.GetValue("job_num");
                    batchEdit.Description = !dr.IsDBNull("batch_dscr") ? dr.GetString("batch_dscr") : null;
                    batchEdit.Updated = !dr.IsDBNull("updated") ? dr.GetSmartDate("updated", true) : null;
                    batchEdit.ThankYou1 = !dr.IsDBNull("thank_you_1") ? dr.GetString("thank_you_1") : null;
                    batchEdit.StudyTopic = !dr.IsDBNull("study_topic") ? dr.GetString("study_topic") : null;
                    batchEdit.ThankYou2 = !dr.IsDBNull("thank_you_2") ? dr.GetString("thank_you_2") : null;
                    batchEdit.MarketingResearchMessage = !dr.IsDBNull("marketing_research_message") ? dr.GetString("marketing_research_message") : null;
                }
                FetchChildren(dr);
            }
            return batchEdit;
        }

        private void FetchChildren(SafeDataReader dr)
        {
            dr.NextResult();
            while (dr.Read())
            {
                _voucherList.Add(FetchVoucherEdit(dr));
            }
        }

        private VoucherEditDto FetchVoucherEdit(SafeDataReader dr)
        {
            var voucherEdit = new VoucherEditDto();
            // Value properties
            voucherEdit.Id = dr.GetInt32("tran_id");
            voucherEdit.Amount = (Decimal?)dr.GetValue("tran_amount");
            voucherEdit.PersonId = !dr.IsDBNull("person_id") ? dr.GetString("person_id") : null;
            voucherEdit.NamePrefix = !dr.IsDBNull("name_prefix") ? dr.GetString("name_prefix") : null;
            voucherEdit.First = !dr.IsDBNull("first_name") ? dr.GetString("first_name") : null;
            voucherEdit.Middle = !dr.IsDBNull("middle_name") ? dr.GetString("middle_name") : null;
            voucherEdit.Last = !dr.IsDBNull("last_name") ? dr.GetString("last_name") : null;
            voucherEdit.Suffix = !dr.IsDBNull("name_suffix") ? dr.GetString("name_suffix") : null;
            voucherEdit.Title = !dr.IsDBNull("title") ? dr.GetString("title") : null;
            voucherEdit.Company = !dr.IsDBNull("company") ? dr.GetString("company") : null;
            voucherEdit.AddressLine1 = !dr.IsDBNull("address_1") ? dr.GetString("address_1") : null;
            voucherEdit.AddressLine2 = !dr.IsDBNull("address_2") ? dr.GetString("address_2") : null;
            voucherEdit.Municipality = !dr.IsDBNull("municipality") ? dr.GetString("municipality") : null;
            voucherEdit.Region = !dr.IsDBNull("region") ? dr.GetString("region") : null;
            voucherEdit.PostalCode = !dr.IsDBNull("postal_code") ? dr.GetString("postal_code") : null;
            voucherEdit.Country = !dr.IsDBNull("country") ? dr.GetString("country") : null;
            voucherEdit.PhoneNumber = !dr.IsDBNull("phone_number") ? dr.GetString("phone_number") : null;
            voucherEdit.EmailAddress = !dr.IsDBNull("email") ? dr.GetString("email") : null;
            voucherEdit.Updated = !dr.IsDBNull("updated") ? dr.GetSmartDate("updated", true) : null;

            return voucherEdit;
        }

        /// <summary>
        /// Inserts a new BatchEdit object in the database.
        /// </summary>
        /// <param name="batchEdit">The Batch Edit DTO.</param>
        /// <returns>The new <see cref="BatchEditDto"/>.</returns>
        public BatchEditDto Insert(BatchEditDto batchEdit)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
            {
                using (var cmd = new SqlCommand("dbo.dsa_AddBatch", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@batch_num", batchEdit.Num).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@batch_date", batchEdit.Date.DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@pay_date", batchEdit.PayDate.DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@batch_amount", batchEdit.Amount == null ? (object)DBNull.Value : batchEdit.Amount.Value).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@job_num", batchEdit.JobNum == null ? (object)DBNull.Value : batchEdit.JobNum.Value).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@batch_dscr", batchEdit.Description == null ? (object)DBNull.Value : batchEdit.Description).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@thank_you_1", batchEdit.ThankYou1 == null ? (object)DBNull.Value : batchEdit.ThankYou1).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@study_topic", batchEdit.StudyTopic == null ? (object)DBNull.Value : batchEdit.StudyTopic).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@thank_you_2", batchEdit.ThankYou2 == null ? (object)DBNull.Value : batchEdit.ThankYou2).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@marketing_research_message", batchEdit.MarketingResearchMessage == null ? (object)DBNull.Value : batchEdit.MarketingResearchMessage).DbType = DbType.String;
                   
                    cmd.ExecuteNonQuery();
                    batchEdit.Num = (int)cmd.Parameters["@batch_num"].Value;
                }
            }
            return batchEdit;
        }

        /// <summary>
        /// Updates in the database all changes made to the BatchEdit object.
        /// </summary>
        /// <param name="batchEdit">The Batch Edit DTO.</param>
        /// <returns>The updated <see cref="BatchEditDto"/>.</returns>
        public BatchEditDto Update(BatchEditDto batchEdit)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
            {
                using (var cmd = new SqlCommand("dbo.dsa_UpdateBatch", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@batch_num", batchEdit.Num).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@batch_date", batchEdit.Date.DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@pay_date", batchEdit.PayDate.DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@batch_amount", batchEdit.Amount == null ? (object)DBNull.Value : batchEdit.Amount.Value).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@job_num", batchEdit.JobNum == null ? (object)DBNull.Value : batchEdit.JobNum.Value).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@batch_dscr", batchEdit.Description == null ? (object)DBNull.Value : batchEdit.Description).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@thank_you_1", batchEdit.ThankYou1 == null ? (object)DBNull.Value : batchEdit.ThankYou1).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@study_topic", batchEdit.StudyTopic == null ? (object)DBNull.Value : batchEdit.StudyTopic).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@thank_you_2", batchEdit.ThankYou2 == null ? (object)DBNull.Value : batchEdit.ThankYou2).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@marketing_research_message", batchEdit.MarketingResearchMessage == null ? (object)DBNull.Value : batchEdit.MarketingResearchMessage).DbType = DbType.String;
                    var rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new DataNotFoundException("BatchEdit");
                }
            }
            return batchEdit;
        }

        /// <summary>
        /// Deletes the BatchEdit object from database.
        /// </summary>
        /// <param name="batch_num">The delete criteria.</param>
        public void Delete(int batch_num)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
            {
                using (var cmd = new SqlCommand("dbo.dsa_DeleteBatchEdit", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@batch_num", batch_num).DbType = DbType.Int32;
                    var rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new DataNotFoundException("BatchEdit");
                }
            }
        }
    }
}
