using System.Collections.Generic;
using System.Threading.Tasks;
using CoopCheck.Reports.Models;

namespace CoopCheck.Reports.Services
{
    public static class ClientSvc
    {
        public static async Task<List<CoopCheckClient>> GetClients()
        {
            using (var ctx = new CoopCheckEntities())
            {
                var x = await (ctx.CoopCheckClients.OrderBy(a => a.ClientName).ToListAsync());
                return x;
            }
        }
    }
}