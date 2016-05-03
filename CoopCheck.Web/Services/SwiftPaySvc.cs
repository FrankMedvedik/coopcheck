using System;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using CoopCheck.Library;

namespace CoopCheck.Web.Services
{
    public class SwiftPaySvc
    {
        private static readonly log4net.ILog log
       = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void PayBatch(int accountId, int batchNum,  string email)
        {
          
                 log.Info(String.Format("inside service - SwiftPay called user {0}  account {1} batchNum {2}", email, accountId, batchNum));
                    try
                    {
                        //CoopCheck.Mocks.BatchSwiftFulfillCommand.BatchSwiftFulfill(batchNum, true);
                        BatchSwiftFulfillCommand.BatchSwiftFulfill(batchNum);
                SendMailSvc.SendEmail(email,
                            String.Format("Swiftpay complete for batch {0}", batchNum), "processing complete");
                    }
                    catch (Exception e)
                    {
                        log.Error(String.Format("SwiftPay failed  account {0} batchNum {1} error {2}",accountId, batchNum, e.Message));
                        SendMailSvc.SendEmail(email,String.Format("Swiftpay Error for batch {0}", batchNum), e.Message);
                    }
                }
        }
    }
