using System;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Hosting;
using CoopCheck.Library;

namespace CoopCheck.Web.Services
{
    public class SwiftPaySvc
    {
        private static readonly log4net.ILog log
       = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static async Task PayBatch(int accountId, int batchNum, WindowsPrincipal user)
        {
            
            log.Info(String.Format("SwiftPay called user {0}  account {1} batchNum {2}", user.Identity.Name, accountId, batchNum));
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    CoopCheck.Mocks.BatchSwiftFulfillCommand.BatchSwiftFulfill(batchNum,true);
                });
                SendMailSvc.SendEmail("fmedvedik@reckner.com",
                    String.Format("Swiftpay complete for batch {0}", batchNum), "processing complete");
            }
            catch (Exception e)
            {
                log.Error(String.Format("SwiftPay failed user {0}  account {1} batchNum {2} error {3}", user.Identity.Name, accountId, batchNum, e.Message));
                    SendMailSvc.SendEmail("fmedvedik@reckner.com",
                    String.Format("Swiftpay Error for batch {0}", batchNum), e.Message);
            }

        }
    }
}