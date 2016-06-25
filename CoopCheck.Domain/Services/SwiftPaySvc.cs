using System;
using System.Reflection;
using CoopCheck.Domain.Contracts.Interfaces;
using CoopCheck.Library;
using log4net;

namespace CoopCheck.Domain.Services
{
    public class SwiftPaySvc : ISwiftPaySvc
    {
        //public SwiftPaySvc(ISvrBatchSwiftFulfillCommand svrBatchSwiftFulfillCommand,
        //                 ISendMailSvc sendMailSvc,
        //                 ISvrBatchSwiftVoidCommand svrBatchSwiftVoidCommand)

        public SwiftPaySvc(ISendMailSvc sendMailSvc)
        {
            _sendMailSvc = sendMailSvc;

        }
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        //private static ISvrBatchSwiftFulfillCommand _svrBatchSwiftFulfillCommand;
        private static ISendMailSvc _sendMailSvc;
        //private static ISvrBatchSwiftVoidCommand _svrBatchSwiftVoidCommand;

        public void PayBatch(int batchNum, string email)
        {
            _log.Info(string.Format("inside service - SwiftPay called user {0} batchNum {1}", email, batchNum));
            try
            {

                SvrBatchSwiftFulfillCommand.Execute(batchNum, email);


                _sendMailSvc.SendEmail(email,string.Format("Swiftpay complete for batch {0}", batchNum), "processing complete");
            }
            catch (Exception e)
            {
                _log.Error(string.Format("SwiftPay failed  batchNum {0} error {1}", batchNum, e.Message));
                _sendMailSvc.SendEmail(email, string.Format("Swiftpay Error for batch {0}", batchNum), e.Message);
            }
        }


        public  void VoidBatch(int batchNum, string email)
        {
            _log.Info(string.Format("inside service - SwiftVoid called user {0}  batchNum {1}", email, batchNum));
            try
            {
                //CoopCheck.Mocks.BatchSwiftFulfillCommand.BatchSwiftFulfill(batchNum, true);
                SvrBatchSwiftVoidCommand.Execute(batchNum, email);


                _sendMailSvc.SendEmail(email,
                    string.Format("SwiftVoid complete for batch {0}", batchNum), "processing complete");
            }
            catch (Exception e)
            {
                _log.Error(string.Format("SwiftVoid failed batchNum {0} error {1}", batchNum,e.Message));
                _sendMailSvc.SendEmail(email, string.Format("Swiftpay Error for batch {0}", batchNum), e.Message);
            }
        }
    }
}