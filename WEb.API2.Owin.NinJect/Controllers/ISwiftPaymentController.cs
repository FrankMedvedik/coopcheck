using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using CoopCheck.Repository;

namespace WEb.API2.Owin.NinJect.Controllers
{
    public interface ISwiftPaymentController
    {
        List<vwPayment> Get();
        List<vwPayment> Get(int batchNum);
        IHttpActionResult SwiftPay(int batchNum);
        Task<IHttpActionResult> SwiftVoid(int batchNum);
    }
}