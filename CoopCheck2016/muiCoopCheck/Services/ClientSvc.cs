using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Repository;

namespace CoopCheck.WPF.Services
{
    public static class ClientSvc   
    {
        public async static Task<List<client>> GetClients()
        {
            using (var ctx = new CoopCheckEntities())
            {

                var x = await(ctx.clients.OrderBy(a => a.ClientName).ToListAsync());
                return x;
            }
        }
    }
    }
