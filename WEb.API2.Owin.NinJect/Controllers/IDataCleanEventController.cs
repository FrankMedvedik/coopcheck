using System.Collections.Generic;
using System.Web.Http;
using DataClean.Contracts.Models;

namespace WEb.API2.Owin.NinJect.Controllers
{
    public interface IDataCleanEventController
    {
        IEnumerable<DataCleanEvent> CleanAddresses([FromBody] IEnumerable<InputStreetAddress> aList);
        DataCleanEvent Get(int id);
    }
}