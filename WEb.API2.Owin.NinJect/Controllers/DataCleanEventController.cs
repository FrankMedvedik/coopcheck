using System.Web.Http;
using DataClean.Contracts.Interfaces;
using DataClean.Contracts.Models;
using DataClean.Repository;

namespace WEb.API2.Owin.NinJect.Controllers
{
    [System.Web.Http.RoutePrefix("api/DataCleanEvent")]
    public class DataCleanEventController : ApiController
    {
        private readonly IDataCleanEventFactory _dataCleanEventFactory;
        public DataCleanEventController()
        {
            //_dataCleanEventFactory = 
            //    new DataCleanEventFactory(new DataCleaner(ConfigurationManager.AppSettings), new DataCleanRespository(),
            //    new DataCleanCriteria()
            //    {
            //         AutoFixPostalCode = true
            //    });

        }
        //public DataCleanEventController(IDataCleanEventFactory dataCleanEventFactory )
        //{
        //    _dataCleanEventFactory = dataCleanEventFactory;
        //}
        public DataCleanEventLog Get(int id)
        {
             DataCleanEntities ctx = new DataCleanEntities();
            return ctx.DataCleanEventLogs.Find(id);
                
        }
        //public IEnumerable<IDataCleanEvent> CleanAddresses([FromBody]IEnumerable<IInputStreetAddress> aList)
        //{
        //    return _dataCleanEventFactory.ValidateAddresses(aList.ToList());
        //}
    }
}