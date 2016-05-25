  using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using DataClean.DataCleaner;
using DataClean.Models;
using DataClean.Repository.Mgr;
using log4net;

namespace CoopCheck.Web.Controllers
{
    [System.Web.Http.RoutePrefix("api/DataCleanEvent")]
    public class DataCleanEventController : ApiController
    {
        private static readonly ILog log
            = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        private readonly DataCleanEventFactory _dcef;

        public DataCleanEventController()
        {
            var c = ConfigurationManager.AppSettings;
            var criteria = new DataCleanCriteria()
            {
                AutoFixAddressLine1 = false,
                AutoFixCity = false,
                AutoFixPostalCode = true,
                AutoFixState = false,
                ForceValidation = false
            };
            _dcef = new DataCleanEventFactory(new DataCleaner(c), new DataCleanRespository(), criteria);
        }
        public DataCleanEvent Get(int id)
        {
            return _dcef.GetExistingEvent(id);
        }


        public IEnumerable<DataCleanEvent> CleanAddresses([FromBody]IEnumerable<InputStreetAddress> aList)
        {
            return  _dcef.ValidateAddresses(aList.ToList());
        }
    }
}