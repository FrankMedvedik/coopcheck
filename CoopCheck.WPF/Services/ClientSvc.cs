using System.Collections.Generic;
using System.Threading.Tasks;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Services
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