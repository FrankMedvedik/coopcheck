using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using CoopCheck.Library;
using CoopCheck.Repository;
using CoopCheck.Web.Services;
using Hangfire;
namespace CoopCheck.Web.Controllers
{
 
    [Authorize]
    [RoutePrefix("api/SwiftPayment")]
    public class SwiftPaymentController : ApiController
    {
        private static readonly log4net.ILog log 
            = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       
        private CoopCheckEntities _ctx = new CoopCheckEntities();
        public IEnumerable<vwPayment> Get(int batchNum)
        {
            log.Info(String.Format("get SwiftPayments batchNum {0}", batchNum));
            return _ctx.vwPayments.Where(x => x.batch_num == batchNum).ToList();
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        public extern static bool DuplicateToken(IntPtr ExistingTokenHandle, int
        SECURITY_IMPERSONATION_LEVEL, out IntPtr DuplicateTokenHandle);


        // POST api/SwiftPayment/SwiftPay?accountId&batchNum=1
        [Route("SwiftPay")]
        public async Task<IHttpActionResult> SwiftPay( int accountId, int batchNum)
        {
            string emailAddress;
            try
            {
                WindowsPrincipal user = RequestContext.Principal as WindowsPrincipal;
                if (user != null && UserAuthSvc.CanWrite(user.Identity.Name))
                {
                    emailAddress = SendMailSvc.uEmail(user.Identity.Name.Replace(@"reckner\", ""));

                    log.Info(String.Format("Email address {0}", emailAddress));
                    log.Info(String.Format("SwiftPay called user {0}  account {1} batch {2} email {3}",
                        user.Identity.Name, accountId, batchNum, emailAddress));
                    //HostingEnvironment.QueueBackgroundWorkItem(clt => SwiftPaySvc.PayBatch(accountId, batchNum, user, email));
                    //Task.Run(() => SwiftPaySvc.PayBatch(accountId, batchNum, user, email));
                    BackgroundJob.Enqueue(() => SwiftPaySvc.PayBatch(accountId, batchNum, emailAddress));
                    return Ok();
                }
                return Unauthorized();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
    }
