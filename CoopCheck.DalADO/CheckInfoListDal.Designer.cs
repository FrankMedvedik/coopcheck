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
    /// DAL SQL Server implementation of <see cref="ICheckInfoListDal"/>
    /// </summary>
    public partial class CheckInfoListDal : ICheckInfoListDal
    {
        /// <summary>
        /// Loads a CheckInfoList collection from the database.
        /// </summary>
        /// <param name="personId">The fetch criteria.</param>
        /// <returns>A list of <see cref="CheckInfoDto"/>.</returns>
        public List<CheckInfoDto> Fetch(string personId)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
            {
                using (var cmd = new SqlCommand("dbo.dsa_GetCheckInfoList", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@person_id", personId).DbType = DbType.String;
                    var dr = cmd.ExecuteReader();
                    return LoadCollection(dr);
                }
            }
        }

        private List<CheckInfoDto> LoadCollection(IDataReader data)
        {
            var checkInfoList = new List<CheckInfoDto>();
            using (var dr = new SafeDataReader(data))
            {
                while (dr.Read())
                {
                    checkInfoList.Add(Fetch(dr));
                }
            }
            return checkInfoList;
        }

        private CheckInfoDto Fetch(SafeDataReader dr)
        {
            var checkInfo = new CheckInfoDto();
            // Value properties
            checkInfo.Id = dr.GetInt32("tran_id");
            checkInfo.Date = !dr.IsDBNull("check_date") ? dr.GetSmartDate("check_date", true) : null;
            checkInfo.BatchNum = (int?)dr.GetValue("batch_num");
            checkInfo.AccountId = (int?)dr.GetValue("account_id");
            checkInfo.CheckNum = !dr.IsDBNull("check_num") ? dr.GetString("check_num") : null;
            checkInfo.Amount = (Decimal?)dr.GetValue("tran_amount");
            checkInfo.ClearedAmount = (Decimal?)dr.GetValue("cleared_amount");
            checkInfo.ClearedDate = !dr.IsDBNull("cleared_date") ? dr.GetSmartDate("cleared_date", true) : null;
            checkInfo.IsPrinted = dr.GetBoolean("print_flag");
            checkInfo.IsCleared = dr.GetBoolean("cleared_flag");
            checkInfo.PersonId = !dr.IsDBNull("person_id") ? dr.GetString("person_id") : null;
            checkInfo.Prefix = !dr.IsDBNull("name_prefix") ? dr.GetString("name_prefix") : null;
            checkInfo.First = !dr.IsDBNull("first_name") ? dr.GetString("first_name") : null;
            checkInfo.Middle = !dr.IsDBNull("middle_name") ? dr.GetString("middle_name") : null;
            checkInfo.Last = !dr.IsDBNull("last_name") ? dr.GetString("last_name") : null;
            checkInfo.Suffix = !dr.IsDBNull("name_suffix") ? dr.GetString("name_suffix") : null;
            checkInfo.Title = !dr.IsDBNull("title") ? dr.GetString("title") : null;
            checkInfo.Company = !dr.IsDBNull("company") ? dr.GetString("company") : null;
            checkInfo.AddressLine1 = !dr.IsDBNull("address_1") ? dr.GetString("address_1") : null;
            checkInfo.AddressLine2 = !dr.IsDBNull("address_2") ? dr.GetString("address_2") : null;
            checkInfo.Municipality = !dr.IsDBNull("municipality") ? dr.GetString("municipality") : null;
            checkInfo.Region = !dr.IsDBNull("region") ? dr.GetString("region") : null;
            checkInfo.PostalCode = !dr.IsDBNull("postal_code") ? dr.GetString("postal_code") : null;
            checkInfo.Country = !dr.IsDBNull("country") ? dr.GetString("country") : null;
            checkInfo.PhoneNumber = !dr.IsDBNull("phone_number") ? dr.GetString("phone_number") : null;
            checkInfo.Email = !dr.IsDBNull("email") ? dr.GetString("email") : null;
            checkInfo.Updated = !dr.IsDBNull("updated") ? dr.GetSmartDate("updated", true) : null;

            return checkInfo;
        }
    }
}
