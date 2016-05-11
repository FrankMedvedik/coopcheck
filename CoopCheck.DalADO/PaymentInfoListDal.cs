using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Csla.Data;
using CoopCheck.DAL;
using System.Configuration;
using System.Linq;
using CoopCheck.DalADO.PromoCodeService;

namespace CoopCheck.DalADO
{
    
    public class PaymentInfoListDal :   IPaymentInfoListDal
    {
        public static string _defaultEmail = "EmailNOtSupplied";

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public List<PaymentInfoDto> FetchBatch(int batchNum)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
            {
                using (var cmd = new SqlCommand("dbo.dal_GetPaymentByBatch", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@batch_num", batchNum).DbType = DbType.Int32;
                    var dr = cmd.ExecuteReader();
                    log.Info(String.Format("FetchBatch called batchNum {0}",batchNum));
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
                using (var cmd = new SqlCommand("dbo.dal_GetPaymentById", ctx.Connection))
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
        public void SvrFulfillSwift(int batch_num, string email = "EmailNOtSupplied")
        {
            log.Info(String.Format("FulfillSwift called batchNum {0} by {1}", batch_num, email));
            var userId = ConfigurationManager.AppSettings["SwiftUserId"];
            var pwd = ConfigurationManager.AppSettings["SwiftPassword"];
            var progId = ConfigurationManager.AppSettings["SwiftProgramId"];
            var issuanceId = ConfigurationManager.AppSettings["SwiftIssuanceProductId"];
            var locId = ConfigurationManager.AppSettings["SwiftLocationId"];
            var srv = new PromoCodeService.PCService_DefClient();

            var b = FetchBatch(batch_num);
            ClientJobDto jraClient = GetClientForJob(b.First().JobNum);

            foreach (var p in b)
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

                    ///* ClientData as follow 
                    //1   Client
                    //2   Job
                    //3   Topic
                    //4   Batch
                    //5   Payor
                    //*/
                    ////   WindowsPrincipal user = RequestContext.Principal as WindowsPrincipal;
                    request.ClientData1 = String.Format("{0} - {1}", jraClient.ClientID, jraClient.JobName);
                    request.ClientData2 = p.JobNum.ToString();
                    request.ClientData3 = p.StudyTopic;
                    request.ClientData4 = p.BatchNum.ToString();
                    request.ClientData5 = email;

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
                    request.EmailMessage = p.StudyTopic;

                    PromoCodeService.GetPromocodeResponse response = srv.GetPromocode(request);

                    if (response.Status == "Valid")
                    {
                        log.Info(String.Format("FulfillSwift succeeded email {0} tran_id {1}", c.EmailAddress, request.PaymentReferenceId));
                        using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
                        {
                            using (var cmd = new SqlCommand("dbo.dal_WritePromoCode", ctx.Connection))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                            
                                cmd.Parameters.AddWithValue("@tran_id", request.PaymentReferenceId).DbType =
                                    DbType.Int32;
                                cmd.Parameters.AddWithValue("@promo_code", response.Promocode).DbType = DbType.String;
                                cmd.Parameters.AddWithValue("@paid_amount", response.Amount).DbType = DbType.Decimal;
                                cmd.Parameters.AddWithValue("@usr", email).DbType =
                                DbType.String;
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        var err = new Csla.WcfPortal.WcfErrorInfo();
                        err.ExceptionTypeName = "PromoCodeError";
                        err.Message = String.Format("tran_id {0} - {1} - {2}", request.PaymentReferenceId,
                            response.ResponseCode, response.ResponseMessage);
                        log.Info(String.Format("FulfillSwift FAILED: email {0} tran_id {1} reason {2} ", 
                            c.EmailAddress, request.PaymentReferenceId, response.ResponseMessage));
                        throw new Csla.DataPortalException(err);
                    }


                }
            }
            ClearPromoCodeBatch(batch_num, "useremail");
        }
        public void FulfillSwift(int batch_num)
        {
            var b = FetchBatch(batch_num);
            log.Info(String.Format("FulfillSwift called batchNum {0}", batch_num));
            var userId = ConfigurationManager.AppSettings["SwiftUserId"];
            var pwd = ConfigurationManager.AppSettings["SwiftPassword"];
            var progId = ConfigurationManager.AppSettings["SwiftProgramId"];
            var issuanceId = ConfigurationManager.AppSettings["SwiftIssuanceProductId"];
            var locId = ConfigurationManager.AppSettings["SwiftLocationId"];
            var srv = new PromoCodeService.PCService_DefClient();
            ClientJobDto jraClient = GetClientForJob(b.First().JobNum);

            foreach (var p in b )
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

                    /* ClientData as follow 
                    1   Client
                    2   Job
                    3   Topic
                    4   Batch
                    5   Payor
                    */
                 //   WindowsPrincipal user = RequestContext.Principal as WindowsPrincipal;
                    request.ClientData1 = String.Format("{0} - {1}", jraClient.ClientID, jraClient.JobName);
                    request.ClientData2 = p.JobNum.ToString();
                    request.ClientData3 = p.StudyTopic;
                    request.ClientData4 = p.BatchNum.ToString();
                    request.ClientData5 = Csla.ApplicationContext.User.Identity.Name;
                    
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
                    request.EmailMessage = p.StudyTopic;

                    PromoCodeService.GetPromocodeResponse response = srv.GetPromocode(request);

                    if (response.Status == "Valid")
                    {
                        log.Info(String.Format("FulfillSwift succeeded email {0} tran_id {1}", c.EmailAddress, request.PaymentReferenceId));
                        using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
                        {
                            using (var cmd = new SqlCommand("dbo.dal_WritePromoCode", ctx.Connection))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@tran_id", request.PaymentReferenceId).DbType =
                                    DbType.Int32;
                                cmd.Parameters.AddWithValue("@promo_code", response.Promocode).DbType = DbType.String;
                                cmd.Parameters.AddWithValue("@paid_amount", response.Amount).DbType = DbType.Decimal;
                                cmd.Parameters.AddWithValue("@usr", Csla.ApplicationContext.User.Identity.Name).DbType =
                                    DbType.String;
                                cmd.ExecuteNonQuery();
                            }
                        }

                    }
                    else
                    {
                        var err = new Csla.WcfPortal.WcfErrorInfo();
                        err.ExceptionTypeName = "PromoCodeError";
                        err.Message = String.Format("tran_id {0} - {1} - {2}", request.PaymentReferenceId,
                            response.ResponseCode, response.ResponseMessage);
                        log.Info(String.Format("FulfillSwift FAILED: email {0} tran_id {1} reason {2} ", c.EmailAddress, request.PaymentReferenceId, response.ResponseMessage));
                        throw new Csla.DataPortalException(err);
                    }


                }
            }
            ClearPromoCodeBatch(batch_num,"heloo");
        }

        class ClientJobDto
        {
            public string ClientID;
            public string JobName;
        }
        private ClientJobDto GetClientForJob(int jobNum)
        {
            try
            {
                using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
                {
                    using (var cmd = new SqlCommand("dbo.dal_GetClientForJob", ctx.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@jobNum", jobNum).DbType = DbType.Int32;
                        cmd.ExecuteNonQuery();
                        var dr = cmd.ExecuteReader();
                        var l = new List<ClientJobDto>();
                        using (var drx = new SafeDataReader(dr))
                        {
                            while (drx.Read())
                            {
                                l.Add(new ClientJobDto
                                {
                                    ClientID = drx.GetString("ClientID"),
                                    JobName = drx.GetString("JobName")
                                });
                            }
                            return l.First();
                        }
                        }
                    }
            }
            catch(Exception )
            {
                return new ClientJobDto
                {
                    ClientID = "Client not found",
                    JobName = "Job not found"
                };
            }
        }

        public void ClearCheckBatch()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
            {
                // for now i have implemented this as a service - ClearCheckSvc since I wanted to use bulk load
                // the call for 7000+ rows was taking over 45 minutes to complete using the csla one row at time code
                // 
                using (var cmd = new SqlCommand("dbo.dal_ClearCheckBatch", ctx.Connection))
                {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@usr", Csla.ApplicationContext.User.Identity.Name).DbType =
                            DbType.String;
                        cmd.ExecuteNonQuery();
                }
            }
        }

        public void ClearPromoCodeBatch(int batch_num, string email)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
            {
                // for now i have implemented this as a service - ClearCheckSvc since I wanted to use bulk load
                // the call for 7000+ rows was taking over 45 minutes to complete using the csla one row at time code
                // 
                using (var cmd = new SqlCommand("dbo.dal_ClearPromoCodeBatch", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@batch_num", batch_num).DbType = DbType.Int32;
                    //cmd.Parameters.AddWithValue("@usr", Csla.ApplicationContext.User.Identity.Name).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@usr", email).DbType = DbType.String;
                    cmd.ExecuteNonQuery();
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

        public void VoidCheck(int tranId)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
            {
                using (var cmd = new SqlCommand("dbo.dal_VoidCheck", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tran_id", tranId).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@usr", Csla.ApplicationContext.User.Identity.Name).DbType =
                        DbType.String;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void SvrVoidSwiftBatch(int batch_num, string email = "EmailNotSupplied")
        {
            var b = FetchBatch(batch_num);
            foreach (PaymentInfoDto p in b)
            {
                if(p.Completed)
                    SvrVoidSwiftPromoCode(p.Id, email);
            }
        }
        public void VoidSwiftBatch(int batch_num)
        {
            var b = FetchBatch(batch_num);
            foreach (PaymentInfoDto p in b)
            {
                VoidSwiftPromoCode(p.Id);
            }
        }

        public void VoidSwiftPromoCode(int tran_id )
        {
            var userId = ConfigurationManager.AppSettings["SwiftUserId"];
            var pwd = ConfigurationManager.AppSettings["SwiftPassword"];
            var progId = ConfigurationManager.AppSettings["SwiftProgramId"];
            var issuanceId = ConfigurationManager.AppSettings["SwiftIssuanceProductId"];
            var locId = ConfigurationManager.AppSettings["SwiftLocationId"];
            var srv = new PromoCodeService.PCService_DefClient();

            
            var p = FetchById(tran_id).First();

            var request = new PromoCodeService.UpdatePromocodeRequest();
            request.UserId = userId;
            request.Password = pwd;
            request.ClientId = userId;
            request.PromocodeProgramId = progId;
            request.LocationId = locId;
           
            request.Promocode = p.CheckNum;
            request.IssuanceProductId = issuanceId;
            request.NewStatus = "Cancel";
            request.NewStatusReasonCode = "BI";
            request.RedemptionMessage = "we have canceled this promo card.";
            var c = new PromoCodeService.Customer
            {
                Address1 = p.Address1,
                Address2 = p.Address2,
                City = p.Municipality,
                CountryCode = "USA",
                EmailAddress = p.Email,
                FirstName = p.FirstName,
                LastName = p.LastName,
                PhoneNumber = p.PhoneNumber,
                PostalCode = p.PostalCode,
                State = p.Region,
                UserName = p.Email
            };

            request.Customer = c;

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
        public void SvrVoidSwiftPromoCode(int tran_id, string email)
        {
            var userId = ConfigurationManager.AppSettings["SwiftUserId"];
            var pwd = ConfigurationManager.AppSettings["SwiftPassword"];
            var progId = ConfigurationManager.AppSettings["SwiftProgramId"];
            var issuanceId = ConfigurationManager.AppSettings["SwiftIssuanceProductId"];
            var locId = ConfigurationManager.AppSettings["SwiftLocationId"];
            var srv = new PromoCodeService.PCService_DefClient();


            var p = FetchById(tran_id).First();

            var request = new PromoCodeService.UpdatePromocodeRequest();
            request.UserId = userId;
            request.Password = pwd;
            request.ClientId = userId;
            request.PromocodeProgramId = progId;
            request.LocationId = locId;

            request.Promocode = p.CheckNum;
            request.IssuanceProductId = issuanceId;
            request.NewStatus = "Cancel";
            request.NewStatusReasonCode = "BI";
            request.RedemptionMessage = "we have canceled this promo card.";
            var c = new PromoCodeService.Customer
            {
                Address1 = p.Address1,
                Address2 = p.Address2,
                City = p.Municipality,
                CountryCode = "USA",
                EmailAddress = p.Email,
                FirstName = p.FirstName,
                LastName = p.LastName,
                PhoneNumber = p.PhoneNumber,
                PostalCode = p.PostalCode,
                State = p.Region,
                UserName = p.Email
            };

            request.Customer = c;

            PromoCodeService.UpdatePromocodeResponse response = srv.UpdatePromocode(request);

            if (response.Status == "Valid")
            {
                using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
                {
                    using (var cmd = new SqlCommand("dbo.dal_VoidPromoCode", ctx.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tran_id", p.Id).DbType = DbType.Int32;
                        cmd.Parameters.AddWithValue("@usr", email).DbType = DbType.String;
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



        public List<PaymentInfoDto> WriteCheckBatch(int batchNum, int accountId, int firstCheckNum)
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
