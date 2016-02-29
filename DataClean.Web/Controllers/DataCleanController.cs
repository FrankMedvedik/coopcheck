
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DataClean.DataCleaner;
using DataClean.Models;

namespace DataClean.Web.Controllers
{

    public class DataCleanController : ApiController
    {
        private DataCleanEventFactory _dataCleanEventFactory;

        public DataCleanController()
        {
                var c = ConfigurationManager.AppSettings;
                var criteria = new DataCleanCriteria{AutoFixPostalCode = true};

                _dataCleanEventFactory = new DataCleanEventFactory(new DataCleaner.DataCleaner(c), new DataClean.Repository.Mgr.DataCleanRespository(), criteria);
        }

        public HttpResponseMessage Get(List<InputStreetAddress> newVouchers)
        {
            
        List<DataCleanEvent> outRows = new List<DataCleanEvent>();

        try
        {
            outRows = _dataCleanEventFactory.ValidateAddresses(newVouchers);

        }
        catch (Exception e)
        {
            Console.Write(e.Message);
           return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
       }
        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, outRows);
        return response;
    }
}
}
