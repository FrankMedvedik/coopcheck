using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using CoopCheck.DAL;

namespace CoopCheck.DalADO
{
    public partial class VoucherEditDal
    {
        public VoucherEditDto Fetch(int id)
        {
            var voucherEdit = new VoucherEditDto();
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
            {
                using (var cmd = new SqlCommand("dbo.dsa_GetVoucher", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tran_id", id).DbType = DbType.Int32;
                    var dr = new SafeDataReader(cmd.ExecuteReader());
                    dr.Read();
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
                }
            }
            return voucherEdit;
        }
    }
}
