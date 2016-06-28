using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WEb.API2.Owin.NinJect.Controllers
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiController
    {
        private readonly IFakeService _fakeService;

        public HomeController(IFakeService fakeService)
        {
            _fakeService = fakeService;
        }

        [Route("")]
        public IHttpActionResult Index(string question)
        {
            if (_fakeService.GetAnswer(question))
                return Ok();

            return BadRequest();
        }
    }
}