using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using DataClean.Contracts.Interfaces;
using log4net;

namespace CoopCheck.Web.Controllers
{
    [System.Web.Http.RoutePrefix("api/DataCleanEvent")]
    public class DataCleanEventController : ApiController
    {
        private readonly IDataCleanEventFactory _dataCleanEventFactory;

        public DataCleanEventController(IDataCleanEventFactory dataCleanEventFactory )
        {
            _dataCleanEventFactory = dataCleanEventFactory;
        }
        public IDataCleanEvent Get(int id)
        {
            return _dataCleanEventFactory.GetExistingEvent(id);
        }
        public IEnumerable<IDataCleanEvent> CleanAddresses([FromBody]IEnumerable<IInputStreetAddress> aList)
        {
            return _dataCleanEventFactory.ValidateAddresses(aList.ToList());
        }
    }
}