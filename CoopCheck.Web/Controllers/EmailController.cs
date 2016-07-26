using System.Web.Http;
using CoopCheck.Web.Services;

namespace CoopCheck.Web.Controllers
{
    public class EmailController : ApiController
    {
        public string Get(string email)
        {
            SendMailSvc.SendEmail(email, "THIS IS A TEST EMAIL","Here is the message");
            return email;
        }
    }
}
