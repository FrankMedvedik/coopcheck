using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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



        // POST api/SwiftPayment/SwiftPay?accountId&batchNum=1
        [Route("SwiftPay")]
        public async Task<IHttpActionResult> SwiftPay( int accountId, int batchNum)
        {
            try
            {
                WindowsPrincipal user = RequestContext.Principal as WindowsPrincipal;
                var email = SendMailSvc.uEmail(HttpContext.Current.User.Identity.Name.Replace(@"reckner\", ""));
                log.Info(String.Format("Email address {0}", email));
                log.Info(String.Format("SwiftPay called user {0}  account {1} batch {2} email {3}", user.Identity.Name, accountId,batchNum, email));
                //HostingEnvironment.QueueBackgroundWorkItem(clt => SwiftPaySvc.PayBatch(accountId, batchNum, user, email));
                //Task.Run(() => SwiftPaySvc.PayBatch(accountId, batchNum, user, email));
                BackgroundJob.Enqueue(() => SwiftPaySvc.PayBatch(accountId, batchNum,  email));
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
    }
