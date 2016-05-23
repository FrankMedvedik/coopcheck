using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;
using CoopCheck.Repository;
using CoopCheck.Web.Services;
using Hangfire;
using log4net;

namespace CoopCheck.Web.Controllers
{
    [System.Web.Http.Authorize]
    [System.Web.Http.RoutePrefix("api/SwiftPayment")]
    public class SwiftPaymentController : ApiController
    {
        private static readonly ILog log
            = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly CoopCheckEntities _ctx = new CoopCheckEntities();

        public IEnumerable<vwPayment> Get(int batchNum)
        {
            log.Info(string.Format("get SwiftPayments batchNum {0}", batchNum));
            return _ctx.vwPayments.Where(x => x.batch_num == batchNum).ToList();
        }

        // POST api/SwiftPayment/SwiftPay?accountId&batchNum=1
        [System.Web.Http.Route("SwiftPay")]
        public async Task<IHttpActionResult> SwiftPay(int batchNum)
        {
            string emailAddress;
            try
            {
                var user = RequestContext.Principal as WindowsPrincipal;
                if (user != null && UserAuthSvc.CanWrite(user.Identity.Name))
                {
                    emailAddress = SendMailSvc.uEmail(user.Identity.Name.Replace(@"reckner\", ""));

                    log.Info(string.Format("Email address {0}", emailAddress));
                    log.Info(string.Format("SwiftPay called user {0}  batch {1} email {2}",
                        user.Identity.Name, batchNum, emailAddress));
                    BackgroundJob.Enqueue(() => SwiftPaySvc.PayBatch(batchNum, emailAddress));
                    return Ok();
                }
                return Unauthorized();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/SwiftPayment/VoidSwiftPay?accountId&batchNum=1
        [Route("SwiftVoid")]
        public async Task<IHttpActionResult> SwiftVoid(int batchNum)
        {
            string emailAddress;
            try
            {
                var user = RequestContext.Principal as WindowsPrincipal;
                if (user != null && UserAuthSvc.CanWrite(user.Identity.Name))
                {
                    emailAddress = SendMailSvc.uEmail(user.Identity.Name.Replace(@"reckner\", ""));

                    log.Info(string.Format("Email address {0}", emailAddress));
                    log.Info(string.Format("SwiftVoid called user {0}  batch {1} email {2}",
                        user.Identity.Name,batchNum, emailAddress));
                    BackgroundJob.Enqueue(() => SwiftPaySvc.VoidBatch(batchNum, emailAddress));
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