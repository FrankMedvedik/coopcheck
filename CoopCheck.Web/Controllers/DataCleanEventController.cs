using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using DataClean.Contracts.Interfaces;
using DataClean.Contracts.Models;
using DataClean.DataCleaner;
using DataClean.Repository.Mgr;
using log4net;

namespace CoopCheck.Web.Controllers
{
    [System.Web.Http.RoutePrefix("api/DataCleanEvent")]
    public class DataCleanEventController : ApiController
    {
        private readonly IDataCleanEventFactory _dataCleanEventFactory;
        public DataCleanEventController()
        {
            _dataCleanEventFactory = 
                new DataCleanEventFactory(new DataCleaner(ConfigurationManager.AppSettings), new DataCleanRespository(),
                new DataCleanCriteria()
                {
                     AutoFixPostalCode = true
                });

        }
        //public DataCleanEventController(IDataCleanEventFactory dataCleanEventFactory )
        //{
        //    _dataCleanEventFactory = dataCleanEventFactory;
        //}
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