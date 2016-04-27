using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;

namespace CoopCheck.Web.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        private static readonly log4net.ILog log
           = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET api/values
        public IEnumerable<string> Get()
        {
            WindowsPrincipal user = RequestContext.Principal as WindowsPrincipal;
            log.Info("get values called");
            return new string[] { "value1", "value2",user?.Identity.Name };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
