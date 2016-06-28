using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CoopCheck.Web.Controllers
{
    [System.Web.Http.RoutePrefix("api/Values")]
    public class ValuesController : ApiController
    {
        private readonly IValuesProvider valuesProvider1;
        private readonly IValuesProvider valuesProvider2;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValuesController"/> class.
        /// </summary>
        /// <param name="valuesProvider">The values provider.</param>
        public ValuesController(IValuesProvider valuesProvider1, IValuesProvider valuesProvider2)
        {
            this.valuesProvider1 = valuesProvider1;
            this.valuesProvider2 = valuesProvider2;
        }

        /// <summary>
        /// Handles: GET /values
        /// </summary>
        /// <returns>The values</returns>
        public IEnumerable<string> Get()
        {
            return this.valuesProvider1.GetValues().Union(this.valuesProvider2.GetValues());
        }
    }
}
