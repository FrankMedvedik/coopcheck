using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using CoopCheck.DAL;
using System.Configuration;

namespace CoopCheck.DalADO
{
    public class PaymentInfoListDal :   IPaymentInfoListDal
    {

        public List<PaymentInfoDto> FetchBatch(int batchNum)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
            {
                using (var cmd = new SqlCommand("dbo.dal_GetPaymentByBatch", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@batch_num", batchNum).DbType = DbType.Int32;
                    var dr = cmd.ExecuteReader();
                    return LoadCollection(dr);
                }
            }
        }
        public List<PaymentInfoDto> FetchJob(int jobNum)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
            {
                using (var cmd = new SqlCommand("dbo.dal_GetPaymentByJob", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@job_num", jobNum).DbType = DbType.Int32;
                    var dr = cmd.ExecuteReader();
                    return LoadCollection(dr);
                }
            }
        }
        public List<PaymentInfoDto> FetchPerson(string personId)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
            {
                using (var cmd = new SqlCommand("dbo.dal_GetPaymentByPerson", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@person_id", personId).DbType = DbType.String;
                    var dr = cmd.ExecuteReader();
                    return LoadCollection(dr);
                }
            }
        }
        public List<PaymentInfoDto> FetchById(int tranId)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
            {
                using (var cmd = new SqlCommand("dbo.dal_GetPaymentByIdn", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tran_id", tranId).DbType = DbType.Int32;
                    var dr = cmd.ExecuteReader();
                    return LoadCollection(dr);
                }
            }
        }
        public List<PaymentInfoDto> FetchByNum(string checkNum)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
            {
                using (var cmd = new SqlCommand("dbo.dal_GetPaymentByNum", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@check_num", checkNum).DbType = DbType.String;
                    var dr = cmd.ExecuteReader();
                    return LoadCollection(dr);
                }
            }
        }
        public void FulfillSwift(int batch_num)
        {
            var b = FetchBatch(batch_num);

            var userId = ConfigurationManager.AppSettings["SwiftUserId"];
            var pwd = ConfigurationManager.AppSettings["SwiftPassword"];
            var progId = ConfigurationManager.AppSettings["SwiftProgramId"];
            var issuanceId = ConfigurationManager.AppSettings["SwiftIssuanceProductId"];
            var locId = ConfigurationManager.AppSettings["SwiftLocationId"];
            var srv = new PromoCodeService.PCService_DefClient();

            foreach (PaymentInfoDto p in b)
            {
                if (!p.Completed && !string.IsNullOrEmpty(p.Email))
                {
                    var request = new PromoCodeService.GetPromocodeRequest();
                    request.UserId = userId;
                    request.Password = pwd;
                    request.ClientId = userId;
                    request.PromocodeProgramId = progId;
                    request.LocationId = locId;
                    request.PaymentReferenceId = p.Id.ToString();
                    request.IssuanceProductId = issuanceId;
                    request.Amount = p.Amount.ToString();

                    var c = new PromoCodeService.Customer();
                    c.Address1 = p.Address1;
                    c.Address2 = p.Address2;
                    c.City = p.Municipality;
                    c.CountryCode = "USA";
                    c.EmailAddress = p.Email;
                    c.FirstName = p.FirstName;
                    c.LastName = p.LastName;
                    c.PhoneNumber = p.PhoneNumber;
                    c.PostalCode = p.PostalCode;
                    c.State = p.Region;
                    c.UserName = p.Email;

                    request.Customer = c;
                    request.RedemptionMessage = p.StudyTopic;
                    request.EmailMessage = p.ThankYou2;

                    PromoCodeService.GetPromocodeResponse response = srv.GetPromocode(request);

                    if (response.Status == "Valid")
                    {
                        using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
                        {
                            using (var cmd = new SqlCommand("dbo.dal_WritePromoCode", ctx.Connection))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@tran_id", request.PaymentReferenceId).DbType = DbType.Int32;
                                cmd.Parameters.AddWithValue("@promo_code", response.Promocode).DbType = DbType.String;
                                cmd.Parameters.AddWithValue("@paid_amount", response.Amount).DbType = DbType.Decimal;
                                cmd.Parameters.AddWithValue("@usr", Csla.ApplicationContext.User.Identity.Name).DbType = DbType.String;
                                cmd.ExecuteNonQuery();
                            }
                        }

                    }
                    else
                    {
                        var err = new Csla.WcfPortal.WcfErrorInfo();
                        err.ExceptionTypeName = "PromoCodeError";
                        err.Message = response.ResponseCode + " : " + response.SwiftErrorReason;

                        throw new Csla.DataPortalException(err);
                    }


                }
           



            
            }
        }

        public void ClearCheckBatch(List<CheckInfoDto> checks)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
            {
                foreach (var check in checks)
                {
                using (var cmd = new SqlCommand("dbo.dal_ClearCheck", ctx.Connection))
                {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tran_id", check.Id).DbType = DbType.Int32;
                        cmd.Parameters.AddWithValue("@cleared_date", check.ClearedDate).DbType = DbType.DateTime;
                        cmd.Parameters.AddWithValue("@cleared_amount", check.ClearedAmount).DbType = DbType.Decimal;
                        cmd.Parameters.AddWithValue("@usr", Csla.ApplicationContext.User.Identity.Name).DbType =
                            DbType.String;
                      cmd.ExecuteNonQuery();
                 }
                }
            }
        }

        public void ClearCheck(int tranId, DateTime clearedDate, decimal clearedAmount)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
            {
                using (var cmd = new SqlCommand("dbo.dal_ClearCheck", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tran_id", tranId).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@cleared_date", clearedDate).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@cleared_amount", clearedAmount).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@usr", Csla.ApplicationContext.User.Identity.Name).DbType =
                        DbType.String;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void VoidSwiftBatch(int batch_num)
        {
            var b = FetchBatch(batch_num);

            var userId = ConfigurationManager.AppSettings["SwiftUserId"];
            var pwd = ConfigurationManager.AppSettings["SwiftPassword"];
            var progId = ConfigurationManager.AppSettings["SwiftProgramId"];
            var issuanceId = ConfigurationManager.AppSettings["SwiftIssuanceProductId"];
            var locId = ConfigurationManager.AppSettings["SwiftLocationId"];
            var srv = new PromoCodeService.PCService_DefClient();

            foreach (PaymentInfoDto p in b)
            {
                if (!p.Completed && !string.IsNullOrEmpty(p.Email))
                {
                    var request = new PromoCodeService.UpdatePromocodeRequest();
                    request.UserId = userId;
                    request.Password = pwd;
                    request.ClientId = userId;
                    request.PromocodeProgramId = progId;
                    request.LocationId = locId;
                    
                    request.IssuanceProductId = issuanceId;
                    request.Amount = "0";
                    request.NewStatus = "Suspend";
                    request.NewStatusReasonCode = "BI";

                    request.Customer.FirstName = p.FirstName;
                    request.Customer.LastName = p.LastName;
                    
                    request.Customer.Address1 = p.Address1;
                    request.Customer.Address2 = p.Address2;
                    request.Customer.City = p.Municipality;
                    request.Customer.City = p.Region;
                    request.Customer.PostalCode = p.PostalCode;
                    request.Customer.CountryCode = p.Country;

                    request.RedemptionMessage = p.StudyTopic;
                    

                    PromoCodeService.UpdatePromocodeResponse response = srv.UpdatePromocode(request);

                    if (response.Status == "Valid")
                    {
                        using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
                        {
                            using (var cmd = new SqlCommand("dbo.dal_VoidPromoCode", ctx.Connection))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@tran_id", p.Id).DbType = DbType.Int32;
                                cmd.Parameters.AddWithValue("@usr", Csla.ApplicationContext.User.Identity.Name).DbType = DbType.String;
                                cmd.ExecuteNonQuery();
                            }
                        }

                    }
                    else
                    {
                        var err = new Csla.WcfPortal.WcfErrorInfo();
                        err.ExceptionTypeName = "PromoCodeError";
                        err.Message = response.ResponseCode + " : " + response.SwiftErrorReason;

                        throw new Csla.DataPortalException(err);
                    }
                }
            }
        }
        public void VoidSwiftCode(int tran_id)
        {
            var userId = ConfigurationManager.AppSettings["SwiftUserId"];
            var pwd = ConfigurationManager.AppSettings["SwiftPassword"];
            var progId = ConfigurationManager.AppSettings["SwiftProgramId"];
            var issuanceId = ConfigurationManager.AppSettings["SwiftIssuanceProductId"];
            var locId = ConfigurationManager.AppSettings["SwiftLocationId"];
            var srv = new PromoCodeService.PCService_DefClient();

            var dal = new VoucherEditDal();

            var p = dal.Fetch(tran_id);

            var request = new PromoCodeService.UpdatePromocodeRequest();
            request.UserId = userId;
            request.Password = pwd;
            request.ClientId = userId;
            request.PromocodeProgramId = progId;
            request.LocationId = locId;

            request.IssuanceProductId = issuanceId;
            request.Amount = "0";
            request.NewStatus = "Suspend";
            request.NewStatusReasonCode = "BI";

            request.Customer.FirstName = p.First;
            request.Customer.LastName = p.Last;

            request.Customer.Address1 = p.AddressLine1;
            request.Customer.Address2 = p.AddressLine2;
            request.Customer.City = p.Municipality;
            request.Customer.City = p.Region;
            request.Customer.PostalCode = p.PostalCode;
            request.Customer.CountryCode = p.Country;

            request.RedemptionMessage = "we have voided this promo card.";


            PromoCodeService.UpdatePromocodeResponse response = srv.UpdatePromocode(request);

            if (response.Status == "Valid")
            {
                using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
                {
                    using (var cmd = new SqlCommand("dbo.dal_VoidPromoCode", ctx.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tran_id", p.Id).DbType = DbType.Int32;
                        cmd.Parameters.AddWithValue("@usr", Csla.ApplicationContext.User.Identity.Name).DbType = DbType.String;
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            else
            {
                var err = new Csla.WcfPortal.WcfErrorInfo();
                err.ExceptionTypeName = "PromoCodeError";
                err.Message = response.ResponseCode + " : " + response.SwiftErrorReason;

                throw new Csla.DataPortalException(err);
            }
        }
        public List<PaymentInfoDto> WriteChecks(int batchNum, int accountId, int firstCheckNum)
        {
            var list = new List<PaymentInfoDto>();
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
            {
                using (var cmd = new SqlCommand("dbo.dal_WriteCheck", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@batch_num", batchNum).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@account_id", accountId).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@next_check_num", firstCheckNum).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@usr", Csla.ApplicationContext.User.Identity.Name).DbType = DbType.String;
                    var dr = cmd.ExecuteReader();
                    return LoadCollection(dr);
                }
            }

        }
        public void CommitChecks(int batchNum, int lastCheckNum)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
            {
                using (var cmd = new SqlCommand("dbo.dal_CommitChecks", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@batch_num", batchNum).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@last_check_num", lastCheckNum).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@usr", Csla.ApplicationContext.User.Identity.Name).DbType = DbType.String;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public int NextCheckNum(int accoundId)
        {
            int retVal = -1;
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
            {
                using (var cmd = new SqlCommand("dbo.dal_GetNextCheckNum", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@account_id", accoundId).DbType = DbType.Int32;
                    var prm = cmd.CreateParameter();
                    prm.ParameterName = "@nextCheckNum";
                    prm.DbType = DbType.Int32;
                    prm.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(prm);
                    cmd.ExecuteNonQuery();
                    retVal = (int)cmd.Parameters["@nextCheckNum"].Value;
                }
            }
            return retVal;
        }
        
        
        private List<PaymentInfoDto> LoadCollection(IDataReader data)
        {
            var paymentInfoList = new List<PaymentInfoDto>();
            using (var dr = new SafeDataReader(data))
            {
                while (dr.Read())
                {
                    paymentInfoList.Add(Fetch(dr));
                }
            }
            return paymentInfoList;
        }
        private PaymentInfoDto Fetch(SafeDataReader dr)
        {
            var paymentInfo = new PaymentInfoDto();
            paymentInfo.Id = dr.GetInt32("tran_id");
            paymentInfo.BatchNum = dr.GetInt32("batch_num");
            paymentInfo.PayDate = dr.GetSmartDate("pay_date");
            paymentInfo.JobNum = dr.GetInt32("job_num");
            paymentInfo.ThankYou1 = dr.GetString("thank_you_1");
            paymentInfo.StudyTopic = dr.GetString("study_topic");
            paymentInfo.ThankYou2 = dr.GetString("thank_you_2");
            paymentInfo.MarketingResearchMessage = dr.GetString("marketing_research_message");
            paymentInfo.CheckDate = dr.GetSmartDate("check_date");
            paymentInfo.CheckNum = dr.GetString("check_num");
            paymentInfo.Amount = dr.GetDecimal("tran_amount");
            paymentInfo.PersonId = dr.GetString("person_id");
            paymentInfo.Prefix = dr.GetString("name_prefix");
            paymentInfo.FirstName = dr.GetString("first_name");
            paymentInfo.MiddleName = dr.GetString("middle_name");
            paymentInfo.LastName = dr.GetString("last_name");
            paymentInfo.NameSuffix = dr.GetString("name_suffix");
            paymentInfo.Title = dr.GetString("title");
            paymentInfo.Company = dr.GetString("company");
            paymentInfo.Address1 = dr.GetString("address_1");
            paymentInfo.Address2 = dr.GetString("address_2");
            paymentInfo.Municipality = dr.GetString("municipality");
            paymentInfo.Region = dr.GetString("region");
            paymentInfo.PostalCode = dr.GetString("postal_code");
            paymentInfo.Country = dr.GetString("country");
            paymentInfo.Email = dr.GetString("email");
            paymentInfo.PhoneNumber = dr.GetString("phone_number");
            paymentInfo.Completed = dr.GetBoolean("print_flag");

            return paymentInfo;

        }

        
    }
}
