using System;
using System.Reflection;
using CoopCheck.Library;
using log4net;

namespace CoopCheck.Web.Services
{
    public class SwiftPaySvc
    {
        private static readonly ILog log
            = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void PayBatch(int batchNum, string email)
        {
            log.Info(string.Format("inside service - SwiftPay called user {0} batchNum {1}", email, batchNum));
            try
            {
                //CoopCheck.Mocks.BatchSwiftFulfillCommand.BatchSwiftFulfill(batchNum, true);
                SvrBatchSwiftFulfillCommand.Execute(batchNum, email);


                SendMailSvc.SendEmail(email,string.Format("Swiftpay complete for batch {0}", batchNum), "processing complete");
            }
            catch (Exception e)
            {
                log.Error(string.Format("SwiftPay failed  batchNum {0} error {1}", batchNum, e.Message));
                SendMailSvc.SendEmail(email, string.Format("Swiftpay Error for batch {0}", batchNum), e.Message);
            }
        }


        public static void VoidBatch(int batchNum, string email)
        {
            log.Info(string.Format("inside service - SwiftVoid called user {0}  batchNum {1}", email, batchNum));
            try
            {
                //CoopCheck.Mocks.BatchSwiftFulfillCommand.BatchSwiftFulfill(batchNum, true);
                SvrBatchSwiftVoidCommand.Execute(batchNum, email);


                SendMailSvc.SendEmail(email,
                    string.Format("SwiftVoid complete for batch {0}", batchNum), "processing complete");
            }
            catch (Exception e)
            {
                log.Error(string.Format("SwiftVoid failed batchNum {0} error {1}", batchNum,e.Message));
                SendMailSvc.SendEmail(email, string.Format("Swiftpay Error for batch {0}", batchNum), e.Message);
            }
        }
    }
}