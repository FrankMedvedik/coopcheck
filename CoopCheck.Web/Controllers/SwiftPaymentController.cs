using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;
using CoopCheck.Domain.Contracts.Interfaces;
using CoopCheck.Repository.Contracts.Interfaces;
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

        private readonly ICoopCheckEntities _ctx;
        private IUserAuthSvc _userAuthSvc;
        private ISwiftPaySvc _swiftPaySvc;
        private ISendMailSvc _sendMailSvc;

        public SwiftPaymentController(ICoopCheckEntities ctx, ISwiftPaySvc swiftPaySvc, IUserAuthSvc userAuthSvc, ISendMailSvc sendMailSvc)
        {
            _ctx = ctx;
            _swiftPaySvc = swiftPaySvc;
            _userAuthSvc = userAuthSvc;
            _sendMailSvc = sendMailSvc;
        }

        public List<IvwPayment> Get(int batchNum)
        {
            log.Info(string.Format("get SwiftPayments batchNum {0}", batchNum));
            return _ctx.vwPayments.Where(x => x.batch_num == batchNum).ToList();
        }

        // POST api/SwiftPayment/SwiftPay?accountId&batchNum=1
        [System.Web.Http.Route("SwiftPay")]
        public  IHttpActionResult SwiftPay(int batchNum)
        {
            string emailAddress;
            try
            {
                var user = RequestContext.Principal as WindowsPrincipal;
                if (user != null && _userAuthSvc.CanWrite(user.Identity.Name))
                {
                    emailAddress = _sendMailSvc.uEmail(user.Identity.Name.Replace(@"reckner\", ""));

                    log.Info(string.Format("Email address {0}", emailAddress));
                    log.Info(string.Format("SwiftPay called user {0}  batch {1} email {2}",
                        user.Identity.Name, batchNum, emailAddress));
                    BackgroundJob.Enqueue(() => _swiftPaySvc.PayBatch(batchNum, emailAddress));
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
                if (user != null && _userAuthSvc.CanWrite(user.Identity.Name))
                {
                    emailAddress = _sendMailSvc.uEmail(user.Identity.Name.Replace(@"reckner\", ""));

                    log.Info(string.Format("Email address {0}", emailAddress));
                    log.Info(string.Format("SwiftVoid called user {0}  batch {1} email {2}",
                        user.Identity.Name,batchNum, emailAddress));
                    BackgroundJob.Enqueue(() => _swiftPaySvc.VoidBatch(batchNum, emailAddress));
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